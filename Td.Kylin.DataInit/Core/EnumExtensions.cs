using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Td.Kylin.DataInit.Core
{
    /// <summary>
    /// 枚举相关操作
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举常数名称与描述集合
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetNameDescription(Type enumType)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (enumType.IsEnum)
            {
                foreach (FieldInfo field in enumType.GetFields(BindingFlags.Static | BindingFlags.Public))
                {
                    string name = field.Name;

                    string description = name;

                    // 获取描述的属性。
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attr != null)
                    {
                        description = attr.Description;
                    }

                    dic.Add(name, description);
                }
            }

            return dic;
        }

        /// <summary>
        /// 获取枚举信息集合
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<EnumDesc<T>> GetEnumDesc<T>(Type enumType) where T : struct
        {
            List<EnumDesc<T>> list = new List<EnumDesc<T>>();

            if (enumType.IsEnum)
            {
                foreach (FieldInfo field in enumType.GetFields(BindingFlags.Static | BindingFlags.Public))
                {
                    string description = "";

                    // 获取描述的属性。
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attr != null)
                    {
                        description = attr.Description;
                    }

                    if (string.IsNullOrWhiteSpace(description)) description = field.Name;

                    list.Add(new EnumDesc<T>
                    {
                        EnumItem = (T)Enum.Parse(enumType, field.Name, true),
                        Value = (int)System.Enum.Parse(enumType, field.Name, true),
                        Name = field.Name,
                        Description = description
                    });
                }
            }

            return list;
        }

        /// <summary>
        /// 获取枚举成员的描述信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDesc<T>(object value)
        {
            if (null == value) return null;

            Type _type = typeof(T);

            if (_type.IsEnum)
            {
                foreach (FieldInfo field in _type.GetFields(BindingFlags.Static | BindingFlags.Public))
                {
                    bool finded = false;

                    if (field.Name.Equals(value.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        finded = true;
                    }
                    else
                    {
                        int _fv = (int)Enum.Parse(_type, field.Name, true);

                        int _val = 0;
                        if (!int.TryParse(value.ToString(), out _val)) _val = -1;
                        finded = _fv == _val;
                    }

                    if (finded)
                    {
                        // 获取描述的属性。
                        DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                        if (attr != null)
                        {
                            return attr.Description;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 枚举描述类
        /// </summary>
        public class EnumDesc<T> where T : struct
        {
            /// <summary>
            /// 枚举
            /// </summary>
            public T EnumItem { get; set; }

            /// <summary>
            /// 常数值表示
            /// </summary>
            public int Value { get; set; }

            /// <summary>
            /// 成员名
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 成员描述
            /// </summary>
            public string Description { get; set; }
        }
    }
}
