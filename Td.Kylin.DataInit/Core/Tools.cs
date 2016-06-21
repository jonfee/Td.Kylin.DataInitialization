using System.IO;
using System.Text;
using Td.AspNet.Utils;

namespace Td.Kylin.DataInit.Core
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// 生成一个新的ID
        /// </summary>
        /// <returns></returns>
        public static long NewId()
        {
            return (long)IDCreater.NewId(1, 1);
        }

        /// <summary>
        /// 加层加密（MD5+SHA1+MD5）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return string.Empty;

            return Cryptography.MD5Encrypt(Cryptography.SHA1Encrypt(Cryptography.MD5Encrypt(str)));
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <returns></returns>
        public static string ReadFile(string filePath)
        {
            if (!File.Exists(filePath)) return string.Empty;

            using (var sr = new StreamReader(filePath, Encoding.UTF8))
            {
                string data = sr.ReadToEnd();

                return data;
            }
        }
    }
}
