using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HomeLibrary
{
    internal class Program
    {
        const string FilePath = @"D:\HomeLibrary\books.json";
        static void Main(string[] args)
        {
            //1   Класс книги. Опишите класс Book со свойствами
            //«Название», «Автор», «Год издания» и «Жанр».
            //Предусмотрите проверку данных:
            //название и автор не могут быть пустыми,
            //год — не больше текущего.

            //2   Список и меню.Храните книги в списке List<Book>.
            //Программа выводит меню и выполняет команды,
            //пока пользователь не выберет выход:
            //добавить книгу, удалить книгу по названию,
            //показать все книги.

            //3   Запросы.Добавьте в меню поиск книг по автору
            //и по жанру, вывод списка, отсортированного по году издания,
            //и статистику — сколько книг каждого жанра.
            //Реализуйте их запросами LINQ.

            //4   Сохранение.При выходе программа сохраняет список
            //в файл «books.json», при запуске — загружает его,
            //если файл существует.

            //5   Надёжность.Некорректный ввод — буквы вместо года,
            //пустое название, неверный пункт меню,
            //повреждённый файл — не должен приводить к аварийному завершению программы:
            //пользователь получает понятное сообщение и продолжает работу.
            bool quit = true; // флаг
            Console.WriteLine("Добро пожааловать в Домашнюю библиотеку!");
            while (quit)
            {
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Удалить книгу по названию");
                Console.WriteLine("3. Показать все книги");
                Console.WriteLine("4. Найти по автору");
                Console.WriteLine("5. Найти по жанру");
                Console.WriteLine("6. Сортировка по году");
                Console.WriteLine("7. Статистика по жанрам");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт: ");
                string input = Console.ReadLine();
                Console.WriteLine();
                switch (input)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        DeleteBookByName();
                        break;
                    case "3":
                        ShowAll();
                        break;
                    case "4":
                        FindByAuthor();
                        break;
                    case "5":
                        FindByGenre();
                        break;
                    case "6":
                        SortByYear();
                        break;
                    case "7":
                        ShowStatistics();
                        break;
                    case "0":
                        quit = false; // флаг выхода
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу, чтобы попробовать снова...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        public static void AddBook()
        {
            string directory = @"D:\HomeLibrary";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            try
            {
                Console.WriteLine("Введите название книги:");
                string name = Console.ReadLine();

                Console.WriteLine("Введите автора:");
                string author = Console.ReadLine();

                Console.WriteLine("Введите год издания:");
                int year = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Введите жанр:");
                string genre = Console.ReadLine();

                Book book = new Book(name, author, year, genre);

                List<Book> getBooks = GetBooks();

                getBooks.Add(book);

                SaveBooks(getBooks);

                Console.WriteLine("Книга добавлена и сохранена.");
                Console.WriteLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: год должен быть числом.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
        public static void DeleteBookByName()
        {
            List<Book> getBooks = GetBooks();
            Console.WriteLine("Введите название книги:");
            string name = Console.ReadLine();

            Book book = getBooks.FirstOrDefault(b => b.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (book != null)
            {
                getBooks.Remove(book);
                SaveBooks(getBooks);
                Console.WriteLine("Книга удалена.");
            }
            else
            {
                Console.WriteLine("Книга не найдена.");
            }
        }
        public static void ShowAll()
        {
            List<Book> getBooks = GetBooks();
            if (getBooks.Count == 0)
            {
                Console.WriteLine("Библиотека пуста.");
            }
            else
            {
                foreach (Book book in getBooks)
                {
                    Console.WriteLine("Название: " + book.Name);
                    Console.WriteLine("Автор: " + book.Author);
                    Console.WriteLine("Год издания: " + book.Year);
                    Console.WriteLine("Жанр: " + book.Genre);
                    Console.WriteLine();
                }
            }
        }
        public static void FindByAuthor()
        {
            List<Book> getBooks = GetBooks();
            Console.WriteLine("Введите автора:");
            string author = Console.ReadLine();

            List<Book> result = getBooks.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList(); ;

            if (result.Count == 0)
            {
                Console.WriteLine("Книги не найдены.");
            }
            else
            {
                foreach (Book book in result)
                {
                    Console.WriteLine("Название: " + book.Name);
                    Console.WriteLine("Автор: " + book.Author);
                    Console.WriteLine("Год издания: " + book.Year);
                    Console.WriteLine("Жанр: " + book.Genre);
                    Console.WriteLine();
                }
            }
        }
        public static void FindByGenre()
        {
            List<Book> getBooks = GetBooks();
            Console.WriteLine("Введите жанр:");
            string genre = Console.ReadLine();

            List<Book> result = getBooks.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();

            if (result.Count == 0)
            {
                Console.WriteLine("Книги не найдены.");
            }
            else
            {
                foreach (Book book in result)
                {
                    Console.WriteLine("Название: " + book.Name);
                    Console.WriteLine("Автор: " + book.Author);
                    Console.WriteLine("Год издания: " + book.Year);
                    Console.WriteLine("Жанр: " + book.Genre);
                    Console.WriteLine();
                }
            }

        }
        public static void SortByYear()
        {
            List<Book> getBooks = GetBooks();
            if (getBooks.Count == 0)
            {
                Console.WriteLine("Библиотека пуста.");
            }
            else
            {
                List<Book> result = getBooks.OrderBy(b => b.Year).ToList();

                foreach (Book book in result)
                {
                    Console.WriteLine(book.Year + " | " + book.Name + " | " + book.Author + " | " + book.Genre);
                }
            }
        }
        public static void ShowStatistics()
        {
            List<Book> getBooks = GetBooks();
            if (getBooks.Count == 0)
            {
                Console.WriteLine("Библиотека пуста.");
                return;
            }
            IEnumerable<IGrouping<string, Book>> result = getBooks.GroupBy(b => b.Genre);
            foreach (IGrouping<string, Book> genre in result)
            {
                Console.WriteLine(genre.Key + ": " + genre.Count() + " книг");
            }
        }
        static List<Book> GetBooks()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Book>();
            }
            try
            {
                string jsonString = String.Empty;
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    jsonString = sr.ReadToEnd();
                };
                List<Book> loadedBooks = JsonSerializer.Deserialize<List<Book>>(jsonString);
                return loadedBooks;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка чтения файла: " + ex.Message);
                return new List<Book>();
            }
        }
        static void SaveBooks(List<Book> books)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(books, options);
            File.WriteAllText(FilePath, jsonString);
        }
    }
}
