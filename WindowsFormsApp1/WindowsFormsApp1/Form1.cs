using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    /*class bd
    {
        public string db;
        public string host;
        public string user;
        public string pass;
        public MySqlConnection myConnection;

        public int ConnectionAndOpen()
        {
            string myConnectionString = "Database=" + db + ";Data Source=" + host + ";User Id=" + user + ";Password=" + pass;
            myConnection = new MySqlConnection(myConnectionString);
            myConnection.Open();
            return 1;
        }

        public bd()
        {
            db = "vt"; host = "localhost";
            user = "root"; pass = "";
        }

        public bd(string b)
        {
            db = b; host = "localhost";
            user = "root"; pass = "";
        }
    }*/


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                    string db = "vt";
                    string host = "localhost";
                    string user = "root";
                    string pass = "";
                    int userroot = 0;

            try
            {
                string myConnectionString = "Database=" + db + ";Data Source=" + host + ";User Id=" + user + ";Password=" + pass;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();

                string sql = "SELECT `users`.`root` FROM `vt`.`users` WHERE `username`= '"+textBox1.Text+"' AND `password`= '"+textBox2.Text+"'";
                
                MySqlCommand command = new MySqlCommand(sql, myConnection);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                    userroot = int.Parse(reader[0].ToString());


                MessageBox.Show("Подключение прошло успешно!");
                if (userroot > 0)
                {
                    Form form2 = new Form2(userroot);
                    form2.Show();
                    this.Hide();
                    myConnection.Close();

                }
                else
                {
                    MessageBox.Show("Неправильный пользователь!");
                    this.Close();
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
