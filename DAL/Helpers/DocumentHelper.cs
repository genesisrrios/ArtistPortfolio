using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public class DocumentHelper
    {
        public static string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute), true).First()).CollectionName;
        }
    }
}
