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
    /// <summary>
    /// 产品库分类数据初始化服务
    /// </summary>
    public class ProductLibraryCategoryInitService : BaseInitService<Library_Category>
    {
        public ProductLibraryCategoryInitService() : base(DataInitType.ProductCategory, "xml/ProductLibraryCategory.xml") { }

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
                first.SetAttributeValue("icon", top.Ico);
                first.SetAttributeValue("disabled", top.Disabled);
                first.SetAttributeValue("description", top.Description);
                first.SetAttributeValue("depth", top.Depth);

                root.Add(first);

                //遍历子分类
                var childrens = this.DbReadData.Where(p => p.ParentID == top.CategoryID);

                foreach (var child in childrens)
                {
                    //定义子分类节点
                    XElement second = new XElement("category");

                    first.SetAttributeValue("id", top.CategoryID);
                    first.SetAttributeValue("name", top.Name);
                    first.SetAttributeValue("icon", top.Ico);
                    first.SetAttributeValue("disabled", top.Disabled);
                    first.SetAttributeValue("description", top.Description);
                    first.SetAttributeValue("depth", top.Depth);

                    first.Add(second);
                }
            }

            xdoc.Save(this.DownloadFile);

            return true;
        }

        public override bool Init(string connectionString)
        {
            return ProductLibraryCategoryProvider.InitDB(this.XmlReadData, connectionString);
        }

        protected override List<Library_Category> ReadDB(string connectionString)
        {
            return ProductLibraryCategoryProvider.DownloadDB(connectionString);
        }

        protected override List<Library_Category> ReadXml()
        {
            XElement xe = XElement.Load(this.XmlFilePath);

            IEnumerable<XElement> top = from em in xe.Elements("topcategory")
                                        select em;

            List<Library_Category> list = new List<Library_Category>();

            foreach (var ti in top)
            {
                var parent = new Library_Category();
                parent.CategoryID = Convert.ToInt64(ti.Attribute("id").Value);
                parent.CreateTime = DateTime.Now;
                parent.DeleteTime = DateTime.Now;
                parent.Disabled = Convert.ToBoolean(ti.Attribute("disabled").Value);
                parent.Ico = ti.Attribute("icon").Value;
                parent.CategoryID = Convert.ToInt64(ti.Attribute("id").Value);
                parent.Name = ti.Attribute("name").Value;
                parent.OrderNo = 0;
                parent.Depth = Convert.ToInt32(ti.Attribute("depth").Value);
                parent.Description = ti.Attribute("description").Value;
                parent.Layer = ti.Attribute("id").Value;
                parent.ParentID = 0;

                list.Add(parent);

                //遍历子行业
                foreach (var category in ti.Elements())
                {
                    var child = new Library_Category();
                    child.CreateTime = DateTime.Now;
                    child.DeleteTime = DateTime.Now;
                    child.Disabled = Convert.ToBoolean(category.Attribute("disabled").Value);
                    child.Ico = category.Attribute("icon").Value;
                    child.CategoryID = Convert.ToInt64(category.Attribute("id").Value);
                    child.Name = category.Attribute("name").Value;
                    child.OrderNo = 0;
                    child.Depth = Convert.ToInt32(category.Attribute("depth").Value);
                    child.Description = category.Attribute("description").Value;
                    child.Layer = parent.Layer + "," + category.Attribute("id").Value;
                    child.ParentID = parent.CategoryID;

                    list.Add(child);
                }
            }

            return list;
        }
    }
}
