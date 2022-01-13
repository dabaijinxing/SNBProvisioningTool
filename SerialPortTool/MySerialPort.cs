using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace SerialPortTool
{
    class MySerialPort
    {
        public static SerialPort SerialPort1 = new SerialPort();                                //申明一个串口类
        public delegate void RecvEventHandler(object sender, SerialDataReceivedEventArgs e);    //申明一个方法

        /// <summary>
        /// 
        /// 获取串口名
        /// </summary>
        /// <returns>可用串口列表</returns>
        public string[] GetSerialPortName()
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames(); //获取电脑上可用串口号
            return ports;
        }

        /// <summary>
        /// 设置串口参数并且打开串口
        /// </summary>
        /// <param name="name">串口名</param>
        /// <param name="baudRate">波特率</param>
        public bool SetParam(string name , int baudRate, RecvEventHandler RecvHandle)
        {
            if (name != "")
            {
                try
                {           
                    SerialPort1.PortName = name;                //设置串口名
                    SerialPort1.BaudRate = baudRate;            //设置波特率 
                    SerialPort1.DataBits = 8;                   //设置数据位 8                             
                    SerialPort1.StopBits = StopBits.One;        //设置停止位
                    SerialPort1.Parity = Parity.None;           //设置奇偶校验
                    SerialPort1.Open();                         //打开串口

                    SerialPort1.ReadTimeout = -1;    //设置超时读取时间
                    SerialPort1.RtsEnable = true;    //定义DataReceived事件，当串口收到数据后触发事件

                    SerialPort1.DataReceived += new SerialDataReceivedEventHandler(RecvHandle);
                }
                catch 
                {
                    return false;         //打开串口失败
                }
            }
            return true;
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            try
            {
                SerialPort1.Dispose();  //释放掉原先的串口资源
                SerialPort1.Close();    //关闭串口
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 串口接收数据 --- 十六进制接收
        /// </summary>
        /// <returns>字节数组，接收到的数据</returns>
        public byte[] HexRecv()
        {
            int len = SerialPort1.BytesToRead;  //获取可以读取的字节数
            byte[] buff = new byte[len];        //创建缓存数据数组
            SerialPort1.Read(buff, 0, len);     //把数据读取到buff数组
            SerialPort1.DiscardInBuffer();      //清除串口接收缓冲区数据
            return buff;
        }

        /// <summary>
        /// 16进制字符串转化成字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] StrToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString = hexString.Insert(hexString.Length - 1, 0.ToString());
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 串口发送数据-- 16进制发送
        /// </summary>
        /// <param name="packByte">要发送的十六进制数据</param>
        public void HexSend(string hexPack)
        {
            int hexPacklen = (hexPack.Length / 2);
            byte[] packByte = StrToHexByte(hexPack); //将hexPack数据包转为字节数组
            try
            {
                if (packByte.Length > 0)
                {
                    SerialPort1.Write(packByte, 0, packByte.Length);
                }
            }
            catch (Exception) { }
        }
    }
}
