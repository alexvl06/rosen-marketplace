// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System.Threading.Tasks;
using Marketplace.Core.Dal;
using Marketplace.Core.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
namespace Marketplace.Dal.Repositories;

public class UserRepository : IUserRepository
{
    #region Fields

    private readonly MarketplaceContext _context;

    #endregion

    #region Constructors

    public UserRepository(MarketplaceContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods


    public async Task<int> CreateNewUser(string name)
    {
        User user = new User();
        user.Username = name;
        _context.users.Add(user);
        await _context.SaveChangesAsync();
        return _context.users.Max(u=>u.Id);
    }


    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.users.ToListAsync();
    }

    public async Task<int> GetUserIdByName(string username)
    {
            User user = await  _context.users.FirstOrDefaultAsync(u=>u.Username==username);
            if(user != null){
                return user.Id;
            }else{
                return 0;
            }
    }

    
    public async Task<string> GetUserNameById(int id)
    {
        User user = await _context.users.FirstOrDefaultAsync(c=>c.Id == id);
        return user.Username;
    }

    #endregion
}