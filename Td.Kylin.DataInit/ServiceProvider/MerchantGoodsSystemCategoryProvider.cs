using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataInit.Data;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.ServiceProvider
{
    /// <summary>
    /// 商家商品系统分类数据业务提供
    /// </summary>
    public class MerchantGoodsSystemCategoryProvider
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool InitDB(IEnumerable<MerchantGoods_SystemCategory> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                var all = db.Library_Category.ToList();
                db.Library_Category.RemoveRange(all);

                db.SaveChanges();

                foreach (var item in items)
                {
                    var model = new MerchantGoods_SystemCategory();
                    model.Name = item.Name;
                    model.CategoryID = item.CategoryID;
                    model.IsDelete = item.IsDelete;
                    model.CreateTime = DateTime.Now;
                    model.OrderNo = 0;
                    model.CategoryPath = item.CategoryPath;
                    model.Icon = item.Icon;
                    model.IsDisabled = item.IsDisabled;
                    model.ParentCategoryID = item.ParentCategoryID;

                    db.MerchantGoods_SystemCategory.Add(model);
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool UpdateDB(IEnumerable<MerchantGoods_SystemCategory> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                foreach (var item in items)
                {
                    var model = db.MerchantGoods_SystemCategory.SingleOrDefault(p => p.CategoryID == item.CategoryID);

                    if (null != model)
                    {
                        db.MerchantGoods_SystemCategory.Attach(model);
                        db.Entry(model).State = EntityState.Modified;
                        model.Name = item.Name;
                        model.IsDelete = item.IsDelete;
                        model.CreateTime = DateTime.Now;
                        model.OrderNo = 0;
                        model.CategoryPath = item.CategoryPath;
                        model.Icon = item.Icon;
                        model.IsDisabled = item.IsDisabled;
                        model.ParentCategoryID = item.ParentCategoryID;
                    }
                    else
                    {
                        model = new MerchantGoods_SystemCategory();
                        model.Name = item.Name;
                        model.CategoryID = item.CategoryID;
                        model.IsDelete = item.IsDelete;
                        model.CreateTime = DateTime.Now;
                        model.OrderNo = 0;
                        model.CategoryPath = item.CategoryPath;
                        model.Icon = item.Icon;
                        model.IsDisabled = item.IsDisabled;
                        model.ParentCategoryID = item.ParentCategoryID;

                        db.MerchantGoods_SystemCategory.Add(model);
                    }
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 从数据库下载数据
        /// </summary>
        /// <returns></returns>
        public static List<MerchantGoods_SystemCategory> DownloadDB(string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                return db.MerchantGoods_SystemCategory.ToList();
            }
        }
    }
}
