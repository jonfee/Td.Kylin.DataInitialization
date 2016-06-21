namespace Td.Kylin.DataInit.Model
{
    /// <summary>
    /// 天道后台管理员账号模型
    /// </summary>
    public class AdminAccountModel
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
    }
}
