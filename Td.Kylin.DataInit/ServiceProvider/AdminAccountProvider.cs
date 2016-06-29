using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataInit.Core;
using Td.Kylin.DataInit.Data;
using Td.Kylin.DataInit.Model;

namespace Td.Kylin.DataInit.ServiceProvider
{
    /// <summary>
    /// 天道后台管理员数据业务提供
    /// </summary>
    public class AdminAccountProvider
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool InitDB(IEnumerable<AdminAccountModel> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                var all = db.Admin_Account.ToList();
                //db.Admin_Account.AttachRange(all);
                db.Admin_Account.RemoveRange(all);

                foreach (var item in items)
                {
                    db.Admin_Account.Add(new Entity.Admin_Account
                    {
                        AdminID = Tools.NewId(),
                        CreateTime = DateTime.Now,
                        DataStatus = true,
                        LastIp = "127.0.0.1",
                        LastTime = DateTime.Now,
                        Logins = 0,
                        Password = item.Password,
                        PowerLevel = item.Role,
                        Realname = item.RealName,
                        Username = item.Account,
                        UserPic = string.Empty
                    });
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="items"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool UpdateDB(IEnumerable<AdminAccountModel> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                foreach (var item in items)
                {
                    var account = db.Admin_Account.FirstOrDefault(p => p.Username.Equals(item.Account, StringComparison.OrdinalIgnoreCase));

                    if (null != account)
                    {
                        db.Admin_Account.Attach(account);
                        db.Entry(account).State = EntityState.Modified;
                        account.DataStatus = true;
                        account.Password = item.Password;
                        account.PowerLevel = item.Role;
                        account.Realname = item.RealName;
                    }
                    else
                    {
                        db.Admin_Account.Add(new Entity.Admin_Account
                        {
                            AdminID = Tools.NewId(),
                            CreateTime = DateTime.Now,
                            DataStatus = true,
                            LastIp = "127.0.0.1",
                            LastTime = DateTime.Now,
                            Logins = 0,
                            Password = item.Password,
                            PowerLevel = item.Role,
                            Realname = item.RealName,
                            Username = item.Account,
                            UserPic = string.Empty
                        });
                    }
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 从数据库中加载管理员账号信息
        /// </summary>
        /// <returns></returns>
        public static List<AdminAccountModel> DownloadDB(string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                var query = from admin in db.Admin_Account
                            select new AdminAccountModel
                            {
                                Account = admin.Username,
                                Password = admin.Password,
                                RealName = admin.Realname,
                                Role = admin.PowerLevel
                            };

                return query.ToList();
            }
        }
    }
}
