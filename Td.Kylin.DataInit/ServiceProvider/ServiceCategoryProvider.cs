using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataInit.Data;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.ServiceProvider
{
    /// <summary>
    /// 系统服务分类数据业务提供
    /// </summary>
    public class ServiceCategoryProvider
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool InitDB(IEnumerable<Service_SystemCategory> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                foreach (var item in items)
                {
                    var model = db.Service_SystemCategory.SingleOrDefault(p => p.CategoryID == item.CategoryID);

                    if (null != model)
                    {
                        db.Service_SystemCategory.Attach(model);
                        db.Entry(model).State = EntityState.Modified;
                        model.IsDisabled = item.IsDisabled;
                        model.Icon = item.Icon;
                        model.CategoryPath = item.CategoryPath;
                        model.Name = item.Name;
                        model.OrderNo = item.OrderNo;
                        model.IsDelete = item.IsDelete;
                        model.ParentCategoryID = item.ParentCategoryID;
                    }
                    else
                    {
                        model = new Service_SystemCategory();
                        model.CategoryID = item.CategoryID;
                        model.IsDisabled = item.IsDisabled;
                        model.Icon = item.Icon;
                        model.CategoryPath = item.CategoryPath;
                        model.Name = item.Name;
                        model.OrderNo = item.OrderNo;
                        model.IsDelete = item.IsDelete;
                        model.ParentCategoryID = item.ParentCategoryID;
                        model.CreateTime = DateTime.Now;

                        db.Service_SystemCategory.Add(model);
                    }
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 从数据库下载数据
        /// </summary>
        /// <returns></returns>
        public static List<Service_SystemCategory> DownloadDB(string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                return db.Service_SystemCategory.ToList();
            }
        }
    }
}
