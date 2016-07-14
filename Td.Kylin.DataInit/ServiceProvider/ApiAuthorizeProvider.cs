using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataInit.Data;
using Td.Kylin.DataInit.Model;

namespace Td.Kylin.DataInit.ServiceProvider
{
    /// <summary>
    /// 接口模块授权数据业务提供
    /// </summary>
    public class ApiAuthorizeProvider
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool InitDB(IEnumerable<ApiAuthorizaModel> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                var all = db.System_ModuleAuthorize.ToList();
                //db.System_ModuleAuthorize.AttachRange(all);
                db.System_ModuleAuthorize.RemoveRange(all);

                foreach (var item in items)
                {
                    db.System_ModuleAuthorize.Add(new Entity.System_ModuleAuthorize
                    {
                        ServerID = item.ServerID,
                        ModuleID = item.ModuleID,
                        AppSecret = item.Secret,
                        Role = item.Role,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    });
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool UpdateDB(IEnumerable<ApiAuthorizaModel> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                foreach (var item in items)
                {
                    var authorize = db.System_ModuleAuthorize.FirstOrDefault(p => p.ServerID.Equals(item.ServerID, StringComparison.OrdinalIgnoreCase) && p.ModuleID.Equals(item.ModuleID, StringComparison.OrdinalIgnoreCase));

                    if (null != authorize)
                    {
                        db.System_ModuleAuthorize.Attach(authorize);
                        db.Entry(authorize).State = EntityState.Modified;
                        authorize.AppSecret = item.Secret;
                        authorize.Role = item.Role;
                        authorize.UpdateTime = DateTime.Now;
                    }
                    else
                    {
                        db.System_ModuleAuthorize.Add(new Entity.System_ModuleAuthorize
                        {
                            ServerID = item.ServerID,
                            ModuleID = item.ModuleID,
                            AppSecret = item.Secret,
                            Role = item.Role,
                            CreateTime = DateTime.Now,
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
        public static List<ApiAuthorizaModel> DownloadDB( string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                var query = from auth in db.System_ModuleAuthorize
                            select new ApiAuthorizaModel
                            {
                                ModuleID = auth.ModuleID,
                                Role = auth.Role,
                                Secret = auth.AppSecret,
                                ServerID = auth.ServerID
                            };

                return query.ToList();
            }
        }
    }
}
