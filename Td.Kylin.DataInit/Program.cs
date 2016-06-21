using System;
using System.Configuration;
using System.Windows.Forms;
using Td.Kylin.DataInit.Data;

namespace Td.Kylin.DataInit
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //数据库连接
            var connectionString = ConfigurationManager.ConnectionStrings["DataConnectionString"].ConnectionString;
            DBConnectHelper.SetConnection(connectionString);

            Application.Run(new MainForm());
        }
    }
}
