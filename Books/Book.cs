using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    public class Book : IComparable, IEquatable<Book>
    {
        public string Name { get; }
        public string Author { get; }
        public decimal Price { get; }
        public int NumberOfPages { get; }
        /// <summary>
        /// ctor 
        /// </summary>
        /// <param name="name">Name of Book</param>
        /// <param name="price">Price of book</param>
        /// <param name="numberOfPages">Number of Pages in book</param>
        /// <param name="author">Author of book</param>
        /// <exception cref="ArgumentException">Arguments must not be not null and price is greater then 0</exception>
        public Book(string name, decimal price, int numberOfPages, string author = "Unknown")
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(author) || price < 0 || numberOfPages < 0)
                throw new ArgumentException();

            Name = name;
            Author = author;
            Price = price;
            NumberOfPages = numberOfPages;
        }
        /// <summary>
        /// object to string (overrided)
        /// </summary>
        /// <returns>string form of Book object</returns>
        public override string ToString()=>$"Name: {Name}, Author: {Author}, Price: {Price}, NumberOfPages: {NumberOfPages}";
        /// <summary>
        /// object Equals (overrided)
        /// </summary>
        /// <param name="obj">Book to be compared with</param>
        /// <returns>true or false</returns>
        public override bool Equals(object obj) => (obj is Book) ? this.Equals(obj) : false;
        /// <summary>
        /// Hash code of an Book object
        /// </summary>
        /// <returns>int value </returns>
        public override int GetHashCode() => (int)Math.Pow((double)(Name.ToString().Length * Author.ToString().Length), 3);
        /// <summary>
        /// Book deault comparer 
        /// </summary>
        /// <param name="book">Book to be compared to this object</param>
        /// <exception cref="ArgumentException">Null referece in argument or not Book object</exception>
        /// <returns></returns>
        public int CompareTo(object book)
        {
            if (book == null || !(book is Book)) throw new ArgumentException($"{nameof(book)} is null or not a Book type object");

            Book newBook = (Book)book;
            return Name.ToString().Length.CompareTo(newBook.Name.ToString().Length);
        }
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="book">Book to be compared to this object</param>
        /// <exception cref="ArgumentException">Argument must not be null</exception>
        /// <returns></returns>
        public bool Equals(Book book)
        {
            if (book == null)
                throw new ArgumentException($"{nameof(book)} must not be null");

            return (Name == book.Name && Author == book.Author) ? true : false;
        }
    }
}
