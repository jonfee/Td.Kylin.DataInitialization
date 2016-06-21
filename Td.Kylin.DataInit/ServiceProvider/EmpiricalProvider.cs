using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataInit.Data;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.ServiceProvider
{
    /// <summary>
    /// 用户经验值获取规则数据提供
    /// </summary>
    public class EmpiricalProvider
    {
        /// <summary>
        /// 初始化用户经验值获取规则
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool InitDB(IEnumerable<System_EmpiricalConfig> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                foreach (var item in items)
                {
                    var model = db.System_EmpiricalConfig.SingleOrDefault(p => p.ActivityType == item.ActivityType);

                    if (null != model)
                    {
                        db.System_EmpiricalConfig.Attach(model);
                        db.Entry(model).State = EntityState.Modified;
                        model.MaxLimit = item.MaxLimit;
                        model.MaxUnit = item.MaxUnit;
                        model.Repeatable = item.Repeatable;
                        model.Score = item.Score;
                        model.UpdateTime = DateTime.Now;
                    }
                    else
                    {
                        model = new System_EmpiricalConfig();
                        model.ActivityType = item.ActivityType;
                        model.MaxLimit = item.MaxLimit;
                        model.MaxUnit = item.MaxUnit;
                        model.Repeatable = item.Repeatable;
                        model.Score = item.Score;
                        model.UpdateTime = DateTime.Now;

                        db.System_EmpiricalConfig.Add(model);
                    }
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 从数据库下载数据
        /// </summary>
        /// <returns></returns>
        public static List<System_EmpiricalConfig> DownloadDB( string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                return db.System_EmpiricalConfig.ToList();
            }
        }
    }
}
