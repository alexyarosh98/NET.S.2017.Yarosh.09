using Books;
using Books.StorageClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksUITests
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book1 = new Book("Name1", 1M, 134, "Author1");
            Book book2 = new Book("Name1", 1.12M, 136, "Author1");
            Console.WriteLine("Compearing 2 books with book Equals: "+book1.Equals(book2));
            Console.WriteLine("\nHashCodes of 2 equals books\n first book :" + book1.GetHashCode() + "\n second book: " + book2.GetHashCode());

            Book book3 = new Book("Name3++", 123.4M, 124);
            Console.WriteLine("\nbook 3 is begger then book1 " + book3.CompareTo(book1));

            BookListService books = new BookListService();
            Console.WriteLine("\n empty ctor of books: "+ books.Count);
            books.AddBook(book1);
            books.AddBook(book3);
            Console.WriteLine("2 elements were added. Size of books - "+ books.Count);
            Console.WriteLine("These elements are : ");
            foreach (Book b in books)
            {
                Console.WriteLine(b.ToString());
            }

            //books.AddBook(book1); - exception

            Console.WriteLine("Removing first book:");
            books.RemoveBook(book1);
            Console.WriteLine("Size of books now - "+ books.Count+ "\n Books consist of : ");
            foreach (Book b in books)
            {
                Console.WriteLine(b.ToString());
            }

            //  books.RemoveBook(book1); - exception

            Book book4 = new Book("LongNameOfBook", 214.412M, 4234);
            Book book5 = new Book("VeryLongNameOfBook", 124M, 412, "Abo");
            books.AddBook(book1);
            books.AddBook(book5);
            books.AddBook(book4);

            Console.WriteLine("\nBefore sorting:");
            foreach (Book b in books)
            {
                Console.WriteLine(b.ToString());
            }

            Console.WriteLine("After sorting:");
            books.SortBooksByTag((Book b1, Book b2)=> -1 * b1.Price.CompareTo(b2.Price));
            foreach (Book b in books)
            {
                Console.WriteLine(b.ToString());
            }

            BookListStorage storage = new BookListStorage() { FilePath = @"C:\Users\alexy\Desktop\text.txt" };
            books.SaveToStorage(storage);
            Console.WriteLine("\nSuccesfully written to the file");
            Console.WriteLine("Lets delete some books: ");
            books.RemoveBook(book3);
            books.RemoveBook(book5);
            foreach (Book b in books)
            {
                Console.WriteLine(b.ToString());
            }
            Console.WriteLine("\n And Load previous items from file");
            books =books.LoadFromStorage(storage);
            foreach (Book b in books)
            {
                Console.WriteLine(b.ToString());
            }
            Console.ReadKey();
        }
      
    }
}
