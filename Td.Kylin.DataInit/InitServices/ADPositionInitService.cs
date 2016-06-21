using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Td.Common;
using Td.Kylin.DataInit.Model;
using Td.Kylin.DataInit.ServiceProvider;

namespace Td.Kylin.DataInit.InitServices
{
    /// <summary>
    /// 广告位数据初始化服务
    /// </summary>
    public class ADPositionInitService : BaseInitService<AdPageModel>
    {
        public ADPositionInitService() : base(DataInitType.AdPosition, "xml/AdPosition.xml") { }

        public override bool Download()
        {
            if (null == this.DbReadData && this.DbReadData.Count() < 1) return false;

            if (File.Exists(this.DownloadFile))
            {
                File.Delete(this.DownloadFile);
            }

            //创建XML对象
            XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));

            //创建一个根节点
            XElement root = new XElement("root");
            xdoc.Add(root);

            var clients = this.DbReadData.Select(p => p.PlatformType).Distinct().ToArray();

            foreach (var pt in clients)
            {
                var ptName = EnumUtility.GetEnumDescription<EnumLibrary.ADTerminal>(pt);

                //创建一个客户端节点
                XElement cli = new XElement("client");

                #region 客户端属性
                //客户端类型ID
                cli.SetAttributeValue("id", pt);
                //客户端名称
                cli.SetAttributeValue("name", ptName);
                #endregion

                //遍历当前客户端下的广告页集合
                foreach(var page in this.DbReadData.Where(p=>p.PlatformType== pt))
                {
                    //创建一个广告页节点
                    XElement pg = new XElement("adpage");
                    pg.SetAttributeValue("id", page.ID);
                    pg.SetAttributeValue("name", page.Name);

                    foreach(var position in page.AdPositionList)
                    {
                        //创建一个广告位节点
                        XElement ps = new XElement("adposition");
                        ps.SetAttributeValue("id", position.ID);
                        ps.SetAttributeValue("code", position.Code);
                        ps.SetAttributeValue("name", position.Name);
                        ps.SetAttributeValue("maxcount", position.MaxCount);
                        ps.SetAttributeValue("enable", position.Enable);
                        ps.SetAttributeValue("displaytype", position.DisplayType);
                        ps.SetAttributeValue("pic", position.PreViewPicture);
                        ps.SetAttributeValue("intro", position.Intro);

                        pg.Add(ps);
                    }

                    cli.Add(pg);
                }

                root.Add(cli);
            }

            xdoc.Save(this.DownloadFile);

            return true;
        }

        public override bool Init(string connectionString)
        {
            return AdPositionProvider.InitDB(this.XmlReadData, connectionString);
        }

        protected override List<AdPageModel> ReadDB(string connectionString)
        {
            return AdPositionProvider.DownloadDB(connectionString);
        }

        protected override List<AdPageModel> ReadXml()
        {
            XElement xe = XElement.Load(this.XmlFilePath);

            //获取客户端
            IEnumerable<XElement> clients = from em in xe.Elements("client")
                                            select em;

            List<AdPageModel> list = new List<AdPageModel>();

            //遍历客户端
            foreach (var client in clients)
            {
                //客户端类型
                int platform = Convert.ToInt32(client.Attribute("key").Value);

                //遍历其下的子节点（即广告页）
                foreach (var page in client.Elements())
                {
                    var adpage = new AdPageModel();
                    adpage.ID = Convert.ToInt64(page.Attribute("id").Value);
                    adpage.Name = page.Attribute("name").Value;
                    adpage.PlatformType = platform;

                    var positionList = new List<AdPositionModel>();
                    //遍历页面下的子节点（即广告位）
                    foreach (var position in page.Elements())
                    {
                        var adposition = new AdPositionModel();
                        adposition.Code = position.Attribute("code").Value;
                        adposition.DisplayType = Convert.ToInt32(position.Attribute("displaytype").Value);
                        adposition.Enable = Convert.ToBoolean(position.Attribute("enable").Value);
                        adposition.ID = Convert.ToInt64(position.Attribute("id").Value);
                        adposition.Intro = position.Attribute("intro").Value;
                        adposition.MaxCount = Convert.ToInt32(position.Attribute("maxcount").Value);
                        adposition.Name = position.Attribute("name").Value;
                        adposition.PreViewPicture = position.Attribute("pic").Value;
                        adposition.PageID = adpage.ID;

                        positionList.Add(adposition);
                    }

                    adpage.AdPositionList = positionList;

                    list.Add(adpage);
                }
            }

            return list;
        }
    }
}
