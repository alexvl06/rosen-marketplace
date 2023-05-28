// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Marketplace.Core.Model;
using Microsoft.Data.Sqlite;

namespace Marketplace.Dal
{
    internal class MarketplaceDb : IMarketplaceDb, IDisposable
    {
        private readonly SqliteConnection _connection;

        public MarketplaceDb()
        {
            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @".."));
            _connection = new SqliteConnection(
                $@"Data Source={path}\Marketplace.Dal\marketplace.db"
            );
        }

        public async Task<bool> CreateNewOffer(Offer offer)
        {
            _connection.Open();
            int CategoryId = await GetCategoryId(offer.CategoryName);
            int UserId = await GetUserId(offer.Username);
            if (UserId == 0)
            {
                UserId = await CreateNewUser(offer.Username);
            }
            await using var command = new SqliteCommand(
                $"INSERT INTO Offer (Id, CategoryId, Description, Location, PictureUrl, PublishedOn, Title, UserId) values ('{offer.Id}',{CategoryId},'{offer.Description}','{offer.Location}','{offer.PictureUrl}','{offer.PublishedOn.ToString("yyyy-MM-dd")}','{offer.Title}',{UserId});",
                _connection
            );
            try
            {

                command.ExecuteNonQuery();
                command.Dispose();
                _connection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public async Task<Category[]> GetAllCategories()
        {
            await using var command = new SqliteCommand(
                "SELECT C.Id, C.Name FROM Category C;",
                _connection
            );

            try
            {
                _connection.Open();
                await using var reader = await command.ExecuteReaderAsync();

                var results = new List<Category>();

                while (await reader.ReadAsync())
                {
                    var category = new Category
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Name = reader.GetString(reader.GetOrdinal("Name"))
                    };

                    results.Add(category);
                }
                command.Dispose();
                _connection.Close();
                return results.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Offer[]> GetOffersByPageIndex(int pageIndex, int pageSize)
        {
            await using var command = new SqliteCommand(
                $"SELECT O.Description, O.Location, O.PictureUrl, O.PublishedOn, O.Title, U.Username, O.UserId, O.CategoryId, C.Name as CategoryName FROM Offer O LEFT JOIN User U ON U.Id = O.UserId LEFT JOIN Category C ON O.CategoryId = C.Id ORDER BY O.PublishedOn DESC LIMIT {pageSize} OFFSET {(pageIndex - 1) * pageSize};",
                _connection
            );

            try
            {
                _connection.Open();
                await using var reader = await command.ExecuteReaderAsync();

                var results = new List<Offer>();

                while (await reader.ReadAsync())
                {
                    var offer = new Offer
                    {
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        Location = reader.GetString(reader.GetOrdinal("Location")),
                        CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                        PictureUrl = reader.GetString(reader.GetOrdinal("PictureUrl")),
                        PublishedOn = reader.GetDateTime(reader.GetOrdinal("PublishedOn")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Username = reader.GetString(reader.GetOrdinal("Username")),
                        UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                        CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"))
                    };

                    results.Add(offer);
                }
                command.Dispose();
                _connection.Close();
                return results.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User[]> GetUsersAsync()
        {
            _connection.Open();
            await using var command = new SqliteCommand(
                "SELECT U.Id, U.Username, COUNT(O.Id) AS Offers\r\n"
                    + "FROM User U LEFT JOIN Offer O ON U.Id = O.UserId\r\n"
                    + "GROUP BY U.Id, U.Username;",
                _connection
            );

            try
            {
                await using var reader = await command.ExecuteReaderAsync();

                var results = new List<User>();

                while (await reader.ReadAsync())
                {
                    var user = new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Username = reader.GetString(reader.GetOrdinal("Username"))
                    };

                    results.Add(user);
                }
                command.Dispose();
                _connection.Close();
                return results.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<int> GetCategoryId(string name)
        {
            await using var command = new SqliteCommand(
                $"SELECT C.Id FROM Category C WHERE C.Name='{name}' LIMIT 1;",
                _connection
            );
            try
            {

                await using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    return reader.GetInt32(reader.GetOrdinal("Id"));

                }
                command.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return 0;
        }

        private async Task<int> GetUserId(string name)
        {
            await using var command = new SqliteCommand(
                $"SELECT U.Id FROM User U WHERE U.Username='{name}' LIMIT 1;",
                _connection
            );
            try
            {
                await using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    return reader.GetInt32(reader.GetOrdinal("Id"));
                    
                }
                command.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return 0;
        }

        private async Task<int> CreateNewUser(string name)
        {
            await using var command = new SqliteCommand(
                $"INSERT INTO User (Username) values ('{name}');",
                _connection
            );
            try
            {
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return await LastUserId();
        }

        private async Task<int> LastUserId()
        {
            await using var command = new SqliteCommand(
                $"SELECT MAX(U.Id) as Id FROM User U;",
                _connection
            );
            try
            {

                await using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    return reader.GetInt32(reader.GetOrdinal("Id"));
                    
                }
                command.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return 0;
        }
    }
}
