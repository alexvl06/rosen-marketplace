// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

namespace Marketplace.Api
{
    using Marketplace.Bl;
    using Marketplace.Core.Bl;
    using Marketplace.Core.Dal;
    using Marketplace.Dal.Repositories;
    using Marketplace.Dal;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using  System.Text.Json.Serialization;
    public class Startup
    {
        #region Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region Constructors

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        #endregion

        #region Methods

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Marketplace.Api v1")
                );
            }

            app.UseHttpsRedirection();
            app.UseCors("MyOrigins");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System
                        .Text
                        .Json
                        .Serialization
                        .JsonIgnoreCondition
                        .WhenWritingNull;
                    options.JsonSerializerOptions.ReferenceHandler =ReferenceHandler.IgnoreCycles;
                });
            services.AddDbContext<MarketplaceContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Marketplace.Api", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "MyOrigins",
                    policy =>
                    {
                        policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("total-offers");
                    }
                );
            });
            
            services.AddScoped<IUserBl, UserBl>();
            services.AddScoped<IOfferBl, OfferBl>();
            services.AddScoped<ICategoryBl, CategoryBl>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOfferRepository, OfferRepository>();
        }

        #endregion
    }
}
