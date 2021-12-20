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
            try
            {
                string myConnectionString = "Database=" + db + ";Data Source=" + host + ";User Id=" + user + ";Password=" + pass;
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();
                MessageBox.Show("Подключение прошло успешно!");
                Form form2 = new Form2();
                form2.Show();
                this.Hide();
                myConnection.Close();
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
