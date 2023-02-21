using MathMetodLibrary;
using System.Security.Cryptography;

namespace Technolog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void StartProgramm_Click(object sender, EventArgs e)
        {
            double Dnar = Convert.ToDouble(boxDnar.Text);
            double Dosn = Convert.ToDouble(boxDosn.Text);
            double Dsk = Convert.ToDouble(boxDsk.Text);
            double Dskv = Convert.ToDouble(boxDskv.Text);
            double Lkr = Convert.ToDouble(boxLkr.Text);
            double Lsk = Convert.ToDouble(boxLsk.Text);
            double Rcc = Convert.ToDouble(boxRcc.Text);
            double Scc = Convert.ToDouble(boxScc.Text);
            double Lsm = Convert.ToDouble(boxLsm.Text);
            double dsm = Convert.ToDouble(boxdsm.Text);
            double R = Convert.ToDouble(boxR.Text);
            double dcc = Convert.ToDouble(boxdcc.Text);
            double Lcc = Convert.ToDouble(boxLcc.Text);
            double Sd = Convert.ToDouble(boxSd.Text);
            double Lg = Convert.ToDouble(boxLg.Text);
            double Kp = Convert.ToDouble(boxKp.Text);
            double Ek = Convert.ToDouble(boxEk.Text);
            double Eg = Convert.ToDouble(boxEg.Text);
            double Kv = Convert.ToDouble(boxkv.Text);
            double ftr = Convert.ToDouble(boxftr.Text);
            double a = Convert.ToDouble(boxa.Text);
            double Tm = Convert.ToDouble(boxTm.Text);
            double Tex = Convert.ToDouble(boxTex.Text);
            double al = Convert.ToDouble(boxal.Text);
            double Pmax = Convert.ToDouble(boxPmax.Text);
            double G0 = Convert.ToDouble(boxG0.Text);
            double G11 = Convert.ToDouble(boxG11.Text);
            double G22 = Convert.ToDouble(boxG22.Text);
            double delta0 = Convert.ToDouble(boxdelta0.Text);
            double ey = Convert.ToDouble(boxey.Text);

            MathMetodLibrary.Calculation.GilzaCalc(Dnar, Dosn, Dsk, Dskv, Lkr, Lsk, Rcc, Scc, Lsm, dsm, R, dcc, Lcc, Sd, Lg, Kp, Ek, Eg, Kv, ftr, a, Tm, Tex, al, Pmax, G0, G11, G22, delta0, ey);
            
            GraphForm graphform = new GraphForm();
            graphform.Show();
        }
    }
}