using MathMetodLibrary;

string path = "../../../../testfile.txt";
// создание листа, в который будут вносится значения данных по мере их чтения из внешнего файла
var Data = new List<Double>();

// чтение файла с исходными данными
using (StreamReader reader = new StreamReader(path))
{
    // чтение файла построчно (пока строки не кончатся)
    string? line;
    while ((line = await reader.ReadLineAsync()) != null)
    {
        string Digits = (String.Empty);
        foreach (char c in line)
        {
            /* берём из каждой строки текстового файла только числа и знаки препинания,
            !!!ВАЖНО!!! разделительный знак - запятая. Никаких точек в данных, а то сломается. */
            if (Char.IsDigit(c))
                Digits += c;
            if (Char.IsPunctuation(c))
                Digits += c;
        }
        //конвертация текста файла в десятичный формат
        Data.Add(Convert.ToDouble(Digits));
    }
}
// Присвоение данных из листа, сформированного выше, соответствующим переменным и их вывод
Console.WriteLine("Исходные данные:");
// Диаметр донной части корпуса наружный
double Dnar = Data[0];
Console.WriteLine("Диаметр донной части корпуса наружный: " + Dnar);
// Диаметр плоского основания донной части
double Dosn = Data[1];
Console.WriteLine("Диаметр плоского основания донной части: " + Dosn);
// Наружный диаметр основания ската
double Dsk = Data[2];
Console.WriteLine("Наружный диаметр основания ската: " + Dsk);
// Внутренний диаметр основания ската
double dsk = Data[3];
Console.WriteLine("Внутренний диаметр основания ската: " + dsk);
// Высота гильзы до основания ската
double Lkr = Data[4];
Console.WriteLine("Высота гильзы до основания ската: " + Lkr);
// Высота гильзы по скату до основания дульца
double Lsk = Data[5];
Console.WriteLine("Высота гильзы по скату до основания дульца: " + Lsk);
// Радиус скругления у дна гильзы
double Rcc = Data[6];
Console.WriteLine("Радиус скругления у дна гильзы: " + Rcc);
// Толщина стенки в сечении сопряжения радиусовы
double Scc = Data[7];
Console.WriteLine("Толщина стенки в сечении сопряжения радиусов: " + Scc);
// Длина придонного участка с малым радиусом
double Lsm = Data[8];
Console.WriteLine("Длина придонного участка с малым радиусом: " + Lsm);
// Внутренний диаметр участка перехода радиусов
double dsm = Data[9];
Console.WriteLine("Внутренний диаметр участка перехода радиусов: " + dsm);
// Радиус скругления участка сопряжения
double R = Data[10];
Console.WriteLine("Радиус скругления участка сопряжения: " + R);
// Внутренний диаметр участка сопряжения
double dcc = Data[11];
Console.WriteLine("Внутренний диаметр участка сопряжения: " + dcc);
// Длина участка сопряжения
double Lcc = Data[12];
Console.WriteLine("Длина участка сопряжения: " + Lcc);
// Толщина дна гильзы
double Sd = Data[13];
Console.WriteLine("Толщина дна гильзы: " + Sd);
// Полная длина гильзы
double Lg = Data[14];
Console.WriteLine("Полная длина гильзы: " + Lg);
// Коэффициент Пуассона
double Kp = Data[16];
Console.WriteLine("Коэффициент Пуассона: " + Kp);
// Модуль упругости материала каморы
double Ek = Data[17];
Console.WriteLine("Модуль упругости материала каморы: " + Ek);
// Модуль упругости материала гильзы
double Eg = Data[18];
Console.WriteLine("Модуль упругости материала гильзы: " + Eg);
// Динамический коэффициент учета скорости деформации на предел текучести
double Kv = Data[19];
Console.WriteLine("Динамический коэффициент учета скорости деформации на предел текучести: " + Kv);
// Коэффициент трения между гильзой и каморой
double ftr = Data[20];
Console.WriteLine("Коэффициент трения между гильзой и каморой: " + ftr);
// Относительная толщина стенки каморы
double a = Data[21];
Console.WriteLine("Относительная толщина стенки каморы: " + a);
// Температура усреднённая по стенке в момент действия максимального давления
double Tm = Data[22];
Console.WriteLine("Температура усреднённая по стенке в момент действия максимального давления: " + Tm);
// Температура усреднённая по стенке в момент экстракции
double Tex = Data[23];
Console.WriteLine("Температура усреднённая по стенке в момент экстракции: " + Tex);
// Коэффициент линейного расширения материала гильзы
double al = Data[24];
Console.WriteLine("Коэффициент линейного расширения материала гильзы: " + al);
// Максимальное давление пороховых газов
double Pmax = Data[25];
Console.WriteLine("Максимальное давление пороховых газов: " + Pmax);
// Предел прочности материала гильзы
double G0 = Data[26];
Console.WriteLine("Коэффициент линейного расширения материала гильзы: " + G0);
// Предел прочности на фланце
double G11 = Data[27];
Console.WriteLine("Предел прочности на фланце: " + G11);
// Предел прочности в донной части
double G22 = Data[28];
Console.WriteLine("Предел прочности в донной части: " + G22);
double delta0 = Data[30];
Console.WriteLine("Начальный зазор: " + delta0);
double ey = Data[31];
Console.WriteLine("Устойчивая деформация: " + ey);
Console.WriteLine();

Calculation.GilzaCalc(Dnar, Dosn, Dsk, dsk, Lkr, Lsk, Rcc, Scc, Lsm, dsm, R, dcc, Lcc, Sd, Lg, Kp, Ek, Eg, Kv, ftr, a, Tm, Tex, al, Pmax, G0, G11, G22, delta0, ey);

