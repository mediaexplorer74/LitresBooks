//

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore; // using System.Data.Entity;

namespace LitresBooks
{
   

    public partial class BookContext : DbContext
    {
        public BookContext() //: base("name=BookContext")
        {
            // TODO
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Publishing_house> Publishing_house { get; set; }
        public virtual DbSet<Quote> Quote { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

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
    }

    public class DbModelBuilder
    {
        // TODO
        internal object Entity<T>()
        {
            throw new NotImplementedException();
        }
    }
}
