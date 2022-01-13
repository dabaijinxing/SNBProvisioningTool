using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortTool
{
    class Log
    {
        //Stream stream = new FileStream(Directory.GetCurrentDirectory() + "\\log.txt", FileMode.Open);

        /// <summary>
        /// 创建log文件
        /// </summary>
        public void LogCreate(string path)
        {
            //string path = Directory.GetCurrentDirectory() + "\\log.txt";
            bool flag = File.Exists(path);          //判断当前路径下log是否存在
            if (!flag)
            {
                FileStream fs = File.Create(path);  //创建文件
                fs.Close();
            }
        }

        /// <summary>
        /// 向log文件中写如内容
        /// </summary>
        /// <param name="path">log 路径</param>
        /// <param name="log"> log 内容</param>
        public void FileWrite(string path, string log)
        {
           
            StreamWriter streamWriter = new StreamWriter(path,true);    //创建StreamWriter 类的实例,追加写入
            streamWriter.WriteLine(log);                                //向文件中写入log
            streamWriter.Flush();                                       //刷新缓存
            streamWriter.Close();                                       //关闭流
        }   
    }
}
