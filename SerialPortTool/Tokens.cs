using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortTool
{
    class Tokens
    {
        private string MyUuid;
        private string MyTokenValue;
        private string MySign;

        public void SetUuid(string uuid)
        {
            MyUuid = uuid; //uuid
        }

        public void SetTokenValue(string tokenValue)
        {
            MyTokenValue = tokenValue; //tokenvalue
        }

        public void SetSign(string sign)
        {
            MySign = sign; //sign
        }

        public string GetUuid()
        {
            return MyUuid;
        }

        public string GetTokenValue()
        {
            return MyTokenValue;
        }

        public string GetSign()
        {
            return MySign;
        }
    }
}
