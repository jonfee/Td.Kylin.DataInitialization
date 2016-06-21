using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataInit.Data;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.ServiceProvider
{
    /// <summary>
    /// 用户等级规则数据业务提供
    /// </summary>
    public class LevelProvider
    {
        /// <summary>
        /// 初始化用户等级规则
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool InitDB(IEnumerable<System_Level> items,string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                var list = db.System_Level.ToList();

                if (null != list && list.Count > 0)
                {
                    db.System_Level.RemoveRange(list);
                }

                foreach (var item in items)
                {
                   var model = new System_Level();
                    model.CreateTime = DateTime.Now;
                    model.Enable = item.Enable;
                    model.Icon = item.Icon;
                    model.LevelID = item.LevelID;
                    model.Min = item.Min;
                    model.Name = item.Name;

                    db.System_Level.Add(model);
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 从数据库下载数据
        /// </summary>
        /// <returns></returns>
        public static List<System_Level> DownloadDB(string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                return db.System_Level.ToList();
            }
        }
    }
}
