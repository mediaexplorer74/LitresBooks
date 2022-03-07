using LitresBooks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
//using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace LitresBooks
{
    class Parser
    {
        // Parse
        public async void Parse(string url)
        {
            IConfiguration config = Configuration.Default.WithDefaultLoader();
            IBrowsingContext context = BrowsingContext.New(config);
            IDocument doc = null;
            //await context.OpenAsync(url);

            try
            {
                doc = await context.OpenAsync(url);
            }
            catch (Exception ex1)
            {
                Debug.WriteLine("context.OpenAsync(url) Exception: " + ex1.Message);
            }


            int id = new int();
            BookContext db = new BookContext();
            List<Book> booklist = new List<Book>();

            Book bookModel = new Book();

            try
            {
                var preId = doc.QuerySelector("div.biblio_book_cover[id^=biblio_book_cover_]").Id;
                preId = preId.ToString().Substring(18);
                id = Convert.ToInt32(preId);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine("doc.QuerySelector Exception: " + ex2.Message);
            }

            try
            {
                Book bm2ch = db.Book.FirstOrDefault(i => i.ID == id);

                if (bm2ch == null)
                {
                    bookModel.ID = id;


                    IEnumerable<IElement> name_type = doc.All.Where(text =>
                        text.LocalName == "h1"
                        && text.ParentElement.LocalName == "div"
                        && text.ParentElement.ClassList.Contains("biblio_book_name")
                        );

                    string type = name_type.ToList()[0].FirstElementChild.TextContent;
                    string name = name_type.ToList()[0].TextContent;
                    name = name.Substring(0, name.Length - type.Length);

                    bookModel.Name = name;
                    bookModel.Type = type;

                    IElement description = doc.All.FirstOrDefault(desc =>
                        desc.LocalName == "p"
                        && desc.ParentElement.LocalName == "div"
                        && desc.ParentElement.ClassList.Contains("biblio_book_descr_publishers")
                        );

                    if (description != null) bookModel.Description = description.TextContent;
                    else bookModel.Description = "Нет описания";

                    IElement price = doc.All.FirstOrDefault(rub =>
                        rub.LocalName == "span"
                        && rub.ClassList.Contains("simple-price")
                        );

                    if (price != null)
                        bookModel.Price = Convert.ToDouble(price.TextContent.Substring(0, price.TextContent.Length - 2));
                    else bookModel.Price = 0;

                    IElement estimate = doc.All.FirstOrDefault(est =>
                        est.LocalName == "div"
                        && est.ClassList.Contains("rating-number")
                        );

                    bookModel.LitresEstimate = Convert.ToDouble(estimate.TextContent);

                    IElement publish = doc.All.FirstOrDefault(pub =>
                        pub.LocalName == "li"
                        && pub.ParentElement.LocalName == "ul"
                        && pub.ParentElement.ClassList.Contains("biblio_book_info_detailed_right")
                        && pub.FirstChild.TextContent.Contains("Правообладатель:")
                        );
                    string pubHome;
                    if (publish.ChildElementCount == 0 || publish.ChildElementCount == 1)
                        pubHome = publish.TextContent.Substring(17);
                    else
                        pubHome = publish.Children.ToList()[1].TextContent;

                    Publishing_house PH = db.Publishing_house.FirstOrDefault(ph =>
                        ph.Name == pubHome
                        );

                    if (PH == null)
                    {
                        Publishing_house newPH = new Publishing_house();
                        newPH.Name = pubHome;
                        newPH.PH_ID = 1;
                        db.Publishing_house.Add(newPH);
                        PH = newPH;
                        db.Publishing_house.Add(PH);
                    }

                    bookModel.Publishing_house = PH;
                    bookModel.PH_ID = PH.PH_ID;

                    IEnumerable<IElement> quotes = doc.All.Where(quo =>
                        quo.LocalName == "div"
                        && quo.ClassList.Contains("quote__text")
                        );

                    foreach (var qu in quotes)
                    {
                        Quote newQuote = new Quote();
                        newQuote.QuoteText = qu.TextContent;
                        newQuote.QuoteID = 1;
                    newQuote.BookID = id;
                        db.Quote.Add(newQuote);
                        bookModel.Quote.Add(newQuote);
                    }

                    IEnumerable<IElement> authors = doc.All.Where(auth =>
                        auth.LocalName == "a"
                        && auth.ClassList.Contains("biblio_book_author__link")
                        && auth.ParentElement.ClassList.Contains("biblio_book_author")
                        );

                    foreach (var a in authors)
                    {
                        Author newAuthor = new Author();
                        newAuthor.AuthorID = 0;
                        Author author = db.Author.FirstOrDefault(q => q.Name == a.TextContent);
                        if (author == null)
                        {
                            string urlA = "https://www.litres.ru";
                            urlA += a.GetAttribute("href");
                            urlA += "ob-avtore/";
                            IDocument docA = await context.OpenAsync(urlA);
                            newAuthor.Name = a.TextContent;
                            newAuthor.AuthorID = 1;
                            IElement descA = docA.All.FirstOrDefault(des =>
                                des.LocalName == "div"
                                && des.ClassList.Contains("person-page__html")
                                );
                            if (descA != null) newAuthor.Description = descA.TextContent;
                            author = newAuthor;
                        }

                        db.Author.Add(author);
                        bookModel.Author.Add(author);
                    }

                    IEnumerable<IElement> genres = doc.All.Where(genr =>
                        genr.LocalName == "a"
                        && genr.ClassList.Contains("biblio_info__link")
                        && genr.ParentElement.FirstElementChild.TextContent == "Жанр:"
                        );

                    foreach (var genre in genres)
                    {
                        Genre g2ch = db.Genre.FirstOrDefault(g => g.Name == genre.TextContent);
                        if (g2ch == null)
                        {
                            Genre newGenre = new Genre();
                            newGenre.Name = genre.TextContent;
                            newGenre.GenreID = 1;
                            db.Genre.Add(newGenre);
                            g2ch = newGenre;
                        }
                        bookModel.Genre.Add(g2ch);
                    }

                    IEnumerable<IElement> series = doc.All.Where(seri =>
                        seri.LocalName == "a"
                        && seri.ClassList.Contains("biblio_book_sequences__link")
                        );

                    foreach (var seria in series)
                    {
                        Series s2ch = db.Series.FirstOrDefault(g => g.Name == seria.TextContent);
                        if (s2ch == null)
                        {
                            Series newSeria = new Series();
                            newSeria.Name = seria.TextContent;
                            newSeria.SeriesID = 1;
                            s2ch = newSeria;
                        }
                        bookModel.Series.Add(s2ch);
                    }


                    bookModel.Actual_date = DateTime.Now.Date;
                    db.Book.Add(bookModel);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
               Debug.WriteLine("DB_Error " + url);
                Debug.WriteLine("Exception: " + ex.Message);
            }
        }
        

        public async Task<List<string>> GetLinks(string url)
        {
            IConfiguration config = Configuration.Default.WithDefaultLoader();
            IBrowsingContext context = BrowsingContext.New(config);
            IDocument doc = await context.OpenAsync(url);
            IEnumerable<IElement> aElements = doc.All.Where(block =>
            block.LocalName == "a"
            && block.ParentElement.LocalName == "div"
            && block.ParentElement.ClassList.Contains("cover-image-wrapper"));
            List<string> output = new List<string>();
            foreach (IElement a in aElements.ToList())
                output.Add($"https://www.litres.ru{a.GetAttribute("href")}");
            return output;
        }

        public async void New()
        {
            //using (BooksContext db = new BooksContext())
            //{
            //    db.Database.ExecuteSqlCommand("Delete from Book");
            //}
            var parser = new Parser();
            string mainPage = "https://www.litres.ru/novie/";
            //System.Diagnostics.Process.Start(mainPage);
            List<string> books = await parser.GetLinks(mainPage);
            foreach (string bookUrl in books)
            {
                parser.Parse(bookUrl);
            }
        }
    }
}
