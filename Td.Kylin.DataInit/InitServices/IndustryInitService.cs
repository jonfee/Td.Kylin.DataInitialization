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
    /// 行业数据初始化服务
    /// </summary>
    public class IndustryInitService : BaseInitService<Merchant_Industry>
    {
        public IndustryInitService() : base(DataInitType.Industry, "xml/Industry.xml") { }

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

            //获取一级行业
            var tops = this.DbReadData.Where(p => p.ParentID == 0).ToList();

            //遍历一级行业
            foreach(var top in tops)
            {
                //定义一级行业节点
                XElement first = new XElement("topindustry");

                first.SetAttributeValue("id", top.IndustryID);
                first.SetAttributeValue("name", top.Name);
                first.SetAttributeValue("icon", top.Icon);
                first.SetAttributeValue("disabled", top.Disabled);
                first.SetAttributeValue("tagstatus", top.TagStatus);

                root.Add(first);

                //遍历子行业
                var childrens = this.DbReadData.Where(p => p.ParentID == top.IndustryID);

                foreach(var child in childrens)
                {
                    //定义子行业节点
                    XElement second = new XElement("industry");

                    second.SetAttributeValue("id", child.IndustryID);
                    second.SetAttributeValue("name", child.Name);
                    second.SetAttributeValue("icon", child.Icon);
                    second.SetAttributeValue("disabled", child.Disabled);
                    second.SetAttributeValue("tagstatus", child.TagStatus);

                    first.Add(second);
                }
            }

            xdoc.Save(this.DownloadFile);

            return true;
        }

        public override bool Init(string connectionString)
        {
            return IndustryProvider.InitDB(this.XmlReadData, connectionString);
        }

        public override bool Reset(string connectionString)
        {
            return IndustryProvider.UpdateDB(this.XmlReadData, connectionString);
        }

        protected override List<Merchant_Industry> ReadDB(string connectionString)
        {
            return IndustryProvider.DownloadDB(connectionString);
        }

        protected override List<Merchant_Industry> ReadXml()
        {
            XElement xe = XElement.Load(this.XmlFilePath);

            IEnumerable<XElement> top = from em in xe.Elements("topindustry")
                                        select em;

            List<Merchant_Industry> list = new List<Merchant_Industry>();

            foreach (var ti in top)
            {
                var parent = new Merchant_Industry();
                parent.CreateTime = DateTime.Now;
                parent.Disabled = Convert.ToBoolean(ti.Attribute("disabled").Value);
                parent.Icon = ti.Attribute("icon").Value;
                parent.IndustryID = Convert.ToInt64(ti.Attribute("id").Value);
                parent.Layer = ti.Attribute("id").Value;
                parent.Name = ti.Attribute("name").Value;
                parent.OrderNo = 0;
                parent.ParentID = 0;
                parent.TagStatus = Convert.ToInt32(ti.Attribute("tagstatus").Value);

                list.Add(parent);
                
                //遍历子行业
                foreach (var industry in ti.Elements())
                {
                    var child = new Merchant_Industry();
                    child.CreateTime = DateTime.Now;
                    child.Disabled = Convert.ToBoolean(industry.Attribute("disabled").Value);
                    child.Icon = industry.Attribute("icon").Value;
                    child.IndustryID = Convert.ToInt64(industry.Attribute("id").Value);
                    child.Layer = parent.Layer + "," + industry.Attribute("id").Value;
                    child.Name = industry.Attribute("name").Value;
                    child.OrderNo = 0;
                    child.ParentID = parent.IndustryID;
                    child.TagStatus = Convert.ToInt32(industry.Attribute("tagstatus").Value);

                    list.Add(child);
                }
            }

            return list;
        }
    }
}
