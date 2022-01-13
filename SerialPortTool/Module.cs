using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SerialPortTool
{
    class Module
    {
        //public static string uuid;
        //public static string token;
        //public static string sign;
        public static string head = "55AA";
        public static string uuid_token_code_id = "0101";
        public static string Mac_sn_code_id = "0102";

        //static string fileLock = "lock";
        //字符串数组数据存储格式 ReadMac[uuid, token, sign, sn, mac] 无SQL测试数据
        public string[,] ReadMac = {{"ff472a17f13b4bdfb723691d88757226", "MYHAMFACAQECAQEESDBGAiEAkW7/FWZUOH1czEip+6/+5Cv9scq9XaIOjrpzideKl+4CIQDZsiCdzP96Nt/RCCEX7YgAoX8UFye+09ajFyK64i9wMTBsAgECAgEBBGQxYjAJAgFmAgEBBAECMBACAWUCAQEECLC47gN+AQAAMBECAgDKAgEBBAgAAAAAAAAACDAWAgIAyQIBAQQNNDkwOTI2LTE3MzgyNTAYAgFnAgEBBBAwJSbQbCFNP5URKqo/EGF0", "MEUCIQCEl3vr167xVMzJFPVYnIwRtPIjy8QGi23D8eOt7CarLQIgIP1CUQ7BIp3xQGp0gO1jwscuJ/CHwdTNhOWjbfDZL7UAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==", "30313233343536373839414243444546", "010203040506"},
                                    {"28ab60c6a8294af1bc0e8beebd39ef66", "MYHAMFACAQECAQEESDBGAiEAmCuaznk9/1fX8jGWnayNtO1qurHvEFIWVLgtC5+yIJ4CIQDOVn+pMDjfkZpZgkZ1GaI98LLsubUuJQ9piSY60POsbTBsAgECAgEBBGQxYjAJAgFmAgEBBAECMBACAWUCAQEECIa47gN+AQAAMBECAgDKAgEBBAgAAAAAAAAACDAWAgIAyQIBAQQNNDkwOTI2LTE3MzgyNTAYAgFnAgEBBBC0icSk8q9NPrSRmIFNU5eo", "MEUCIQDnYPxPcIP/o9b2DKEOyMIotDSNHNChzV+yUJI3BIEWmQIgEbd8q7PinuLGHYMPSjiVFIBRISH1oMyOrm7wI4OBF2sAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==", "30313233343536373839414243444546", "060504030201"}};

        /// <summary>
        /// 整形16进制转化成字符串样式的十六进制 如：0xae00cf => "AE00CF 
        /// </summary>
        /// <param name="bytes">整形十六进制字节数组</param>
        /// <returns>字符型样式的十六进制</returns>
        public string HexToString(byte[] bytes) 
        {
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                }
                hexString = strB.ToString();
            }
            return hexString;
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="argStr">Base64数据</param>
        /// <returns>解码成功的16进制字符串</returns>
        public string Base64ToHex(string argStr)
        {
            
            byte[] outputb = Convert.FromBase64String(argStr);  //解码 -- byte中存放整形的16进制数
            string str = HexToString(outputb);                  //将整形的16进制数据，转化成string样式的
            return str;
        }
        /// <summary>
        /// 组包转化成字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public byte[] StrToHexByte(string hexString)
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
        /// 异或和校验
        /// </summary>
        /// <param name="data">校验数据</param>
        /// <param name="temp">输出校验结果</param>
        /// 
        public void BCC(byte[] data, out int temp)
        {
            temp = 0;
            for (int index = 0; index < data.Length; index++)
            {
                temp = temp ^ data[index];
            }
        }

        public string StrToHexToHexstr(string str)
        {
            string strResult = "";
            string strData1 = str;
            byte[] data = Encoding.ASCII.GetBytes(strData1);
            for (int i = 0; i < data.Length; i++)
            {
                strResult += data[i].ToString("X2");
            }
            return strResult;
         
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string time = Convert.ToInt64(ts.TotalSeconds).ToString();
            string tradeTime = DateTime.Now.ToString("[yyyy/MM/dd/HH:mm:ss]", DateTimeFormatInfo.InvariantInfo);
            return tradeTime;
        }
        /// <summary>
        /// 创建token数据包
        /// </summary>
        /// <param name="head"></param>
        /// <param name="codeid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool CreateDatePack(string token, string uuid, string sign,string mac, string sn,out string hexPack)
        {
            string pack;    //数据包
            string len;     //数据包长度
            string strxor;  //异或运算数据包
            string xor;     //异或运算值
            //string hexPack; //字节数据包

            //string data = uuid + Base64ToHex(token) + Base64ToHex(sign);
            string data1 = uuid + Base64ToHex(token) + Base64ToHex(sign);
            //计算len长度
            int llength = (uuid_token_code_id.Length + data1.Length) / 2 + 3;
            len = llength.ToString("X").PadLeft(4, '0');
            //异或运算 
            strxor = uuid_token_code_id + data1 + len; //异或数据组包(LEN|PAYLAOD)
            byte[] byteXor = StrToHexByte(strxor); //string转化字节类
            int temp;
            BCC(byteXor, out temp);
            xor = temp.ToString("X").PadLeft(2, '0');
            //数据组包
            pack = data1 + xor;
            //转化成十六进制数据包
            hexPack = head + len + uuid_token_code_id + pack;

            return true;
        }
        /// <summary>
        /// 创建mac_sn数据包
        /// </summary>
        /// <param name="head"></param>
        /// <param name="codeid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool CreateMacPack(string token, string uuid, string sign, string mac, string sn, out string MachexPack)
        {
            string pack;    //数据包
            string len;     //数据包长度
            string strxor;  //异或运算数据包
            string xor;     //异或运算值
            //string hexPack; //字节数据包
            string sn_hexstring = StrToHexToHexstr(sn);

            //MessageBox.Show(sn_hexstring);       //测试

            string data1 = sn_hexstring + mac;
            //计算len长度
            int llength = (Mac_sn_code_id.Length + data1.Length) / 2 + 3;
            len = llength.ToString("X").PadLeft(4, '0');
            //异或运算 
            strxor = Mac_sn_code_id + data1 + len; //异或数据组包(LEN|PAYLAOD)
            byte[] byteXor = StrToHexByte(strxor); //string转化字节类
            int temp;
            BCC(byteXor, out temp);
            xor = temp.ToString("X").PadLeft(2, '0');
            //数据组包
            pack = data1 + xor;
            //转化成十六进制数据包
            MachexPack = head + len + Mac_sn_code_id + pack;

            return true;
        }

        /// <summary>          
        /// 解析返回数据包
        /// </summary>
        /// <param name="str"> 接收到的数据包</param>
        /// <param name="msg">解析用来界面显示的数据</param>
        public bool ResolveData(string str, out string msg)
        {
            msg = "";
            string pack = str;
            string head;
            string len;
            string code_id;
            string data;
            string xor;
            string tradeTime;
            if (pack != null)
            {
                if (pack.Length > 12)
                {
                    head = pack.Substring(0, 4);                //提取head Substring(0, 4); 起始下标，4结束下标
                    len = pack.Substring(4, 4);                 //提取len
                    code_id = pack.Substring(8, 4);             //提取code_id
                    string tmp = pack.Substring(12);            //去掉前12个字节
                    data = tmp.Substring(0, tmp.Length - 2);    //去掉后两个字节 -- PAYLOAD
                    xor = pack.Substring(pack.Length - 2);      //提取校验值

                    //异或校验 
                    string strxor = code_id + data + len;       //异或数据组包(LEN|PAYLAOD)
                    byte[] byteXor = StrToHexByte(strxor);      //string转化字节类                               
                    int temp;

                    BCC(byteXor, out temp);
                    xor = temp.ToString("X").PadLeft(2, '0');
                    //测试
                    /*if (head == "55AA")
                    {
                        tradeTime = DateTime.Now.ToString("[yyyy-MM-dd-HH:mm:ss]", DateTimeFormatInfo.InvariantInfo);
                       //msg = tradeTime + "<--成功-->\r\n";
                        msg = tradeTime + "<--成功-->\r\n" + "head:" + head + "\r\n" + "len:" + len + "\r\n" + "code_id:" + code_id + "\r\n" + "data:" + data + "\r\n" + "xor:" + xor + "\r\n";
                    }*/
                    if (head == "55AA" && xor == temp.ToString("X").PadLeft(2, '0') && code_id == "010A" && data == "00")
                    {
                        tradeTime = DateTime.Now.ToString("[yyyy-MM-dd-HH:mm:ss]", DateTimeFormatInfo.InvariantInfo);
                        //msg = tradeTime + "<--成功-->\r\n";
                        msg = tradeTime + "<--成功-->\r\n" + "head:" + head + "\r\n" + "len:" + len + "\r\n" + "code_id:" + code_id + "\r\n" + "data:" + data + "\r\n" + "xor:" + xor + "\r\n";
                    }
                    else
                    {
                        tradeTime = DateTime.Now.ToString("[yyyy-MM-dd-HH:mm:ss]", DateTimeFormatInfo.InvariantInfo);
                        msg = tradeTime + "<--失败-->\r\n";
                        //msg = tradeTime + "<--失败-->\r\n" + "head:" + head + "\r\n" + "len:" + len + "\r\n" + "code_id:" + code_id + "\r\n" + "data:" + data + "\r\n" + "xor:" + xor + "\r\n";
                        return false;
                    }
                }
                else
                {
                    tradeTime = DateTime.Now.ToString("[yyyy-MM-dd-HH:mm:ss]", DateTimeFormatInfo.InvariantInfo);
                    msg = tradeTime + "<--失败-->\r\n";
                    //msg = tradeTime + "<--失败-->\r\n" + "head:" + head + "\r\n" + "len:" + len + "\r\n" + "code_id:" + code_id + "\r\n" + "data:" + data + "\r\n" + "xor:" + xor + "\r\n";
                    return false;
                }
            }
            else
            {
                tradeTime = DateTime.Now.ToString("[yyyy-MM-dd-HH:mm:ss]", DateTimeFormatInfo.InvariantInfo);
                //msg = tradeTime + "<--超时失败-->\r\n";
                msg = "<--超时失败-->\r\n";
                //msg = tradeTime + "<--失败-->\r\n" + "head:" + head + "\r\n" + "len:" + len + "\r\n" + "code_id:" + code_id + "\r\n" + "data:" + data + "\r\n" + "xor:" + xor + "\r\n";
                return false;
            }
            return true;
        }
    }
}
