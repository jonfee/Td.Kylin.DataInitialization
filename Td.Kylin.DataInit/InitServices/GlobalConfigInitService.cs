using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Td.Common;
using Td.Kylin.DataInit.ServiceProvider;
using Td.Kylin.Entity;

namespace Td.Kylin.DataInit.InitServices
{
    /// <summary>
    /// 全局配置初始化服务
    /// </summary>
    public class GlobalConfigInitService : BaseInitService<System_GlobalResources>
    {
        public GlobalConfigInitService() : base(DataInitType.GlobalConfig, "xml/GlobalConfig.xml") { }

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

            //配置按模块分组
            var types = this.DbReadData.GroupBy(p => p.ResourceType).ToDictionary(k => k.Key, v => v.ToList());

            foreach(var rt in types)
            {
                //配置模块类型名称
                string moduleName = EnumUtility.GetEnumDescription<EnumLibrary.GlobalConfigType>(rt.Key);

                //创建模块节点
                XElement module = new XElement("module");
                module.SetAttributeValue("id", rt.Key);
                module.SetAttributeValue("name", moduleName);

                root.Add(module);

                //模块内配置分组
                var groups = rt.Value.GroupBy(p => p.Group).ToDictionary(k => k.Key, v => v.ToList());

                foreach(var g in groups)
                {
                    //创建分组节点
                    XElement group = new XElement("group");
                    group.SetAttributeValue("name", g.Key);

                    module.Add(group);

                    //遍历分组下的配置
                    foreach(var config in g.Value)
                    {
                        XElement option = new XElement("option");

                        option.SetAttributeValue("type", config.ResourceKey);
                        option.SetAttributeValue("name", config.Name);
                        option.SetAttributeValue("value", config.Value);
                        option.SetAttributeValue("unit", config.ValueUnit);

                        group.Add(option);
                    }
                }

            }

            xdoc.Save(this.DownloadFile);

            return true;
        }

        public override bool Init(string connectionString)
        {
            return GlobalConfigProvider.InitDB(this.XmlReadData,connectionString);
        }

        protected override List<System_GlobalResources> ReadDB(string connectionString)
        {
            return GlobalConfigProvider.DownloadDB(connectionString);
        }

        protected override List<System_GlobalResources> ReadXml()
        {
            XElement xe = XElement.Load(this.XmlFilePath);

            IEnumerable<XElement> modules = from em in xe.Elements("module")
                                            select em;

            List<System_GlobalResources> list = new List<System_GlobalResources>();

            foreach (var md in modules)
            {
                //配置模块类型
                int recourceType = Convert.ToInt32(md.Attribute("id").Value);

                //遍历其下的分组
                foreach (var group in md.Elements())
                {
                    //分组名
                    string groupName = group.Attribute("name").Value;

                    //遍历分组下的配置项
                    foreach (var option in group.Elements())
                    {
                        var config = new System_GlobalResources();

                        config.ResourceType = recourceType;
                        config.ResourceKey = Convert.ToInt32(option.Attribute("type").Value);
                        config.Name = option.Attribute("name").Value;
                        config.Group = groupName;
                        config.Value = option.Attribute("value").Value;
                        config.ValueUnit = Convert.ToInt32(option.Attribute("unit").Value);
                        config.UpdateTime = DateTime.Now;

                        list.Add(config);
                    }
                }
            }

            return list;
        }
    }
}
