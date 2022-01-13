using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPortTool
{
    public partial class Form1 : Form
    {
        public static Form1 form1;
        static MySerialPort SerialPort1 = new MySerialPort();               //实例化一个串口对象
        static Module Module1  = new Module();                              //实例化一个组件对象
        static Log MyLog = new Log();                                       //实例化一个日志对象
        public static string uuid_token_code_id = "0101";                   //初始化code_id
        public static string Mac_sn_code_id = "0102";
        string logPath = Directory.GetCurrentDirectory() + "\\log.txt";     //应用程序的当前工作目录下生成log日志文件
        string hexPack = "";                                                //初始化发送数据包
        string MachexPack = "";

        int estimate_flag = 0; //判断是token包还是sn包
        int uart_rev_flag = 0;
        int rev_timeout_flag = 0;
        int uart_sended_flag = 0;
        int uart_mac_sn_sended_flag = 0;
        int uart_token_uuid_sended_flag = 0;
        int uart_mac_sn_rec_flag = 0;

        float X = 0; //定义窗体的宽
        float Y = 0; //定义窗体的高

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            form1 = this;
        }
        /// <summary>
        /// 初始化窗口显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //窗口初始化代码
            this.MaximizeBox = false;       //窗口最大化按钮失效
            this.MinimumSize = this.Size;   //窗口最小尺寸为默认尺寸
            this.Resize += new EventHandler(modular_calEchoPhaseFromSignal1_Resize); //窗体调整大小时引发事件
            X = this.Width;                 //获取窗体的宽度
            Y = this.Height;                //获取窗体的高度
            setTag(this);                   //调用方法

            //初始化波特率 - 组合框
            BudComboBox.Items.Add("1200");   
            BudComboBox.Items.Add("2400");
            BudComboBox.Items.Add("4800");
            BudComboBox.Items.Add("9600");
            BudComboBox.Items.Add("19200");
            BudComboBox.Items.Add("38400");
            BudComboBox.Items.Add("115200");
            BudComboBox.SelectedIndex = 6;    //波特率选择的索引为6，即默认为115200波特率

            CloseButton.Enabled = false;
            DownloadButton.Enabled = false;
            MyLog.LogCreate(logPath);         //创建日志文件            
        }
        /// <summary>
        /// 控制窗体大小
        /// </summary>
        private void setTag(Control cons)
        {
            //遍历窗体中的控件
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size + ":" + con.Name;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        /// <summary>
        /// 遍历窗体中的控件，重新设置控件的值
        /// </summary>
        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Visible = false;
            }
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组
                float a = Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的值，宽度
                con.Width = (int)a;//宽度
                a = Convert.ToSingle(mytag[1]) * newy;//高度
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;//左边距离
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;//上边缘距离
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * newx;//字体大小
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
            foreach (Control con in cons.Controls)
            {
                con.Visible = true;
            }
        }
        /// <summary>
        /// 窗体缩放事件，随窗体改变控件大小随之改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void modular_calEchoPhaseFromSignal1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;  //窗体宽度缩放比例
            float newy = (this.Height) / Y; //窗体高度缩放比例
            setControls(newx, newy, this);  //随窗体改变控件大小
            // this.Text = this.Width.ToString() + " " + this.Height.ToString();//窗体标题栏文本
        }
        /// <summary>
        /// 获取串口名，并添加到组合框条目中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PortComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            PortComboBox.Items.Clear(); //清除当前串口号中的所有串口名称
            string[] port = SerialPort1.GetSerialPortName();   //获取串口名
            for (int i = 0; i < port.Length; i++)
            {
                PortComboBox.Items.Add(port[i]);               //添加到组合框条目中
            }
        }
        /// <summary>
        /// 设置串口参数打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PortComboBox.Items.Count == 0)
                {
                    MessageBox.Show("请选择一个串口", "提示");
                }
                else
                {
                    string com = PortComboBox.SelectedItem.ToString();                      //获取当前条目的值
                    int baudrate = Convert.ToInt32(BudComboBox.SelectedItem.ToString());
                    if (SerialPort1.SetParam(com, baudrate, DataReceived))
                    {
                        //MessageBox.Show("串口打开成功！");
                        PortComboBox.Enabled = false;
                        BudComboBox.Enabled = false;
                        OpenButton.Enabled = false;
                        DownloadButton.Enabled = true;
                        CloseButton.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("串口无效或已被占用", "错误提示");
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// 设置串口参数关闭串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            SerialPort1.Close();
            PortComboBox.Enabled = true;
            BudComboBox.Enabled  = true;
            OpenButton.Enabled   = true;
            CloseButton.Enabled = false;
            DownloadButton.Enabled = false;
        }
        /// <summary>
        ///接收数据函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            System.Threading.Thread.Sleep(100);    //延时100ms等待接收完数据
            string recv;
                    
            //this.Invoke就是跨线程访问ui的方法，也是文本的范例
            this.Invoke((EventHandler)delegate
            {
                if (uart_sended_flag == 1)
                {
                    uart_sended_flag = 0;
                    uart_rev_flag = 1;
                    //estimate_flag = 1; //发送sn，收到的返回值

                    Byte[] ReceivedData = SerialPort1.HexRecv();    //接收串口数据
                    String RecvDataText = null;                     //定义字符串
                    for (int i = 0; i < ReceivedData.Length; i++)
                    {
                        RecvDataText += (ReceivedData[i].ToString("X2")); //串口接收字符数组，字符依次转换为字符串
                    }
                    bool flag = Module1.ResolveData(RecvDataText, out recv); //解析返回数据包
                    if (flag && uart_token_uuid_sended_flag == 1 && uart_mac_sn_sended_flag == 0)
                    {
                        SerialPort1.HexSend(MachexPack); //串口十六进制发送组包数据
                        uart_sended_flag = 1;
                        uart_mac_sn_sended_flag = 1;
                        uart_token_uuid_sended_flag = 0;
                        flag = false;
                    }
                    else if (flag && uart_token_uuid_sended_flag == 0 && uart_mac_sn_sended_flag == 1)
                    {
                        uart_mac_sn_sended_flag = 0;
                        //将组包的数据写入log
                        MyLog.FileWrite(logPath, Module1.GetTimeStamp() + "烧录成功：" + hexPack + MachexPack);        //加入时间戳
                        DecideTextBox.Text = "成功";
                        DecideTextBox.ForeColor = Color.Green;
                        flag = false;
                    }
                    else {
                        //将组包的数据写入log
                        MyLog.FileWrite(logPath, Module1.GetTimeStamp() + "烧录失败：" + hexPack + MachexPack);        //加入时间戳
                        DecideTextBox.Text = "失败";
                        DecideTextBox.ForeColor = Color.Red;
                    }
                    LogRichTextBox.Text += recv;     //转换后的字符串显示到tbxRecvData上面
                }
            });
        }
        /// <summary>
        ///开始启动数据组包，串口发送数据，烧入设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            string strMAC = tbxReadMac.Text.Trim();
            string strOrder = txt_Order.Text.Trim(); //移除文本框前后的空白字符
            string token, uuid, sign, mac, sn;
            try
            {                
                string MAC = tbxReadMac.Text;
                Antilost_SQL.SQLDeal sQLDeal = new Antilost_SQL.SQLDeal();
                Antilost_SQL.SQLDeal newSql = sQLDeal;

                DataTable tabSql = newSql.GetBurnInfo_ByMAC(strOrder, strMAC);      //从数据库中获取order工单号和MAC地址
                //通过order和MAC地址，获取设备的SN码、sign、token、uuid
                mac = tabSql.Rows[0][2].ToString();     
                sn = tabSql.Rows[0][9].ToString();      
                sign = tabSql.Rows[0][10].ToString();   
                token = tabSql.Rows[0][12].ToString();  
                uuid = tabSql.Rows[0][13].ToString();

                licensedatatbx.Text += "uuid:" + uuid + "\r\n";
                licensedatatbx.Text += "token:" + token + "\r\n";
                licensedatatbx.Text += "sign:" + sign + "\r\n";
                //创建uuid_token_sign数据包
                bool ret = Module1.CreateDatePack(token, uuid, sign, mac, sn, out hexPack);
                //licensedatatbx.Text += "hexPack:" + hexPack + "\r\n";   //将组包数据打印到测试文本框中

                licensedatatbx.Text += "mac:" + mac + "\r\n";
                licensedatatbx.Text += "sn:" + sn + "\r\n";
                bool Macret = Module1.CreateMacPack(token, uuid, sign, mac, sn, out MachexPack);
                //licensedatatbx.Text += "MachexPack:" + MachexPack + "\r\n";   //将组包数据打印到测试文本框中

                if (ret == true && Macret == true)
                {
                    SerialPort1.HexSend(hexPack); //串口十六进制发送组包数据
                    uart_token_uuid_sended_flag = 1;
                    uart_sended_flag = 1;
                }
      
                //MessageBox.Show(hexPack);       //测试
                return;                       
            }
            catch { }     
        }

        private void LogRichTextBox_TextChanged(object sender, EventArgs e)
        {
            this.LogRichTextBox.SelectionStart = this.LogRichTextBox.Text.Length;
            this.LogRichTextBox.ScrollToCaret();    //滚动条自动处于最下部
        }

        private void licensedatatbx_TextChanged(object sender, EventArgs e)
        {
            this.licensedatatbx.SelectionStart = this.licensedatatbx.Text.Length;
            this.licensedatatbx.ScrollToCaret();    //滚动条自动处于最下部
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            LogRichTextBox.Clear();  //清除log内容
            licensedatatbx.Clear();  //清除数据内容
        }

        private void btnClearMac_Click(object sender, EventArgs e)
        {
            tbxReadMac.Clear();      //清除MAC地址
        }

        private void PortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

    }
}
