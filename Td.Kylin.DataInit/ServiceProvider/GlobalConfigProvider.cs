using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataInit.Data;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.ServiceProvider
{
    /// <summary>
    /// 全局配置数据业务提供
    /// </summary>
    public class GlobalConfigProvider
    {
        /// <summary>
        /// 初始化全局配置
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool InitDB(IEnumerable<System_GlobalResources> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                var all = db.System_GlobalResources.ToList();
                //db.System_GlobalResources.AttachRange(all);
                db.System_GlobalResources.RemoveRange(all);

                foreach (var item in items)
                {
                    var model = new System_GlobalResources();
                    model.ResourceType = item.ResourceType;
                    model.ResourceKey = item.ResourceKey;
                    model.Group = item.Group;
                    model.Name = item.Name;
                    model.UpdateTime = DateTime.Now;
                    model.Value = item.Value;
                    model.ValueUnit = item.ValueUnit;

                    db.System_GlobalResources.Add(model);
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool UpdateDB(IEnumerable<System_GlobalResources> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                foreach (var item in items)
                {
                    var model = db.System_GlobalResources.FirstOrDefault(p => p.ResourceType == item.ResourceType && p.ResourceKey == item.ResourceKey);

                    if (null != model)
                    {
                        db.System_GlobalResources.Attach(model);
                        db.Entry(model).State = EntityState.Modified;
                        model.Group = item.Group;
                        model.Name = item.Name;
                        model.UpdateTime = DateTime.Now;
                        model.Value = item.Value;
                        model.ValueUnit = item.ValueUnit;
                    }
                    else
                    {
                        model = new System_GlobalResources();
                        model.ResourceType = item.ResourceType;
                        model.ResourceKey = item.ResourceKey;
                        model.Group = item.Group;
                        model.Name = item.Name;
                        model.UpdateTime = DateTime.Now;
                        model.Value = item.Value;
                        model.ValueUnit = item.ValueUnit;

                        db.System_GlobalResources.Add(model);
                    }
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 从数据库下载数据
        /// </summary>
        /// <returns></returns>
        public static List<System_GlobalResources> DownloadDB(string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                return db.System_GlobalResources.ToList();
            }
        }
    }
}
