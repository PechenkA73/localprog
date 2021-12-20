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
using System.Net;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form f2 = Application.OpenForms[1];
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string db = "vt";
            string host = "localhost";
            string user = "root";
            string pass = "";
            string myConnectionString = "Database=" + db + ";Data Source=" + host + ";User Id=" + user + ";Password=" + pass;

            MySqlConnection myConnection = new MySqlConnection(myConnectionString);
            myConnection.Open();

            string sql = "SELECT `cars`.`model` FROM `vt`.`cars` "; 
            MySqlCommand command = new MySqlCommand(sql, myConnection);
            MySqlDataReader reader = command.ExecuteReader();

            MessageBox.Show(sql);
            if (textBox1.Text == "`cars`.`model`")
               
        }
    }
}
