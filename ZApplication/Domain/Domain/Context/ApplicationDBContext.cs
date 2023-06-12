using Domain.Common;
using Domain.Entites;
using Domain.Entites.Config;
using Microsoft.EntityFrameworkCore;

namespace Domain.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        #region DbSet
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRols { get; set; }
        public DbSet<Role> Rols { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        #endregion


        #region Config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.Entity<User>().HasData(new User() { Id = 1, FirstName = "Mohammad", LastName = "Zarrabi", Password = PasswordHelper.PasswordToSHA256("1234"), EmailAddress = "mhzsam@gmail.com" });
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());

        }
        #endregion
    }
}
