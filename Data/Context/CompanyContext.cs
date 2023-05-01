using Lab3.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Data.Context;

public class CompanyContext : IdentityDbContext<User>
{
    public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().ToTable("Users");
        builder.Entity<IdentityUserClaim<string>>().ToTable("User's Claim");


    }
}
