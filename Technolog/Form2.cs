using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathMetodLibrary;
using Syncfusion.Windows.Forms.Chart;

namespace Technolog
{
    public partial class GraphForm : Form
    {
        public GraphForm()
        {
            InitializeComponent();
            boxX.Text = Convert.ToString(MathMetodLibrary.Calculation.x);
            boxRcc.Text = Convert.ToString(MathMetodLibrary.Calculation.rcc);
            boxEk.Text = Convert.ToString(MathMetodLibrary.Calculation.ek);
            boxEt.Text = Convert.ToString(MathMetodLibrary.Calculation.et);
            boxDelta1Max.Text = Convert.ToString(MathMetodLibrary.Calculation.delta1max);
            boxDelta1Min.Text = Convert.ToString(MathMetodLibrary.Calculation.delta1min);
            boxRza.Text = Convert.ToString(MathMetodLibrary.Calculation.Rza);
            boxDeltaL.Text = Convert.ToString(MathMetodLibrary.Calculation.deltaL);
            // Создание графиков
            ChartSeries graphSi = new ChartSeries("Si");
            ChartSeries graphG02 = new ChartSeries("G02");
            ChartSeries graphGr = new ChartSeries("Gr");
            ChartSeries graphGoOB = new ChartSeries("GoOB");
            ChartSeries graphGzOB = new ChartSeries("GzOB");
            ChartSeries graphdeltal = new ChartSeries("DeltaL");
            ChartSeries graphpza = new ChartSeries("DeltaL");
            // Заполнение всех графиков
            for (int x = 0; x < 1100; x++)
            {
                //Толщина стенки
                double y = MathMetodLibrary.Calculation.Si[x];
                graphSi.Points.Add(x, y);
                graphSi.SortPoints = true;
                graphSi.Type = ChartSeriesType.Line;
                this.GraphSi.Series.Add(graphSi);
                //Механические свойства
                double y1 = MathMetodLibrary.Calculation.G02[x];
                graphG02.Points.Add(x, y1);
                graphG02.SortPoints = true;
                graphG02.Type = ChartSeriesType.Line;
                this.GraphG02.Series.Add(graphG02);                
                //Радиальные напряжения
                double y2 = MathMetodLibrary.Calculation.Gr[x];
                graphGr.Points.Add(x, y2);
                graphGr.SortPoints = true;
                graphGr.Type = ChartSeriesType.Line;
                this.GraphG.Series.Add(graphGr);
                //Осевые? напряжения
                double y3 = MathMetodLibrary.Calculation.GzOB[x];
                graphGzOB.Points.Add(x, y3);
                graphGzOB.SortPoints = true;
                graphGzOB.Type = ChartSeriesType.Line;
                this.GraphG.Series.Add(graphGzOB);
                //Тангенциальные? напряжения
                double y4 = MathMetodLibrary.Calculation.GoOB[x];
                graphGoOB.Points.Add(x, y4);
                graphGoOB.SortPoints = true;
                graphGoOB.Type = ChartSeriesType.Line;
                this.GraphG.Series.Add(graphGoOB);
                //Конечный зазор
                double y5 = MathMetodLibrary.Calculation.delta1[x];
                graphdeltal.Points.Add(x, y5);
                graphdeltal.SortPoints = true;
                graphdeltal.Type = ChartSeriesType.Line;
                this.GraphDeltaL.Series.Add(graphdeltal);
                //Сила защемления
                double y6 = MathMetodLibrary.Calculation.Pza[x];
                graphpza.Points.Add(x, y6);
                graphpza.SortPoints = true;
                graphpza.Type = ChartSeriesType.Line;
                this.GraphPza.Series.Add(graphpza);
            }
            ChartSeries graphezsum = new ChartSeries("Ezsum");
            ChartSeries graphezsk = new ChartSeries("Ezsk");
            //Поперечный разрыв
            for (int x = 0; x < 200; x++)
            {
                // Суммарная осевая деформация
                double y0 = MathMetodLibrary.Calculation.Ezsum[x];
                graphezsum.Points.Add(x, y0);
                graphezsum.SortPoints = true;
                graphezsum.Type = ChartSeriesType.Line;
                this.GraphEzsum.Series.Add(graphezsum);
                // Скорректированная деформация
                double y1 = MathMetodLibrary.Calculation.Ezsk[x];
                graphezsk.Points.Add(x, y1);
                graphezsk.SortPoints = true;
                graphezsk.Type = ChartSeriesType.Line;
                this.GraphEzsum.Series.Add(graphezsk);
            }

        }

        private void ButtonSi_Click(object sender, EventArgs e)
        {
            this.GraphG02.Hide();
            this.GraphG.Hide();
            this.GraphDeltaL.Hide();
            this.GraphPza.Hide();
            this.GraphEzsum.Hide();
            this.GraphSi.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.GraphSi.Hide();
            this.GraphG.Hide();
            this.GraphDeltaL.Hide();
            this.GraphPza.Hide();
            this.GraphEzsum.Hide();
            this.GraphG02.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.GraphSi.Hide();
            this.GraphG02.Hide();
            this.GraphDeltaL.Hide();
            this.GraphPza.Hide();
            this.GraphEzsum.Hide();
            this.GraphG.Show();            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.GraphSi.Hide();
            this.GraphG.Hide();
            this.GraphG02.Hide();
            this.GraphPza.Hide();
            this.GraphEzsum.Hide();
            this.GraphDeltaL.Show();            
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.GraphSi.Hide();
            this.GraphG.Hide();
            this.GraphG02.Hide();
            this.GraphDeltaL.Hide();
            this.GraphEzsum.Hide();
            this.GraphPza.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            this.GraphSi.Hide();
            this.GraphG.Hide();
            this.GraphG02.Hide();
            this.GraphDeltaL.Hide();
            this.GraphPza.Hide();
            this.GraphEzsum.Show();            
        }
    }
}
