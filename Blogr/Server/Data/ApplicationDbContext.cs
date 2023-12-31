﻿using Blogr.Client.Pages;
using Blogr.Data;
using Blogr.Server.Models;
using Blogr.Shared;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Blogr.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        //Uses EF to create a table called blogs
        public DbSet<Blog?> Blogs { get; set; } = null!;
        public DbSet<BlogContent> BlogContent => Set<BlogContent>();
        public DbSet<BlogAnalytics> BlogAnalytics => Set<BlogAnalytics>();

        public DbSet<BlogViewers> BlogViewers => Set<BlogViewers>();

        public DbSet<UserImage> UserImages => Set<UserImage>();

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().Navigation(e => e.Photo).AutoInclude();
            builder.Entity<BlogAnalytics>()
                .HasMany(e => e.Viewers)
                .WithOne(e => e.Analytics)
                .HasForeignKey(e => e.AnalyticsId)
                .IsRequired();
        }
    }
}