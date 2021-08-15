using Microsoft.EntityFrameworkCore;
namespace password_manager_backend.Data
{
    public class password_manager_backendContext : DbContext
    {
        public password_manager_backendContext (DbContextOptions<password_manager_backendContext> options)
            : base(options)
        {
        }

        public DbSet<password_manager_backend.Models.UserLoginModel> UserLoginModel { get; set; }

        public DbSet<password_manager_backend.Models.UserInfoModel> UserInfoModel { get; set; }

        public DbSet<password_manager_backend.Models.SaveAccountInfoModel> SaveAccountInfoModel { get; set; }
        public DbSet<password_manager_backend.Models.RecentlyUsedPassword> RecentlyUsedPasswords { get; set; }

    }
}
