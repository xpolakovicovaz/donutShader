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
    
    class Program
    {
        private static double r1 = 10;
        private static double r2 = 20;
        private static char[] c = new char[22] { ',', ',', '-', '=', ':', '<', 'c', 'e', 'x', 'i', 'I', '3', 'H', 'V', 'U', 'O', 'K', '8', '0', 'N', 'M', ' ' };

        private static double angel1=0;
        private static double angel2=0;
        private static double angel3=0;


        public static int width = 100;

        public static double step = 200;
        static void Main(string[] args)
        {
            Console.SetWindowSize(width, width);
            int i = 0;
            var r = new Random();
            double subangle1 = 0;
            double subangle2 = 0;
            double subangle3 = 0;
            alfabetay[,] screen = new alfabetay[width, width];
   
            while (true)
            {
                try
                {
                    if (i % 20 == 0)
                        subangle1 = (r.Next() % 2) * 2 * Math.PI / step;
                    if (i % 25 == 0)
                        subangle2 = (r.Next() % 2) * 2 * Math.PI / step;
                    if (i % 30 == 0)
                    {
                        if (subangle1 == 0 && subangle2 == 0)
                            subangle3 = 2 * Math.PI / step;
                        else
                            subangle3 = (r.Next() % 2) * 2 * Math.PI / step;
                    }
                    if (subangle1 == 0 && subangle2 == 0)
                    {
                        subangle3 = 2 * Math.PI / step;
                        subangle2 = 2 * Math.PI / step;
                    }
                    angel1 += subangle1;
                    angel2 += subangle2;
                    angel3 += subangle3;

                    drawRotatedDonut(screen);
                    i++;
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    var qw = 4;
                    qw++;
                }
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
            for (double alfa  = -Math.PI; alfa <= Math.PI; alfa +=Math.PI/180)
            {
                for (double beta = -Math.PI ; beta <= Math.PI ; beta += Math.PI / 180)
                {
                    var a = GetPoint(alfa, beta);
                    if (screen[(int)Math.Round(a.Item1, 0, MidpointRounding.AwayFromZero)+ width/2, (int)Math.Round(a.Item3, 0, MidpointRounding.AwayFromZero)+ width/2] == null
                        || (screen[(int)Math.Round(a.Item1, 0, MidpointRounding.AwayFromZero)+ width/2, (int)Math.Round(a.Item3, 0, MidpointRounding.AwayFromZero)+ width/2].y < a.Item2))
                        screen[(int)Math.Round(a.Item1, 0, MidpointRounding.AwayFromZero)+ width/2, (int)Math.Round(a.Item3, 0, MidpointRounding.AwayFromZero)+ width/2] = new alfabetay(alfa, beta, a.Item2);
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
            return (-Math.Cos(angel1) * Math.Sin(angel2) * Math.Cos(angel3) + Math.Sin(angel1) * Math.Sin(angel3)) * (r2 * Math.Cos(beta) + r1 * Math.Cos(alfa) * Math.Cos(beta))
                + (Math.Cos(angel1) * Math.Sin(angel2) * Math.Sin(angel3) + Math.Sin(angel1) * Math.Cos(angel3)) * (r2 * Math.Sin(beta) + r1 * Math.Cos(alfa) * Math.Sin(beta))
                + (Math.Cos(angel1) * Math.Cos(angel2) * r1 * Math.Sin(alfa));
        }

        private static double GetY(double alfa, double beta)
        {
            return (Math.Sin(angel1) * Math.Sin(angel2) * Math.Cos(angel3) + Math.Cos(angel1) * Math.Sin(angel3)) * (r2 * Math.Cos(beta) + r1 * Math.Cos(alfa) * Math.Cos(beta))
                + (-Math.Sin(angel1) * Math.Sin(angel2) * Math.Sin(angel3) + Math.Cos(angel1) * Math.Cos(angel3)) * (r2 * Math.Sin(beta) + r1 * Math.Cos(alfa) * Math.Sin(beta))
                + (-Math.Sin(angel1) * Math.Cos(angel2) * r1 * Math.Sin(alfa));
        }

        private static double GetX(double alfa, double beta)
        {
            return ( Math.Cos(angel2) * Math.Cos(angel3)) * (r2 * Math.Cos(beta) + r1 * Math.Cos(alfa) * Math.Cos(beta))
                + (- Math.Cos(angel2) * Math.Sin(angel3) ) * (r2 * Math.Sin(beta) + r1 * Math.Cos(alfa) * Math.Sin(beta))
                + (Math.Sin(angel2) * r1 * Math.Sin(alfa));
        }

        private static void drawDonut()
        {
            string s = "";
            for (int z = -30; z <= 30; z++)
            {
                for (int x = -30; x <= 30; x++)
                {
                    var a = GetAngles(x, z);
                    if (a == null)
                        s += c[21];
                    else
                        s += GetShade(a.Item1, a.Item2);
                }
               // Console.WriteLine(s);s = "";
                s += Environment.NewLine;
            }
            Console.WriteLine(@s);
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

        private static double GetSS(double alpha, double beta)
        {
            return -Math.Cos(alpha) * Math.Sin(beta);
        }
        private static double GetRotatedSS(double alfa, double beta)
        {
            return (             (Math.Sin(angel1) * Math.Sin(angel2) * Math.Cos(angel3) + Math.Cos(angel1) * Math.Sin(angel3)) * (Math.Cos(alfa) * Math.Cos(beta))
                + (-Math.Sin(angel1) * Math.Sin(angel2) * Math.Sin(angel3) + Math.Cos(angel1) * Math.Cos(angel3)) * ( Math.Cos(alfa) * Math.Sin(beta))
                + (-Math.Sin(angel1) * Math.Cos(angel2) * Math.Sin(alfa)));
        }
        private static Tuple<double, double> GetAngles(int x, int z)
        {
            double alpha1, alpha2, beta1, beta2, beta3, beta4;
            double maxy = -100;
            Tuple<double, double> result = null;

            alpha1 = Math.Asin(z / r1);
            if (double.IsNaN(alpha1))
                return result;
            alpha2 =Math.PI -alpha1;

            beta1 = Math.Acos(x / (r2 + (r1 * Math.Cos(alpha1))));
            beta2 = -beta1;
            beta3 = Math.Acos(x / (r2 + (r1 * Math.Cos(alpha2))));
            beta4 = -beta3;

            if (!double.IsNaN(beta1)) if (isPoint(alpha1, beta1, x, z) && getY(alpha1, beta1) > maxy) { maxy = getY(alpha1, beta1); result = new Tuple<double, double>(alpha1, beta1); }
            if (!double.IsNaN(beta1)) if (isPoint(alpha1, beta2, x, z) && getY(alpha1, beta2) > maxy) { maxy = getY(alpha1, beta2); result = new Tuple<double, double>(alpha1, beta2); }
            if (!double.IsNaN(beta2)) if (isPoint(alpha1, beta3, x, z) && getY(alpha1, beta3) > maxy) { maxy = getY(alpha1, beta3); result = new Tuple<double, double>(alpha1, beta3); }
            if (!double.IsNaN(beta2)) if(isPoint(alpha1, beta4, x, z) && getY(alpha1, beta4) > maxy) { maxy = getY(alpha1, beta4); result = new Tuple<double, double>(alpha1, beta4); }

            if (!double.IsNaN(beta1)) if (isPoint(alpha2, beta1, x, z) && getY(alpha2, beta1) > maxy) { maxy = getY(alpha2, beta1); result = new Tuple<double, double>(alpha2, beta1); }
            if (!double.IsNaN(beta1)) if(isPoint(alpha2, beta2, x, z) && getY(alpha2, beta2) > maxy) { maxy = getY(alpha2, beta2); result = new Tuple<double, double>(alpha2, beta2); }
            if (!double.IsNaN(beta2)) if(isPoint(alpha2, beta3, x, z) && getY(alpha2, beta3) > maxy) { maxy = getY(alpha2, beta3); result = new Tuple<double, double>(alpha2, beta3); }
            if (!double.IsNaN(beta2)) if (isPoint(alpha2, beta4, x, z) && getY(alpha2, beta4) > maxy) { maxy = getY(alpha2, beta4); result = new Tuple<double, double>(alpha2, beta4); }

            return result;
        }
        private static bool isPoint(double alpha, double beta, int x, int z)
        {
            return Math.Round(r1 * Math.Sin(alpha), 0, MidpointRounding.AwayFromZero) == z
                && Math.Round(r2 * Math.Cos(beta) + r1 * Math.Cos(alpha) * Math.Cos(beta), 0, MidpointRounding.AwayFromZero) == x;
        }

        private static double getY(double alpha, double beta)
        {
            return Math.Round(r2 * Math.Sin(beta) + r1 * Math.Cos(alpha) * Math.Sin(beta),2,MidpointRounding.AwayFromZero);
        }

    }
}
