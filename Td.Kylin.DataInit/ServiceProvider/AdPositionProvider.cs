using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataInit.Data;
using Td.Kylin.DataInit.Model;

namespace Td.Kylin.DataInit.ServiceProvider
{
    /// <summary>
    /// 广告位数据业务提供
    /// </summary>
    public class AdPositionProvider
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="items"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool InitDB(IEnumerable<AdPageModel> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                var allPages = db.Ad_Page.ToList();
                //db.Ad_Page.AttachRange(allPages);
                db.Ad_Page.RemoveRange(allPages);

                var allPositions = db.Ad_Position.ToList();
                //db.Ad_Page.AttachRange(allPages);
                db.Ad_Position.RemoveRange(allPositions);

                db.SaveChanges();

                foreach (var item in items)
                {
                    db.Ad_Page.Add(new Entity.Ad_Page
                    {
                        PageID = item.ID,
                        PageName = item.Name,
                        PlatformType = item.PlatformType
                    });

                    foreach (var position in item.AdPositionList)
                    {
                        var adposition = new Entity.Ad_Position();

                        adposition.ADDisplayType = position.DisplayType;
                        adposition.Code = position.Code;
                        adposition.CreateTime = DateTime.Now;
                        adposition.Enable = position.Enable;
                        adposition.Name = position.Name;
                        adposition.Intro = position.Intro;
                        adposition.MaxCount = position.MaxCount;
                        adposition.PageID = position.PageID;
                        adposition.PositionID = position.ID;
                        adposition.PreviewPicture = position.PreViewPicture;

                        db.Ad_Position.Add(adposition);
                    }
                }

                return db.SaveChanges() > 0;
            }
        }

        public static bool UpdateDB(IEnumerable<AdPageModel> items, string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                if (null == items || items.Count() < 1) return false;

                foreach (var item in items)
                {
                    var page = db.Ad_Page.SingleOrDefault(p => p.PageID == item.ID);

                    if (null != page)
                    {
                        db.Ad_Page.Attach(page);
                        db.Entry(page).State = EntityState.Modified;
                        page.PageName = item.Name;
                        page.PlatformType = item.PlatformType;
                    }
                    else
                    {
                        db.Ad_Page.Add(new Entity.Ad_Page
                        {
                            PageID = item.ID,
                            PageName = item.Name,
                            PlatformType = item.PlatformType
                        });
                    }

                    foreach (var position in item.AdPositionList)
                    {
                        var adposition = db.Ad_Position.SingleOrDefault(p => p.PositionID == position.ID);

                        if (null != adposition)
                        {
                            db.Ad_Position.Attach(adposition);
                            db.Entry(adposition).State = EntityState.Modified;
                            adposition.ADDisplayType = position.DisplayType;
                            adposition.Code = position.Code;
                            adposition.CreateTime = DateTime.Now;
                            adposition.Enable = position.Enable;
                            adposition.Name = position.Name;
                            adposition.Intro = position.Intro;
                            adposition.MaxCount = position.MaxCount;
                            adposition.PageID = position.PageID;
                            adposition.PreviewPicture = position.PreViewPicture;
                        }
                        else
                        {
                            adposition = new Entity.Ad_Position();
                            adposition.ADDisplayType = position.DisplayType;
                            adposition.Code = position.Code;
                            adposition.CreateTime = DateTime.Now;
                            adposition.Enable = position.Enable;
                            adposition.Name = position.Name;
                            adposition.Intro = position.Intro;
                            adposition.MaxCount = position.MaxCount;
                            adposition.PageID = position.PageID;
                            adposition.PositionID = position.ID;
                            adposition.PreviewPicture = position.PreViewPicture;

                            db.Ad_Position.Add(adposition);
                        }
                    }
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 从数据库下载数据
        /// </summary>
        /// <returns></returns>
        public static List<AdPageModel> DownloadDB(string connectionString)
        {
            using (var db = new DataContext(connectionString))
            {
                var list = (from ps in (from position in db.Ad_Position
                                        select new AdPositionModel
                                        {
                                            Code = position.Code,
                                            DisplayType = position.ADDisplayType,
                                            Enable = position.Enable,
                                            ID = position.PositionID,
                                            Intro = position.Intro,
                                            MaxCount = position.MaxCount,
                                            Name = position.Name,
                                            PageID = position.PageID,
                                            PreViewPicture = position.PreviewPicture

                                        }).AsEnumerable()
                            join page in db.Ad_Page
                            on ps.PageID equals page.PageID
                            into myjoin
                            from page in myjoin.DefaultIfEmpty()
                            group ps by new
                            {
                                PageID = page != null ? page.PageID : 0,
                                PageName = page != null ? page.PageName : "",
                                PlatformType = page != null ? page.PlatformType : 0
                            }
                                into g
                            select new AdPageModel
                            {
                                ID = g.Key.PageID,
                                Name = g.Key.PageName,
                                PlatformType = g.Key.PlatformType,
                                AdPositionList = g.ToList()
                            }).ToList();

                return list;
            }
        }
    }
}
