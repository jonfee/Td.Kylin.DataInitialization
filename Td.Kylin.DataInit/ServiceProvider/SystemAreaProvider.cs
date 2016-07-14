using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataInit.Data;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.ServiceProvider
{
    /// <summary>
    /// 全国区域数据业务提供
    /// </summary>
    public class SystemAreaProvider
    {
        /// <summary>
        /// 初始化全国区域
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool InitDB(IEnumerable<System_Area> items,string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                var all = db.System_Area.ToList();
                db.System_Area.RemoveRange(all);

                foreach (var item in items)
                {
                    var area = db.System_Area.SingleOrDefault(p => p.AreaID == item.AreaID);

                    if (null != area)
                    {
                        db.System_Area.Attach(area);
                        db.Entry(area).State = EntityState.Modified;
                        area.AreaName = item.AreaName;
                        area.Depth = item.Depth;
                        area.HasChild = item.HasChild;
                        area.Layer = item.Layer;
                        area.NameSpell = item.NameSpell;
                        area.ParentID = item.ParentID;
                        area.Points = item.Points;
                        area.UpdateTime = DateTime.Now;
                    }
                    else
                    {
                        db.System_Area.Add(new Entity.System_Area
                        {
                            AreaID = item.AreaID,
                            AreaName = item.AreaName,
                            Depth = item.Depth,
                            HasChild = item.HasChild,
                            Layer = item.Layer,
                            NameSpell = item.NameSpell,
                            ParentID = item.ParentID,
                            Points = item.Points,
                            UpdateTime = DateTime.Now
                        });
                    }
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool UpdateDB(IEnumerable<System_Area> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                foreach (var item in items)
                {
                    var area = db.System_Area.SingleOrDefault(p => p.AreaID == item.AreaID);

                    if (null != area)
                    {
                        db.System_Area.Attach(area);
                        db.Entry(area).State = EntityState.Modified;
                        area.AreaName = item.AreaName;
                        area.Depth = item.Depth;
                        area.HasChild = item.HasChild;
                        area.Layer = item.Layer;
                        area.NameSpell = item.NameSpell;
                        area.ParentID = item.ParentID;
                        area.Points = item.Points;
                        area.UpdateTime = DateTime.Now;
                    }
                    else
                    {
                        db.System_Area.Add(new Entity.System_Area
                        {
                            AreaID = item.AreaID,
                            AreaName = item.AreaName,
                            Depth = item.Depth,
                            HasChild = item.HasChild,
                            Layer = item.Layer,
                            NameSpell = item.NameSpell,
                            ParentID = item.ParentID,
                            Points = item.Points,
                            UpdateTime = DateTime.Now
                        });
                    }
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 从数据库下载数据
        /// </summary>
        /// <returns></returns>
        public static List<System_Area> DownloadDB(string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                return db.System_Area.ToList();
            }
        }
    }
}
