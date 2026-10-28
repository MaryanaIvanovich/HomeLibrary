using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace HomeLibrary
{
    internal class Book
    {
        private string name;
        private string author;
        private int year;
        private string genre;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Название книги не может быть пустым.");
                    value = "Название";
                }
                else { name = value; }
            }
        }
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Автор не может быть пустым.");
                    value = "Автор";
                }
                else { author = value; }
            }
        }
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                if (value <= 0 || value > DateTime.Now.Year)
                {
                    throw new ArgumentException("Год издания указан некорркетно!");
                }
                else { year = value; }
            }
        }
        public string Genre 
        {
            get
            {
                return genre;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Жанр не может быть пустым.");
                    value = "Жанр";
                }
                else { genre = value; }
            }
        }
        public Book(string name, string author, int year, string genre) 
        {
            Name = name;
            Author = author;
            Year = year;
            Genre = genre;
        }

    }
}
