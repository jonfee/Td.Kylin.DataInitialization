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
    /// 全国区域数据初始化服务
    /// </summary>
    public class SystemAreaInitService : BaseInitService<System_Area>
    {
        public SystemAreaInitService() : base(DataInitType.AreaCity, "xml/SystemArea.xml") { }

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
            XElement root = new XElement("china");
            xdoc.Add(root);

            foreach(var area in this.DbReadData)
            {
                XElement node = new XElement("area");

                node.SetAttributeValue("id", area.AreaID);
                node.SetAttributeValue("name", area.AreaName);
                node.SetAttributeValue("depth", area.Depth);
                node.SetAttributeValue("haschild", area.HasChild);
                node.SetAttributeValue("layer", area.Layer);
                node.SetAttributeValue("namespell", area.NameSpell);
                node.SetAttributeValue("parentid", area.ParentID);
                node.SetAttributeValue("points", area.Points);

                root.Add(node);
            }

            xdoc.Save(this.DownloadFile);

            return true;
        }

        public override bool Init(string connectionString)
        {
            return SystemAreaProvider.InitDB(this.XmlReadData, connectionString);
        }

        protected override List<System_Area> ReadDB(string connectionString)
        {
            return SystemAreaProvider.DownloadDB(connectionString);
        }

        protected override List<System_Area> ReadXml()
        {
            XElement xe = XElement.Load(this.XmlFilePath);

            IEnumerable<XElement> elements = from em in xe.Elements("area")
                                             select em;

            List<System_Area> list = new List<System_Area>();

            foreach (var em in elements)
            {
                System_Area model = new System_Area();

                model.AreaID = Convert.ToInt32(em.Attribute("id").Value);
                model.AreaName = em.Attribute("name").Value;
                model.Depth = Convert.ToInt32(em.Attribute("depth").Value);
                model.HasChild = Convert.ToBoolean(em.Attribute("haschild").Value);
                model.Layer = em.Attribute("layer").Value;
                model.NameSpell = em.Attribute("namespell").Value;
                model.ParentID = Convert.ToInt32(em.Attribute("parentid").Value);
                model.Points = em.Attribute("points").Value;
                model.UpdateTime = DateTime.Now;

                list.Add(model);
            }

            return list;
        }
    }
}
