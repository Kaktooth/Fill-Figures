using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Diagnostics;


namespace WindowsFormsGraphics3
{
    public partial class Form1 : Form
    {
        PointF[] point;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                Brush b = Brushes.Black;
                Point arr1 = new Point(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                arr1.sort = "Y";
                Point arr2 = new Point(Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text));
                arr2.sort = "Y";
                Point arr3 = new Point(Convert.ToInt32(textBox5.Text), Convert.ToInt32(textBox6.Text));
                arr3.sort = "Y";
                Point[] arr = new Point[3] { arr1, arr2, arr3 };

                Array.Sort(arr);
                Point A = new Point();
                A.X = arr[0].X;
                A.Y = arr[0].Y;
                Point B = new Point();
                B.X = arr[1].X;
                B.Y = arr[1].Y;
                Point C = new Point();
                C.X = arr[2].X;
                C.Y = arr[2].Y;
                PointF[] p = new PointF[3] { new PointF(A.X, A.Y), new PointF(B.X, B.Y), new PointF(C.X, C.Y) };
                MessageBox.Show($"{A.X}, {A.Y}   {B.X}, {B.Y}    {C.X}, {C.Y}");
                point = p;
                g.DrawLines(new Pen(Color.Black), p);
                g.DrawLine(new Pen(Color.Black), new PointF(A.X, A.Y), new PointF(C.X, C.Y));
                float x1 = 0;
                float x2 = 0;
                float tmp = 0;

                for (int i = (int)A.Y; i <= C.Y; i++)
                {
                    x1 = A.X + (i - (int)A.Y) * (C.X - A.X) / (C.Y - A.Y);
                    if (i < B.Y)
                    {
                        x2 = A.X + (i - (int)A.Y) * (B.X - A.X) / (B.Y - A.Y);
                    }
                    else
                    {
                        if (C.Y == B.Y) x2 = B.X;
                        else x2 = B.X + (i - B.Y) * (C.X - B.X) / (C.Y - B.Y);
                    }

                    if (x1 > x2) { tmp = x1; x1 = x2; x2 = tmp; }

                    g.DrawLine(new Pen(Color.Black), new PointF(x1, i), new PointF(x2, i));

                }

                pictureBox1.Refresh();
            }
            stopwatch.Stop();
            label10.Text = "Runtime: " + stopwatch.Elapsed.ToString();
        }
        public class Point : IComparable
        {
            public float X { get; set; }
            public float Y { get; set; }
            public string sort = "";
            public Point() { }
            public Point(Point p)
            {
                this.X = p.X;
                this.Y = p.Y;
            }
            public Point(PointF p)
            {
                this.X = p.X;
                this.Y = p.Y;
            }
            public Point(float x, float y)
            {
                this.X = x;
                this.Y = y;
            }
            public override string ToString()
            {
                return $" {X}, {Y} ";
            }



            public int CompareTo(object obj)
            {
                Point p = obj as Point;
                if (p != null)
                {
                    if (sort == "Y")
                    {
                        if (this.Y <= p.Y)
                        {
                            return -1;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        if (this.X <= p.X)
                        {
                            return -1;
                        }
                        else
                        {
                            return 1;
                        }
                    }

                }
                else { throw new Exception("Невозможно сравнить"); }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var g = Graphics.FromImage(pictureBox2.Image))
            {
                Brush b = Brushes.Black;

                Point A = new Point();
                A.X = Convert.ToInt32(textBox1.Text);
                A.Y = Convert.ToInt32(textBox2.Text);
                Point B = new Point();
                B.X = Convert.ToInt32(textBox3.Text);
                B.Y = Convert.ToInt32(textBox4.Text);
                Point C = new Point();
                C.X = Convert.ToInt32(textBox5.Text);
                C.Y = Convert.ToInt32(textBox6.Text);
                Point D = new Point();
                D.X = Convert.ToInt32(textBox7.Text);
                D.Y = Convert.ToInt32(textBox8.Text);
                Point Fill = new Point();
                Fill.X = Convert.ToInt32(textBox9.Text);
                Fill.Y = Convert.ToInt32(textBox10.Text);

                Point[] polygon = new Point[4] { A, B, C, D };
                Array.Sort(polygon);
                var polygonX = new Point[4] { polygon[0], polygon[1], polygon[2], polygon[3] };
                string str = "";
              

                Array.Sort(polygon);
                var polygonY = polygon;

                g.DrawLines(new Pen(Color.Black), new PointF[] { new PointF(A.X, A.Y), new PointF(B.X, B.Y), new PointF(C.X, C.Y), new PointF(D.X, D.Y) });
                g.DrawLine(new Pen(Color.Black), new PointF(A.X, A.Y), new PointF(D.X, D.Y));
                pictureBox3.Image = pictureBox2.Image;
                foreach (var s in polygonX)
                {
                    str += s.ToString();
                }
                MessageBox.Show(str);
                str = "";
                foreach (var s in polygonY)
                {
                    str += s.ToString();
                }
                MessageBox.Show(str);
                PixelFill((int)Fill.X,(int)Fill.Y, Color.Black, Color.Red);
                pictureBox2.Refresh();
            }

            stopwatch.Stop();
            label11.Text = "Runtime: " + stopwatch.Elapsed.ToString();
        }

        void PixelFill(int x, int y, Color BorderColor, Color color)
        {

            using (var g = Graphics.FromImage(pictureBox2.Image))
            {
                Brush b = new SolidBrush(BorderColor);
                Bitmap bmp = new Bitmap(pictureBox2.Image);
                int c = 0;
                if (x > 0 && y > 0 && x < 300 && y < 300)
                {
                    c = bmp.GetPixel(x, y).A;
                    if (c != Color.Black.A)
                    {


                        g.FillRectangle(b, x, y, 1, 1);

                        PixelFill(x - 1, y, BorderColor, color);
                        PixelFill(x + 1, y, BorderColor, color);
                        PixelFill(x, y - 1, BorderColor, color);
                        PixelFill(x, y + 1, BorderColor, color);

                    }
                }
                
            }
        }
        private void clear_Click(object sender, EventArgs e)
        {

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            pictureBox3.Image = new Bitmap(pictureBox3.Width, pictureBox3.Height);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                //MouseEventArgs me = (MouseEventArgs)e;
                //var P = me.Location;

                Point P = new Point(Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox10.Text));


                Brush b = Brushes.Black;
                var arr1 = new int[2] { Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox1.Text) };
                var arr2 = new int[2] { Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox3.Text) };
                var arr3 = new int[2] { Convert.ToInt32(textBox6.Text), Convert.ToInt32(textBox5.Text) };
                var arr = new int[3] { arr1[0], arr2[0], arr3[0] };

                Array.Sort(arr);
                Vector2 A = new Vector2();

                A.X = arr[0] == arr1[0] ? arr1[1] : arr[0] == arr2[0] ? arr2[1] : arr[0] == arr3[0] ? arr3[1] : 1;
                A.Y = arr[0];
                Vector2 B = new Vector2();
                B.X = arr[1] == arr1[0] ? arr1[1] : arr[1] == arr2[0] ? arr2[1] : arr[1] == arr3[0] ? arr3[1] : 1;
                B.Y = arr[1];
                Vector2 C = new Vector2();
                C.X = arr[2] == arr1[0] ? arr1[1] : arr[2] == arr2[0] ? arr2[1] : arr[2] == arr3[0] ? arr3[1] : 1;
                C.Y = arr[2];
                PointF[] p = new PointF[3] { new PointF(A.X, A.Y), new PointF(B.X, B.Y), new PointF(C.X, C.Y) };

                point = p;

                float x1 = 0;
                float x2 = 0;
                float tmp = 0;


                x1 = A.X + (P.Y - (int)A.Y) * (C.X - A.X) / (C.Y - A.Y);
                if (P.Y <= B.Y)
                {
                    x2 = A.X + (P.Y - (int)A.Y) * (B.X - A.X) / (B.Y - A.Y);
                }
                else
                {
                    if (C.Y == B.Y) x2 = B.X;
                    else x2 = B.X + (P.Y - B.Y) * (C.X - B.X) / (C.Y - B.Y);
                }

                if (x1 > x2) { tmp = x1; x1 = x2; x2 = tmp; }

                if (P.X >= x1 && P.X <= x2 && P.Y >= A.Y && P.Y <= C.Y) { MessageBox.Show("Triangle in Point"); }
                else { MessageBox.Show("Point not in a triangle"); }


            }
        }
    }
}


