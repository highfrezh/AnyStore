using AnyStore.BLL;
using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
        public static string loggedIn;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblUserType_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Code to close window
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (txtUsername.Text == "high" || txtPassword.Text == "high") // This line of Code Is added by me for automatic login . 
            {
               
                frmAdminDashboard high = new frmAdminDashboard();
                high.Show();

                this.Hide();
            }                                                              //And it End here
           // else
           // {
                l.username = txtUsername.Text.Trim();
                l.password = txtPassword.Text.Trim();
                l.user_type = cmbUserType.Text.Trim();

                //Checking the user credential
                bool success = dal.loginCheck(l);
                if (success == true)
                {
                    //Login successfull
                    MessageBox.Show("Login successful.");
                    loggedIn = l.username;
                    //Need to open respective form base on user_type
                    switch (l.user_type)
                    {
                        case "Admin":
                            {
                                // Display Admin Dashboard
                                frmAdminDashboard admin = new frmAdminDashboard();
                                admin.Show();
                                this.Hide();

                            }
                            break;

                        case "User":
                            {
                                // Display User Dashboard
                                frmUserDashboard user = new frmUserDashboard();
                                user.Show();
                                this.Hide();

                            }
                            break;

                        default:
                            {
                                // Display an error message 
                                MessageBox.Show("Invalid User Type");
                            }
                            break;

                    }


                }
                else
                {
                    //Login failed
                    MessageBox.Show("Login Failed. Try Again");
                }
           // }
        }
    }
}
