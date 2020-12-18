using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.KeyUp += Button1_Click;
            button2.Click += Button2_Click;
            toolTip1.SetToolTip(button2, "L - для чистки");
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            var @checked = new[] { Controls }
            .SelectMany(t => t.OfType<RadioButton>())
            .Where(z => z.Checked).ToArray();
            KeyEventArgs keyEvent = new KeyEventArgs(Keys.None);
            if (keyEvent.KeyCode == Keys.Enter || true || keyEvent.KeyCode == Keys.Delete)
            {
                foreach (var i in @checked)
                {
                    switch (i.Name)
                    {
                        case "radioButton1":
                            string a = textBox1.Text;
                            var arr = a
                                .ToCharArray().Where(t => Char.IsLetterOrDigit(t) || Char.IsWhiteSpace(t) ||
                                Char.IsPunctuation(t) || Char.IsNumber(t) || Char.IsSeparator(t) || Char.IsControl(t))
                                .GroupBy(t => t)
                                .ToDictionary(t => t.Key, k => k.Count());
                            int zSum = arr.Sum(t => t.Value);
                            arr.ToList().ForEach(t =>
                            {
                                string s = string.Format($"\"{ t.Key}\":{ t.Value}");
                                if (checkBox2.Checked) s += $" - { ((double)t.Value / (double)zSum):p8}{ Environment.NewLine}";
                                textBox2.Text += s;
                                var our = textBox2.Text
                                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                if (checkBox1.Checked)
                                    our.Sort();
                                textBox2.Clear();
                                our.ForEach(c => textBox2.Text += c + Environment.NewLine);
                            });
                            break;
                        case "radioButton2":
                            a = textBox1.Text;
                            arr = a
                                .ToLower()
                                .ToCharArray().Where(t => Char.IsLetterOrDigit(t) || Char.IsWhiteSpace(t) ||
                                Char.IsPunctuation(t) || Char.IsNumber(t) || Char.IsSeparator(t) || Char.IsControl(t))
                                .GroupBy(t => t)
                                .ToDictionary(t => t.Key, k => k.Count());
                            zSum = arr.Sum(t => t.Value);
                            arr.ToList().ForEach(t =>
                            {
                                string s = string.Format($"\"{ t.Key}\":{ t.Value}");
                                if (checkBox2.Checked) s += $" - { ((double)t.Value / (double)zSum):p8}{ Environment.NewLine}";
                                textBox2.Text += s;
                                var our = textBox2.Text
                                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                if (checkBox1.Checked)
                                    our.Sort();
                                textBox2.Clear();
                                our.ForEach(c => textBox2.Text += c + Environment.NewLine);
                            });
                            break;
                        case "radioButton3":
                            a = textBox1.Text;
                            arr = a
                                .ToUpper()
                                .ToCharArray().Where(t => Char.IsLetterOrDigit(t) || Char.IsWhiteSpace(t) ||
                                Char.IsPunctuation(t) || Char.IsNumber(t) || Char.IsSeparator(t) || Char.IsControl(t))
                                .GroupBy(t => t)
                                .ToDictionary(t => t.Key, k => k.Count());
                            zSum = arr.Sum(t => t.Value);
                            arr.ToList().ForEach(t =>
                            {
                                string s = string.Format($"\"{ t.Key}\":{ t.Value}");
                                if (checkBox2.Checked) s += $" - { ((double)t.Value / (double)zSum):p8}{ Environment.NewLine}";
                                textBox2.Text += s;
                                var our = textBox2.Text
                                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                if (checkBox1.Checked)
                                    our.Sort();
                                textBox2.Clear();
                                our.ForEach(c => textBox2.Text += c + Environment.NewLine);
                            });
                            break;
                    }
                }

            }
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (button2.Enabled)
            {
                textBox1.Clear();
                textBox2.Clear();
            }
            KeyEventArgs keyEvent = new KeyEventArgs(Keys.None);
            if (keyEvent.KeyCode == Keys.L)
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
        }
    }
}
