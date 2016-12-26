using HtmlAgilityPack;
using Ravlyk.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ravlyk.Services
{
    public class WebService
    {
        public static int categoryId = 1;
        private List<ShopModel> _shops = new List<ShopModel>();

        public async Task<List<ShopModel>> GetShopsAsync()
        {
            var root = await LoadHtml("http://www.ravlyk.club/");
            var shopList = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("categories-main")).Single();
            int shopIndex = 1, divIndex = 1;
            _shops.Clear();

            while (shopList.ChildNodes[1].ChildNodes[divIndex].ChildNodes[1].GetAttributeValue("href", "") != "mailto:ravlyk.club@gmail.com")
            {
                var url = "http://ravlyk.club/" + shopList.ChildNodes[1].ChildNodes[divIndex].ChildNodes[1].GetAttributeValue("href", "");
                var shop = new ShopModel();
                if ((shop = await LoadShopAsync(url, shopIndex)) != null)
                {
                    _shops.Add(shop);
                    shopIndex++;
                   
                }
                divIndex += 2;
            }
            return _shops;
        }

        public async Task<ShopModel> LoadShopAsync(string url, int shopId)
        {          
            try 
            {
                var root = await LoadHtml(url);
                var shopList = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("col-sm-3 col-sm-offset-0 col-xs-10 col-xs-offset-1 main-category")).Single();
                var shop = new ShopModel()
                {
                    Id = shopId,
                    ImagePath = shopList.ChildNodes[1].GetAttributeValue("src", ""),
                    Title = (shopList.ChildNodes.Count == 7) ? shopList.ChildNodes[3].InnerText : shopList.ChildNodes[3].InnerText,
                    Address = (shopList.ChildNodes.Count == 7) ? "" : shopList.ChildNodes[5].InnerText,
                    WorkTime = (shopList.ChildNodes.Count == 7) ? shopList.ChildNodes[5].InnerText : shopList.ChildNodes[6].InnerText,
                    Type = (shopList.ChildNodes.Count == 7) ? "" : shopList.ChildNodes[7].InnerText,
                    Description = (shopList.ChildNodes.Count == 7) ? "" : shopList.ChildNodes[8].InnerText,
                    Categories = await GetCategoriesAsync(root)

                };
               
                _shops.Add(shop);
                return shop;
            }
            catch
            {
                return null;
            }
           
        }

        public ShopModel LoadShopModelById(int shopId)
        {            
            return _shops.FirstOrDefault(x => x.Id == shopId);
        }
          
        private async static Task<List<CategoryModel>> GetCategoriesAsync(HtmlNode root)
        {
            List<CategoryModel> _categories = new List<CategoryModel>() { };
            var divList = root.Descendants().Where(n => n.GetAttributeValue("id", "").Equals("column-left")).Single().ChildNodes[1].ChildNodes;
            bool flag = false;
            foreach (var item in divList)
            {
                if (item.NodeType == HtmlNodeType.Element)
                {
                    if (!flag)
                        flag = true;
                    else
                    {
                        string url = item.ChildNodes[3].GetAttributeValue("href", "");

                        _categories.Add(new CategoryModel()
                        {
                            Id = categoryId,
                            ImagePath = item.ChildNodes[1].GetAttributeValue("src", ""),
                            Title = item.ChildNodes[3].InnerText,
                            Dishes = await GetDishesAsync(url)
                        });
                        categoryId++;
                    }
                }
            }
            return _categories;
        }

        public CategoryModel LoadCategoryModelById(int shopId, int categoryId)
        {
            var shop = LoadShopModelById(shopId);
            return shop.Categories.FirstOrDefault(x => x.Id == categoryId);
        }

        private static async Task<List<DishModel>> GetDishesAsync(string url)
        {
            var root = await LoadHtml(url);
            var divList = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("product-thumb"));
            var id = 1;
            List<DishModel> _dishes = new List<DishModel>() { };
            foreach (var item in divList)
            {
                _dishes.Add(new DishModel()
                {
                    Id = int.Parse(Regex.Replace(item.ChildNodes[1].ChildNodes[0].Id, "[^0-9]", "", RegexOptions.Singleline)),
                    ImagePath = item.ChildNodes[1].ChildNodes[0].GetAttributeValue("src", ""),
                    Title = (item.ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerText.Contains("&quot;")) ?
                        item.ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerText.Replace("&quot;", "''") : item.ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerText,
                    Price = item.ChildNodes[3].ChildNodes[3].ChildNodes[1].InnerText,
                    Description = (item.ChildNodes[3].ChildNodes[1].ChildNodes.Count == 3) ? "" : item.ChildNodes[3].ChildNodes[1].ChildNodes[3].ChildNodes[1].InnerText.Replace("&nbsp;", "").Replace("\n","")
                });
                id++;
            }
            return _dishes;
        }

        public DishModel LoadDishModelById(int shopId, int categoryId, int dishId)
        {
            var category = LoadCategoryModelById(shopId, categoryId);
            return category.Dishes.FirstOrDefault(x => x.Id == dishId);         
        }

        public List<ShopModel> GetShops()
        {
            return _shops;
        }

        private async static Task<HtmlNode> LoadHtml(string url)
        {
            using (var client = new HttpClient())
            {
                url = url.Replace("amp;", "");
                var htmlPage = await client.GetStringAsync(url);
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlPage);
                return htmlDocument.DocumentNode;
            }
        }
    }
}
