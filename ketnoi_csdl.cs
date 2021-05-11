using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.SqlClient;
namespace app_xin
{
    class ketnoi_csdl
    {
        SqlConnection ketnoi = new SqlConnection(@"Data Source=DESKTOP-NITTPQ3\SQLEXPRESS;Initial Catalog=HTNC4;Integrated Security=True");
        public void hienthi(DataGridView _dt)
        {
            try
            {
                ketnoi.Open();
                string selectSQL = "select *from HTNC4";
                SqlDataAdapter data1 = new SqlDataAdapter(selectSQL, ketnoi);
                DataTable dt1 = new DataTable();
                data1.Fill(dt1);
                _dt.DataSource = dt1;
                ketnoi.Close();
            }
            catch (Exception ex)
            { }
        }
        public void themdulieu(int id, string thoigian, string mucnuoc)
        {
            try
            {
                ketnoi.Open();
                string insertSQL = "INSERT INTO HTNC4 values (@ID,@TG,@MNC)";
                SqlCommand com = new SqlCommand(insertSQL, ketnoi);
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("ID", SqlDbType.Int).Value = id;
                com.Parameters.AddWithValue("TG", SqlDbType.NVarChar).Value = thoigian;
                com.Parameters.AddWithValue("MNC", SqlDbType.NChar).Value = mucnuoc;
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
        }
    }
}
