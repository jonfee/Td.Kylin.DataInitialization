using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataInit.Data;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.ServiceProvider
{
    /// <summary>
    /// 产品库分类数据业务提供
    /// </summary>
    public class ProductLibraryCategoryProvider
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool InitDB(IEnumerable<Library_Category> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                var all = db.Library_Category.ToList();
                db.Library_Category.RemoveRange(all);

                foreach (var item in items)
                {
                    var model = new Library_Category();
                    model.Name = item.Name;
                    model.CategoryID = item.CategoryID;
                    model.Depth = item.Depth;
                    model.Description = item.Description;
                    model.Disabled = item.Disabled;
                    model.IsDelete = item.IsDelete;
                    model.Ico = item.Ico;
                    model.Layer = item.Layer;
                    model.ParentID = item.ParentID;
                    model.ProductNumber = 0;
                    model.CreateTime = DateTime.Now;
                    model.DeleteTime = DateTime.Now;
                    model.OrderNo = 0;

                    db.Library_Category.Add(model);
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool UpdateDB(IEnumerable<Library_Category> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                foreach (var item in items)
                {
                    var model = db.Library_Category.SingleOrDefault(p => p.CategoryID == item.CategoryID);

                    if (null != model)
                    {
                        db.Library_Category.Attach(model);
                        db.Entry(model).State = EntityState.Modified;
                        model.Name = item.Name;
                        model.IsDelete = item.IsDelete;
                        model.Depth = item.Depth;
                        model.Description = item.Description;
                        model.Disabled = item.Disabled;
                        model.IsDelete = item.IsDelete;
                        model.Ico = item.Ico;
                        model.Layer = item.Layer;
                        model.ParentID = item.ParentID;
                    }
                    else
                    {
                        model = new Library_Category();
                        model.Name = item.Name;
                        model.CategoryID = item.CategoryID;
                        model.Depth = item.Depth;
                        model.Description = item.Description;
                        model.Disabled = item.Disabled;
                        model.IsDelete = item.IsDelete;
                        model.Ico = item.Ico;
                        model.Layer = item.Layer;
                        model.ParentID = item.ParentID;
                        model.ProductNumber = 0;
                        model.CreateTime = DateTime.Now;
                        model.DeleteTime = DateTime.Now;
                        model.OrderNo = 0;

                        db.Library_Category.Add(model);
                    }
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 从数据库下载数据
        /// </summary>
        /// <returns></returns>
        public static List<Library_Category> DownloadDB(string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                return db.Library_Category.ToList();
            }
        }
    }
}
