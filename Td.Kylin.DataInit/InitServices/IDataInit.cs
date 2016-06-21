using System;

namespace Td.Kylin.DataInit.InitServices
{
    /// <summary>
    /// 数据初始化操作接口
    /// </summary>
    public interface IDataInit
    {
        /// <summary>
        /// 初始化到数据库
        /// </summary>
        /// <returns></returns>
        bool Init();

        /// <summary>
        /// 从数据库下载最新
        /// </summary>
        /// <returns></returns>
        bool Download();

        /// <summary>
        /// 异常处理
        /// </summary>
        void OnException(Exception ex);
    }
}
