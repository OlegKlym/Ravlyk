using Caliburn.Micro;
using Ravlyk.Entities;
using Ravlyk.Models;
using Ravlyk.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ravlyk.Services
{
    public class DatabaseService : Screen
    {
        public SQLiteConnection _database;
        public SQLiteConnection Database
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

        public List<ShopEntity> GetShops()
        {
            return (from i in Database.Table<ShopEntity>() select i).ToList();
        }

        public List<CategoryEntity> GetCategories()
        {
            return (from i in Database.Table<CategoryEntity>() select i).ToList();
        }

        public ShopEntity GetItem(int id)
        {
            return Database.Get<ShopEntity>(id);
        }

        public void ClearDB()
        {
            Database.DeleteAll<ShopEntity>();
        }

        public List<ShopModel> GetShopsFromBD()
        {
            var db = IoC.Get<DatabaseService>().GetShops();
            List<ShopModel> shops = new List<ShopModel>();
            foreach (var item in db)
                shops.Add(new ShopModel()
                {
                    Id = item.Id,
                    ImagePath = item.ImagePath,
                    Title = item.Title,
                    Address = item.Address,
                    Type = item.Type,
                    Description = item.Description,
                    Categories =  GetCategoriesFromBD(item.Id)
                });
            return shops;
        }

        public List<CategoryModel> GetCategoriesFromBD(int shopId)
        {
            var db = IoC.Get<DatabaseService>().GetCategories();
            List<CategoryModel> categories = new List<CategoryModel>();
            foreach (var item in db)
            {
                if (item.Id_Shop == shopId)
                    categories.Add(new CategoryModel()
                    {
                        Id = item.Id_Category,
                        ImagePath = item.ImagePath,
                        Title = item.Title
                    });
            }
            return categories;
        }

        public void InsertShop(ShopModel shop)
        {
            Database.Insert(new ShopEntity()
            {
                ImagePath = shop.ImagePath,
                Title = shop.Title,
                Address = shop.Address,
                Type = shop.Type,
                Description = shop.Description
            });
        }

        public void InsertCategory(int shopId, int id)
        {
            var category = IoC.Get<WebService>().LoadCategoryModelById(shopId, id);
           
            Database.Insert(new CategoryEntity()
            {
                Id_Shop = shopId,
                ImagePath = category.ImagePath,
                Title = category.Title
            });
        }

       
    }
}
