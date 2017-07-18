using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.StorageClasses
{
    public class BookListStorage : IBookListStorage
    {
        public string FilePath { get; set; }

        /// <summary>
        /// Saving data to binary file
        /// </summary>
        /// <param name="books">Data to be saved</param>
        public void SaveData(BookListService books)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(FilePath, FileMode.Create)))
            {
                foreach (Book b in books)
                {
                    writer.Write(b.Name);
                    writer.Write(b.Price);
                    writer.Write(b.NumberOfPages);
                    writer.Write(b.Author);
                }
            }
        }
        /// <summary>
        /// Loading data from binary file
        /// </summary>
        /// <exception cref="InvalidOperationException">FilePath is wrong</exception>
        /// <returns>before saved data from binary file</returns>
        public BookListService LoadData()
        {
            if (!File.Exists(FilePath)) throw new InvalidOperationException("Enter a correct filepath");

            BookListService books = new BookListService();

            using (BinaryReader reader = new BinaryReader(File.Open(FilePath, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    string name = reader.ReadString();
                    decimal price = reader.ReadDecimal();
                    int numberOfPages = reader.ReadInt32();
                    string author = reader.ReadString();

                    books.AddBook(new Book(name, price, numberOfPages, author));
                }
            }

            return books;
        }
    }
}
