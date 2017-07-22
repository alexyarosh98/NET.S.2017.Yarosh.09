using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Books.StorageClasses
{
    public class XmlFileStorage:IBookListStorage
    {
        public string FilePath { get; set; }
        
        /// <summary>
        /// Savind data in a xml file
        /// </summary>
        /// <param name="books">data to be saved</param>
        public void SaveData(IEnumerable<Book> books)
        {
            if (!File.Exists(FilePath)) throw new InvalidOperationException("Enter a correct filepath");

            XDocument xDocument=new XDocument();
            XElement header=new XElement("Books");
            XElement[] xElementArr = new XElement[books.Count()];
            int index = 0;
            foreach (Book book in books)
            {
                xElementArr[index++]=new XElement("Book",new XAttribute("id",index),
                    new XElement("Name",book.Name),
                    new XElement("Author",book.Author),
                    new XElement("Price",book.Price.ToString("F")),
                    new XElement("NumberOfPages",book.NumberOfPages));
            }
            for (int i = 0; i < books.Count(); i++)
            {
                header.Add(xElementArr[i]);
            }
            xDocument.Add(header);
            xDocument.Save(FilePath);
        }

        /// <summary>
        /// Loadind data from xml file
        /// </summary>
        /// <returns>array of books</returns>
        public Book[] LoadData()
        {
            int capacity = 10;
            Book[] books=new Book[capacity];
            int index = 0;
           XDocument xDocument=XDocument.Load(FilePath);
            foreach (XElement xElement in xDocument.Element("Books").Elements())
            {
                string name = xElement.Element("Name").Value;
                string author = xElement.Element("Author").Value;
                Console.WriteLine(xElement.Element("Price").Value);
                decimal price = decimal.Parse(string.Format("{0:N}",xElement.Element("Price").Value));
                int np = int.Parse(xElement.Element("NumberOfPages").Value);
                
                if(index==capacity) Array.Resize(ref books,capacity*=2);
                books[index]=new Book(name,price,np,author);
                index++;
            }
            Array.Resize(ref books,index);
            return books;
        }
    }
}
