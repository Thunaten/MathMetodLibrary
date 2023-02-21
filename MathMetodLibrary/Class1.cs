namespace MathMetodLibrary
{
    public class Calculation
    {
        public static List<double> Si = new List<double>();
        public static List<double> G02 = new List<double>();
        public static List<double> Gr = new List<double>();
        public static List<double> GoOB = new List<double>();
        public static List<double> GzOB = new List<double>();
        public static List<double> delta1 = new List<double>();
        public static List<double> Pza = new List<double>();
        public static List<double> Ezsum = new List<double>();
        public static List<double> Ezsk = new List<double>();
        public static double x;
        public static double rcc;
        public static double ek;
        public static double et;
        public static double delta1max;
        public static double delta1min;
        public static double Rza;
        public static double deltaL;
        // static_public_void(тип выводимих в конце данных)_название_(Тип и кол-во получаемых данных через запятую)
        // Расчёт гильзы
        public static void GilzaCalc(double Dnar, double Dosn, double Dsk, double dsk, double Lkr, double Lsk, double Rcc, double Scc, double Lsm, double dsm, double R, double dcc, double Lcc, double Sd, double Lg, double Kp, double Ek, double Eg, double Kv, double ftr, double a, double Tm, double Tex, double al, double Pmax, double G0, double G11, double G22, double delta0, double ey)
        {
            Si.Clear();
            G02.Clear();
            Gr.Clear();
            GoOB.Clear();
            GzOB.Clear();
            delta1.Clear();
            Pza.Clear();
            Ezsum.Clear();
            Ezsk.Clear();
            
            double K = Lkr - Sd;
            // Округление переменной
            K = Math.Floor(K);

            // Создание листа значений от 0 до k
            var Zk = new List<int>();
            for (int z = 0; z <= K; z++)
            {
                Zk.Add(z);
            }

            int max = 0;
            // выборка максимального значения в листе
            foreach (int c in Zk)
            {
                for (; c > max; max = c) ;
            }

            double n = 20;
            double NN = max * n;

            // создание листа со значениями от 0 до NN, сечений гильзы
            var zi = new List<double>();
            for (double i = 0; i <= NN; i++)
            {
                double half = i / n;
                zi.Add(half);
            }

            // Переменные "дельта"
            double deltaDnar = ((Dnar - Dsk) / (Lkr - Sd));
            double deltadvn = ((dcc - dsk) / (Lkr - (Lcc + Sd)));
            double deltaRd = Math.Sqrt(Math.Pow(R, 2) - Math.Pow((R - R - Lcc + Lsm), 2));

            // Заполнение листа Di - наружный диаметр по сечениям
            var Di = new List<double>();
            foreach (double c in zi)
            {
                double d = Dnar - deltaDnar * c;
                Di.Add(d);
            }

            // Заполнение листа di - внутренний диаметр по сечениям
            var di = new List<double>();
            foreach (double c in zi)
            {
                if (c <= Lsm)
                {
                    double d = Dosn + 2 * Math.Sqrt(Math.Pow(Rcc, 2) - Math.Pow((Rcc - c), 2));
                    di.Add(d);
                }
                else if (c > Lsm && c <= Lcc)
                {
                    double d = dsm + 2 * (Math.Sqrt(Math.Pow(R, 2) - Math.Pow((R - R - Lcc + Lsm + c - Lsm), 2)) - deltaRd);
                    di.Add(d);
                }
                else if (c > Lcc && c <= (Lkr - Sd))
                {
                    double d = dcc - deltadvn * (c - Lcc - Lsm);
                    di.Add(d);
                }
            }

            // Заполнение листа Si - толщина стенки гильзы по сечениям
            for (int i = 0; i <= NN; i++)
            {
                double si = (Di[i] - di[i]) / 2;
                Si.Add(si);
            }

            // Расчёт предела текучести по сечениям

            //Переменные
            double L1 = Lg - Sd - Lcc;
            double L2 = Lg - Lsk;
            double deltaG01 = ((G11 - G0) / ((Lg - Sd) - L1));
            double deltaG02 = ((G22 - G11) / (L1 - L2));
            double deltaz = Lg - L1 - Sd;

            // Расчёт
            G02 = new List<double>();
            foreach (double c in zi)
            {
                if (c <= deltaz)
                {
                    double d = G0 + deltaG01 * c;
                    G02.Add(d);
                }
                else
                {
                    double d = (G11 + deltaG02 * (c - deltaz));
                    G02.Add(d);
                }
            }

            // !!!Здесь расхождение с маткадовскими расчётами, скорее всего из-за округлений в оных!!!
            // Длина моментного участка корпуса гильзы
            Console.WriteLine();
            Console.WriteLine("Результаты расчётов:");
            x = ((Math.PI * Math.Sqrt(dsm * Scc / 2)) / (Math.Pow((3 * (1 - Math.Pow(Kp, 2))), 0.25)));
            x = Math.Round(x, 2);
            Console.WriteLine("Длина моментного участка корпуса гильзы: " + x);

            // Средний радиус стенки гильзы в сечении сопряжения
            rcc = ((Rcc + Scc) / 2);
            // Округление до сотых
            rcc = Math.Round(rcc, 2);
            Console.WriteLine("Средний радиус стенки гильзы в сечении сопряжения: " + rcc);

            double k = ((Math.Pow((3 * (1 - Math.Pow(Kp, 2))), 0.25)) / ((Math.PI * Math.Sqrt(Rcc * Scc))));
            double fs = Sd / Scc;
            double Q0 = ((Pmax / (4 * k)) - (((1 - Kp) - fs * (2 * Kp)) / (fs + (1 / Math.Sqrt(fs)) - ((Math.Pow((Math.Pow(fs, 2) - 1), 2)) / (2 * (Math.Pow(fs, 3) + Math.Sqrt(fs)))))));
            double M0 = (-Q0 * (Math.Sqrt(fs) / (2 * k)) * ((Math.Pow(fs, 2) - 1) / (Math.Pow(fs, 2) * Math.Sqrt(fs) + 1)));

            // создание листа со значениями от 0 до NN
            var ji = new List<int>();
            for (int i = 0; i <= NN; i++)
            {
                ji.Add(i);
            }

            var Qx = new List<double>();
            var No = new List<double>();
            var Pr = new List<double>();
            double pr;

            foreach (int c in ji)
            {
                double qx = ((2 * Math.Pow(k, 2) * M0 * (Math.Exp(-k * zi[c])) * ((Math.Sin(k * zi[c])) - (Math.Cos(k * zi[c])))) - (2 * k * Q0 * (Math.Exp(-k * zi[c])) * Math.Cos(k * zi[c])));
                Qx.Add(qx);
                double no = ((2 * k * rcc * Q0 * Math.Exp(-k * zi[c]) * Math.Cos(k * zi[c])) + (2 * Math.Pow(k, 2) * rcc * M0 * Math.Exp(-k * zi[c]) * ((Math.Cos(k * zi[c])) - (Math.Sin(k * zi[c])))));
                No.Add(no);
                if (zi[c] <= x)
                {
                    pr = ((Kv * G02[c] - (No[c] / Si[c])) * (Math.Log(Di[c] / di[c]) + (Qx[c] / (Kv * G02[c] - (No[c] / Si[c])))));
                    Pr.Add(pr);
                }
                else if (zi[c] > x && zi[c] <= (Lkr - Sd))
                {
                    pr = Math.Log(Di[c] / di[c]) * Kv * G02[c];
                    Pr.Add(pr);
                }
            }

            Gr = new List<double>();
            foreach (int c in ji)
            {
                double gr = -(Pmax - Pr[c]);
                Gr.Add(gr);
            }

            // Вспомогательные переменные; есть к ним вопросики...
            int MM = 0;
            for (int c = 0; zi[c] <= x; c++)
            {
                MM = c;
            }

            int w = 0;
            for (int c = 0; zi[c] <= Lcc; c++)
            {
                w = c;
            }

            // Осевые и тангенциальные напряжения (и связанные расчёты)
            var Dsr = new List<double>();
            foreach (int c in ji)
            {
                double dsred = (Di[c] + di[c]) / 2;
                Dsr.Add(dsred);
            }

            var Fj = new List<double>();
            foreach (int c in ji)
            {
                double fj = ((Math.PI / 4) * ((Math.Pow(Di[c], 2)) - (Math.Pow(di[c], 2))));
                Fj.Add(fj);
            }

            var deltalj = new List<double>();
            foreach (int c in ji)
            {
                double lj = zi[c] - x;
                deltalj.Add(lj);
            }

            var Ptr = new List<double>();
            foreach (int c in ji)
            {
                double ptr = Math.PI * Dsr[c] * ftr * deltalj[c] * (-Gr[c]);
                Ptr.Add(ptr);
            }

            double b = 0.75;
            double Fsn = 0;

            for (int c = 0; zi[c] <= x; c++)
            {
                double S1 = Di[c];
                double S2 = di[c];
                Fsn = Math.PI / 4 * (Math.Pow(S1, 2) - Math.Pow(S2, 2));
            }

            double Nxsn = b * Kv * G02[MM] * Fsn;

            // Создание листа со значениями от 0 до w
            var I1 = new List<int>();
            for (int i = 0; i <= w; i++)
            {
                I1.Add(i);
            }

            var Gz1 = new List<double>();
            foreach (int c in I1)
            {
                double gzl = (Nxsn - Ptr[c]) / Fj[c];
                Gz1.Add(gzl);
            }

            var Go1 = new List<double>();
            foreach (int c in I1)
            {
                double o1 = (((Gr[c] + Gz1[c]) / 2) + (0.5 * Math.Sqrt((4 * Math.Pow((Kv * G02[c]), 2)) - (3 * Math.Pow((Gz1[c] - Gr[c]), 2)))));
                Go1.Add(o1);
            }

            // Создание листа со значениями от w до NN
            var I2 = new List<int>();
            for (int i = w; i <= NN; i++)
            {
                I2.Add(i);
            }

            var Gz2 = new List<double>();
            var Go2 = new List<double>();
            foreach (int c in I2)
            {
                double go2 = ((2 / Math.Sqrt(3)) * Kv * G02[c] + Gr[c]);
                Go2.Add(go2);
                double ZZ = ((Gr[c] + go2) / 2);
                Gz2.Add(ZZ);
            }
            // Расчёт напряжений завершён

            // Распределение конечного зазора по длине корпуса гильзы

            // Создание общих листов Gz и Go от 0 до NN
            GzOB = new List<double>();
            GoOB = new List<double>();
            foreach (int c in I1)
            {
                GzOB.Add(Gz1[c]);
                GoOB.Add(Go1[c]);
            }
            foreach (int c in I2)
            {
                int i = c - w;
                GzOB.Add(Gz2[i]);
                GoOB.Add(Go2[i]);
            }

            // Упругая деформация каморы
            ek = ((2 * Pmax) / (3 * Ek) * ((2 * Math.Pow(a, 2) + 1) / (Math.Pow(a, 2) - 1)));
            Console.WriteLine("Упругая деформация каморы: " + ek);

            // Тепловая деформация каморы
            et = al * (Tm - Tex);
            Console.WriteLine("Тепловая деформация каморы: " + et);

            // Упругая деформация гильзы
            var ez = new List<double>();
            foreach (int c in ji)
            {
                double ezupr = ((GoOB[c] / Eg) - (Kp / Eg) * (Gr[c] - GzOB[c]));
                ez.Add(ezupr);
            }

            delta1 = new List<double>();
            foreach (int c in ji)
            {
                double delta1upr = ez[c] + et - ek;
                delta1.Add(delta1upr);
            }

            // выборка максимального значения в листе
            delta1max = 0;
            foreach (double c in delta1)
            {
                for (; c > delta1max; delta1max = c) ;
            }

            // выборка минимального значения в листе
            delta1min = 1;
            foreach (double c in delta1)
            {
                for (; c < delta1min; delta1min = c) ;
            }
            Console.WriteLine("Максимальное значение упругой деформации гильзы: " + delta1max);
            Console.WriteLine("Минимальное значение упругой деформации гильзы: " + delta1min);
            //Расчёт распределения конечного зазора по длине завершён

            // Распределение силы защемления по длине корпуса гильзы

            var M = new List<double>();
            foreach (int c in ji)
            {
                double m = (2 * Si[c]) / Di[c];
                M.Add(m);
            }

            var Pk = new List<double>();
            foreach (int c in ji)
            {
                double pk = ((-delta1[c] * Eg * (2 - M[c]) * M[c] * 3) / (2 * (3 - 4 * M[c])));
                Pk.Add(pk);
            }

            Pza = new List<double>();
            foreach (int c in ji)
            {
                double pza = (Math.PI * 0.05 * Di[c] * Pk[c] * ftr);
                Pza.Add(pza);
            }

            Rza = 0;
            foreach (int c in ji)
            {
                if (Pza[c] <= 0)
                {
                    Rza = Rza + 0;
                }
                else
                {
                    Rza = Rza + Pza[c];
                }
            }

            // Вывод данных для проверки
            Console.WriteLine("Суммарная максимальная общая сила защемления: " + Rza);

            // Расчёт на поперечный разрыв

            //Создание листов данных и их заполнение значениями из соответствующих листов напряжений
            var G1 = new List<double>();
            foreach (int c in ji)
            {
                if (GoOB[c] > GzOB[c] && GoOB[c] > Gr[c])
                {
                    G1.Add(GoOB[c]);
                }
                else if (GzOB[c] > GoOB[c] && GzOB[c] > Gr[c])
                {
                    G1.Add(GzOB[c]);
                }
                else
                {
                    G1.Add(Gr[c]);
                }
            }

            var G2 = new List<double>();
            foreach (int c in ji)
            {
                if ((GzOB[c] > GoOB[c] && GoOB[c] > Gr[c]) || (Gr[c] > GoOB[c] && GoOB[c] > GzOB[c]))
                {
                    G2.Add(GoOB[c]);
                }
                else if ((GoOB[c] > GzOB[c] && GzOB[c] > Gr[c]) || (Gr[c] > GzOB[c] && GzOB[c] > GoOB[c]))
                {
                    G2.Add(GzOB[c]);
                }
                else
                {
                    G2.Add(Gr[c]);
                }
            }

            var G3 = new List<double>();
            foreach (int c in ji)
            {
                if (GoOB[c] < GzOB[c] && GoOB[c] < Gr[c])
                {
                    G3.Add(GoOB[c]);
                }
                else if (GzOB[c] < GoOB[c] && GzOB[c] < Gr[c])
                {
                    G3.Add(GzOB[c]);
                }
                else
                {
                    G3.Add(Gr[c]);
                }
            }

            // Вид напряженного состояния
            var Vg = new List<double>();
            foreach (int c in ji)
            {
                double vg = (2 * G2[c] - G1[c] - G3[c]) / (G1[c] - G3[c]);
                vg = Math.Round(vg, 3);
                Vg.Add(vg);
            }

            // Осевая деформация корпуса
            var Ez = new List<double>();
            foreach (int c in ji)
            {
                if (GzOB[c] > GoOB[c])
                {
                    double Ezi = ((delta0 * (3 - Vg[c])) / (2 * Vg[c]));
                    Ez.Add(Ezi);
                }
                else
                {
                    double Ezi = ((delta0 * (2 * Vg[c])) / (3 - Vg[c]));
                    Ez.Add(Ezi);
                }
            }

            // Упругая деформация корпуса
            var Eupr = new List<double>();
            foreach (int c in ji)
            {
                double upr = (GzOB[c] / (Eg)) - ((Kp / Eg) * (GoOB[c] + Gr[c]));
                Eupr.Add(upr);
            }

            //Суммарная осевая деформация
            Ezsum = new List<double>();
            foreach (int c in ji)
            {
                double ezsum = (Ez[c] + Eupr[c]);
                Ezsum.Add(ezsum);
            }

            //Выборка максимального значения
            double deltaEmax = 0;
            foreach (double c in Ezsum)
            {
                for (; c > deltaEmax; deltaEmax = c) ;
            }
            double deltaE = deltaEmax - ey;

            //Скорректированные значения деформации
            Ezsk = new List<double>();
            foreach (int c in I1)
            {
                double ezsk = Ez[c] - deltaE;
                Ezsk.Add(ezsk);
            }

            //Общее удлинение
            deltaL = 0;
            foreach (int c in I1)
            {
                if (Ezsk[c] <= 0)
                {
                    deltaL = deltaL + 0;
                }
                else
                {
                    deltaL = deltaL + (0.05 * Ezsk[c]);
                }
            }
            Console.WriteLine("Общее удлинение: " + deltaL);

            //Максимальный допустимый осевой зазор
            double nox = 0.1;
            //Допустимая величина упругой деформации узла запирания
            double nuz = 0.5 * (deltaL - 0.5 * nox);
            Console.WriteLine("Допустимая величина упругой деформации узла запирания: " + deltaL);

            /*foreach (double c in Ezsum)
            {
                Console.WriteLine(c);
            }*/
        }
    }
}