using Caliburn.Micro;
using Ravlyk.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ravlyk.Services
{
    public class DatabaseService :Screen
    {
        public SQLiteConnection _database;
        public  SQLiteConnection Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new SQLiteConnection("shops.db");
                }
                return _database;
            }
            set
            {
                _database = value;
            }
        }

        public DatabaseService()
        {
            string databasePath = DependencyService.Get<ISQLiteService>().GetDatabasePath();
            Database = new SQLiteConnection(databasePath);
            Database.CreateTable<ShopEntity>();
           
        }

        public List<ShopEntity> GetItems()
        {
            return (from i in Database.Table<ShopEntity>() select i).ToList();
        }

        public ShopEntity GetItem(int id)
        {
            return Database.Get<ShopEntity>(id);
        }


        public int InsertItem(ShopModel item)
        {
           return Database.Insert(item);
        }
    }
}
