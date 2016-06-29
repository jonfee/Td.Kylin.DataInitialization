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
    /// 服务分类数据初始化服务
    /// </summary>
    public class ServiceCategoryInitService : BaseInitService<Service_SystemCategory>
    {
        public ServiceCategoryInitService() : base(DataInitType.ServiceCategory, "xml/ServiceCategory.xml") { }

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

            //获取一级分类
            var tops = this.DbReadData.Where(p => p.ParentCategoryID == 0).ToList();

            //遍历一级分类
            foreach (var top in tops)
            {
                //定义一级分类节点
                XElement first = new XElement("topcategory");

                first.SetAttributeValue("id", top.CategoryID);
                first.SetAttributeValue("name", top.Name);
                first.SetAttributeValue("icon", top.Icon);
                first.SetAttributeValue("isdisabled", top.IsDisabled);

                root.Add(first);

                //遍历子分类
                var childrens = this.DbReadData.Where(p => p.ParentCategoryID == top.CategoryID);

                foreach (var child in childrens)
                {
                    //定义子分类节点
                    XElement second = new XElement("category");

                    second.SetAttributeValue("id", child.CategoryID);
                    second.SetAttributeValue("name", child.Name);
                    second.SetAttributeValue("icon", child.Icon);
                    second.SetAttributeValue("isdisabled", child.IsDisabled);

                    first.Add(second);
                }
            }

            xdoc.Save(this.DownloadFile);

            return true;
        }

        public override bool Init(string connectionString)
        {
            return ServiceCategoryProvider.InitDB(this.XmlReadData, connectionString);
        }

        public override bool Reset(string connectionString)
        {
            return ServiceCategoryProvider.UpdateDB(this.XmlReadData, connectionString);
        }

        protected override List<Service_SystemCategory> ReadDB(string connectionString)
        {
            return ServiceCategoryProvider.DownloadDB(connectionString);
        }

        protected override List<Service_SystemCategory> ReadXml()
        {
            XElement xe = XElement.Load(this.XmlFilePath);

            IEnumerable<XElement> top = from em in xe.Elements("topcategory")
                                        select em;

            List<Service_SystemCategory> list = new List<Service_SystemCategory>();

            foreach (var ti in top)
            {
                var parent = new Service_SystemCategory();
                parent.CreateTime = DateTime.Now;
                parent.IsDisabled = Convert.ToBoolean(ti.Attribute("isdisabled").Value);
                parent.Icon = ti.Attribute("icon").Value;
                parent.CategoryID = Convert.ToInt64(ti.Attribute("id").Value);
                parent.CategoryPath = ti.Attribute("id").Value;
                parent.Name = ti.Attribute("name").Value;
                parent.OrderNo = 0;
                parent.ParentCategoryID = 0;

                list.Add(parent);

                //遍历子行业
                foreach (var category in ti.Elements())
                {
                    var child = new Service_SystemCategory();
                    child.CreateTime = DateTime.Now;
                    child.IsDisabled = Convert.ToBoolean(category.Attribute("isdisabled").Value);
                    child.Icon = category.Attribute("icon").Value;
                    child.CategoryID = Convert.ToInt64(category.Attribute("id").Value);
                    child.CategoryPath = parent.CategoryPath + "," + category.Attribute("id").Value;
                    child.Name = category.Attribute("name").Value;
                    child.OrderNo = 0;
                    child.ParentCategoryID = parent.CategoryID;

                    list.Add(child);
                }
            }

            return list;
        }
    }
}
