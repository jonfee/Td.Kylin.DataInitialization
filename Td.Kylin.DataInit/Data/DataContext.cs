using Microsoft.Data.Entity;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.Data
{
    public partial class DataContext : DbContext
    {
        private string _connectString;

        public DataContext(string connectString)
        {
            _connectString = connectString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //系统模块接口授权
            modelBuilder.Entity<System_ModuleAuthorize>(entity =>
            {
                entity.HasKey(p => new { p.ServerID, p.ModuleID });
            });

            //天道管理员账户
            modelBuilder.Entity<Admin_Account>(entity =>
            {
                entity.Property(p => p.AdminID).ValueGeneratedNever();
                entity.HasKey(p => p.AdminID);
            });

            //商家行业
            modelBuilder.Entity<Merchant_Industry>(entity =>
            {
                entity.Property(p => p.IndustryID).ValueGeneratedNever();
                entity.HasKey(p => p.IndustryID);
            });
            modelBuilder.Entity<System_Area>(entity =>
            {
                entity.Property(p => p.AreaID).ValueGeneratedNever();
                entity.HasKey(p => p.AreaID);
            });

            //系统模块接口授权
            modelBuilder.Entity<System_ModuleAuthorize>(entity =>
            {
                entity.HasKey(p => new { p.ServerID, p.ModuleID });
            });

            //系统全局配置
            modelBuilder.Entity<System_GlobalResources>(entity =>
            {
                entity.HasKey(p => new { p.ResourceType, p.ResourceKey });
            });

            //系统积分配置
            modelBuilder.Entity<System_PointsConfig>(entity =>
            {
                entity.Property(p => p.ActivityType).ValueGeneratedNever();
                entity.HasKey(p => p.ActivityType);
            });

            //系统经验值配置
            modelBuilder.Entity<System_EmpiricalConfig>(entity =>
            {
                entity.Property(p => p.ActivityType).ValueGeneratedNever();
                entity.HasKey(p => p.ActivityType);
            });

            //系统用户等级配置
            modelBuilder.Entity<System_Level>(entity =>
            {
                entity.Property(p => p.LevelID).ValueGeneratedNever();
                entity.HasKey(p => p.LevelID);
            });

            //广告页
            modelBuilder.Entity<Ad_Page>(entity =>
            {
                entity.Property(p => p.PageID).ValueGeneratedNever();
                entity.HasKey(p => p.PageID);
            });

            /// <summary>
            /// 广告位
            /// </summary>
            modelBuilder.Entity<Ad_Position>(entity =>
            {
                entity.Property(p => p.PositionID).ValueGeneratedNever();
                entity.HasKey(p => p.PositionID);
            });
        }
    }
}
