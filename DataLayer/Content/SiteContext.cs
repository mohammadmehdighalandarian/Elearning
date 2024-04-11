using System.Reflection.Metadata.Ecma335;
using System.Security;
using DataLayer.Entities.Course;
using DataLayer.Entities.Order;
using DataLayer.Entities.Permision;
using DataLayer.Entities.User;
using DataLayer.Entities.Wallet;
using DataLayer.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Content
{
    public class SiteContext:DbContext
    {

        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserDiscountCode> UserDiscountCodes { get; set; }

        #endregion

        #region Wallet

        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        #endregion

        #region Permission

        public DbSet<Permision> Permission { get; set; }
        public DbSet<RolePermision> RolePermission { get; set; }

        #endregion

        #region Course

        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
       // public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseVote> CourseVotes { get; set; }

        #endregion

        #region Order

        public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderDetail> OrderDetails { get; set; }
        //public DbSet<Discount> Discounts { get; set; }

        #endregion

        public SiteContext(DbContextOptions<SiteContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UserMapping());
            //modelBuilder.ApplyConfiguration(new UserRoleMapping());
            //modelBuilder.ApplyConfiguration(new RoleMapping());
            //modelBuilder.ApplyConfiguration(new WalletMapping());
            //modelBuilder.ApplyConfiguration(new WalletTypeMapping());
            modelBuilder.Entity<User>().HasQueryFilter(u => u.Isdeleted==false);
            modelBuilder.Entity<Role>().HasQueryFilter(x => x.IsDeleted==false);
            modelBuilder.Entity<CourseGroup>().HasQueryFilter(x => x.IsDelete == false);

            var assembly=typeof(UserMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            

        }
    }
}
