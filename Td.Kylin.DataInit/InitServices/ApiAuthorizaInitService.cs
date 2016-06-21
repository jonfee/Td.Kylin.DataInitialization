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
    /// API与模块授权数据初始化服务
    /// </summary>
    public class ApiAuthorizaInitService : BaseInitService<ApiAuthorizaModel>
    {
        public ApiAuthorizaInitService() : base(DataInitType.ApiAuthoriza, "xml/ApiAuthoriza.xml") { }

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

            foreach(var item in this.DbReadData)
            {
                XElement node = new XElement("authorize");

                node.SetAttributeValue("serverid", item.ServerID);
                node.SetAttributeValue("moduleid", item.ModuleID);
                node.SetAttributeValue("secret", item.Secret);
                node.SetAttributeValue("role", item.Role);

                root.Add(node);
            }

            xdoc.Save(this.DownloadFile);

            return true;
        }

        public override bool Init(string connectionString)
        {
            return ApiAuthorizeProvider.InitDB(this.XmlReadData, connectionString);
        }

        protected override List<ApiAuthorizaModel> ReadDB(string connectionString)
        {
            return ApiAuthorizeProvider.DownloadDB(connectionString);
        }

        protected override List<ApiAuthorizaModel> ReadXml()
        {
            XElement xe = XElement.Load(this.XmlFilePath);

            IEnumerable<XElement> elements = from em in xe.Elements("authorize")
                                             select em;

            List<ApiAuthorizaModel> list = new List<ApiAuthorizaModel>();

            foreach (var em in elements)
            {
                ApiAuthorizaModel model = new ApiAuthorizaModel();

                model.ServerID = em.Attribute("serverid").Value;
                model.ModuleID = em.Attribute("moduleid").Value;
                model.Secret = em.Attribute("secret").Value;
                model.Role = Convert.ToInt32(em.Attribute("role").Value);

                list.Add(model);
            }

            return list;
        }
    }
}
