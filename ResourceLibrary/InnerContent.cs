using System.IO;
using System.Collections.Generic;

namespace ResourceLibrary
{
    public class InnerContent
    {
        // 资源列表
        private static Stack<string[]> contents = new Stack<string[]>();

        public static string[] GetResource()
        {
            // 返回结果
            return contents.Count > 0 ? contents.Pop() : null;
        }

        public static int LoadResources()
        {
            // 清理内容
            contents.Clear();

            // 加载资源文件
            byte[] bytes = (byte[])ResourceLibrary.Properties.Resources.ResourceManager.GetObject("InnerContent.txt");
            // 创建内存流
            MemoryStream ms = new MemoryStream(bytes);
            // 创建阅读器
            StreamReader sr = new StreamReader(ms);

            string strLine;
            do
            {
                // 读取一行
                strLine = sr.ReadLine();
                // 检查结果
                if(strLine != null && strLine.Length > 0)
                {
                    // 分割字符串
                    string[] parameters = strLine.Split(",");
                    // 检查参数
                    if(parameters != null)
                    {
                        // 加入参数
                        if(parameters.Length == 2
                            || parameters.Length == 3) contents.Push(parameters);
                    }
                }

            } while (strLine != null);

            // 关闭阅读器
            sr.Close();
            // 关闭内存流
            ms.Close();
            // 返回结果
            return contents.Count;
        }
    }
}
