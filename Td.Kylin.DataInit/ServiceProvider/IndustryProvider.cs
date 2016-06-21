using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Td.Kylin.DataInit.Data;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.ServiceProvider
{
    /// <summary>
    /// 行业数据业务提供
    /// </summary>
    public class IndustryProvider
    {
        /// <summary>
        /// 初始化行业
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool InitDB(IEnumerable<Merchant_Industry> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                foreach (var item in items)
                {
                    var model = db.Merchant_Industry.SingleOrDefault(p => p.IndustryID == item.IndustryID);

                    if (null != model)
                    {
                        db.Merchant_Industry.Attach(model);
                        db.Entry(model).State = EntityState.Modified;
                        model.Disabled = item.Disabled;
                        model.Icon = item.Icon;
                        model.Layer = item.Layer;
                        model.Name = item.Name;
                        model.OrderNo = item.OrderNo;
                        model.ParentID = item.ParentID;
                        model.TagStatus = item.TagStatus;
                    }
                    else
                    {
                        model = new Merchant_Industry();
                        model.IndustryID = item.IndustryID;
                        model.CreateTime = DateTime.Now;
                        model.Disabled = item.Disabled;
                        model.Icon = item.Icon;
                        model.Layer = item.Layer;
                        model.Name = item.Name;
                        model.OrderNo = item.OrderNo;
                        model.ParentID = item.ParentID;
                        model.TagStatus = item.TagStatus;

                        db.Merchant_Industry.Add(model);
                    }
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 从数据库下载数据
        /// </summary>
        /// <returns></returns>
        public static List<Merchant_Industry> DownloadDB(string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                return db.Merchant_Industry.ToList();
            }
        }
    }
}
