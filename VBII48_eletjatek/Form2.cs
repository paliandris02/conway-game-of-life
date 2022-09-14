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
    public partial class Form2 : Form
    {
        int sor, oszlop, sejtekSzama, ms;
        Random rnd = new Random();
        Timer timer = new Timer();
        bool generaltE = false;
        bool[,] újPálya;
        bool[,] pálya;
        Brush ecsetÉlő = new SolidBrush(Color.Fuchsia);
        Brush ecsetHalott = new SolidBrush(Color.White);
        int sejtSzélesség = 6, sejtMagasság = 6;
        public Form2(int oszlop, int sor, int sejtekSzama,int ms)
        {
            InitializeComponent();
            this.sor = sor;
            this.oszlop = oszlop;
            this.sejtekSzama = sejtekSzama;
            this.ms = ms;
            Text = $"Életjáték X:{sor}, Y:{oszlop}, kezdő sejtek száma:{sejtekSzama}, változás közti idő: {ms} ms";
            label1.Text = $"Élő sejtek száma: {sejtekSzama}";
            timer.Interval = ms;
            timer.Tick += Timer_Tick;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            pálya = new bool[sor, oszlop];
            újPálya = new bool[sor, oszlop];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (generaltE) timer.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            Hide();
            Form1 elsőForm = new Form1();
            elsőForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            int[] kezdetiElok = new int[sejtekSzama];
            int osszSejtSzam = oszlop * sor;
            int i = 0;
            while (i < sejtekSzama)
            {
                int eloIndex = rnd.Next(0, osszSejtSzam + 1);
                bool voltMar = false;
                for (int j = 0; j < kezdetiElok.Length; j++)
                {
                    if (kezdetiElok[j] == eloIndex)
                    {
                        voltMar = true;
                    }
                }
                if (!voltMar)
                {
                    kezdetiElok[i] = eloIndex;
                    i++;
                }
            }

            Graphics G = this.CreateGraphics();
            for (i = 0; i < this.sor; i++)
            {
                for (int j = 0; j < this.oszlop; j++)
                {
                    bool el_e = false;
                    for (int o = 0; o < kezdetiElok.Length; o++)
                    {
                        if (i * oszlop + j == kezdetiElok[o]) el_e = true;
                    }
                    if (el_e)
                    {
                        G.FillRectangle(ecsetÉlő, 6 * i, 6 * (j+7), sejtSzélesség, sejtMagasság);
                    }
                    else
                    {
                        G.FillRectangle(ecsetHalott, 6 * i, 6 * (j+7), sejtSzélesség, sejtMagasság);
                    }
                    pálya[i, j] = el_e;
                }
            }
            generaltE = true;
            label1.Text = $"Élő sejtek száma: {sejtekSzama}";
        }


        private void Timer_Tick(object sender, EventArgs e)
        {   
            for (int i = 0; i < sor; i++)
            {
                for (int j = 0; j < oszlop; j++)
                {
                    újPálya[i, j] = következőÁllapot(pálya[i, j],pálya, i,j);
                }
            }

            for (int i = 0; i < sor; i++)
            {
                for (int j = 0; j < oszlop; j++)
                {
                    pálya[i, j] = újPálya[i, j];
                }
            }

            int adottÁllapotnyiÉlőDarab = 0;
            Graphics G = this.CreateGraphics();
            for (int i = 0; i < sor; i++)
            {
                for (int j = 0; j < oszlop; j++)
                {
                    if (pálya[i, j])
                    {
                        G.FillRectangle(ecsetÉlő, sejtSzélesség * i, sejtMagasság * (j+7), sejtSzélesség, sejtMagasság);
                        adottÁllapotnyiÉlőDarab++;

                    }
                    else
                    {
                        G.FillRectangle(ecsetHalott, sejtSzélesség * i, sejtMagasság * (j+7), sejtSzélesség, sejtMagasság);
                    }

                }
            }
            label1.Text = $"Élő sejtek száma: {adottÁllapotnyiÉlőDarab}";
        }
        bool következőÁllapot(bool állapot, bool[,] pálya,int sor,int oszlop)
        {
                int élőSzomszédok = 0;
                //bal felette lévő
                if (sor - 1 >= 0 && oszlop-1 >= 0)
                {
                    if (pálya[sor - 1, oszlop - 1]) élőSzomszédok++;
                }
                //közvetlen felette lévő
                if(sor - 1 >= 0)
                {
                    if (pálya[sor - 1, oszlop]) élőSzomszédok++;
                }
                //jobb felette lévő
                if(sor - 1 >= 0 && oszlop + 1 <= pálya.GetUpperBound(1))
                {
                    if (pálya[sor - 1, oszlop + 1]) élőSzomszédok++;
                }
                //közvetlen mellette balra lévő
                if (oszlop -1 >=0)
                {
                    if (pálya[sor, oszlop - 1]) élőSzomszédok++;
                }
                //közvetlen mellette jobbra lévő
                if (oszlop + 1 <= pálya.GetUpperBound(1))
                {
                    if (pálya[sor, oszlop + 1]) élőSzomszédok++;
                }
                //bal alatta lévő
                if(sor + 1 <= pálya.GetUpperBound(0) && oszlop - 1 >= 0)
                {
                    if (pálya[sor + 1, oszlop - 1]) élőSzomszédok++;
                }
                //közvetlen alatta lévő
                if (sor + 1 <= pálya.GetUpperBound(0))
                {
                    if (pálya[sor + 1, oszlop]) élőSzomszédok++;
                }
                //jobb alatta lévő
                if (sor + 1 <= pálya.GetUpperBound(0) && oszlop + 1 <= pálya.GetUpperBound(1)) 
                {
                    if (pálya[sor + 1, oszlop + 1]) élőSzomszédok++;
                }

                if (!állapot && élőSzomszédok == 3) return true;
                if (állapot && élőSzomszédok > 1 && élőSzomszédok < 4) return true;
                return false;
        }
    }
}
