using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Td.Kylin.DataInit.Core;
using Td.Kylin.DataInit.Data;
using Td.Kylin.DataInit.InitServices;

namespace Td.Kylin.DataInit
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Init();
        }

        void Init()
        {
            //数据库配置信息
            if (null != DBConnectionRoot.InitDBConnection)
            {
                this.txtInitDbServer.Text = DBConnectionRoot.InitDBConnection.DataServer;
                this.txtInitDbName.Text = DBConnectionRoot.InitDBConnection.DataBase;
                this.txtInitDbAccount.Text = DBConnectionRoot.InitDBConnection.LoginAccount;
                this.txtInitDbPwd.Text = DBConnectionRoot.InitDBConnection.Password;
            }
            if (null != DBConnectionRoot.DownloadSourceDBConnection)
            {
                this.txtDownDbServer.Text = DBConnectionRoot.DownloadSourceDBConnection.DataServer;
                this.txtDownDbName.Text = DBConnectionRoot.DownloadSourceDBConnection.DataBase;
                this.txtDownDbAccount.Text = DBConnectionRoot.DownloadSourceDBConnection.LoginAccount;
                this.txtDownDbPwd.Text = DBConnectionRoot.DownloadSourceDBConnection.Password;
            }

            //初始化类型项下拉控件数据绑定 combInitType
            var list = EnumExtensions.GetNameDescription(typeof(DataInitType)).ToList();
            list.Insert(0, new KeyValuePair<string, string>("0", "请选择"));

            this.combInitType.ValueMember = "key";
            this.combInitType.DisplayMember = "value";
            this.combInitType.DataSource = list;

            writerDelegate = WriteMessage;
            MsgWriter.InitInstance(this, writerDelegate);

            progressDelegate = UpdateProgress;
            ProgressUpdater.InitInstance(this, progressDelegate);
        }

        #region 重画下拉框
        protected void combInitType_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            e.DrawBackground();
            e.DrawFocusRectangle();

            dynamic data = combInitType.Items[e.Index];

            e.Graphics.DrawString(data.Value.ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 3);
        }
        #endregion

        #region 初始化事件触发

        /// <summary>
        /// 更新到默认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelReset_Click(object sender, EventArgs e)
        {
            UpdateDBConnection();

            IDataInit service = GetService();

            if (null != service)
            {
                service.Reset();
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInit_Click(object sender, EventArgs e)
        {
            UpdateDBConnection();

            IDataInit service = GetService();

            if (null != service)
            {
                service.Init();
            }
        }

        /// <summary>
        /// 下载最新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownload_Click(object sender, EventArgs e)
        {
            UpdateDBConnection();

            IDataInit service = GetService();

            if (null != service)
            {
                service.Download();
            }
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <returns></returns>
        private IDataInit GetService()
        {
            string combValue = this.combInitType.SelectedValue.ToString();

            if (combValue == "0")
            {
                MessageBox.Show("请选择要操作的数据类型");
                return null;
            }

            DataInitType initType = (DataInitType)Enum.Parse(typeof(DataInitType), combValue);

            IDataInit service = null;

            switch (initType)
            {
                case DataInitType.AdPosition:
                    service = new ADPositionInitService();
                    break;
                case DataInitType.ApiAuthoriza:
                    service = new ApiAuthorizaInitService();
                    break;
                case DataInitType.AreaCity:
                    service = new SystemAreaInitService();
                    break;
                case DataInitType.DefaultTianDaoAdminAccount:
                    service = new AdminAccountInitService();
                    break;
                case DataInitType.Empirical:
                    service = new EmpiricalInitService();
                    break;
                case DataInitType.GlobalConfig:
                    service = new GlobalConfigInitService();
                    break;
                case DataInitType.Industry:
                    service = new IndustryInitService();
                    break;
                case DataInitType.Level:
                    service = new UserLevelInitService();
                    break;
                case DataInitType.Points:
                    service = new PointsInitService();
                    break;
                case DataInitType.ServiceCategory:
                    service = new ServiceCategoryInitService();
                    break;
                case DataInitType.ProductLibraryCategory:
                    service = new ProductLibraryCategoryInitService();
                    break;
                case DataInitType.MerchantGoodsSystemCategory:
                    service = new MerchantGoodsSystemCategoryInitService();
                    break;
                case DataInitType.JobCategory:
                    service = new JobCategoryInitService();
                    break;
            }

            return service;
        }

        /// <summary>
        /// 更新数据库信息
        /// </summary>
        private void UpdateDBConnection()
        {
            var initDb = new DBConfig
            {
                DataServer = this.txtInitDbServer.Text.Trim(),
                DataBase = this.txtInitDbName.Text.Trim(),
                LoginAccount = this.txtInitDbAccount.Text.Trim(),
                Password = this.txtInitDbPwd.Text.Trim()
            };

            var downDb = new DBConfig
            {
                DataServer = this.txtDownDbServer.Text.Trim(),
                DataBase = this.txtDownDbName.Text.Trim(),
                LoginAccount = this.txtDownDbAccount.Text.Trim(),
                Password = this.txtDownDbPwd.Text.Trim()
            };

            DBConnectHelper.SetConnection(initDb, downDb);
        }

        #endregion

        #region////////////////// 结果/消息输出/////////////////////

        /// <summary>
        /// 消息输出委托事件
        /// </summary>
        private MsgWriter.WriterDelegate writerDelegate;

        /// <summary>
        /// 输出消息
        /// </summary>
        /// <param name="message"></param>
        private void WriteMessage(string message, bool padTime = true)
        {
            if (padTime)
            {
                message += DateTime.Now.ToString("      ### yyyy-MM-dd HH:mm:ss ###");
            }
            this.rtxtMsg.AppendText(message);
            this.rtxtMsg.AppendText("\n");
            this.rtxtMsg.Focus();
        }

        #endregion

        #region ///////////////进度/////////////

        private ProgressUpdater.ProgressDelegate progressDelegate;

        /// <summary>
        /// 更新进度
        /// </summary>
        /// <param name="value"></param>
        private void UpdateProgress(int value)
        {
            this.probarInit.Value = value;
        }

        #endregion
    }
}
