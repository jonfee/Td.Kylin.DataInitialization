using System.Collections.Generic;

namespace Td.Kylin.DataInit.Model
{
    /// <summary>
    /// 广告页模型
    /// </summary>
    public class AdPageModel
    {
        /// <summary>
        /// 广告页ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 广告页名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属平台
        /// </summary>
        public int PlatformType { get; set; }

        /// <summary>
        /// 页面中广告位集合
        /// </summary>
        public List<AdPositionModel> AdPositionList { get; set; }
    }

    /// <summary>
    /// 广告位模型
    /// </summary>
    public class AdPositionModel
    {
        /// <summary>
        /// 广告位ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 广告位名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 广告位编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 所属广告页ID
        /// </summary>
        public long PageID { get; set; }

        /// <summary>
        /// 最多允许的广告数
        /// </summary>
        public int MaxCount { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 广告展示方式
        /// </summary>
        public int DisplayType { get; set; }

        /// <summary>
        /// 广告位预览图
        /// </summary>
        public string PreViewPicture { get; set; }

        /// <summary>
        /// 广告位描述
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 限制宽
        /// </summary>
        public int LimitWidth { get; set; }

        /// <summary>
        /// 限制高
        /// </summary>
        public int LimitHeight { get; set; }

        /// <summary>
        /// 预览样式
        /// </summary>
        public string PreviewStyle { get; set; }
    }
}
