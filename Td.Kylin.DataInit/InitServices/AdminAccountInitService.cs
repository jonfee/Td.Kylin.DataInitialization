using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Td.Kylin.DataInit.Model;
using Td.Kylin.DataInit.ServiceProvider;

namespace Td.Kylin.DataInit.InitServices
{
    /// <summary>
    /// 天道后台管理员数据初始化服务
    /// </summary>
    public class AdminAccountInitService : BaseInitService<AdminAccountModel>
    {
        public AdminAccountInitService() : base(DataInitType.DefaultTianDaoAdminAccount, "xml/TiandaoAdminAccount.xml") { }

        public override bool Download()
        {
            if (null == this.DbReadData && this.DbReadData.Count() < 1) return false;

            if (File.Exists(this.DownloadFile))
            {
                File.Delete(this.DownloadFile);
            }

            //创建XML对象
            XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));

            //创建一个根节点
            XElement root = new XElement("root");
            xdoc.Add(root);

            foreach (var admin in this.DbReadData)
            {
                XElement node = new XElement("admin");

                node.SetAttributeValue("name", admin.RealName);
                node.SetAttributeValue("account", admin.Account);
                node.SetAttributeValue("password", admin.Password);
                node.SetAttributeValue("role", admin.Role);

                root.Add(node);
            }

            xdoc.Save(this.DownloadFile);

            return true;
        }

        public override bool Init(string connectionString)
        {
            return AdminAccountProvider.InitDB(this.XmlReadData, connectionString);
        }

        public override bool Reset(string connectionString)
        {
            return AdminAccountProvider.UpdateDB(this.XmlReadData, connectionString);
        }

        protected override List<AdminAccountModel> ReadDB(string connectionString)
        {
            return AdminAccountProvider.DownloadDB(connectionString);
        }

        protected override List<AdminAccountModel> ReadXml()
        {
            XElement xe = XElement.Load(this.XmlFilePath);

            IEnumerable<XElement> elements = from em in xe.Elements("admin")
                                             select em;

            List<AdminAccountModel> list = new List<AdminAccountModel>();

            foreach (var em in elements)
            {
                AdminAccountModel model = new AdminAccountModel();

                model.Account = em.Attribute("account").Value;
                model.Password = em.Attribute("password").Value;
                model.Role = Convert.ToInt32(em.Attribute("role").Value);
                model.RealName = em.Attribute("name").Value;

                list.Add(model);
            }

            return list;
        }
    }
}
