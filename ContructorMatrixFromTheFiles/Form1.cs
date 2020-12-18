using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        int n = 0, m = 0;
        public Form1()
        {
            InitializeComponent();
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;

            dataGridView2.ColumnHeadersHeight = 4;
            dataGridView2.RowHeadersWidth = 4;
            dataGridView2.Font = new Font("Times New Roman", 12);
            dataGridView2.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            button4.Enabled = false;


            dataGridView2.MinimumSize = new Size(20, 20);
            dataGridView2.MaximumSize = new Size(400, 400);
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        public string[] FrDigit(TextBox tx)
        {
            return tx.Text
                  .Trim()
                  .Split()
                  .Select(t =>
                  {
                      var ch = t.ToCharArray();
                      foreach (var i in ch)
                          if (!char.IsDigit(i))
                              t = t.Replace(i, ' ');
                          else continue;
                      return t;
                  }).ToArray();
        }
        private void button3_Click(object sender, EventArgs e)
        {


            bool flagTrueForOne = false;
            bool flagTrueForTwo = false;

            if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrEmpty(textBox4.Text)) textBox4.BackColor = Color.Red;
            else
            {
                flagTrueForOne = true;
                textBox4.BackColor = Color.White;
            }
            if (string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrEmpty(textBox5.Text)) textBox5.BackColor = Color.Red;
            else
            {
                flagTrueForTwo = true;
                textBox5.BackColor = Color.White;
            }
            if (flagTrueForOne && flagTrueForTwo)
            {
                button4.Enabled = true;

                foreach (var i in FrDigit(textBox4))
                //  dataGridView2.RowCount = int.Parse(i);
                {

                    textBox6.Text += i.Trim();
                    n = dataGridView2.RowCount = int.Parse(i);
                }

                textBox6.Text += Environment.NewLine;
                foreach (var i in FrDigit(textBox5))
                //  dataGridView2.RowCount = int.Parse(i);
                {
                    textBox6.Text += i.Trim();
                    m = dataGridView2.ColumnCount = int.Parse(i);
                }

                int sizeWidth = 0, sizeHeight = 0;
                if (n != 1 && m != 1)
                {
                    for (int j = 0; j < n; j++)
                    {
                        sizeHeight = dataGridView2.Rows[j].Height = (int)((60 * n) / 2);
                    }

                    for (int j = 0; j < m; j++)
                    {
                        sizeWidth = dataGridView2.Columns[j].Width = (int)((60 * m) / 2);
                    }
                }
                else
                {
                    sizeHeight = 40;
                    sizeWidth = 30;
                }

                if (n != 1 && m != 1)
                    dataGridView2.Size = new Size((sizeWidth + 2) * n, (sizeHeight + 2) * m);
                else dataGridView2.Size = new Size(sizeWidth * 3, sizeHeight * m);


            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;

            OpenFileDialog fl = new OpenFileDialog();
            fl.Title = "Open";
            fl.Filter = "Information Text File (*.txt)|*.txt";
            fl.CheckFileExists = true;
            fl.CheckPathExists = true;
            //string[,] strList = new string[n, m];
            string[] needDetectedInformation = default(string[]);
            if (fl.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = File.OpenText(fl.FileName);

                string[] str = sr.ReadToEnd().Split(new char[] { '\n', ',', ' ', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                needDetectedInformation = (string[])str.Clone();
                for (int i = 0; i < str.Length; i++)
                {
                    textBox6.Text += str[i];
                }
                m = File.ReadLines(fl.FileName).Select(t =>
                {
                    return t.Split().SkipWhile(z => z == " ").Count();
                }).Max();

                n = File.ReadAllLines(fl.FileName)
                    .Where(t => t != " ").Count();

                //n++; m++;
                //forLine = str.Split(' ');
                //m = n = forLine.Length;
                sr.Close();



                dataGridView2.RowCount = n;
                dataGridView2.ColumnCount = m;

                int sizeWidth = 0, sizeHeight = 0;
                for (int j = 0; j < n; j++)
                {
                    sizeHeight = dataGridView2.Rows[j].Height = (int)((60 * n) / 3);
                }

                for (int j = 0; j < m; j++)
                {
                    sizeWidth = dataGridView2.Columns[j].Width = (int)((60 * m) / 3);
                }

                dataGridView2.Size = new Size(140, 140);
                dataGridView2.MinimumSize = new Size(20, 20);
                dataGridView2.MaximumSize = new Size(180, 180);
                int soCount = needDetectedInformation.Length;

                Array.Resize(ref needDetectedInformation, n * m);
                int[,] arrList = new int[n, m];
                // string[] strList = textBox6.Text.Split(new char[] { '\n', '\r', '\t' }, StringSplitOptions.None);

                //needDetectedInformation = new string (needDetectedInformation.Skip(needDetectedInformation.IndexOf(' ')).ToArray());
                for (int i = 0, k = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (needDetectedInformation[k] != " " && needDetectedInformation[k] != null)
                            arrList[i, j] = int.Parse(needDetectedInformation[k]);
                        else continue;
                        k++;
                    }
                }

                //n--;
                //m--;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        dataGridView2[j, i].Value = arrList[i, j];
                    }
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;

            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Save file (*.txt)|*.txt";
            sv.Title = "Save";
            sv.CreatePrompt = true;
            sv.OverwritePrompt = true;

            int[,] arr = new int[n, m];

            if (sv.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = File.CreateText(sv.FileName);
                //StreamWriter sw = new StreamWriter(sv.FileName);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (dataGridView2[j, i].Value != null)
                        {
                            arr[i, j] = int.Parse(dataGridView2[j, i].Value.ToString());
                            sw.Write(arr[i, j].ToString());
                        }
                    }
                    sw.WriteLine();
                }

                //sw.Write(dataGridView2[int.Parse(textBox4.Text), int.Parse(textBox5.Text)]);
                sw.Close();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int k = 0;
            bool flag = true;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (dataGridView2[j, i].Value == null)
                    {
                        dataGridView2[j, i].Style.BackColor = Color.Red;
                        flag = false;
                    }
                    else
                        dataGridView2[j, i].Style.BackColor = Color.White;
                }
                if (flag) k++;
                else k--;
            }

            textBox6.Text += k + Environment.NewLine;
            if (k == n || k == m)
            {
                textBox6.Text += Environment.NewLine;
                int[,] arr = new int[n, m];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        dataGridView2[j, i].Style.BackColor = Color.White;
                        arr[i, j] = int.Parse(dataGridView2.Rows[i].Cells[j].Value.ToString());
                        textBox6.Text += arr[i, j] + " ";
                    }
                    textBox6.Text += Environment.NewLine;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(Control i in this.Controls)
            {
                if (i is TextBox)
                    ((TextBox)i).Text = string.Empty;
                if (i is DataGridView)
                {
                    ((DataGridView)i).Columns.Clear();
                    ((DataGridView)i).Rows.Clear();
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
