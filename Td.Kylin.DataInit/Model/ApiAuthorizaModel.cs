namespace Td.Kylin.DataInit.Model
{
    /// <summary>
    /// 接口模块授权模型
    /// </summary>
    public class ApiAuthorizaModel
    {
        /// <summary>
        /// 接口服务ID
        /// </summary>
        public string ServerID { get; set; }

        /// <summary>
        /// 需要授权的模块ID
        /// </summary>
        public string ModuleID { get; set; }

        /// <summary>
        /// 授权密钥
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public int Role { get; set; }
    }
}
