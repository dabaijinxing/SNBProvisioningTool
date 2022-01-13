using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Antilost_SQL
{
    public class SQLDeal
    {
        public DataTable GetBurnInfo_ByMAC(string strOrder, string strMAC)
        {
            string strRemain = "P7提示--" + "GetBurnInfo_ByMAC";


            SQLHelper.SQLData newSql = new SQLHelper.SQLData(); //实例SQLHelper.SQLData
            newSql.SetConnSQL();

            string strSql = "select * from [Burn_Info] where OrderNo='" + strOrder + "' and MAC='" + strMAC + "'";

            DataTable TabDIPTest = newSql.GetTable(newSql.strPower7SEG_Conn, strSql);

            if (TabDIPTest == null)
            {
                MessageBox.Show("获取MAC记录失败，请检查网络是否正常！", strRemain, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
            }

            if (TabDIPTest.Rows.Count == 0)
            {
                MessageBox.Show("数据库中不存在MAC的记录，请检查工单号或MAC是否输入正确！", strRemain, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
            }
            else
            {
                //MessageBox.Show("获取成功");
                return GetTable_SN_UID_KEY(strOrder, strMAC);

            }
        }

        private DataTable GetTable_SN_UID_KEY(string strOrderNo, string strMAC)
        {
            string strRemain = "P7提示--" + "GetBurnInfo_ByMAC";

            SQLHelper.SQLData newSql = new SQLHelper.SQLData();
            newSql.SetConnSQL();

            SqlConnection con = new SqlConnection(newSql.strPower7SEG_Conn);
            SqlCommand com = new SqlCommand();
            com.CommandTimeout = 100000;

            com.CommandText = "GetBurnInfo_ByMAC";
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = con;

            SqlParameter par = new SqlParameter("@strOrder", SqlDbType.VarChar);
            par.Value = strOrderNo;
            com.Parameters.Add(par);

            SqlParameter par1 = new SqlParameter("@strMAC", SqlDbType.VarChar);
            par1.Value = strMAC;
            com.Parameters.Add(par1);

            try
            {
                DataSet ds = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter(com);

                adapter.Fill(ds);

                if (ds.Tables.Count == 0)
                {
                    MessageBox.Show("获取MAC记录失败，请检查网络是否正常！", strRemain, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                else
                {
                    return ds.Tables[0];
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
