using System.ComponentModel;

namespace Td.Kylin.DataInit
{
    /// <summary>
    /// 数据初始化类型
    /// </summary>
    public enum DataInitType
    {
        /// <summary>
        /// API授权数据
        /// </summary>
        [Description("API授权数据")]
        ApiAuthoriza,
        /// <summary>
        /// 天道后台管理员数据
        /// </summary>
        [Description("天道后台管理员数据")]
        DefaultTianDaoAdminAccount,
        /// <summary>
        /// 广告位数据
        /// </summary>
        [Description("广告位数据")]
        AdPosition,
        /// <summary>
        /// 全国区域数据
        /// </summary>
        [Description("全国区域数据")]
        AreaCity,
        /// <summary>
        /// 全局配置数据
        /// </summary>
        [Description("全局配置数据")]
        GlobalConfig,
        /// <summary>
        /// 商家行业数据
        /// </summary>
        [Description("商家行业数据")]
        Industry,
        /// <summary>
        /// 积分规则数据
        /// </summary>
        [Description("积分规则数据")]
        Points,
        /// <summary>
        /// 经验值规则数据
        /// </summary>
        [Description("经验值规则数据")]
        Empirical,
        /// <summary>
        /// 等级规则数据
        /// </summary>
        [Description("等级规则数据")]
        Level,
        /// <summary>
        /// 商品分类数据
        /// </summary>
        [Description("商品分类数据")]
        ProductCategory,
        /// <summary>
        /// 服务分类数据
        /// </summary>
        [Description("服务分类数据")]
        ServiceCategory,
        /// <summary>
        /// 岗位分类数据
        /// </summary>
        [Description("岗位分类数据")]
        JobCategory
    }
}
