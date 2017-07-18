using Books.StorageClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books
{

    public class BookListService:IEnumerable<Book>
    {
        private Book[] books;
        private int capacity = 10;
        /// <summary>
        /// amount of elements in BookListService
        /// </summary>
        public int Count { get; private set; }
        private IEqualityComparer<Book> comparer;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="comparer">Logic of comparing Book objects</param>
        public BookListService(IEqualityComparer<Book> comparer=null)
        {
            books = new Book[capacity];
            if (comparer == null) this.comparer = EqualityComparer<Book>.Default;
            else this.comparer = comparer;
        }
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="books">array of Book objects</param>
        /// <param name="comparer">logic of comparing Book objects</param>
        /// <exception cref="ArgumentNullException">First argument must not be null</exception>
        public BookListService(IEnumerable<Book> books, IEqualityComparer<Book> comparer = null) : this(comparer)
        {
            if (books == null)
                throw new ArgumentNullException($"{nameof(books)} must not be null");

            foreach (Book b in books)
            {
                AddBook(b);
            }
        }
        /// <summary>
        /// Add elements to BooksListServide
        /// </summary>
        /// <param name="book">Book to be added</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <exception cref="InvalidOperationException">This book is already exist in BookListServide</exception>
        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException($"{nameof(book)} must not be null");
            if (Contains(book))
                throw new InvalidOperationException($"{nameof(book)} is already exist");

            if (Count == capacity)
            {
                capacity *= 2;
                Array.Resize(ref books, capacity);
            }
            books[Count++] = book;
        }
        /// <summary>
        /// Remove book from BookListServide
        /// </summary>
        /// <param name="book">book to be deleted</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <exception cref="InvalidOperationException">No such element in BookListServide</exception>
        public void RemoveBook(Book book)
        {
            if(book==null) throw new ArgumentNullException($"{nameof(book)} must not be null");
            if (!Contains(book))
                throw new InvalidOperationException($"{nameof(book)} doesn't belong to the current context");

            int index = 0;
            for (int i = 0; index < Count; i++)
            {
                if (comparer.Equals(books[index], book)) break;
                index++;
            }
            for (int i = index; i < Count - 1; i++)
            {
                books[i] = books[i + 1];
            }
            Count--;
        }
        /// <summary>
        /// Findeing book by tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Book FindBookByTag(Predicate<Book> tag)
        {
            if (tag == null)
                throw new ArgumentNullException($"{nameof(tag)} must not be null");

            List<Book> list = new List<Book>(books);
            return list.Find(tag);
        }
        /// <summary>
        /// Sorting books by users rule
        /// </summary>
        /// <param name="comparer">rule of sorting books</param>
        /// <exception cref="ArgumentNullException">Argument must be not null</exception>
        public void SortBooksByTag(Comparison<Book> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException($"{nameof(comparer)} must not be null");
            Array.Resize(ref books, Count);
            SortAlgorithms.MergeSort.Sort<Book>(books, comparer);
        }

      /// <summary>
      /// Saving data to binary file
      /// </summary>
      /// <param name="storage">Storage provider</param>
      /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        public void SaveToStorage(IBookListStorage storage)
        {
            if (storage == null) throw new ArgumentNullException($"{nameof(storage)} must not be null");

            storage.SaveData(this);
        }
        /// <summary>
        /// loading data from binary file
        /// </summary>
        /// <param name="storage"></param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <returns>Before safed BookListServide</returns>
        public BookListService LoadFromStorage(IBookListStorage storage)
        {
            if (storage == null) throw new ArgumentNullException($"{nameof(storage)} must not be null");

            return storage.LoadData();
        }
        public IEnumerator<Book> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return books[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private bool Contains(Book book)
        {

            if (book == null) return false;

            for (int i = 0; i < Count; i++)
            {
                if (comparer.Equals(books[i], book)) return true;
            }
            return false;
        }

    }
}
