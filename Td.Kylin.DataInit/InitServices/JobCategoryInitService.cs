using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Td.Kylin.DataInit.ServiceProvider;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.InitServices
{
    public class JobCategoryInitService : BaseInitService<Job_Category>
    {
        public JobCategoryInitService() : base(DataInitType.JobCategory, "xml/JobCategory.xml") { }

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
            var tops = this.DbReadData.Where(p => p.ParentID == 0).ToList();

            //遍历一级分类
            foreach (var top in tops)
            {
                //定义一级分类节点
                XElement first = new XElement("topcategory");

                first.SetAttributeValue("id", top.CategoryID);
                first.SetAttributeValue("name", top.Name);

                root.Add(first);

                //遍历子分类
                var childrens = this.DbReadData.Where(p => p.ParentID == top.CategoryID);

                foreach (var child in childrens)
                {
                    //定义子分类节点
                    XElement second = new XElement("category");

                    first.SetAttributeValue("id", top.CategoryID);
                    first.SetAttributeValue("name", top.Name);

                    first.Add(second);
                }
            }

            xdoc.Save(this.DownloadFile);

            return true;
        }

        public override bool Init(string connectionString)
        {
            return JobCategoryProvider.InitDB(this.XmlReadData, connectionString);
        }

        public override bool Reset(string connectionString)
        {
            return JobCategoryProvider.UpdateDB(this.XmlReadData, connectionString);
        }

        protected override List<Job_Category> ReadDB(string connectionString)
        {
            return JobCategoryProvider.DownloadDB(connectionString);
        }

        protected override List<Job_Category> ReadXml()
        {
            XElement xe = XElement.Load(this.XmlFilePath);

            IEnumerable<XElement> top = from em in xe.Elements("topcategory")
                                        select em;

            List<Job_Category> list = new List<Job_Category>();

            foreach (var ti in top)
            {
                var parent = new Job_Category();
                parent.CategoryID = Convert.ToInt64(ti.Attribute("id").Value);
                parent.Name = ti.Attribute("name").Value;

                list.Add(parent);

                //遍历子行业
                foreach (var category in ti.Elements())
                {
                    var child = new Job_Category();
                    child.CategoryID = Convert.ToInt64(category.Attribute("id").Value);
                    child.Name = category.Attribute("name").Value;
                    child.CreateTime = DateTime.Now;
                    child.ParentID = parent.CategoryID;

                    list.Add(child);
                }
            }

            return list;
        }
    }
}
