using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public class AppDbContext : IdentityDbContext<UserEntity>
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // nie usuwac
        var adminId = "9d09f09d-910b-4843-9289-175fb1ddb1c7";
        var createdAt = new DateTime(2025, 04, 08);
        var adminUser = new UserEntity()
        {
            Id = adminId,
            Email = "admin@wsei.edu.pl",
            NormalizedEmail = "admin@wsei.edu.pl".ToUpper(),
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            EmailConfirmed = true,
            ConcurrencyStamp = adminId,
            SecurityStamp = adminId,
            PasswordHash = "AQAAAAIAAYagAAAAEKlWAm650sN2+IJ1e1VRAr6S+3TRskIPnRf6jkmKpgevjslFUDrtoox6nwDVe0zqQg=="
        };

        //PasswordHasher<UserEntity> ph = new PasswordHasher<UserEntity>();
        //var hash = ph.HashPassword(adminUser, "1234!");
        //Console.WriteLine(hash);
        //adminUser.PasswordHash = hash;
        builder.Entity<UserEntity>()
            .HasData(
                adminUser
            );
        builder.Entity<UserEntity>()
            .OwnsOne(u => u.Details)
            .HasData(new
            {
                UserEntityId = adminId,
                CreatedAt = createdAt
            });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"data source=d:\Data\app.db");
    }
}