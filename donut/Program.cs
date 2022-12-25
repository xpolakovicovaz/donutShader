using System;
using System.Threading;

namespace donut
{
    public class alfabetay 
    {
        public double alfa;
        public double beta;
        public double y;
        public alfabetay(double alfa, double beta, double y)
        {
            this.alfa = alfa;
            this.beta = beta;
            this.y = y;
        }
    }
    public class angle
    {
        public double angel1;
        public double angel2;
        public double angel3;
        public angle()
        {
            angel1 = 0; 
            angel2 = 0;
            angel3 = 0;
        }
        public angle(double angel1, double angel2, double angel3)
        {
            this.angel1 = angel1;
            this.angel2 = angel2;
            this.angel3 = angel3;
        }
        public void add(angle otherAngle)
        {
            angel1 += otherAngle.angel1;
            angel2 += otherAngle.angel2;
            angel3+= otherAngle.angel3;               
        }
    }
    class Program
    {
        //tube radius
        private static double r1 = 8;
        //ring radius
        private static double r2 = 12;
        //shades
        private static char[] c = new char[22] { '.', ',', '-', '=', ':', '<', 'c', 'e', 'x', 'i', 'I', '3', 'H', 'V', 'U', 'O', 'K', '8', '0', 'N', 'M', ' ' };
        //rotation of main axis in current step
        private static angle rotation;
        //width of screen
        public static int width = 50;
        //triangulation steps
        public static double steps = 200;
        static void Main(string[] args)
        {
            Console.SetWindowSize(width, width);
            int i = 0;
            var r = new Random();
            angle subangle = new angle();
            rotation = new angle();
            alfabetay[,] screen = new alfabetay[width, width];
   
            while (true)
            {
                try
                {
                    calculateSubangle(subangle, i, r);
                    rotation.add(subangle);                   

                    drawRotatedDonut(screen);
                    i++;
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    var qw = 4;
                }
            }
 
        }
        //calculate rotation increment in i-th step
        private static void calculateSubangle(angle subangle, int i, Random r)
        {
            if (i % 20 == 0)
                subangle.angel1 = (r.Next() % 2) * 2 * Math.PI / steps;
            if (i % 25 == 0)
                subangle.angel2 = (r.Next() % 2) * 2 * Math.PI / steps;
            if (i % 30 == 0)
                subangle.angel3 = (r.Next() % 2) * 2 * Math.PI / steps;

            if (subangle.angel1 == 0 && subangle.angel2 == 0)
            {
                subangle.angel2 = 2 * Math.PI / steps;
                subangle.angel3 = 2 * Math.PI / steps;
            }
        }

        private static void clearscrean(alfabetay[,] screen)
        {
            for (int i = 0; i < width; i++)
                for (int j = 0; j < width; j++)
                    screen[i, j] = null;
        }

        private static void drawRotatedDonut(alfabetay[,] screen)
        {
            clearscrean(screen);
            string s = "";
            Tuple<double, double, double> point;
            int x;
            int z;
            for (double alfa  = -Math.PI; alfa <= Math.PI; alfa +=Math.PI/180)
            {
                for (double beta = -Math.PI ; beta <= Math.PI ; beta += Math.PI / 180)
                {
                    point = GetPoint(alfa, beta);
                    x = (int)Math.Round(point.Item1, 0, MidpointRounding.AwayFromZero) + width / 2;
                    z = (int)Math.Round(point.Item3, 0, MidpointRounding.AwayFromZero) + width / 2;
                    if (screen[x,z] == null || (screen[x,z].y < point.Item2))
                        screen[x,z] = new alfabetay(alfa, beta, point.Item2);
                }
            }
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == width - 1 || j == 0 || j == width - 1)
                        s += "+";
                    else
                    {
                        if (screen[i, j] == null)
                            s += c[21];
                        else
                            s += GetShade(screen[i, j].alfa, screen[i, j].beta);
                    }
                }
                s += Environment.NewLine;
            }
            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(@s);
            Console.SetCursorPosition(0, 0);

        }

        private static Tuple<double, double, double> GetPoint(double alfa, double beta)
        {
            return new Tuple<double, double, double>(
                GetX(alfa, beta),
                GetY(alfa, beta),
                GetZ(alfa, beta)
                );
        }

        private static double GetZ(double alfa, double beta)
        {
            return (-Math.Cos(rotation.angel1) * Math.Sin(rotation.angel2) * Math.Cos(rotation.angel3) + Math.Sin(rotation.angel1) * Math.Sin(rotation.angel3)) * (r2 * Math.Cos(beta) + r1 * Math.Cos(alfa) * Math.Cos(beta))
                + (Math.Cos(rotation.angel1) * Math.Sin(rotation.angel2) * Math.Sin(rotation.angel3) + Math.Sin(rotation.angel1) * Math.Cos(rotation.angel3)) * (r2 * Math.Sin(beta) + r1 * Math.Cos(alfa) * Math.Sin(beta))
                + (Math.Cos(rotation.angel1) * Math.Cos(rotation.angel2) * r1 * Math.Sin(alfa));
        }

        private static double GetY(double alfa, double beta)
        {
            return (Math.Sin(rotation.angel1) * Math.Sin(rotation.angel2) * Math.Cos(rotation.angel3) + Math.Cos(rotation.angel1) * Math.Sin(rotation.angel3)) * (r2 * Math.Cos(beta) + r1 * Math.Cos(alfa) * Math.Cos(beta))
                + (-Math.Sin(rotation.angel1) * Math.Sin(rotation.angel2) * Math.Sin(rotation.angel3) + Math.Cos(rotation.angel1) * Math.Cos(rotation.angel3)) * (r2 * Math.Sin(beta) + r1 * Math.Cos(alfa) * Math.Sin(beta))
                + (-Math.Sin(rotation.angel1) * Math.Cos(rotation.angel2) * r1 * Math.Sin(alfa));
        }

        private static double GetX(double alfa, double beta)
        {
            return ( Math.Cos(rotation.angel2) * Math.Cos(rotation.angel3)) * (r2 * Math.Cos(beta) + r1 * Math.Cos(alfa) * Math.Cos(beta))
                + (- Math.Cos(rotation.angel2) * Math.Sin(rotation.angel3) ) * (r2 * Math.Sin(beta) + r1 * Math.Cos(alfa) * Math.Sin(beta))
                + (Math.Sin(rotation.angel2) * r1 * Math.Sin(alfa));
        }

      
        private static char GetShade(double alpha, double beta)
        {
            double ss = GetRotatedSS(alpha, beta);
            if (ss < 0)
                return c[21];
            else
            {
                int i = (int)Math.Round((ss ) * 20, 0, MidpointRounding.AwayFromZero);
                return c[i];
            }
        }

        private static double GetRotatedSS(double alfa, double beta)
        {
            return ((Math.Sin(rotation.angel1) * Math.Sin(rotation.angel2) * Math.Cos(rotation.angel3) + Math.Cos(rotation.angel1) * Math.Sin(rotation.angel3)) * (Math.Cos(alfa) * Math.Cos(beta))
                + (-Math.Sin(rotation.angel1) * Math.Sin(rotation.angel2) * Math.Sin(rotation.angel3) + Math.Cos(rotation.angel1) * Math.Cos(rotation.angel3)) * ( Math.Cos(alfa) * Math.Sin(beta))
                + (-Math.Sin(rotation.angel1) * Math.Cos(rotation.angel2) * Math.Sin(alfa)));
        }
     
    }
}
