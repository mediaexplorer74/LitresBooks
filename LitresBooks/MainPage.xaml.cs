// MainPage

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
using Microsoft.EntityFrameworkCore; // !
using System.Data;//.Entity;
using System.ComponentModel.DataAnnotations.Schema;


//
namespace LitresBooks
{
    // Класс Phone, который будет представлять модель телефона
    // о-вторых, для создания связи "один-ко-многим" зависимая модель должна 
    // содержать ключ. В нашем случае зависимой моделью является Phone, 
    // поэтому он определяет ключ в виде двух свойств:
    //    1) public int CompanyId { get; set; }
    //    2) public Company Company { get; set; }
    public class Phone
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }


        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }

    // Класс Company, который представляет компанию-производителя телефона
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Phone> Phones { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }


    public class DbModelBuilder
    {
        // TODO
        internal object Entity<T>()
        {
            throw new NotImplementedException();
        }
    }

    // После определения моделей нам надо добавить в проект класс контекста данных, 
    // через который приложение будет взаимодействовать с базой данных.
    // Итак, добавим в проект новый класс MobileContext :
    public class MobileContext : DbContext
    {
        // Класс контекста данных должен быть унаследован от базового класса DbContext, 
        // а для взаимодействия с таблицами в базе данных в нем определяются свойства 
        // по типу DbSet<T>. 
        // То есть через свойство Companies будет идти взаимодействие с таблицей компаний, 
        // а через свойство Phones - взаимодействие с таблицей телефонов.
        public DbSet<Company> Companies { get; set; }
        public DbSet<Phone> Phones { get; set; }

        // ---------------------------------------------
        //public DbSet<Genre> Genres { get; set; }

        
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Publishing_house> Publishing_house { get; set; }
        public virtual DbSet<Quote> Quote { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        // ---------------------------------------------
        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .HasMany(e => e.Book)
                .WithMany(e => e.Author)
                .Map(m => m.ToTable("Author_Book").MapLeftKey("AuthorID").MapRightKey("ID"));

            modelBuilder.Entity<Book>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Genre)
                .WithMany(e => e.Book)
                .Map(m => m.ToTable("Book_Genre").MapLeftKey("ID").MapRightKey("GenreID"));

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Series)
                .WithMany(e => e.Book)
                .Map(m => m.ToTable("Series_Book").MapLeftKey("ID").MapRightKey("SeriesID"));

            modelBuilder.Entity<Genre>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Publishing_house>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Publishing_house>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Quote>()
                .Property(e => e.QuoteText)
                .IsUnicode(false);

            modelBuilder.Entity<Series>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
        */
        // ---------------------------------------------


        // В конструкторе контекста мы генерируем базу данных, 
        // которая соответствует определению моделей, с помощью 
        // выражения Database.EnsureCreated().
        public MobileContext()
        {
            Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Mobile.db");
        }
    }




    //MainPage class
    public sealed partial class MainPage : Page
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


        public MainPage()
        {
            this.InitializeComponent();

            // -----------------------------------------------------------------
            this.Loaded += MainPage_Loaded;

#region "Temp"
            // TEMP ------------------------------------------------------------
            /*
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 671);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 53);
            this.button1.TabIndex = 0;
            this.button1.Text = "Обновить базу данных";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_ClickAsync);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 331);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 50);
            this.button2.TabIndex = 3;
            this.button2.Text = "Показать список книг";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(12, 33);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(449, 292);
            this.listBox1.TabIndex = 4;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(468, 33);
            this.listBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(449, 292);
            this.listBox2.TabIndex = 5;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(468, 327);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "ID книги: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(528, 327);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "       00   ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Список книг";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(465, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Информация о книге";
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 16;
            this.listBox3.Location = new System.Drawing.Point(925, 33);
            this.listBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(449, 292);
            this.listBox3.TabIndex = 11;
            this.listBox3.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(925, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Выбранная колонка: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1069, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "label6";

            
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(236, 350);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1139, 375);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button6);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.textBox2);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Controls.Add(this.button5);
            this.tabPage3.Controls.Add(this.button4);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(1131, 346);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Редактировать";
            this.tabPage3.UseVisualStyleBackColor = true;
            
            
            
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(5, 290);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(200, 50);
            this.button6.TabIndex = 27;
            this.button6.Text = "Сохранить изменения";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(717, 234);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 17);
            this.label10.TabIndex = 26;
            this.label10.Text = "Предупреждения";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(717, 257);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(407, 83);
            this.textBox2.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(715, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 17);
            this.label9.TabIndex = 24;
            this.label9.Text = "label9";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(715, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(310, 17);
            this.label8.TabIndex = 19;
            this.label8.Text = "Текст выбранного пункта до редактирования";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(213, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(266, 17);
            this.label7.TabIndex = 23;
            this.label7.Text = "Поле для редактирования/добавления";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(213, 26);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(495, 314);
            this.textBox1.TabIndex = 22;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(5, 180);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(200, 50);
            this.button5.TabIndex = 21;
            this.button5.Text = "Изменить поле";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(7, 26);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(200, 50);
            this.button4.TabIndex = 20;
            this.button4.Text = "Добавить";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(7, 82);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(200, 50);
            this.button3.TabIndex = 19;
            this.button3.Text = "Удалить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button9);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.button7);
            this.tabPage1.Controls.Add(this.textBox12);
            this.tabPage1.Controls.Add(this.textBox11);
            this.tabPage1.Controls.Add(this.textBox10);
            this.tabPage1.Controls.Add(this.textBox9);
            this.tabPage1.Controls.Add(this.textBox8);
            this.tabPage1.Controls.Add(this.textBox7);
            this.tabPage1.Controls.Add(this.textBox6);
            this.tabPage1.Controls.Add(this.textBox5);
            this.tabPage1.Controls.Add(this.textBox4);
            this.tabPage1.Controls.Add(this.textBox3);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(1131, 346);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Создать новую запись";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(957, 208);
            this.button9.Margin = new System.Windows.Forms.Padding(4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(148, 52);
            this.button9.TabIndex = 23;
            this.button9.Text = "Очистить поля";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(847, 10);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(231, 34);
            this.label17.TabIndex = 22;
            this.label17.Text = "Введите данные о новой книге, \r\nчтобы добавить ее в базу данных";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(957, 267);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(148, 52);
            this.button7.TabIndex = 21;
            this.button7.Text = "Добавить новую книгу";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(264, 294);
            this.textBox12.Margin = new System.Windows.Forms.Padding(4);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(480, 22);
            this.textBox12.TabIndex = 20;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(264, 262);
            this.textBox11.Margin = new System.Windows.Forms.Padding(4);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(480, 22);
            this.textBox11.TabIndex = 19;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(264, 70);
            this.textBox10.Margin = new System.Windows.Forms.Padding(4);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(480, 22);
            this.textBox10.TabIndex = 18;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(264, 102);
            this.textBox9.Margin = new System.Windows.Forms.Padding(4);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(480, 22);
            this.textBox9.TabIndex = 17;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(264, 230);
            this.textBox8.Margin = new System.Windows.Forms.Padding(4);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(480, 22);
            this.textBox8.TabIndex = 16;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(264, 198);
            this.textBox7.Margin = new System.Windows.Forms.Padding(4);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(480, 22);
            this.textBox7.TabIndex = 15;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(264, 166);
            this.textBox6.Margin = new System.Windows.Forms.Padding(4);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(480, 22);
            this.textBox6.TabIndex = 14;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(264, 134);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(480, 22);
            this.textBox5.TabIndex = 13;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(264, 38);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(480, 22);
            this.textBox4.TabIndex = 12;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(264, 6);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(480, 22);
            this.textBox3.TabIndex = 11;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(180, 298);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(74, 17);
            this.label21.TabIndex = 10;
            this.label21.Text = "Описание";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(151, 234);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 17);
            this.label20.TabIndex = 9;
            this.label20.Text = "Издательство";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 170);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(245, 17);
            this.label19.TabIndex = 8;
            this.label19.Text = "Серия книг (через \"точку-запятую\")";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(32, 266);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(223, 17);
            this.label18.TabIndex = 7;
            this.label18.Text = "Цитаты (через \"точку-запятую\")";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(141, 202);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(111, 17);
            this.label16.TabIndex = 5;
            this.label16.Text = "Оценка ЛитРес";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(179, 138);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 17);
            this.label15.TabIndex = 4;
            this.label15.Text = "Тип книги";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(212, 106);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 17);
            this.label14.TabIndex = 3;
            this.label14.Text = "Цена";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(32, 42);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(221, 17);
            this.label13.TabIndex = 2;
            this.label13.Text = "Авторы (через \"точку-запятую\")";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 74);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(229, 17);
            this.label12.TabIndex = 1;
            this.label12.Text = "Жанр(ы) (через \"точку-запятую\")";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(137, 10);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "Название книги";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label24);
            this.tabPage2.Controls.Add(this.button8);
            this.tabPage2.Controls.Add(this.label23);
            this.tabPage2.Controls.Add(this.label22);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(1131, 346);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Удалить запись";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(43, 242);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(100, 17);
            this.label24.TabIndex = 3;
            this.label24.Text = "                       ";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(39, 139);
            this.button8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(197, 53);
            this.button8.TabIndex = 2;
            this.button8.Text = "Удалить книгу";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(39, 64);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(112, 17);
            this.label23.TabIndex = 1;
            this.label23.Text = "                          ";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(35, 25);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(347, 17);
            this.label22.TabIndex = 0;
            this.label22.Text = "Выберите книгу, чтобы удалить ее из базы данных";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1131, 346);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Сгенерировать отчет";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.button11);
            this.groupBox2.Location = new System.Drawing.Point(558, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(567, 333);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Рейтинг популярных жанров";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(193, 184);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(155, 57);
            this.button11.TabIndex = 0;
            this.button11.Text = "Создать отчет";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox13);
            this.groupBox1.Controls.Add(this.button10);
            this.groupBox1.Controls.Add(this.listBox4);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Location = new System.Drawing.Point(4, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(547, 333);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Показать книги по жанру с описанием";
            // 
            // textBox13
            // 
            this.textBox13.Enabled = false;
            this.textBox13.Location = new System.Drawing.Point(313, 235);
            this.textBox13.Multiline = true;
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(228, 84);
            this.textBox13.TabIndex = 4;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(371, 59);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(170, 57);
            this.button10.TabIndex = 3;
            this.button10.Text = "Создать отчет";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.ItemHeight = 16;
            this.listBox4.Location = new System.Drawing.Point(7, 59);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(299, 260);
            this.listBox4.TabIndex = 2;
            this.listBox4.SelectedIndexChanged += new System.EventHandler(this.listBox4_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 38);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(239, 17);
            this.label25.TabIndex = 1;
            this.label25.Text = "Выберите интересующий вас жанр";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(137, 91);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(306, 51);
            this.label26.TabIndex = 1;
            this.label26.Text = "В данном отчете будет представлена \r\nинформация о 10 самых популярных жанрах,\r\nпо" +
    " которым написано не менее 3-х книг.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1388, 736);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Книги-новинки ЛитРес";
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            */

#endregion

           
        }//MainPage end


        // При загрузке страницы срабатывает обработчик MainPage_Loaded, 
        // в котором получаем список компаний из базы данных и устанавливаем его 
        // в качестве источника данных для ListView
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            using (MobileContext db = new MobileContext())
            {
                companiesList.ItemsSource = db.Companies.ToList();


                //*******************

                // -----------------------------------------------------------------

                try
                {
                    //foreach (var genre in db.Genre.OrderByDescending(q => q.Book.Count))
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

                //*******************

            }
        }


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

        }//button2_Click end

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

        public void listBox3_SelectedIndexChanged(object sender, RoutedEventArgs e)
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

        public void button3_Click(object sender, RoutedEventArgs e)
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

        public void button5_Click(object sender, RoutedEventArgs e)
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

        }//


        // 
        public void button4_Click(object sender, RoutedEventArgs e)
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

        // 
        public void button6_Click(object sender, RoutedEventArgs e)
        {
            DBSave();
        }

        public void DBSave()
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

        // Создать отчет
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


        // Создать отчет
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

        // 
        private void listBox4_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            textBox13.Text = "";
        }

        // 
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

            // 
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
                string[] header = new string[3] { "№", "Название жанра", "Рейтинг" };
                TableCreateGenre(dictionary.Count, 3, header, dictionary);

                string fileName = "Отчет по рейтингу жанров(" + Guid.NewGuid().ToString() + ")";
                TrySave(fileName);
                appClose();
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

            //
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

            //
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

            //
            public void appClose()
            {
                //object saveChanges = word.WdSaveOptions.wdPromptToSaveChanges;
                //object originalFormat = word.WdOriginalFormat.wdWordDocument;
                //object routeDocument = Type.Missing;
                //wordapp.Quit(ref saveChanges, ref originalFormat, ref routeDocument);
            }

        }//class end

        // 
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

        }//button1_ClickAsync end

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //
        }
    }//class end

}








