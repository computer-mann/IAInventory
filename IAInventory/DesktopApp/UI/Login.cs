using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystemCsharp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp signup = new SignUp();
            signup.Show();
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            var ran = new Random().Next(10);
                      
                        if (ran > 5)
                        {
                            Home home = new Home();
                            this.Hide();
                            home.Show();
                        }
                        if (ran % 2 != 0)
                        {
                            Manager_home manager = new Manager_home();
                            this.Hide();
                            manager.Show();
                        }
                        if (ran %2 == 0)
                        {
                            Admin_home admin = new Admin_home();
                            this.Hide();
                            admin.Show();
                        }
                    
                
           
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
