using System.Text.RegularExpressions;

namespace Td.Kylin.DataInit.Data
{
    /// <summary>
    /// 数据库连接配置类
    /// </summary>
    public class DBConnectionRoot
    {
        /// <summary>
        /// 初始化的数据库连接对象
        /// </summary>
        public static DBConfig InitDBConnection;

        /// <summary>
        /// 下载源数据库连接对象
        /// </summary>
        public static DBConfig DownloadSourceDBConnection;
    }

    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DBConfig
    {
        /// <summary>
        /// 数据库服务器地址
        /// </summary>
        public string DataServer { get; set; }

        /// <summary>
        /// 数据库名
        /// </summary>
        public string DataBase { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginAccount { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return string.Format(@"data source={0};initial catalog={1};persist security info=True;user id={2};password={3};MultipleActiveResultSets=True;", DataServer, DataBase, LoginAccount, Password);
            }
        }
    }

    public class DBConnectHelper
    {
        /// <summary>
        /// 设置数据库连接配置信息
        /// </summary>
        /// <param name="connectionString"></param>
        public static void SetConnection(string connectionString)
        {
            var serverMatch = new Regex(@"(server|host|data source)=(?<server>[^;""’’]+)", RegexOptions.IgnoreCase).Match(connectionString);
            var dbMatch = new Regex(@"(database|initial catalog)=(?<database>[^;""]+)", RegexOptions.IgnoreCase).Match(connectionString);
            var userMatch = new Regex(@"user id=(?<userid>[^;""]+)", RegexOptions.IgnoreCase).Match(connectionString);
            var pwdMatch = new Regex(@"password=(?<password>[^;""]+)", RegexOptions.IgnoreCase).Match(connectionString);

            DBConfig config = new DBConfig();

            if (serverMatch != null) config.DataServer = serverMatch.Groups["server"].Value;
            if (dbMatch != null) config.DataBase = dbMatch.Groups["database"].Value;
            if (userMatch != null) config.LoginAccount = userMatch.Groups["userid"].Value;
            if (pwdMatch != null) config.Password = pwdMatch.Groups["password"].Value;

            DBConnectionRoot.InitDBConnection = config;
            DBConnectionRoot.DownloadSourceDBConnection = config;
        }

        /// <summary>
        /// 设置数据库连接配置信息
        /// </summary>
        /// <param name="initConfig">初始化目标数据库配置</param>
        /// <param name="downloadConfig">下载源数据库配置</param>
        public static void SetConnection(DBConfig initConfig, DBConfig downloadConfig)
        {
            DBConnectionRoot.InitDBConnection = initConfig;
            DBConnectionRoot.DownloadSourceDBConnection = downloadConfig;
        }
    }
}
