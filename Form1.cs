using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.CodeDom.Compiler;
using MathNet.Symbolics;
using Express = MathNet.Symbolics.SymbolicExpression;

namespace gold_cross
{
    public partial class Form1 : Form
    {
        int k1 = 0;
        int k2 = 0;
        string result;
        string result2;
        
        public Form1()
        {
            InitializeComponent();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "График";
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        public void Raschet()
        {
            try
            {
                GraphPane pane = zedGraphControl1.GraphPane;
                pane.CurveList.Clear();
                PointPairList list = new PointPairList();
                PointPairList list2 = new PointPairList();
                double min = Convert.ToDouble(textBox2.Text);
                double max = Convert.ToDouble(textBox3.Text);
                double a = min;
                double b = max;
                double h = (Math.Abs(b - a)) / 100;

                var expr = Express.Parse(textBox4.Text);
                Func<double, double> f = expr.Compile("x");


                LineItem myCurve2 = pane.AddCurve("Sinc", list2, Color.Pink, SymbolType.None);


                for (double x = min; x <= max; x += h)
                {
                    list.Add(x, f(x));

                }


                LineItem myCurve = pane.AddCurve("Sinc", list, Color.Pink, SymbolType.None);
                zedGraphControl1.AxisChange();
                // Обновляем график
                zedGraphControl1.Invalidate();

            }
            catch (Exception err)
            {
                MessageBox.Show($"Ошибочка вышла: \n {err.Message}");
            }
        }

        public string ForLable()
        {
            try
            {
                double min = Convert.ToDouble(textBox2.Text);
                double max = Convert.ToDouble(textBox3.Text);
                double ep = Convert.ToDouble(textBox1.Text);
                double a = min;
                double b = max;
                double h = (Math.Abs(b - a)) / 100;
                double y = 0.618 * a + 0.382 * b;
                double z = 0.618 * b + 0.382 * a;


                var expr = Express.Parse(textBox4.Text);
                Func<double, double> f = expr.Compile("x");

                while (Math.Abs(b - a) >= ep)
                {

                    double x = (a + b) / 2;
                    double x1 = b - (b - a) / 1.618;
                    double x2 = a + (b - a) / 1.618;

                    double f1 = f(x1);
                    double f2 = f(x2);

                    
                   if (f1 < f2)
                    {
                        b = x2;
                    }
                    else
                    {
                        a = x1;
                    }
                }


                double xmin = (a + b) / 2;
                double fmin = f(xmin);

                return xmin.ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Ошибочка вышла: \n {err.Message}");
                return "";
            }
        }
        public string ForLable2()
        {
            try
            {
                double min = Convert.ToDouble(textBox2.Text);
                double max = Convert.ToDouble(textBox3.Text);
                double ep = Convert.ToDouble(textBox1.Text);
                double a = min;
                double b = max;
                double h = (Math.Abs(b - a)) / 100;
                double y = 0.618 * a + 0.382 * b;
                double z = 0.618 * b + 0.382 * a;


                var expr = Express.Parse(textBox4.Text);
                Func<double, double> f = expr.Compile("x");




                while (Math.Abs(b - a) >= ep)
                {

                    double x = (a + b) / 2;
                    double x1 = b - (b - a) / 1.618;
                    double x2 = a + (b - a) / 1.618;

                    double f1 = f(x1);
                    double f2 = f(x2);

                    
                    if (f1 < f2)
                    {
                        b = x2;
                    }
                    else
                    {
                        a = x1;
                    }
                }

                double xmin = (a + b) / 2;
                double fmin = f(xmin);

                return fmin.ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Ошибочка вышла: \n {err.Message}");
                return "";
            }
        }

        public Task RunSch()
        {
            Task task1 = Task.Run(() =>
            {
               var task = new Task(Raschet);
               task.Start();
            });
            return task1;
        }

        public void Back()
        {
            
            
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            PointPairList list = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();
            PointPairList list4 = new PointPairList();
            double min = Convert.ToDouble(textBox2.Text);
            double max = Convert.ToDouble(textBox3.Text);
            double a = min;
            double b = max;
            double h = (Math.Abs(b - a)) / 100;

            var expr = Express.Parse(textBox4.Text);
            Func<double, double> f = expr.Compile("x");
            double x;
            for (x = min; x <= max; x += h)
            {
                list.Add(x, f(x));
                
            }
            double x2;
            for (x2 = max; x2 >= min; x2 -= h)
            {
                list4.Add(x2, f(x2));

            }
            

            if (k1 == 1)
            {
                list2.Add(b, f(x));
                list2.Add(a, f(x2));
                list2.Add(x / 2, f(x) / 2);
                list2.Add(x2 / 2, f(x2) / 2);

            }
            else
            {
                list2.Add(b, f(x));
                list2.Add(a, f(x2));
            }
            LineItem invis = pane.AddCurve("Sinc", list3, Color.Red, SymbolType.None);
            invis.Line.IsVisible = false;
            LineItem dotmin = pane.AddCurve("Sinc", list3, Color.Red, SymbolType.Star);
            dotmin.Line.IsVisible = false;
            LineItem dot = pane.AddCurve("Sinc", list2, Color.Pink, SymbolType.Star);
            dot.Line.IsVisible = false;
            LineItem myCurve = pane.AddCurve("Sinc", list, Color.Pink, SymbolType.None);
            zedGraphControl1.AxisChange();
            // Обновляем график
            zedGraphControl1.Invalidate();
        }

        public Task RunBack()
        {
            Task task1 = Task.Run(() =>
            {
                var task = new Task(Back);
                task.Start();
            });
            return task1;
        }

        public void Forward()
        {


            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            PointPairList list = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();
            PointPairList list4 = new PointPairList();
            double min = Convert.ToDouble(textBox2.Text);
            double max = Convert.ToDouble(textBox3.Text);
            double a = min;
            double b = max;
            double h = (Math.Abs(b - a)) / 100;

            var expr = Express.Parse(textBox4.Text);
            Func<double, double> f = expr.Compile("x");
            double x;
            for (x = min; x <= max; x += h)
            {
                list.Add(x, f(x));

            }
            double x2;
            for (x2 = max; x2 >= min; x2 -= h)
            {
                list4.Add(x2, f(x2));

            }
            string xmin = result;
            string fmin = result2;

            if (k2 == 1)
            {
                list2.Add(b, f(x));
                list2.Add(a, f(x2));
            }
            else if (k2 == 2)
            {
                list2.Add(b, f(x));
                list2.Add(a, f(x2));
                list2.Add(x / 2, f(x) / 2);
                list2.Add(x2 / 2, f(x2) / 2);
            }
            else
            {
                list2.Add(b, f(x));
                list2.Add(a, f(x2));
                list2.Add(x / 2, f(x) / 2);
                list2.Add(x2 / 2, f(x2) / 2);
                list3.Add(Convert.ToDouble(xmin), Convert.ToDouble(fmin));
            }
            LineItem invis = pane.AddCurve("Sinc", list3, Color.Red, SymbolType.None);
            invis.Line.IsVisible = false;
            LineItem dotmin = pane.AddCurve("Sinc", list3, Color.Red, SymbolType.Star);
            dotmin.Line.IsVisible = false;
            LineItem dot = pane.AddCurve("Sinc", list2, Color.Pink, SymbolType.Star);
            dot.Line.IsVisible = false;
            LineItem myCurve = pane.AddCurve("Sinc", list, Color.Pink, SymbolType.None);
            zedGraphControl1.AxisChange();
            // Обновляем график
            zedGraphControl1.Invalidate();
        }

        public Task RunForward()
        {
            Task task1 = Task.Run(() =>
            {
                var task = new Task(Forward);
                task.Start();
            });
            return task1;
        }
        private async void рассчитатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var task = new Task(Mathem);
            // task.Start();
            result = await Task.Run(ForLable);
            this.label5.Text = result;
            result2 = await Task.Run(ForLable2);
            this.label6.Text = result2;
            RunSch();
            k1 = 0;
            k2 = 0;


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e) { }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load_1(object sender, EventArgs e)
        {

        }



        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void рассчитатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void рассчитатьToolStripMenuItem1_Click_1(object sender, EventArgs e) //назад
        {
            if (k2 > 0)
            {
                --k2;
                ++k1;
                RunBack();
            }
            else
            {
                MessageBox.Show("сначала нажмите вперед");
            }
        }

        private void впередToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (k2 > 0)
            {
                k1 = 0;                
            }
            ++k2;
            RunForward();
        }
    }
}

