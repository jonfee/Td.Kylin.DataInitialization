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
    /// 商家商品系统分类数据初始化服务
    /// </summary>
    public class MerchantGoodsSystemCategoryInitService : BaseInitService<MerchantGoods_SystemCategory>
    {
        public MerchantGoodsSystemCategoryInitService() : base(DataInitType.MerchantGoodsSystemCategory, "xml/MerchantGoodsSystemCategory.xml.xml") { }

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
                first.SetAttributeValue("disabled", top.IsDisabled);

                root.Add(first);

                //遍历子分类
                var childrens = this.DbReadData.Where(p => p.ParentCategoryID == top.CategoryID);

                foreach (var child in childrens)
                {
                    //定义子分类节点
                    XElement second = new XElement("category");

                    second.SetAttributeValue("id", top.CategoryID);
                    second.SetAttributeValue("name", top.Name);
                    second.SetAttributeValue("icon", top.Icon);
                    second.SetAttributeValue("disabled", top.IsDisabled);

                    first.Add(second);
                }
            }

            xdoc.Save(this.DownloadFile);

            return true;
        }

        public override bool Init(string connectionString)
        {
            return MerchantGoodsSystemCategoryProvider.InitDB(this.XmlReadData, connectionString);
        }

        public override bool Reset(string connectionString)
        {
            return MerchantGoodsSystemCategoryProvider.UpdateDB(this.XmlReadData, connectionString);
        }

        protected override List<MerchantGoods_SystemCategory> ReadDB(string connectionString)
        {
            return MerchantGoodsSystemCategoryProvider.DownloadDB(connectionString);
        }

        protected override List<MerchantGoods_SystemCategory> ReadXml()
        {
            XElement xe = XElement.Load(this.XmlFilePath);

            IEnumerable<XElement> top = from em in xe.Elements("topcategory")
                                        select em;

            List<MerchantGoods_SystemCategory> list = new List<MerchantGoods_SystemCategory>();

            foreach (var ti in top)
            {
                var parent = new MerchantGoods_SystemCategory();
                parent.CategoryID = Convert.ToInt64(ti.Attribute("id").Value);
                parent.CreateTime = DateTime.Now;
                parent.IsDisabled = Convert.ToBoolean(ti.Attribute("disabled").Value);
                parent.Icon = ti.Attribute("icon").Value;
                parent.CategoryID = Convert.ToInt64(ti.Attribute("id").Value);
                parent.Name = ti.Attribute("name").Value;
                parent.OrderNo = 0;
                parent.CategoryPath = ti.Attribute("id").Value;
                parent.ParentCategoryID = 0;
                parent.IsDelete = false;

                list.Add(parent);

                //遍历子类
                foreach (var category in ti.Elements())
                {
                    var child = new MerchantGoods_SystemCategory();
                    child.CreateTime = DateTime.Now;
                    child.IsDisabled = Convert.ToBoolean(category.Attribute("disabled").Value);
                    child.Icon = category.Attribute("icon").Value;
                    child.CategoryID = Convert.ToInt64(category.Attribute("id").Value);
                    child.Name = category.Attribute("name").Value;
                    child.OrderNo = 0;
                    child.CategoryPath =parent.CategoryPath+","+ category.Attribute("id").Value;
                    child.ParentCategoryID = parent.CategoryID;
                    child.IsDelete = false;

                    list.Add(child);
                }
            }

            return list;
        }
    }
}
