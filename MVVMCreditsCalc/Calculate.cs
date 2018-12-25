using GalaSoft.MvvmLight;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVVMCreditsCalc
{

    public class Calculate : ViewModelBase, INotifyPropertyChanged
    {
        #region Variables
        double sumCalculate;
        private double sumConst;
        private DateTime dataZaim;
        int srok;

        private Dictionary<string, int> sroks = new Dictionary<string, int>()
        {
            ["3 месяца"] =3,
            ["6 месяцев"] =6,
            ["9 месяцев" ]= 9,
            ["1 год"] =12,
            ["1 год и 3 месяца"] =15,
            ["1 год и 6 месяцев"] =18,
            ["2 года"] =24,
            ["3 года"] =36,
            ["5 лет"] =60
        };
        
        public DateTime n;
        double zadoljennostKred;
        double nachislPrc;
        double osnDolg;
        double summPlatezh;
        double procent;

      
        //текстовое представление переменной
        //для форматированного вывода даты займа
        private string dz;
        ObservableCollection<Calculate> listCalculations = new ObservableCollection<Calculate>();
        private bool tfDiff;
        private bool tfAnnuit;
        private string cbSrokTmp;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public Calculate()
        {
            dataZaim=DateTime.Now;
            dz = DateTime.Now.ToString("dd.MM.yyyy");
        }
        public Calculate(DateTime d, double zadoljennost_kred, double summ_platezh, double osn_dolg, double nachisl_procent)
        {
            zadoljennostKred = zadoljennost_kred;
            summPlatezh = summ_platezh;
            osnDolg = osn_dolg;
            nachislPrc = nachisl_procent;
            dz = d.Date.ToString("dd.MM.yyyy");
            dataZaim = d;
        }



        #region Property
        //Свойство вывода на View и форматирования даты займа
        public string Dz
        {
            get { return dz; }
            set
            {
                Set(ref dz, value);
                RaisePropertyChanged(nameof(Dz));
            }
        }
        public Dictionary<string, int> Sroks
        {
            get{return sroks;}
            set {
                Set(ref sroks, value);
                RaisePropertyChanged(nameof(Sroks));
            }
        }
        public double NachislPrc
        {
            get { return nachislPrc; }
        }
        public double OsnDolg
        {
            get { return osnDolg; }
        }
        public double Zadolzh
        {
            get { return zadoljennostKred; }
        }
        public double SumPlat
        {
            get { return summPlatezh; }
            set { summPlatezh = value; }
        }
        public double Prc
        {
            set
            {
                Set(ref procent, value);
                RaisePropertyChanged(nameof(Prc));
            }
            get { return procent; }
        }

        //public int Srok
        //{
        //    set { srok = value; }
        //    get { return srok; }
        //}
       
        public double Sum
        {
            set
            {
                Set(ref sumCalculate, value);
                RaisePropertyChanged(nameof(Sum));
            }
            get { return sumCalculate; }
        
        }
        public DateTime DataZaima
        {
            set
            {
                Set(ref dataZaim, value);
                RaisePropertyChanged(nameof(DataZaima));
            }
            get { return dataZaim; }
        }
        public ObservableCollection<Calculate> Graph
        {
            set { listCalculations = value; }
            get { return listCalculations; }
        }
        public string CbSrokTmp
        {
            set { cbSrokTmp = value; }
            get { return cbSrokTmp; }
        }
        public bool TrueFalseDiff
        {
            get { return tfDiff; }
            set {
                Set(ref tfDiff, value);
                RaisePropertyChanged(nameof(TrueFalseDiff));
            }
        }
        public bool TrueFalseAnnuit
        {
            get { return tfAnnuit; }
            set {
                Set(ref tfAnnuit, value);
                RaisePropertyChanged(nameof(TrueFalseAnnuit));
            }
        }





        #endregion
        /// <summary>
        /// Метод определяющий какой тип выплат был использован в рассчете
        /// </summary>
        /// <returns></returns>
        private string TypePayToOpenSave()
        {
            if (tfAnnuit)
                return "Ануитентный";
            else return "Дифференцированный";
        }

        private void CsvToTypePayCalculate(string param)
        {
            if (TypePayToOpenSave() == "Ануитентный")
                TrueFalseAnnuit = true;
            else TrueFalseDiff = true;
        }

        /// <summary>
        /// Метод передачи переменных из файла CSV для рассчета и передачи listCalculation в DataGrid
        /// </summary>
        /// <param name="s">Принимает параметр List<string> для передачи значений переменным класса Calculate</param>
        private void CsvToCalc(List<string> s)
        {
            Prc = Convert.ToDouble(Regex.Replace(s[0],@"[^\d,. ]+",""));
            DataZaima = Convert.ToDateTime(s[1]);
            cbSrokTmp = s[2];
            sumCalculate = Convert.ToDouble(Regex.Replace(s[3], @"[^\d,. ]+", ""));
            CsvToTypePayCalculate(s[4]);
        }

        /// <summary>
        /// Метод для сохранения рассчета в формат CSV
        /// </summary>
        public void Save()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = @"CSV-файл (*.csv)|*.csv";
            //счетчик строк
            int i=1;
            if (saveFile.ShowDialog() == true)
            {
                //открываем или создаем файл .csv для записи
                using (StreamWriter sw = new StreamWriter(saveFile.OpenFile(), Encoding.Default))
                {
                    //запись данных в определенном порядке
                    sw.WriteLine($"Процентная ставка:;{procent} %\n" +
                                 $"Дата займа:;{DataZaima.ToString("dd.MM.yyyy")}\n" +
                                 $"Срок погашения:;{cbSrokTmp}\n" +
                                 $"Сумма:;{sumConst}\n" +
                                 $"Тип выплат:;{TypePayToOpenSave()}\n");
                    sw.WriteLine("№;Дата платежа;Задолженность по кредиту;Сумма платежа;Основной долг;Начисленные проценты");
                 foreach (var cell in listCalculations)
                        {
                            sw.Write($"{i++};" +
                                     $"{cell.Dz};" +
                                     $"{cell.Zadolzh.ToString("##.##")};" +
                                     $"{cell.SumPlat.ToString("##.##")};" +
                                     $"{cell.OsnDolg.ToString("##.##")};" +
                                     $"{cell.NachislPrc.ToString("##.##")};");
                            sw.WriteLine();
                        }
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// Метод для определения текущей валюты рассчета
        /// используется при печати документа
        /// </summary>
        /// <returns></returns>
        private string CurrencyChange()
        {
            if (procent == 9)
                return "USD";
            else if (procent == 6)
                return "EUR";
            else return "Рубль";
        }

        /// <summary>
        /// Метод печати рассчета
        /// </summary>
        public void PrintPages()
        {
            //открываем диалоговое окно принтера
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                //нумератор строк
                int i = 1;
                //шапка документа
                TextBlock header = new TextBlock();
                header.Inlines.Add($"Процентная ставка: {procent}%\n" +
                                   $"Дата займа: {DataZaima.ToString("dd.MM.yyyy")}\n" +
                                   $"Валюта: {CurrencyChange()}\n" +
                                   $"Срок погашения: {cbSrokTmp}\n" +
                                   $"Сумма: {sumConst} {CurrencyChange()}\n" +
                                   $"Тип выплат: {TypePayToOpenSave()}");

                //таблица рассчета документа для печати
                DataTable dt = new DataTable();
                //DataSet ds=new DataSet();
                //создаем колонки
                dt.Columns.Add(new DataColumn("№", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Дата платежа", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Остаток", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Сумма платежа", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Основной платеж", System.Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Проценты", System.Type.GetType("System.String")));
               
                //заполняем сроки таблиц
                foreach (var cell in listCalculations)
                {
                    DataRow rowAdd = dt.NewRow();
                    rowAdd["№"] = i.ToString();
                    rowAdd["Дата платежа"] = cell.Dz;
                    rowAdd["Остаток"] = cell.Zadolzh.ToString("C");
                    rowAdd["Сумма платежа"] = cell.SumPlat.ToString("C");
                    rowAdd["Основной платеж"] = cell.OsnDolg.ToString("C");
                    rowAdd["Проценты"] = cell.NachislPrc.ToString("P");
                    dt.Rows.InsertAt(rowAdd, i++);
                }
                //создаем экземпляр класса StoreDataSetPaginator
                //для вывода на печать и рассчета печатаемых страниц
                StoreDataSetPaginator dsPaginator = new StoreDataSetPaginator(dt, header,
                    new Typeface("Calibri"), 14, 69 * 0.75,
                    new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));
                try
                {
                    printDialog.PrintDocument(dsPaginator, "printing...");
                }
                catch 
                {
                    MessageBox.Show("Пустой документ,\n составьте документ заново");
                }
                
            }

        }

        //public void PRINT()
        //{
        //    PrintDialog printDialog = new PrintDialog();
        //    if (printDialog.ShowDialog() == true)
        //    {
        //        // Создать визуальный элемент для страницы
        //        DrawingVisual visual = new DrawingVisual();

        //        // Получить контекст рисования
        //        using (DrawingContext dc = visual.RenderOpen())
        //        {
        //            DataGrid dg=new DataGrid();
        //            string s;
        //            TextBlock vis = new TextBlock();
        //            TextBlock header = new TextBlock();
        //            header.Inlines.Add($"Процентная ставка: { procent}%\n" +
        //                               $"Дата займа: {DataZaima}\n" +
        //                               $"Срок погашения: {cbSrokTmp}\n" +
        //                               $"Сумма: {sumConst} руб.\n" +
        //                               $"Тип выплат: {TypePayToOpenSave()}\n" +
        //                               "Дата платежа|Остаток|Сумма платежа" +
        //                               "|Основной долг|проценты");
        //            foreach (var cell in listCalculations)
        //            {
        //                s = $"  {cell.Dz}   |" +
        //                    $"  {cell.Zadolzh.ToString("##.##")}   | " +
        //                    $"  {cell.SumPlat.ToString("##.##")}    | " +
        //                    $"  {cell.OsnDolg.ToString("##.##")}     |  " +
        //                    $" {cell.NachislPrc.ToString("##.##")}|\n";
        //                vis.Inlines.Add(s);
        //            }


        //            // Определить текст, который необходимо печатать
        //            FormattedText text = new FormattedText(vis.Text,
        //                System.Globalization.CultureInfo.CurrentCulture,
        //                FlowDirection.LeftToRight,
        //                new Typeface("Calibri"), 20, Brushes.Black);
        //            FormattedText textHeader = new FormattedText(header.Text,
        //                System.Globalization.CultureInfo.CurrentCulture,
        //                FlowDirection.LeftToRight,
        //                new Typeface("Calibri"), 22, Brushes.Black);

        //            // Указать максимальную ширину, в пределах которой выполнять перенос текста, 
        //            text.MaxTextWidth = printDialog.PrintableAreaWidth;

        //            // Получить размер выводимого текста. 
        //            Size textSize = new Size(text.Width, text.Height);

        //            // Найти верхний левый угол, куда должен быть помещен текст. 
        //            double margin = 150 * 0.25;

        //            // Нарисовать содержимое, 
        //            int tmp = 200;
        //            dc.DrawText(text, new Point(margin + 10, tmp));
        //            dc.DrawText(textHeader, new Point(margin + 10, 50));


        //            dc.DrawLine(new Pen(Brushes.CadetBlue, 7), new Point(600, tmp), new Point(margin, tmp));
        //            for (int i = 0; i < listCalculations.Count; i++)
        //            {
        //                dc.DrawLine(new Pen(Brushes.Black, 1), new Point(600, tmp += 25), new Point(50, tmp));
        //            }
        //            // Добавить рамку (прямоугольник без фона). 
        //            dc.DrawRectangle(null, new Pen(Brushes.Black, 1),
        //                new Rect(margin, margin, printDialog.PrintableAreaWidth - margin * 2,
        //                    printDialog.PrintableAreaHeight - margin * 2));
        //        }

        //        // Напечатать визуальный элемент. 
        //        printDialog.PrintVisual(visual, "Печать с помощью классов визуального уровня");
        //    }
        //}

        /// <summary>
        /// Метод открытия документа Csv сохраненного ранее
        /// </summary>
        public void CalculateOpenCsv()
        {
          //создание спска типа string для дальнейшей передачи значений классу Calculate
            List<string> arr=new List<string>();
            //открытие диалогового окна
            OpenFileDialog ofd = new OpenFileDialog();
            //фильтр выбора документа по расширению
            ofd.Filter = @"CSV-файл (*.csv)|*.csv";
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    //открытие файлового текстового потоков
                    FileStream save = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(save, Encoding.Default);
                    //цикличное считывание строк из документа
                    do
                {
                    string[] s = sr.ReadLine().Split(';');
                        //выбор строк равным двум словам(шапка документа) для
                        //присвоения переменных и рассчета кредита
                    if (s.Length ==2)
                        arr.Add(s[1]);
                } while (!sr.EndOfStream);
                    //очистка предыдущих расчетов
                    listCalculations.Clear();
                    //присвоение переменных для рассчета
                    CsvToCalc(arr);
                    //метод рассчета кредита
                    CalculationClick();
                    sr.Close();
                }
                catch
                {
                    MessageBox.Show("Файл открыт другой программой\nили нарушен порядок выгруженного документа");
                }
            }
        }

        /// <summary>
        /// Метод для кнопки рассчета погашения кредита
        /// </summary>
        public void CalculationClick()
        {
            try
            {
                if (sumCalculate!= 0 && procent != 0)
                {
                    //сохраняем дату займа и сумму в дополнительные переменные
                    //для их неизменности
                    n=dataZaim;
                    sumConst = Sum;
                    listCalculations.Clear();
                    if (tfAnnuit == true || tfDiff == true)
                    {
                        //устанавливаем срок погашения в месяцах
                        srok = sroks[cbSrokTmp];
                        int a = srok - 1;
                        //вызов метода рассчета кредита
                        CalcCred(a);
                        Sum = sumConst;
                    }
                    else if (tfAnnuit == false || tfDiff == false)
                    {
                        MessageBox.Show("Выберете метод расчёта:");
                    }
                }
            }
            catch { MessageBox.Show("Заполните все поля!"); }
            //возврат даты займа
            dataZaim = n;            
        }

        /// <summary>
        /// Метод кнопки очистки данных для объекта, сбрасывает все поля на 0
        /// </summary>
        public void ClearClic()
        {
            Sum = 0;
            sumConst = 0;
            zadoljennostKred = 0;
            summPlatezh = 0;
            osnDolg = 0;
            nachislPrc = 0;
            Prc = 0;
            DataZaima = DateTime.Now;
            Dz = DateTime.Now.ToString("dd.MM.yyyy");
            listCalculations.Clear();

        }

        /// <summary>
        /// Метод рассчета кредита, принимает в качестве параметра число срока платежа,
        /// и выбирает нужный вариант рассчета кредита
        /// </summary>
        /// <param name="a">число срока рассчета платежа в месяцах</param>
        private void CalcCred(int a)
        {
            dataZaim = dataZaim.AddMonths(1);
            //определение типа рассчета кредита и последующий рассчет
            if (tfDiff == true)
            {
                for (int i = 0; i <= a; i++)
                {
                    //метод рассчета
                    RasschetCredidsDiffer();
                    //добавление в коллекцию
                    listCalculations.Add(new Calculate(dataZaim, zadoljennostKred, SumPlat, osnDolg, nachislPrc)); ;
                    //дополнительные рассчеты
                    dataZaim = dataZaim.AddMonths(1);
                    Sum = sumCalculate - osnDolg;
                    srok--;
                }
            }
            else if (tfAnnuit == true)
            {
                for (int i = 0; i <= a; i++)
                {
                    CalculateCreditsAnnuitentn();
                    listCalculations.Add(new Calculate(dataZaim, zadoljennostKred, SumPlat, osnDolg, nachislPrc));
                    dataZaim = dataZaim.AddMonths(1);
                    Sum = sumCalculate - osnDolg;
                    srok--;
                }
            }

            Dz = dataZaim.AddMonths(-1).ToString("dd.MM.yyyy");
        }



        /// <summary>
        /// Метод аннуитентного рассчета кредита
        /// </summary>
        private void CalculateCreditsAnnuitentn()
        {
            summPlatezh = sumCalculate * ((procent / 100.00 / 12.00) / (1 - Math.Pow((1 + (procent / 100.00 / 12.00)), srok - (srok * 2))));
            nachislPrc = (procent / 12) * sumCalculate / 100;
            osnDolg = summPlatezh - nachislPrc;
            zadoljennostKred = sumCalculate;
        }

        /// <summary>
        /// Метод дифференциального рассчета кредита
        /// </summary>
        private void RasschetCredidsDiffer()
        {
            osnDolg = sumCalculate / srok;
            nachislPrc = (procent / 12) * sumCalculate / 100;
            summPlatezh = nachislPrc + osnDolg;
            zadoljennostKred = sumCalculate;

        }
        
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
