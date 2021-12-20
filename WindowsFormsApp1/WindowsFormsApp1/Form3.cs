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
    public partial class Form3 : Form
    {
        
        CheckBox[] chBoxes = new CheckBox[20];
        int chBoxCount = 0;
        int chBoxCountCars = 0;

        TextBox[] txBoxes = new TextBox[20];

        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            string db = "vt";
            string host = "localhost";
            string user = "root";
            string pass = "";
            string myConnectionString = "Database=" + db + ";Data Source=" + host + ";User Id=" + user + ";Password=" + pass;

            MySqlConnection myConnection = new MySqlConnection(myConnectionString);
            myConnection.Open();


            string sql = "SHOW COLUMNS FROM `vt`.`cars`";
            MySqlCommand command = new MySqlCommand(sql, myConnection);
            MySqlDataReader reader = command.ExecuteReader();
            MessageBox.Show("Чтение прошло успешно!");

            

            while (reader.Read())
            {

                if (reader[0].ToString() != "id")
                {
                    chBoxes[chBoxCount] = new CheckBox();
                    chBoxes[chBoxCount].AutoSize = true;
                    chBoxes[chBoxCount].Text = reader[0].ToString();
                    chBoxes[chBoxCount].Location = new Point(50, 100 + chBoxCount * 30);
                    this.Controls.Add(chBoxes[chBoxCount]);
                    txBoxes[chBoxCount] = new TextBox();
                    txBoxes[chBoxCount].AutoSize = true;
                    txBoxes[chBoxCount].Location = new Point(150, 100 + chBoxCount * 30);
                    this.Controls.Add(txBoxes[chBoxCount]);
                    chBoxCount++;
                }
            }
            chBoxCountCars = chBoxCount;
            reader.Close();



            sql = "SHOW COLUMNS FROM `vt`.`owners`";

            command = new MySqlCommand(sql, myConnection);
            reader = command.ExecuteReader();


            while (reader.Read())
            {
                if (reader[0].ToString() != "id")
                {
                    chBoxes[chBoxCount] = new CheckBox();
                    chBoxes[chBoxCount].AutoSize = true;
                    chBoxes[chBoxCount].Text = reader[0].ToString();
                    chBoxes[chBoxCount].Location = new Point(50, 100 + chBoxCount * 30);
                    this.Controls.Add(chBoxes[chBoxCount]);
                    txBoxes[chBoxCount] = new TextBox();
                    txBoxes[chBoxCount].AutoSize = true;
                    txBoxes[chBoxCount].Location = new Point(150, 100 + chBoxCount * 30);
                    this.Controls.Add(txBoxes[chBoxCount]);
                    chBoxCount++;
                }
                
            }
            reader.Close();


            myConnection.Close();

            string strUrl = "http://localhost/ugavuga.jpg";
            

            WebRequest webreq = WebRequest.Create(strUrl);
            WebResponse webres = webreq.GetResponse();
            Stream stream = webres.GetResponseStream();

            Image image = Image.FromStream(stream);

            stream.Close();

            pictureBox1.Image = image;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string db = "vt";
            string host = "localhost";
            string user = "root";
            string pass = "";
            dataGridView1.Rows.Clear();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns.RemoveAt(0);
            try
            {
                dataGridView1.Visible = true;

                string myConnectionString = "Database=" + db + ";Data Source=" + host + ";User Id=" + user + ";Password=" + pass;
                
                MySqlConnection myConnection = new MySqlConnection(myConnectionString);
                myConnection.Open();



                string[] dataGridFild = new string[20];
                int k = 1;
                bool where = false;
                bool firsttable = false;
                
                string sql = "SELECT `cars`.`id`  ";
                for (int i = 0; i < chBoxCountCars; i++)
                    if (chBoxes[i].Checked == true)
                    {
                        firsttable = true;
                        if (k > 0) sql = sql + ",`cars`. `" + chBoxes[i].Text + "` ";
                        else sql = sql + " `cars`.`" + chBoxes[i].Text + "` ";
                        dataGridFild[k] = chBoxes[i].Text;
                        k++;
                    }
                bool secondtable = false;
                for (int i = chBoxCountCars; i < chBoxCount; i++)
                    if (chBoxes[i].Checked == true)
                    {
                        secondtable = true;
                        if (k > 0) sql = sql + ",`owners`. `" + chBoxes[i].Text + "` ";
                        else sql = sql + " `owners`.`" + chBoxes[i].Text + "` ";
                        dataGridFild[k] = chBoxes[i].Text;
                        k++;
                    }
                sql = sql + " FROM ";
                if(firsttable)
                {
                    sql = sql + "`vt`.`cars`";
                }

                if (firsttable & secondtable)
                {
                    sql = sql + ", ";
                }

                if (secondtable)
                {
                    sql = sql + "`vt`.`owners`";
                }

                if (firsttable & secondtable)
                {
                    sql = sql + " WHERE `cars`.`owner` = `owners`.`id`";
                }

                if (where)
                {
                    int k_where = 0;
                    sql = sql + "  WHERE ";
                    for (int i = 0; i < chBoxCount; i++)
                        if (chBoxes[i].Checked == true)
                        {
                            if (txBoxes[i].Text != "")
                                if (k_where > 0) sql = sql + " and `" + chBoxes[i].Text + "` = '" + txBoxes[i].Text + "'  ";
                                else { sql = sql + "`" + chBoxes[i].Text + "` = '" + txBoxes[i].Text + "'  "; k_where++; }
                            


                        }
                }

                MySqlCommand command = new MySqlCommand(sql, myConnection);
                MySqlDataReader reader = command.ExecuteReader();

                for (int i = 0; i < k; i++)
                    dataGridView1.Columns.Add(dataGridFild[i], dataGridFild[i]);

                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[k]);
                    for (int i = 0; i < k; i++)
                        data[data.Count - 1][i] = reader[i].ToString();
                }
                reader.Close();
                myConnection.Close();
                foreach (string[] s in data)
                    dataGridView1.Rows.Add(s);
            }
                catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex);
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form f2 = Application.OpenForms[1];
            f2.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)

        {   int indexCurrent = dataGridView1.CurrentCell.RowIndex;
            string s = dataGridView1[0, indexCurrent].Value.ToString();

            string sql = "SELECT `pic` FROM `vt`.`cars` WHERE `id`= '";
            sql = sql + s + "'";
            
            
            string db = "vt";
            string host = "localhost";
            string user = "root";
            string pass = "";
            string myConnectionString = "Database=" + db + ";Data Source=" + host + ";User Id=" + user + ";Password=" + pass;

            MySqlConnection myConnection = new MySqlConnection(myConnectionString);
            myConnection.Open();

            MySqlCommand command = new MySqlCommand(sql, myConnection);
            MySqlDataReader reader = command.ExecuteReader();

            string strUrl = "http://localhost/ugavuga.jpg";

            if (reader.Read())
            {
                strUrl = "http://localhost/" + reader[0].ToString();
            }
            WebRequest webreq = WebRequest.Create(strUrl);
            WebResponse webres = webreq.GetResponse();
            Stream stream = webres.GetResponseStream();
            Image image;

            image = Image.FromStream(stream);

            pictureBox1.Image = image;
            stream.Close();
        }

    }
}
