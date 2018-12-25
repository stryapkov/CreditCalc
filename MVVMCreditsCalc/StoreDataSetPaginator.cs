using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace MVVMCreditsCalc
{
    public class StoreDataSetPaginator : DocumentPaginator
    {
        private int swapDraw170 = 170;
        private DataTable dt;
        private Typeface typeface;
        private double fontSize;
        private double margin;
        private Size pageSize;
        private TextBlock tb;
        private delegate void DrawLine(Pen pen,Point point0,Point point1);
        public override Size PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                PaginateData();
            }
        }

        public StoreDataSetPaginator(DataTable dt,TextBlock tb, Typeface typeface, double fontSize, double margin, Size pageSize)
        {
            this.dt = dt;
            this.typeface = typeface;
            this.fontSize = fontSize;
            this.margin = margin;
            this.pageSize = pageSize;
            this.tb = tb;
            PaginateData();
        }

        /// <summary>
        /// ///////////////////////
        /// </summary>

        private int pageCount;

        private int rowsPerPage;

        private void PaginateData()
        {
            double a = 1;
            if (GetFormattedText(dt.Rows[0]["Остаток"].ToString()).Width > 137.7)
            {a = 1.8; }
            // Создать тестовую строку для измерения
            FormattedText text = GetFormattedText("A");

            // Подсчитать строки, которые умещаются на странице
            rowsPerPage = (int) ((pageSize.Height - margin * 2) /(text.Height*a));

            // Оставить строку для заголовка 
            rowsPerPage -= 10;

            pageCount = (int) Math.Ceiling((double) dt.Rows.Count / rowsPerPage);
        }
  

        private FormattedText GetFormattedText(string text)
        {
            return GetFormattedText(text, typeface);
        }

        private FormattedText GetFormattedText(string text, Typeface typeface)
        {
            return new FormattedText(
                text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                typeface, fontSize, Brushes.Black);
        }
        private FormattedText GetFormattedText(string text, Typeface typeface, double fontSizeHead)
        {
            return new FormattedText(
                text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                typeface, fontSizeHead, Brushes.Black);
        }



        // Всегда возвращает true, потому что количество страниц обновляется
        // немедленно и синхронно, когда изменяется размер страницы. 
        // Никогда не находится в неопределенном состоянии
        public override bool IsPageCountValid
        {
            get { return true; }
        }

        public override int PageCount
        {
            get { return pageCount; }
        }

        public override IDocumentPaginatorSource Source
        {
            get { return null; }
        }


        public override DocumentPage GetPage(int pageNumber)
        {
            // Создать тестовую строку для измерения
            FormattedText text = GetFormattedText("A");

            // Размеры столбцов относительно ширины символа "A"
            double[] col = new double[6];
            col[0]=  margin;
            col[1]=col[0] + text.Width * 3;
            col[2]=col[1] + text.Width * 13;
            col[3]=col[2] + text.Width * 17;
            col[4]=col[3] + text.Width * 16;
            col[5]=col[4] + text.Width * 18;
        

            // Вычислить диапазон строк, которые попадают в эту страницу

            int minRow = pageNumber * rowsPerPage;
            int maxRow = minRow + rowsPerPage;
         

            // Создать визуальный элемент для страницы
            DrawingVisual visual = new DrawingVisual();

            // Установить позицию в верхний левый угол печатаемой области
            Point point = new Point(margin, margin+swapDraw170);

            using (DrawingContext dc = visual.RenderOpen())
            {
                // Нарисовать заголовки столбцов
                #region Нарисовать заголовки столбцов
                Typeface columnHeaderTypeface = new Typeface(typeface.FontFamily, FontStyles.Normal, FontWeights.Bold,
                    FontStretches.Normal);
                point.X = col[0];
                text = GetFormattedText("№", columnHeaderTypeface, 16);
                dc.DrawText(text, point);
                point.X=col[1];
                text = GetFormattedText("Дата платежа", columnHeaderTypeface,16);
                dc.DrawText(text, point);
                text = GetFormattedText("Остаток", columnHeaderTypeface, 16);
                point.X = col[2];
                dc.DrawText(text, point);
                text = GetFormattedText("Сумма платежа", columnHeaderTypeface, 16);
                point.X = col[3];
                dc.DrawText(text, point);
                text = GetFormattedText("Основной платеж", columnHeaderTypeface, 16);
                point.X = col[4];
                dc.DrawText(text, point);
                text = GetFormattedText("Проценты", columnHeaderTypeface, 16);
                point.X = col[5];
                dc.DrawText(text, point);
                
                dc.DrawText((GetFormattedText(tb.Text, new Typeface("Calibri"),18)), 
                    new Point(margin + 10, 50));
                
                // Нарисовать линию подчеркивания
                dc.DrawLine(new Pen(Brushes.Black, 2),
                    new Point(margin, margin + text.Height+swapDraw170),
                    new Point(pageSize.Width - margin, margin + text.Height+swapDraw170));
                dc.DrawLine(new Pen(Brushes.AliceBlue, 10),
                    new Point(margin, margin + text.Height + 140),
                    new Point(pageSize.Width - margin, margin + text.Height + 140));

                point.Y += text.Height;
#endregion
                // Нарисовать значения столбцов
                for (int i = minRow; i < maxRow; i++)
                {
                    // Проверить конец последней (частично заполненной) страницы
                    if (i > (dt.Rows.Count - 1)) break;

                    point.X = col[0];
                    text = GetFormattedText(dt.Rows[i]["№"].ToString());
                    dc.DrawText(text, point);

                    point.X = col[1];
                    text = GetFormattedText(dt.Rows[i]["Дата платежа"].ToString());
                    text.MaxTextWidth = col[2] - col[1];
                    //dc.DrawLine(new Pen(Brushes.Gray, 1),
                    //    new Point(point.X, point.Y),
                    //    new Point(point.X, point.Y + text.Height));
                    dc.DrawText(text, point);

                    // Добавить второй столбец                    
                    text = GetFormattedText(dt.Rows[i]["Остаток"].ToString());
                    point.X = col[2];
                    text.MaxTextWidth = col[3] - col[2];
                   //dc.DrawLine(new Pen(Brushes.Gray, 1),
                    //    new Point(point.X, point.Y),
                    //    new Point(point.X, point.Y + text.Height));
                    dc.DrawText(text, point);


                    text = GetFormattedText(dt.Rows[i]["Сумма платежа"].ToString());
                    point.X = col[3];
                    text.MaxTextWidth = col[4] - col[3];
                    //dc.DrawLine(new Pen(Brushes.Gray, 1),
                    //    new Point(point.X - 5, point.Y),
                    //    new Point(point.X - 5, point.Y + text.Height));
                    dc.DrawText(text, point);

                    text = GetFormattedText(dt.Rows[i]["Основной платеж"].ToString());
                    point.X = col[4];
                    text.MaxTextWidth = col[5] - col[4];
                    //dc.DrawLine(new Pen(Brushes.Gray, 1),
                    //    new Point(point.X - 5, point.Y),
                    //    new Point(point.X - 5, point.Y + text.Height));
                    dc.DrawText(text, point);


                    text = GetFormattedText(dt.Rows[i]["Проценты"].ToString());
                    point.X = col[5];
                    //dc.DrawLine(new Pen(Brushes.Gray, 1),
                    //    new Point(point.X - 5, point.Y),
                    //    new Point(point.X - 5, point.Y + text.Height));
                    dc.DrawText(text, point);



                    if (GetFormattedText(dt.Rows[i]["Остаток"].ToString()).Width > (col[3] - col[2])|
                        GetFormattedText(dt.Rows[i]["Сумма платежа"].ToString()).Width > (col[4] - col[3]))
                    {
                        point.Y += text.Height * 2;
                    }
                    else point.Y += text.Height;
                    
                  

                   //painting line rows
                    dc.DrawLine(new Pen(Brushes.Gray, 1),
                        new Point(margin, point.Y),
                        new Point(pageSize.Width - margin, point.Y));
                   

                }
                for (int i=1;i<dt.Columns.Count;i++)
                {
                    dc.DrawLine(new Pen(Brushes.Gray, 1),
                        new Point(col[i], margin + text.Height + swapDraw170),
                        new Point(col[i], point.Y));
                }
            }
            

            return new DocumentPage(visual, pageSize, new Rect(pageSize), new Rect(pageSize));
        }
    }
}
