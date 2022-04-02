using System;

namespace coord
{
    public class Complex
    {
        private double real; 
        private double img;

        public Complex()
        {
            real = 0;
            img = 0;
        }
        public Complex(double real, double img)
        {
            this.real = real;
            this.img = img;
        }
        public Complex(double real)
        {
            this.real = real;
            img = 0;
        }
        ~Complex() {}

        public double Real
        {
            get { return Math.Round(real, 5); }
            set { real = value; }
        }

        public double Img
        {
            get { return Math.Round(img, 5); }
            set { img = value; }
        }

        public static Complex operator +(Complex obj1, Complex obj2) 
        {
            Complex answ = new Complex();
            answ.Img = obj1.img + obj2.img;
            answ.Real = obj1.real + obj2.real;
            return answ;
        }

        public static Complex operator -(Complex obj1, Complex obj2)
        {
            Complex answ = new Complex();
            answ.Img = obj1.img - obj2.img;
            answ.Real = obj1.real - obj2.real;
            return answ;
        }

        public static Complex operator *(Complex obj1, Complex obj2)
        {
            Complex answ = new Complex();
            answ.Real = obj1.real * obj2.real - obj1.img * obj2.img;
            answ.Img = obj1.real * obj2.img + obj1.img * obj2.real;
            return answ;
        }
        public static Complex operator /(Complex obj1, Complex obj2)
        {
            Complex answ = new Complex();
            answ.real = (obj1.real * obj2.real + obj1.img * obj2.img) / (obj2.real * obj2.real + obj2.img * obj2.img);
            answ.img = (obj1.img * obj2.real - obj1.real * obj2.img) / (obj2.real * obj2.real + obj2.img * obj2.img);
            if (double.IsNaN(answ.real) || double.IsNaN(answ.img))
            {
                answ.real = 0;
                answ.img = 0;
            }
            return answ;
        }

        public static bool operator ==(Complex obj1, Complex obj2)
        {
            return obj1.Img == obj2.Img && obj1.Real == obj2.Real;
        }

        public static bool operator !=(Complex obj1, Complex obj2)
        {
            return obj1.Img != obj2.Img || obj1.Real != obj2.Real;
        }

        private double AnglePhi()
        {
            return Math.Atan2(this.img, this.real);
        }
        public double Abs()
        {
            return Math.Sqrt(this.real * this.real + this.img * this.img);
        }

        public Complex Sqrt()
        {
            Complex answ = new Complex();
            answ.real = Math.Sqrt((this.real + Math.Sqrt(this.real * this.real + this.img * this.img)) / 2);
            answ.img = Math.Sqrt((-this.real + Math.Sqrt(this.real * this.real + this.img * this.img)) / 2);
            return answ;
        }

        public Complex Pow(int n) // по формуле Муавра
        {
            Complex answ = new Complex();
            answ.real = Math.Pow(this.Abs(), n) * Math.Cos(this.AnglePhi() * n);
            answ.img = Math.Pow(this.Abs(), n) * Math.Sin(this.AnglePhi() * n);
            return answ;
        }

        public PolarCoord ToPolar()
        {
            PolarCoord answ = new PolarCoord();
            answ.Angle = this.AnglePhi();
            answ.Rad = this.Abs();
            return answ;
        }
    }

    public class PolarCoord
    {
        private double rad;
        private double angle;

        public PolarCoord()
        {
            rad = 0;
            angle = 0;
        }
        public PolarCoord(double rad, double angle)
        {
            this.rad = rad;
            this.Angle = angle;
        }
        public PolarCoord(double rad)
        {
            this.rad = rad;
            this.angle = 0;
        }
        ~PolarCoord() {}

        public double Rad
        {
            get { return Math.Round(rad, 5); }
            set { rad = value; }
        }
        public double Angle
        {
            get 
            {
                if (angle > Math.PI)
                {
                    return Math.Round(angle - 2 * Math.PI, 5);
                }
                else if (angle < -Math.PI)
                {
                    return Math.Round(2 * Math.PI + angle, 5);
                }
                else
                {
                    return Math.Round(angle, 5);
                }
            }
            set 
            {
                angle = value % ( 2 * Math.PI); 
            }
        }

        public static PolarCoord operator +(PolarCoord obj1, PolarCoord obj2)
        {
            PolarCoord answ = new PolarCoord();
            answ.rad = Math.Sqrt(Math.Pow(obj1.rad * Math.Cos(obj1.angle) + obj2.rad * Math.Cos(obj2.angle),2) + Math.Pow(obj1.rad * Math.Sin(obj1.angle) + obj2.rad * Math.Sin(obj2.angle), 2));
            answ.Angle = Math.Atan2(obj1.rad * Math.Sin(obj1.angle) + obj2.rad * Math.Sin(obj2.angle), obj1.rad * Math.Cos(obj1.angle) + obj2.rad * Math.Cos(obj2.angle));
            return answ;
        }

        public static PolarCoord operator -(PolarCoord obj1, PolarCoord obj2)
        {
            PolarCoord answ = new PolarCoord();
            answ.rad = Math.Sqrt(Math.Pow(obj1.rad * Math.Cos(obj1.angle) - obj2.rad * Math.Cos(obj2.angle), 2) + Math.Pow(obj1.rad * Math.Sin(obj1.angle) - obj2.rad * Math.Sin(obj2.angle), 2));
            answ.Angle = Math.Atan2(obj1.rad * Math.Sin(obj1.angle) - obj2.rad * Math.Sin(obj2.angle), obj1.rad * Math.Cos(obj1.angle) - obj2.rad * Math.Cos(obj2.angle));
            return answ;
        }

        public static PolarCoord operator *(PolarCoord obj1, PolarCoord obj2) //по формуле Эйлера
        {
            PolarCoord answ = new PolarCoord();
            answ.rad = obj1.rad * obj2.rad;
            answ.Angle = obj1.angle + obj2.angle;
            return answ;
        }

        public static bool operator ==(PolarCoord obj1, PolarCoord obj2)
        {
            double epsilon = 0.0001;
            return (obj1.Angle - obj1.Angle % epsilon) == (obj2.Angle - obj2.Angle % epsilon) && (obj1.Rad - obj1.Rad % epsilon) == (obj2.Rad - obj2.Rad % epsilon);
        }

        public static bool operator !=(PolarCoord obj1, PolarCoord obj2)
        {
            return obj1.Angle != obj2.Angle || obj1.Rad != obj2.Rad;
        }

        public PolarCoord Pow(int n) // по формуле Муавра
        {
            PolarCoord answ = new PolarCoord();
            answ.rad = Math.Pow(this.rad, n);
            answ.Angle = n * this.angle; 
            return answ;
        }

        public PolarCoord Sqrt() 
        {
            PolarCoord answ = new PolarCoord();
            answ.rad = Math.Sqrt(this.rad);
            answ.Angle = this.angle/2;
            return answ;
        }

        public static PolarCoord operator /(PolarCoord obj1, PolarCoord obj2) //по формуле Эйлера
        {
            PolarCoord answ = new PolarCoord();
            answ.rad = obj1.rad / obj2.rad;
            answ.Angle = obj1.angle - obj2.angle;
            if (double.IsNaN(answ.rad) || double.IsNaN(answ.Angle))
            {
                answ.rad = 0;
                answ.angle = 0;
            }
            return answ;
        }

        public double Abs()
        {
            return this.rad;
        }
        public Complex ToComplex()
        {
            Complex answ = new Complex();
            answ.Real = this.rad * Math.Sin(this.angle);
            answ.Img = this.rad * Math.Cos(this.angle);
            return answ;
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            //double b1 = -Math.Sqrt(2) / 2;
            //double b2 = -Math.Sqrt(2) / 2;
            //Complex a1 = new Complex(b1, b2);
            //Complex a2 = new Complex(4, 10);
            // Complex a3 = a1.Pow(202200000);

            //Console.WriteLine(a3.Real + " " + a3.Img);
            //Console.WriteLine(a1.abs());
            //double a = -4;
            //float b = -5;
            // Console.WriteLine(Math.Atan2(a, b) * 57);

            double[] a_real = new double[5] { 1, 0, 0, -10, 0.2 };
            double[] a_img = new double[5] { 3, 10, 0, -20, 0.45 };

            double[] b_real = new double[5] { 2, -1, 0, -30, -0.2 };
            double[] b_img = new double[5] { 5, 45, 0, -45, 0.77 };

            double[] answ_real = new double[5] { 0.58621, 0.22211, 0, 0.41026, 0.48428 };
            double[] answ_img = new double[5] { 0.03448, -0.00494, 0, 0.05128, -0.38553 };

            for (int i = 0; i < 5; i++)
            {
                Complex a = new Complex(answ_real[i], answ_img[i]);
                PolarCoord b = a.ToPolar();
                Console.Write(b.Rad + ", ");
            }
            Console.WriteLine();
            for (int i = 0; i < 5; i++)
            {
                Complex a = new Complex(answ_real[i], answ_img[i]);
                PolarCoord b = a.ToPolar();
                Console.Write(b.Angle + ", ");
            }


            //Assert.AreEqual(account_c.Real, 0, 0.22211);
            //Assert.AreEqual(account_c.Img, -0.00494);

            //ssert.AreEqual(account_c.Real, 0.58620);
            //Assert.AreEqual(account_c.Img, 0.03448);
            Console.ReadKey();
        }
    }
}