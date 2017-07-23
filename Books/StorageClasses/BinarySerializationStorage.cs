using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Books.StorageClasses
{
    public class BinarySerializationStorage:IBookListStorage
    {
        public string FilePath { get; set; }
        /// <summary>
        /// Savind data serialized to binary file
        /// </summary>
        /// <param name="books">books to be serealizeds</param>
        public void SaveData(IEnumerable<Book> books)
        {
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter=new BinaryFormatter();

                
                    binaryFormatter.Serialize(fileStream,books);
                
            }
        }
        /// <summary>
        /// Loading data from binaryserialized file
        /// </summary>
        /// <returns>array of books</returns>
        public Book[] LoadData()
        {
            if (!File.Exists(FilePath)) throw new InvalidOperationException("Enter a correct filepath");
            
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open))
            {
                BinaryFormatter binaryFormatter=new BinaryFormatter();

                Book[] books = (Book[]) binaryFormatter.Deserialize(fileStream);
                return books;
            }
            
        }
    }
}
