﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.StorageClasses
{/// <summary>
/// storage provider
/// </summary>
    public interface IBookListStorage
    {
        void SaveData(IEnumerable<Book> books);
        Book[] LoadData();
    }
}
