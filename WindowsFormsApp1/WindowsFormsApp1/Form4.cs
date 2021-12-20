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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form f2 = Application.OpenForms[1];
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String s,s2,s3,s4,s5;
            string db = "vt";
            string host = "localhost";
            string user = "root";
            string pass = "";
            try
            {
                dataGridView1.Visible = true;

                string myConnectionString = "Database=" + db + ";Data Source=" + host + ";User Id=" + user + ";Password=" + pass;

                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();
                MySqlCommand command;
                DataGridViewRow row;
                MessageBox.Show(dataGridView1.Rows.Count.ToString());
                for (int i = 0; i < dataGridView1.Rows.Count - 1; ++i)
                {
                    row = dataGridView1.Rows[i];
                    s = row.Cells[0].Value.ToString();
                        s2 = row.Cells[1].Value.ToString();
                        s3 = row.Cells[2].Value.ToString();
                        s4 = row.Cells[3].Value.ToString();
                        s5 = row.Cells[4].Value.ToString();

                        string sql = "INSERT INTO `cars`(`id`, `number`, `owner`, `model`, `color`, `year`) VALUES (NULL,'"+s3+"','"+s5+"','"+s+"','"+s2+"','"+s3+"')";
                        MessageBox.Show(sql);
                        command = new MySqlCommand(sql, myConnection);
                        command.ExecuteNonQuery();
                }

                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex);
            }

            
        }

        
    }
}
