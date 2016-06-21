using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Td.Kylin.DataInit.ServiceProvider;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.InitServices
{
    /// <summary>
    /// 用户经验值获得规则初始化服务
    /// </summary>
    public class EmpiricalInitService : BaseInitService<System_EmpiricalConfig>
    {
        public EmpiricalInitService() : base(DataInitType.Empirical, "xml/Empirical.xml") { }

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
            XElement root = new XElement("empirical");
            xdoc.Add(root);

            foreach(var item in this.DbReadData)
            {
                XElement node = new XElement("rule");

                node.SetAttributeValue("biztype", item.ActivityType);
                node.SetAttributeValue("score", item.Score);
                node.SetAttributeValue("maxscore", item.MaxLimit);
                node.SetAttributeValue("unit", item.MaxUnit);
                node.SetAttributeValue("repeatable", item.Repeatable);

                root.Add(node);
            }

            xdoc.Save(this.DownloadFile);

            return true;
        }

        public override bool Init(string connectionString)
        {
            return EmpiricalProvider.InitDB(this.XmlReadData, connectionString);
        }

        protected override List<System_EmpiricalConfig> ReadDB(string connectionString)
        {
            return EmpiricalProvider.DownloadDB(connectionString);
        }

        protected override List<System_EmpiricalConfig> ReadXml()
        {
            XElement xe = XElement.Load(this.XmlFilePath);

            IEnumerable<XElement> elements = from em in xe.Elements("rule")
                                             select em;

            List<System_EmpiricalConfig> list = new List<System_EmpiricalConfig>();

            foreach (var em in elements)
            {
                System_EmpiricalConfig model = new System_EmpiricalConfig();

                model.ActivityType = Convert.ToInt32(em.Attribute("biztype").Value);
                model.MaxLimit = Convert.ToInt32(em.Attribute("maxscore").Value);
                model.MaxUnit = Convert.ToInt32(em.Attribute("unit").Value);
                model.Repeatable = Convert.ToBoolean(em.Attribute("repeatable").Value);
                model.Score = Convert.ToInt32(em.Attribute("score").Value);
                model.UpdateTime = DateTime.Now;

                list.Add(model);
            }

            return list;
        }
    }
}
