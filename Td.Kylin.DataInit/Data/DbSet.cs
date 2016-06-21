using Microsoft.Data.Entity;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.Data
{
    public partial class DataContext
    {
        /// <summary>
        /// 全局配置
        /// </summary>
        public DbSet<System_GlobalResources> System_GlobalResources { get { return Set<System_GlobalResources>(); } }

        /// <summary>
        /// 系统积分配置
        /// </summary>
        public DbSet<System_PointsConfig> System_PointsConfig { get { return Set<System_PointsConfig>(); } }

        /// <summary>
        /// 系统经验值配置
        /// </summary>
        public DbSet<System_EmpiricalConfig> System_EmpiricalConfig { get { return Set<System_EmpiricalConfig>(); } }

        /// <summary>
        /// 用户等级规则
        /// </summary>
        public DbSet<System_Level> System_Level { get { return Set<System_Level>(); } }
        
        /// <summary>
        /// 模块授权
        /// </summary>
        public DbSet<System_ModuleAuthorize> System_ModuleAuthorize { get { return Set<System_ModuleAuthorize>(); } }

        /// <summary>
        /// 管理员
        /// </summary>
        public DbSet<Admin_Account> Admin_Account { get { return Set<Admin_Account>(); } }

        /// <summary>
        /// 商家所属行业
        /// </summary>
        public DbSet<Merchant_Industry> Merchant_Industry { get { return Set<Merchant_Industry>(); } }

        /// <summary>
        /// 全国区域
        /// </summary>
        public DbSet<System_Area> System_Area { get { return Set<System_Area>(); } }

        /// <summary>
        /// 广告页
        /// </summary>
        public DbSet<Ad_Page> Ad_Page { get { return Set<Ad_Page>(); } }

        /// <summary>
        /// 广告位
        /// </summary>
        public DbSet<Ad_Position> Ad_Position { get { return Set<Ad_Position>(); } }
    }
}
