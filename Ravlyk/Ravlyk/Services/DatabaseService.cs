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
            LoadCategories(shop);
            LoadDishes(shop);

        }


        public void LoadCategories(ShopModel shop)
        {
            var n = GetCategories().Count + shop.Categories.Count;
            for (var j = GetCategories().Count + 1; j <= n; j++)
            {
                InsertCategory(shop.Id, j);

            }

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

        public void LoadDishes(ShopModel shop)
        {
            for (var i = GetCategories().Count - shop.Categories.Count + 1; i <= GetCategories().Count; i++)
            {
                foreach(var dish in IoC.Get<WebService>().LoadCategoryModelById(shop.Id, i).Dishes)
                    InsertDish(shop.Id, i, dish.Id);
            }
        }

        public void InsertDish(int shopId, int categoryId, int id)
        {
            var dish = IoC.Get<WebService>().LoadDishModelById(shopId, categoryId, id);

            Database.Insert(new DishEntity()
            {
                Id_Dish = dish.Id,
                Id_Shop = shopId,
                Id_Category = categoryId,
                ImagePath = dish.ImagePath,
                Title = dish.Title,
                Price = dish.Price,
                Description = dish.Description
            });
        }


        public List<ShopEntity> GetShops()
        {
            return (from i in Database.Table<ShopEntity>() select i).ToList();
        }

        public List<CategoryEntity> GetCategories()
        {
            return (from i in Database.Table<CategoryEntity>() select i).ToList();
        }

        public List<DishEntity> GetDishes()
        {
            return (from i in Database.Table<DishEntity>() select i).ToList();
        }

        public string GetTitle(int shopId, int categoryId)
        {
            if (categoryId == 0)
            {
                var db = IoC.Get<DatabaseService>().GetShops();
                foreach (var item in db)
                    if (item.Id == shopId)
                        return item.Title;
            }
            else
            {
                var db = IoC.Get<DatabaseService>().GetCategories();
                foreach (var item in db)
                    if (item.Id_Category == categoryId)
                        return item.Title;
            }
            return null;

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
                });
            return shops;
        }

        public List<CategoryModel> GetCategoriesFromBD(int shopId)
        {
            var db = IoC.Get<DatabaseService>().GetCategories();
            List<CategoryModel> categories = new List<CategoryModel>();
            foreach (var item in db)
            {
                if (shopId == item.Id_Shop)
                {
                    categories.Add(new CategoryModel()
                    {
                        Id = item.Id_Category,
                        ImagePath = item.ImagePath,
                        Title = item.Title,

                    });
                }

            }
            return categories;
        }

        public List<DishModel> GetDishesFromBD(int shopId, int categoryId)
        {
            var db = IoC.Get<DatabaseService>().GetDishes();
            List<DishModel> dishes = new List<DishModel>();
            foreach (var item in db)
            {
                if (categoryId == item.Id_Category && shopId == item.Id_Shop)
                {
                    dishes.Add(new DishModel()
                    {
                        Id = item.Id_Dish,
                        ImagePath = item.ImagePath,
                        Title = item.Title,
                        Price = item.Price,
                        Description = item.Description
                    });
                }
            }
            return dishes;
        }

        public DishModel GetDish(int dishId)
        {
            var db = IoC.Get<DatabaseService>().GetDishes();
            DishModel dish = new DishModel();
            foreach (var item in db)
            {
                if (item.Id_Dish == dishId)
                    return new DishModel()
                    {
                        Id = item.Id_Dish,
                        ImagePath = item.ImagePath,
                        Title = item.Title,
                        Price = item.Price,
                        Description = item.Description
                    };
            }
            return null;
        }

        public void SetFavor(int dishId)
        {
            var db = IoC.Get<DatabaseService>().GetDishes();
            foreach (var item in db)
                if (item.Id_Dish == dishId)
                {
                    if (item.Favourite)
                    {
                        item.Favourite = false;
                        Database.Update(item);
                    }
                    else
                    {
                        item.Favourite = true;
                        Database.Update(item);
                    }

                }
        }

        public bool IsFavor(int dishId)
        {

            var db = IoC.Get<DatabaseService>().GetDishes();
            foreach (var item in db)
                if (item.Id_Dish == dishId)
                {
                    return item.Favourite;
                }
            return false;
        }


        public ObservableCollection<DishModel> GetFavor()
        {
            ObservableCollection<DishModel> favor = new ObservableCollection<DishModel>();
            var db = IoC.Get<DatabaseService>().GetDishes();
            foreach (var item in db)
                if (item.Favourite)
                    favor.Add(new DishModel()
                    {
                        Id = item.Id_Dish,
                        ImagePath = item.ImagePath,
                        Title = item.Title,
                        Price = item.Price,
                        Description = item.Description
                    });
            return favor;
        }

      

    }
}
