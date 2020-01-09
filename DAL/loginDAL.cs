using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class loginDAL
    {
        //Static string to connect to database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        public bool loginCheck(loginBLL l)
        {
            // create a boolen variable and set it value to false and return it value;
            bool isSuccess = false;

            //Connecting to Database 
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //SQL quary to check login
                string sql = "SELECT * FROM tbl_user WHERE username=@username AND password=@password AND user_type=@user_type";

                // create SQL command to pass value
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);
                cmd.Parameters.AddWithValue("@user_type", l.user_type);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                // checking the row in the Table 
                if (dt.Rows.Count>0)
                {
                    // Login successful 
                    isSuccess = true;
                }
                else
                {
                    // Login Fail
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
