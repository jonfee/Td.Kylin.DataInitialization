using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Td.Kylin.DataInit.Core;
using Td.Kylin.DataInit.Data;

namespace Td.Kylin.DataInit.InitServices
{
    /// <summary>
    /// 数据初始化服务抽象基类
    /// </summary>
    public abstract class BaseInitService<TResult> : IDataInit
    {
        public BaseInitService(DataInitType initType, string xmlDataFile)
        {
            this.Name = EnumExtensions.GetDesc<DataInitType>(initType.ToString());

            this.XmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlDataFile);
        }

        /// <summary>
        /// XML文件路径
        /// </summary>
        protected string XmlFilePath { get; private set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// XML中读取数据
        /// </summary>
        public IEnumerable<TResult> XmlReadData { get; private set; }

        /// <summary>
        /// DB中读取数据
        /// </summary>
        public IEnumerable<TResult> DbReadData { get; private set; }

        /// <summary>
        /// 下载文件路径
        /// </summary>
        protected string DownloadFile
        {
            get
            {
                return XmlFilePath.ToLower().Replace(".xml", "_download.xml");
            }
        }

        /// <summary>
        /// 抽象从文件中读取初始数据方法
        /// </summary>
        /// <returns></returns>
        protected abstract List<TResult> ReadXml();

        /// <summary>
        /// 抽象从数据库中读取数据方法
        /// </summary>
        /// <returns></returns>
        protected abstract List<TResult> ReadDB(string connectionString);

        /// <summary>
        /// 实现接口 Init（初始化到数据库）
        /// </summary>
        /// <returns></returns>
        bool IDataInit.Init()
        {
            return ThreadPool.QueueUserWorkItem((state) =>
             {
                 try
                 {
                     MsgWriter.Instance.Write(string.Format("[{0}]初始化开始……", this.Name));

                     ProgressUpdater.Instance.Update(0);

                     Stopwatch watch = new Stopwatch();
                     watch.Start();

                     this.XmlReadData = ReadXml();

                     ProgressUpdater.Instance.Update(30);

                     bool success = Init(DBConnectionRoot.InitDBConnection.ConnectionString);

                     watch.Stop();

                     //完成后操作
                     if (success)
                     {
                         MsgWriter.Instance.Write(string.Format("[{0}]初始化成功，用时{1}毫秒。", this.Name, watch.ElapsedMilliseconds));

                         string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this.XmlReadData);

                         MsgWriter.Instance.Write(string.Format("[{0}]初始化数据结果：", this.Name), false);
                         MsgWriter.Instance.Write(jsonData, false);
                     }
                     else
                     {
                         MsgWriter.Instance.Write(string.Format("[{0}]初始化失败！", this.Name));
                     }

                     ProgressUpdater.Instance.Update(100);
                 }
                 catch (Exception ex)
                 {
                     OnException(ex);
                 }
             }, null);
        }

        /// <summary>
        /// 实现接口Download（从数据库下载最新）
        /// </summary>
        /// <returns></returns>
        bool IDataInit.Download()
        {
            return ThreadPool.QueueUserWorkItem((state) =>
            {
                try
                {
                    MsgWriter.Instance.Write(string.Format("[{0}]下载最新数据开始……", this.Name));

                    ProgressUpdater.Instance.Update(0);

                    Stopwatch watch = new Stopwatch();
                    watch.Start();

                    this.DbReadData = ReadDB(DBConnectionRoot.DownloadSourceDBConnection.ConnectionString);

                    ProgressUpdater.Instance.Update(30);

                    bool success = Download();

                    watch.Stop();

                    //完成后操作
                    if (success)
                    {
                        MsgWriter.Instance.Write(string.Format("[{0}]下载最新数据成功，用时{1}毫秒。", this.Name, watch.ElapsedMilliseconds));

                        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this.DbReadData);

                        MsgWriter.Instance.Write(string.Format("[{0}]下载最新数据数据结果：", this.Name), false);
                        MsgWriter.Instance.Write(jsonData, false);
                    }
                    else
                    {
                        MsgWriter.Instance.Write(string.Format("[{0}]下载最新数据失败！", this.Name));
                    }

                    ProgressUpdater.Instance.Update(100);
                }
                catch (Exception ex)
                {
                    OnException(ex);
                }
            }, null);
        }


        /// <summary>
        /// 抽象初始化到数据库执行方法
        /// </summary>
        /// <returns></returns>
        public abstract bool Init(string connectionString);

        /// <summary>
        /// 抽象下载最新数据执行方法
        /// </summary>
        /// <returns></returns>
        public abstract bool Download();

        /// <summary>
        /// 异常处理－实现接口 
        /// </summary>
        /// <param name="ex"></param>
        public void OnException(Exception ex)
        {
            if (null == ex) return;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("异常信息：{0}", ex.Message));
            sb.AppendLine(string.Format("异常堆栈：{0}", ex.StackTrace));

            MsgWriter.Instance.Write(sb.ToString(), true);
        }
    }
}
