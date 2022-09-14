using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VBII48_eletjatek
{
    public partial class Form1 : Form
    {
        Form3 harmadikForm = new Form3();
        public Form1()
        {
            InitializeComponent();
            Text = "Életjáték";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            int a;
            foreach (Control control in Controls)
            {
                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    if (!int.TryParse(control.Text, out a) || int.Parse(control.Text) == 0)
                    {
                        MessageBox.Show($"Hibás adat: {control.Text} ");
                        return;
                    }
                }
            }
            if ((int.Parse(textBox1.Text) * int.Parse(textBox2.Text)) < int.Parse(textBox3.Text))
            { 
                MessageBox.Show("A sejtek száma nem lehet több mint sor * oszlop.");
                return;
            }
            
            Hide();
            Form2 másodikForm = new Form2(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text),int.Parse(textBox4.Text));
            másodikForm.Show();
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            harmadikForm.Show();
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            harmadikForm.Hide();   
        }
    }
}
