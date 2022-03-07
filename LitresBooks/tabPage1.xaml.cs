// tabPage1

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Text;
using System.Threading.Tasks;
//using word = Microsoft.Office.Interop.Word;
//using excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;


// LitresBooks namespace
namespace LitresBooks
{
    //tabPage1 class
    public sealed partial class tabPage1 : Page
    {

        List<Book> books = new List<Book>();
        //BookContext db = new BookContext();
        List<Genre> genres = new List<Genre>();
        List<string> Columns = new List<string>() { "Название книги: ",
                                                    "Цена: ",
                                                    "Тип книги: ",
                                                    "Оценка LitRes: ",
                                                    "Издательство: ",
                                                    "Автор(ы): ",
                                                    "Жанры: ",
                                                    "Серия книг: ",
                                                    "Цитаты: ",
                                                    "Описание: "};
        Book selectedBook;
        int selectedColumn;
        int selectedData;
        int selectedColumn2add;


        public tabPage1()
        {
            this.InitializeComponent();

            /*
            using (MobileContext db = new MobileContext())
            {
                try
                {
                    foreach (var genre in db.Genre.OrderByDescending(q => q.Book.Count))
                    {
                        listBox4.Items.Add(genre.Name);
                        genres.Add(genre);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("get Genre.OrderByDescending Exception: " + ex.Message);
                }
            }
            */
        }//



        public async void button1_ClickAsync(object sender, RoutedEventArgs e)
        {
            //using (BooksContext db = new BooksContext())
            //{
            //    db.Database.ExecuteSqlCommand("Delete from Book");
            //}
            var parser = new Parser();
            string mainPage = "https://www.litres.ru/novie/";
            List<string> books = await parser.GetLinks(mainPage);
            //System.Diagnostics.Process.Start(mainPage);
            foreach (string bookUrl in books)
            {
                parser.Parse(bookUrl);
            }
            //parser.Parse(books[0]);

        }//end


        // button2_Click handler
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            using (MobileContext db = new MobileContext())
            {
                listBox1.Items.Clear();
                books.Clear();

                try
                {
                    foreach (var book in db.Book)
                    {
                        books.Add(book);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("foreach (var book in db.Book) Exception: " + ex.Message);
                }

                listBox1.Items.Clear();

                try
                {
                    foreach (var book in books)
                    {
                        listBox1.Items.Add(book.Name);
                    }
                }
                catch (Exception ex2)
                {
                    Debug.WriteLine("foreach (var book in books) Exception:" + ex2.Message);
                }
            }

        }//end


        //
        private void listBox1_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            Info2list2();
        }

        private void listBox2_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            selectedColumn = listBox2.SelectedIndex;
            if (selectedColumn > -1)
            {
                listBox3.Items.Clear();
                label6.Text = Columns[selectedColumn].Substring(0, Columns[selectedColumn].Length - 2);
                foreach (var item in Get_column(selectedColumn))
                    listBox3.Items.Add(item);
                if ((selectedColumn > -1 && selectedColumn < 5) || selectedColumn == 9)
                {
                    listBox3.SelectedIndex = 0;
                }
            }
        }

        private List<string> Get_column(int i)
        {
            List<string> columnInfo = new List<string>();
            switch (i)
            {
                case 0:
                    columnInfo.Add(selectedBook.Name);
                    break;
                case 1:
                    columnInfo.Add(selectedBook.Price.ToString());
                    break;
                case 2:
                    columnInfo.Add(selectedBook.Type);
                    break;
                case 3:
                    columnInfo.Add(selectedBook.LitresEstimate.ToString());
                    break;
                case 4:
                    columnInfo.Add(selectedBook.Publishing_house.Name);
                    break;
                case 5:
                    foreach (var item in selectedBook.Author)
                        columnInfo.Add(item.Name);
                    break;
                case 6:
                    //TODO
                    //foreach (var item in selectedBook.Genre)
                    //{
                    //    columnInfo.Add(item.Name);
                    //}
                    break;
                case 7:
                    foreach (var item in selectedBook.Series)
                        columnInfo.Add(item.Name);
                    break;
                case 8:
                    foreach (var item in selectedBook.Quote)
                        columnInfo.Add(item.QuoteText);
                    break;
                case 9:
                    columnInfo.Add(selectedBook.Description);
                    break;
            }
            return columnInfo;
        }

        private void listBox3_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";

            selectedData = listBox3.SelectedIndex;
            if (selectedData > -1)
            {
                label9.Text = listBox3.Items[selectedData].ToString();
                if ((selectedColumn > -1 && selectedColumn < 5) || selectedColumn == 9)
                {
                    textBox1.Text = listBox3.Items[selectedData].ToString();
                }
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (selectedColumn > 4 && selectedColumn < 9 && selectedData > -1)
            {
                switch (selectedColumn)
                {
                    case 5:
                        List<Author> newAuthors = new List<Author>();
                        foreach (var item in selectedBook.Author)
                            if (item.Name != label9.Text)
                            {
                                newAuthors.Add(item);
                            }
                        selectedBook.Author = newAuthors;
                        break;
                    case 6:
                        List<Genre> newGenre = new List<Genre>();
                        //TODO
                        //foreach (var item in selectedBook.Genre)
                        //{
                        //    if (item.Name != label9.Text)
                        //    {
                        //        newGenre.Add(item);
                        //    }
                        //}
                        //selectedBook.Genre = newGenre;
                        break;
                    case 7:
                        List<Series> newSeries = new List<Series>();
                        foreach (var item in selectedBook.Series)
                            if (item.Name != label9.Text)
                            {
                                newSeries.Add(item);
                            }
                        selectedBook.Series = newSeries;
                        break;
                    case 8:
                        List<Quote> newQuote = new List<Quote>();
                        foreach (var item in selectedBook.Quote)
                            if (item.QuoteText != label9.Text)
                            {
                                newQuote.Add(item);
                            }
                        selectedBook.Quote = newQuote;
                        break;
                }
            }
            else textBox2.Text = "Информацию о данной колонке нельзя удалять";
            Info2list2();
            listBox3.Items.Clear();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            string data2red = textBox1.Text;
            switch (selectedColumn)
            {
                case 0:
                    selectedBook.Name = data2red;
                    break;
                case 1:
                    selectedBook.Price = Convert.ToDouble(data2red);
                    break;
                case 2:
                    selectedBook.Type = data2red;
                    break;
                case 3:
                    selectedBook.LitresEstimate = Convert.ToDouble(data2red);
                    break;
                case 4:
                    selectedBook.Publishing_house.Name = data2red;
                    break;
                case 9:
                    selectedBook.Description = data2red;
                    break;
                default:
                    textBox2.Text = "В данной колонке можно только добавить/удалить запись";
                    break;
            }
            Info2list2();
            listBox3.Items.Clear();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            using (MobileContext db = new MobileContext())
            {
                if (selectedColumn > 4 && selectedColumn < 9 && textBox1.Text != "")
                {
                    switch (selectedColumn)
                    {
                        case 5:
                            List<Author> Authors = new List<Author>();
                            foreach (var item in selectedBook.Author)
                                Authors.Add(item);

                            Author a2ch = db.Author.FirstOrDefault(q => q.Name == textBox1.Text);
                            if (a2ch == null)
                            {
                                Author newAuthor = new Author();
                                newAuthor.AuthorID = 0;
                                newAuthor.Name = textBox1.Text;
                                a2ch = newAuthor;
                            }

                            //TODO
                            //a2ch.Book.Add(selectedBook);

                            Authors.Add(a2ch);
                            selectedBook.Author = Authors;
                            //DBSave();
                            break;
                        case 6:
                            Genre g2ch = db.Genre.FirstOrDefault(q => q.Name == textBox1.Text);
                            if (g2ch == null)
                            {
                                Genre newGenre = new Genre();
                                newGenre.GenreID = 0;
                                newGenre.Name = textBox1.Text;
                                g2ch = newGenre;
                            }
                            //TODO
                            //selectedBook.Genre.Add(g2ch);

                            g2ch.Book.Add(selectedBook);
                            DBSave();
                            break;
                        case 7:
                            Series s2ch = db.Series.FirstOrDefault(q => q.Name == textBox1.Text);
                            if (s2ch == null)
                            {
                                Series newSeries = new Series();
                                newSeries.SeriesID = 0;
                                newSeries.Name = textBox1.Text;
                                s2ch = newSeries;
                            }
                            selectedBook.Series.Add(s2ch);
                            s2ch.Book.Add(selectedBook);
                            DBSave();
                            break;
                        case 8:
                            Quote newQuote = new Quote();
                            newQuote.QuoteID = 0;
                            newQuote.QuoteText = textBox1.Text;
                            selectedBook.Quote.Add(newQuote);
                            DBSave();
                            break;
                    }
                }
                else textBox2.Text = "Информацию к данной колонке нельзя добавить";
                Info2list2();
                listBox3.Items.Clear();
            }

        }//end

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            DBSave();
        }

        private void DBSave()
        {
            using (MobileContext db = new MobileContext())
            {
                //db.Book.Remove(db.Book.FirstOrDefault(q => q.ID == id));
                //db.SaveChanges();
                //db.Book.Add(selectedBook);
                db.SaveChanges();
                int selectedBookIndex = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(selectedBookIndex);
                listBox1.Items.Insert(selectedBookIndex, selectedBook.Name);
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                textBox1.Text = "";
                textBox2.Text = "Сохранение прошло успешно";
                label2.Text = "";
                label6.Text = "";
                label9.Text = "";
            }
        }//end

        public void button7_Click(object sender, RoutedEventArgs e)
        {
            using (MobileContext db = new MobileContext())
            {
                try
                {
                    Book newBook = new Book();
                    if (db.Book.Count() != 0)
                        newBook.ID = db.Book.Max(q => q.ID) + 1;
                    newBook.Name = textBox3.Text;
                    newBook.Price = Convert.ToDouble(textBox9.Text);
                    newBook.Type = textBox5.Text;
                    newBook.LitresEstimate = Convert.ToDouble(textBox7.Text);
                    newBook.Description = textBox12.Text;
                    newBook.Actual_date = DateTime.Now.Date;
                    string[] words = textBox4.Text.Split(';');
                    foreach (var w2ch in words)
                    {
                        string word = w2ch.Trim();
                        Author item = db.Author.FirstOrDefault(q => q.Name == word);
                        if (item == null)
                        {
                            Author newItem = new Author();
                            newItem.Name = word;
                            item = newItem;
                        }
                        newBook.Author.Add(item);
                    }
                    words = textBox10.Text.Split(';');
                    foreach (var w2ch in words)
                    {
                        string word = w2ch.Trim();
                        Genre item = db.Genre.FirstOrDefault(q => q.Name == word);
                        if (item == null)
                        {
                            Genre newItem = new Genre();
                            newItem.Name = word;
                            item = newItem;
                        }
                        //TODO
                        //newBook.Genre.Add(item);
                    }
                    words = textBox6.Text.Split(';');
                    foreach (var w2ch in words)
                    {
                        string word = w2ch.Trim();
                        Series item = db.Series.FirstOrDefault(q => q.Name == word);
                        if (item == null)
                        {
                            Series newItem = new Series();
                            newItem.Name = word;
                            item = newItem;
                        }
                        newBook.Series.Add(item);
                    }
                    words = textBox11.Text.Split(';');
                    foreach (var w2ch in words)
                    {
                        string word = w2ch.Trim();
                        Quote item = db.Quote.FirstOrDefault(q => q.QuoteText == word);
                        if (item == null)
                        {
                            Quote newItem = new Quote();
                            newItem.QuoteText = word;
                            item = newItem;
                        }
                        newBook.Quote.Add(item);
                    }

                    string PH = textBox8.Text.Trim();
                    Publishing_house ph2ch = db.Publishing_house.FirstOrDefault(q => q.Name == PH);
                    if (ph2ch == null)
                    {
                        Publishing_house newItem = new Publishing_house();
                        newItem.Name = PH;
                        db.Publishing_house.Add(newItem);
                        ph2ch = newItem;
                    }
                    ph2ch.Book.Add(newBook);
                    db.Book.Add(newBook);
                    DBSave();
                }
                catch
                {
                    label17.Text = "Некорректные данные, \nпопробуйте снова";
                }
            }
        }//end


        // Delete from DB
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            using (MobileContext db = new MobileContext())
            {
                if (listBox1.SelectedIndex > -1)
                {
                    int id = Convert.ToInt32(label2.Text);
                    int selectedBookIndex = listBox1.SelectedIndex;
                    db.Book.Remove(db.Book.FirstOrDefault(q => q.ID == id));
                    DBSave();
                    books.RemoveAt(selectedBookIndex);
                    listBox1.Items.RemoveAt(selectedBookIndex);
                    label24.Text = "Удаление произведено успешно";
                }
            }
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
        }

        private void Info2list2()
        {
            if (listBox1.SelectedIndex > -1)
            {
                listBox2.Items.Clear();
                selectedBook = books[listBox1.SelectedIndex];
                label2.Text = selectedBook.ID.ToString();
                var authors = "  ";
                foreach (var auth in selectedBook.Author)
                    authors += auth.Name + ", ";
                
                var genres = "  ";
                //TODO
                //foreach (var gen in selectedBook.Genre)
                //{
                //    genres += gen.Name + ", ";
                //}
                
                var series = "  ";
                foreach (var ser in selectedBook.Series)
                    series += ser.Name + ", ";
                var quotes = "  ";
                foreach (var quo in selectedBook.Quote)
                    quotes += quo.QuoteText + ", ";

                listBox2.Items.Add(Columns[0] + selectedBook.Name);
                listBox2.Items.Add(Columns[1] + selectedBook.Price);
                listBox2.Items.Add(Columns[2] + selectedBook.Type);
                listBox2.Items.Add(Columns[3] + selectedBook.LitresEstimate);
                if (selectedBook.Publishing_house != null)
                    listBox2.Items.Add(Columns[4] + selectedBook.Publishing_house.Name);
                listBox2.Items.Add(Columns[5] + authors.Substring(0, authors.Length - 2));
                listBox2.Items.Add(Columns[6] + genres.Substring(0, genres.Length - 2));
                listBox2.Items.Add(Columns[7] + series.Substring(0, series.Length - 2));
                listBox2.Items.Add(Columns[8] + quotes.Substring(0, quotes.Length - 2));
                listBox2.Items.Add(Columns[9] + selectedBook.Description);


                label23.Text = selectedBook.Name;
                label24.Text = "";
            }
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            Genre selectedGenre = genres[listBox4.SelectedIndex];
            List<Book> booksOfGenres = new List<Book>();
            foreach (var bookOfGenre in selectedGenre.Book.OrderByDescending(q => q.LitresEstimate))
            {
                booksOfGenres.Add(bookOfGenre);
            }
            if (booksOfGenres.Count() == 0)
            {
                textBox1.Text = "Нет книг с данным жанром";
            }
            else
            {
                string path = @"C:\Users\acer\Desktop\Учеба\АИС\Отчет по жанру.doc";
                WordReportTopBookOfGenre wordReport = new WordReportTopBookOfGenre(path);
                wordReport.GenerateReportTopBooksOfG(selectedGenre.Name, booksOfGenres);
            }
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, double> genreEstimateDict = new Dictionary<string, double>();
            foreach (var genre in genres)
            {
                if (genre.Book.Count > 2)
                {
                    double raitingSum = 0;
                    int bookCounter = 0;
                    foreach (var bookOfGenre in genre.Book)
                    {
                        raitingSum += bookOfGenre.LitresEstimate;
                        bookCounter++;
                    }
                    double genreEstimate = 0;
                    if (bookCounter != 0)
                    {
                        genreEstimate = raitingSum / bookCounter;
                    }
                    if (!genreEstimateDict.ContainsKey(genre.Name))
                    {
                        genreEstimateDict.Add(genre.Name, Math.Round(genreEstimate, 2));
                    }
                }
            }

            GenerateReportTopGenres(genreEstimateDict);
        }

        private void listBox4_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            textBox13.Text = "";
        }

        private void GenerateReportTopGenres(Dictionary<string, double> dictionary)
        {
            /*
            excel.Application excelApp = new excel.Application();
            excel.Workbook workbook;
            excel.Worksheet workSheet;

            workbook = excelApp.Workbooks.Add();
            //workSheet = (excel.Worksheet)workbook.Worksheets.get_Item(1);
            workSheet = (excel.Worksheet)workbook.ActiveSheet;

            int i = 1;
            foreach (var word in dictionary.OrderByDescending(q => q.Value))
            {
                workSheet.Cells[1, i] = word.Key;
                workSheet.Cells[2, i] = word.Value;
                i++;
            }

            excel.Range Erange = workSheet.Range["B3"];
            Erange.Formula = "=SUM(A2:J2)";
            Erange.FormulaHidden = false;

            excel.Borders border = Erange.Borders;
            border.LineStyle = excel.XlLineStyle.xlContinuous;

            excel.ChartObjects chObs = (excel.ChartObjects)workSheet.ChartObjects();
            excel.ChartObject chOb = chObs.Add(5, 50, 300, 300);
            excel.Chart xlchart = chOb.Chart;
            excel.Range Erange2 = workSheet.Range["A1:J1"];
            excel.Range Erange3 = workSheet.Range["A3:J1"];

            xlchart.ChartType = excel.XlChartType.xlColumnClustered;

            excel.SeriesCollection seriesCollection = (excel.SeriesCollection)xlchart.SeriesCollection(Type.Missing);

            excel.Series series = seriesCollection.NewSeries();
            //series.XValues = workSheet.Range["A1:J1"];
            string[] matrix = new string[10];
            for (int j = 0; j < 10; j++)
                matrix[j] = (string)(workSheet.Cells[1, j + 1] as excel.Range).Value;
            //series.XValues = workSheet.Range[workSheet.Cells[1, 3]];
            series.XValues = matrix;
            series.Values = workSheet.get_Range("A2", "J2");

            xlchart.HasTitle = true;
            xlchart.ChartTitle.Text = "Жанры и их рейтинги";

            xlchart.HasLegend = true;
            series.Name = "Жанры";

            excelApp.Visible = true;
            excelApp.UserControl = true;

            string outputPath = @"C:\Users\acer\Desktop\Учеба\АИС\Отчет по рейтингу жанров (" + Guid.NewGuid().ToString() + ").xlsx";
            workbook.SaveAs(outputPath);

            object misValue = System.Reflection.Missing.Value;
            xlchart.Export("C:\\Users\\acer\\Desktop\\Учеба\\АИС\\Graf.bmp", "BMP", misValue);

            string path = @"C:\Users\acer\Desktop\Учеба\АИС\Отчет по рейтингу жанров.doc";
            WordReportTopBookOfGenre wordReport = new WordReportTopBookOfGenre(path);
            wordReport.GenerateReportTopGenres(dictionary);

            excelApp.Quit();
            */
        }

        public class WordReportTopBookOfGenre
        {
            
            //public word.Application wordapp = new word.Application();
            //public word.Documents worddocuments;
            //public word.Document worddocument;
            //static string path = @"C:\Users\acer\Desktop\Учеба\АИС\Отчет по жанру.doc";

            public WordReportTopBookOfGenre(string pathFrom)
            {
                /*
                path = pathFrom;
                wordapp = new word.Application();
                wordapp.Visible = true;

                object newTemplate = false;
                object documentType = word.WdNewDocumentType.wdNewBlankDocument;
                object visible = true;

                wordapp.Documents.Add(path, newTemplate, ref documentType, ref visible);

                worddocuments = wordapp.Documents;
                worddocument = worddocuments.get_Item(1);
                //wordapp.Visible = false;
                worddocument.Activate();
                */
            }

            public void GenerateReportTopBooksOfG(string genreName, List<Book> booksOfGenre)
            {
                /*
                string[] header = new string[3] { "Название книги", "Оценка", "Описание" };
                TableCreateBook(booksOfGenre.Count, 3, header, booksOfGenre);
                Replace("{ЖАНР}", genreName.ToUpper());
                string fileName = "Отчет по жанру(" + genreName + ")";
                TrySave(fileName);
                appClose();
                */
            }

            public void GenerateReportTopGenres(Dictionary<string, double> dictionary)
            {
                /*
                string[] header = new string[3] { "№", "Название жанра", "Рейтинг" };
                TableCreateGenre(dictionary.Count, 3, header, dictionary);

                string fileName = "Отчет по рейтингу жанров(" + Guid.NewGuid().ToString() + ")";
                TrySave(fileName);
                appClose();
                */
            }

            public void Replace(string wordr, string replacement)
            {
                /*
                word.Range range = worddocument.StoryRanges[word.WdStoryType.wdMainTextStory];
                range.Find.ClearFormatting();

                range.Find.Execute(FindText: wordr, ReplaceWith: replacement);

                //TrySave();
                */
            }

            public void TrySave(string fileName)
            {
                /*
                try
                {
                    string outputPath = @"C:\Users\acer\Desktop\Учеба\АИС\" + fileName + ".doc";
                    worddocument.SaveAs(outputPath, word.WdSaveFormat.wdFormatDocument);
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                */
            }

            public void TableCreateBook(int row, int col, string[] header, List<Book> books)
            {
                /*
                Object start = 94;
                Object end = 95;
                word.Range wordrange = worddocument.Range(ref start, ref end);
                //wordrange.InsertBreak(word.WdBreakType.wdPageBreak);
                wordrange.Bold = 400;
                Object defaultTableBehavior = word.WdDefaultTableBehavior.wdWord9TableBehavior;
                Object autoFitBehavior = word.WdAutoFitBehavior.wdAutoFitWindow;
                word.Table wordtable = worddocument.Tables.Add(wordrange, row + 1, col, ref defaultTableBehavior, ref autoFitBehavior);

                for (int c = 1; c <= col; c++)
                {
                    word.Range wordcellrange = worddocument.Tables[1].Cell(1, c).Range;
                    wordcellrange.Text = header[c - 1];
                }

                for (int i = 0; i < row; i++)
                {
                    word.Range wordcellrange = worddocument.Tables[1].Cell(i + 2, 1).Range;
                    wordcellrange.Text = $"{books[i].Name} ({books[i].Type})";
                    wordcellrange = worddocument.Tables[1].Cell(i + 2, 2).Range;
                    wordcellrange.Text = books[i].LitresEstimate.ToString();
                    wordcellrange = worddocument.Tables[1].Cell(i + 2, 3).Range;
                    wordcellrange.Text = books[i].Description;
                }
                wordtable.AllowAutoFit = true;
                word.Column firstCol = wordtable.Columns[2];
                firstCol.AutoFit();
                Single firstColAutoWidth = firstCol.Width;
                wordtable.AutoFitBehavior(word.WdAutoFitBehavior.wdAutoFitWindow);
                firstCol.SetWidth(firstColAutoWidth, word.WdRulerStyle.wdAdjustFirstColumn);
                */
            }

            public void TableCreateGenre(int row, int col, string[] header, Dictionary<string, double> dictionary)
            {
                /*
                Object start = 80;
                Object end = 81;
                word.Range wordrange = worddocument.Range(ref start, ref end);
                //wordrange.InsertBreak(word.WdBreakType.wdPageBreak);
                wordrange.Bold = 400;
                Object defaultTableBehavior = word.WdDefaultTableBehavior.wdWord9TableBehavior;
                Object autoFitBehavior = word.WdAutoFitBehavior.wdAutoFitWindow;
                word.Table wordtable = worddocument.Tables.Add(wordrange, row + 1, col, ref defaultTableBehavior, ref autoFitBehavior);

                for (int c = 1; c <= col; c++)
                {
                    word.Range wordcellrange = worddocument.Tables[1].Cell(1, c).Range;
                    wordcellrange.Text = header[c - 1];
                }

                int cellCounter = 2;
                foreach (var genre in dictionary)
                {
                    word.Range wordcellrange = worddocument.Tables[1].Cell(cellCounter, 1).Range;
                    wordcellrange.Text = (cellCounter - 1).ToString();
                    wordcellrange = worddocument.Tables[1].Cell(cellCounter, 2).Range;
                    wordcellrange.Text = genre.Key;
                    wordcellrange = worddocument.Tables[1].Cell(cellCounter, 3).Range;
                    wordcellrange.Text = genre.Value.ToString();
                    cellCounter++;
                }

                wordtable.AllowAutoFit = true;
                word.Column firstCol = wordtable.Columns[1];
                firstCol.AutoFit();
                Single firstColAutoWidth = firstCol.Width;
                wordtable.AutoFitBehavior(word.WdAutoFitBehavior.wdAutoFitWindow);
                firstCol.SetWidth(firstColAutoWidth, word.WdRulerStyle.wdAdjustFirstColumn);

                object rEnd = worddocument.Content.End;
                int rt = (int)rEnd;
                object rStart = rt - 1;
                word.Range picRange = worddocument.Range(ref rStart, ref rEnd);

                picRange.InlineShapes.AddPicture(@"C:\Users\acer\Desktop\Учеба\АИС\Graf.bmp");
                */
            }

            public void appClose()
            {
                /*
                object saveChanges = word.WdSaveOptions.wdPromptToSaveChanges;
                object originalFormat = word.WdOriginalFormat.wdWordDocument;
                object routeDocument = Type.Missing;
                wordapp.Quit(ref saveChanges, ref originalFormat, ref routeDocument);
                */
            }
        }

    }//class end

}








