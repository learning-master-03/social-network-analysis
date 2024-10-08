﻿using System;
using Acme.BookStore.Books;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace TodoApp.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class TodoAppDbContext :
    AbpDbContext<TodoAppDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<PuppeteerConfiguration> PuppeteerConfigurations { get; set; }
    public DbSet<TikiCategory> TikiCategories { get; set; }
    public DbSet<TikiProduct> TikiProducts { get; set; }
    public DbSet<TikiProductImage> TikiProductImages { get; set; }
    public DbSet<TikiReview> TikiReviews { get; set; }
    public DbSet<TikiProductLink> TikiProductLinks { get; set; }
    public DbSet<ProxyJobs> ProxyJobs { get; set; }
    public DbSet<Proxies> Proxies { get; set; }



    #endregion

    public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(TodoAppConsts.DbTablePrefix + "YourEntities", TodoAppConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        builder.Entity<TodoItem>(b =>
   {
       b.ToTable("TodoItems");
   });

        builder.Entity<PuppeteerConfiguration>(b =>
       {
           b.ToTable("PuppeteerConfigurations");
       });

        builder.Entity<TikiCategory>(b =>
   {
       b.ToTable("TikiCategories");
       b.ConfigureByConvention(); //auto configure for the base class props

       //    b.Property(x => x.IsActive).HasDefaultValue(true);
       //    b.Property(x => x.LastModificationTime).HasDefaultValue(DateTime.Now);
       //    b.Property(x => x.CreationTime).HasDefaultValue(DateTime.Now);
       //    b.Property(x => x.CreationBy).HasDefaultValue("system");
       //    b.Property(x => x.LastModificationBy).HasDefaultValue("system");
       //    b.Property(x => x.Url).IsRequired().HasMaxLength(500);

       b.ConfigureByConvention(); //auto configure for the base class props

   });
        builder.Entity<TikiProduct>(b =>
   {
       b.ToTable("TikiProducts");
       b.ConfigureByConvention(); //auto configure for the base class props


   });
        builder.Entity<ProxyJobs>(b =>
   {
       b.ToTable("ProxyJobs");
       b.ConfigureByConvention(); //auto configure for the base class props


   });        builder.Entity<TikiProduct>(b =>
   {
       b.ToTable("TikiProducts");
       b.ConfigureByConvention(); //auto configure for the base class props


   });
        builder.Entity<Proxies>(b =>
{
    b.ToTable("Proxies");
    b.ConfigureByConvention(); //auto configure for the base class props


});
        builder.Entity<TikiProductLink>(b =>
{
    b.ToTable("TikiProductLinks");
    b.ConfigureByConvention(); //auto configure for the base class props


});
        builder.Entity<TikiReview>(b =>
{
    b.ToTable("TikiReviews");
    b.ConfigureByConvention(); //auto configure for the base class props


});

        builder.Entity<Book>(b =>
            {
                b.ToTable("Books");
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });
    }
}
