using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Td.Kylin.DataInit.Core;
using Td.Kylin.DataInit.ServiceProvider;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.InitServices
{
    /// <summary>
    /// 用户等级规则数据初始化服务
    /// </summary>
    public class UserLevelInitService : BaseInitService<System_Level>
    {
        public UserLevelInitService() : base(DataInitType.Level, "xml/UserLevel.xml") { }

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

            foreach (var item in this.DbReadData)
            {
                XElement node = new XElement("level");

                node.SetAttributeValue("name", item.Name);
                node.SetAttributeValue("min", item.Min);
                node.SetAttributeValue("icon", item.Icon);
                node.SetAttributeValue("enable", item.Enable);

                root.Add(node);
            }

            xdoc.Save(this.DownloadFile);

            return true;
        }

        public override bool Init(string connectionString)
        {
            return LevelProvider.InitDB(this.XmlReadData, connectionString);
        }

        public override bool Reset(string connectionString)
        {
            return LevelProvider.UpdateDB(this.XmlReadData, connectionString);
        }

        protected override List<System_Level> ReadDB(string connectionString)
        {
            return LevelProvider.DownloadDB(connectionString);
        }

        protected override List<System_Level> ReadXml()
        {
            XElement xe = XElement.Load(this.XmlFilePath);

            IEnumerable<XElement> elements = from em in xe.Elements("level")
                                             select em;

            List<System_Level> list = new List<System_Level>();

            foreach (var em in elements)
            {
                System_Level model = new System_Level();

                model.Min = Convert.ToInt32(em.Attribute("min").Value);
                model.Enable = Convert.ToBoolean(em.Attribute("enable").Value);
                model.CreateTime = DateTime.Now;
                model.Icon = em.Attribute("icon").Value;
                model.Name = em.Attribute("name").Value;
                model.LevelID = Tools.NewId();

                list.Add(model);
            }

            return list;
        }
    }
}
