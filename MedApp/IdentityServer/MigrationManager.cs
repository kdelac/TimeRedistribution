﻿using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace IdentityServer
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                using (var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
                {
                    try
                    {
                        context.Database.Migrate();

                        if (!context.Clients.Any())
                        {
                            foreach (var client in InMemoryConfiguration.Clients)
                            {
                                context.Clients.Add(client.ToEntity());
                            }
                            context.SaveChanges();
                        }

                        if (!context.IdentityResources.Any())
                        {
                            foreach (var resource in InMemoryConfiguration.IdentityResources)
                            {
                                context.IdentityResources.Add(resource.ToEntity());
                            }
                            context.SaveChanges();
                        }

                        if (!context.ApiScopes.Any())
                        {
                            foreach (var apiScope in InMemoryConfiguration.ApiScopes)
                            {
                                context.ApiScopes.Add(apiScope.ToEntity());
                            }

                            context.SaveChanges();
                        }

                        if (!context.ApiResources.Any())
                        {
                            foreach (var resource in InMemoryConfiguration.ApiResources)
                            {
                                context.ApiResources.Add(resource.ToEntity());
                            }
                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }

            return host;
        }
    }
}
