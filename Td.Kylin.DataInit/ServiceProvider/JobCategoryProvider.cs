using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataInit.Data;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.ServiceProvider
{
    /// <summary>
    /// 岗位分类数据业务提供
    /// </summary>
    public class JobCategoryProvider
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool InitDB(IEnumerable<Job_Category> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                var all = db.Job_Category.ToList();
                //db.Job_Category.AttachRange(all);
                db.Job_Category.RemoveRange(all);

                db.SaveChanges();

                foreach (var item in items)
                {
                    var model = new Job_Category();
                    model.Name = item.Name;
                    model.ParentID = item.ParentID;
                    model.OrderNo = 0;
                    model.ApplyCount = 0;
                    model.CategoryID = item.CategoryID;
                    model.RecruitmentCount = 0;
                    model.ResumeCount = 0;
                    model.TagStatus = 0;
                    model.CreateTime = DateTime.Now;

                    db.Job_Category.Add(model);
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool UpdateDB(IEnumerable<Job_Category> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                foreach (var item in items)
                {
                    var model = db.Job_Category.SingleOrDefault(p => p.CategoryID == item.CategoryID);

                    if (null != model)
                    {
                        db.Job_Category.Attach(model);
                        db.Entry(model).State = EntityState.Modified;
                        model.Name = item.Name;
                        model.ParentID = item.ParentID;
                    }
                    else
                    {
                        model = new Job_Category();
                        model.Name = item.Name;
                        model.ParentID = item.ParentID;
                        model.OrderNo = 0;
                        model.ApplyCount = 0;
                        model.CategoryID = item.CategoryID;
                        model.RecruitmentCount = 0;
                        model.ResumeCount = 0;
                        model.TagStatus = 0;
                        model.CreateTime = DateTime.Now;

                        db.Job_Category.Add(model);
                    }
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 从数据库下载数据
        /// </summary>
        /// <returns></returns>
        public static List<Job_Category> DownloadDB(string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                return db.Job_Category.ToList();
            }
        }
    }
}
