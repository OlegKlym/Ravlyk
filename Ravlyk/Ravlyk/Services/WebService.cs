﻿using Caliburn.Micro;
using HtmlAgilityPack;
using Ravlyk.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ravlyk.Services
{
    public class WebService
    {
        private List<ShopModel> _shops = new List<ShopModel>();

        public async Task<ShopModel> LoadShopAsync(string url, int shopId)
        {
            var existingShop = _shops.FirstOrDefault(x => x.Id == shopId);
            if (existingShop != null)
            {
                return existingShop;
            }

            var root = await LoadHtml(url);
            var shopList = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("col-sm-3 col-sm-offset-0 col-xs-10 col-xs-offset-1 main-category")).Single();
            var shop = new ShopModel()
            {
                Id = shopId,
                ImagePath = shopList.ChildNodes[1].GetAttributeValue("src", ""),
                Title = (shopList.ChildNodes.Count == 7) ? shopList.ChildNodes[3].InnerText : shopList.ChildNodes[3].InnerText,
                Address = (shopList.ChildNodes.Count == 7)  ? "" : shopList.ChildNodes[5].InnerText,
                WorkTime = (shopList.ChildNodes.Count == 7) ? shopList.ChildNodes[5].InnerText : shopList.ChildNodes[6].InnerText,
                Type = (shopList.ChildNodes.Count == 7) ? "" : shopList.ChildNodes[7].InnerText,
                Description = (shopList.ChildNodes.Count == 7) ? "" : shopList.ChildNodes[8].InnerText,
                Categories = await GetCategories(root)

            };

            _shops.Add(shop);
        
            return shop;
        }

        public ShopModel LoadShopModelById(int shopId)
        {
            return _shops.FirstOrDefault(x => x.Id == shopId);
        }
          
        private async static Task<List<CategoryModel>> GetCategories(HtmlNode root)
        {

            List<CategoryModel> _categories = new List<CategoryModel>() { };
            var divList = root.Descendants().Where(n => n.GetAttributeValue("id", "").Equals("column-left")).Single().ChildNodes[1].ChildNodes;
            bool flag = false;
            var id = 1;
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
                            Id = id.ToString(),
                            ImagePath = item.ChildNodes[1].GetAttributeValue("src", ""),
                            Title = item.ChildNodes[3].InnerText,
                            Dishes = await GetDishes(url)
                        });
                        id++;
                    }
                }
            }
            return _categories;
        }

        public CategoryModel LoadCategoryModelById(int shopId, string categoryId)
        {
            var shop = LoadShopModelById(shopId);
            return shop.Categories.FirstOrDefault(x => x.Id == categoryId);
        }

        private static async Task<List<DishModel>> GetDishes(string url)
        {
            var root = await LoadHtml(url);
            var divList = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("product-thumb"));
            var id = 1;
            List<DishModel> _dishes = new List<DishModel>() { };
            foreach (var item in divList)
            {
                _dishes.Add(new DishModel()
                {
                    Id = id.ToString(),
                    ImagePath = item.ChildNodes[1].ChildNodes[0].GetAttributeValue("src", ""),
                    Title = (item.ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerText.Contains("&quot;")) ?
                        item.ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerText.Replace("&quot;", "''") : item.ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerText,
                    Price = item.ChildNodes[3].ChildNodes[3].ChildNodes[1].InnerText,
                    Description = (item.ChildNodes[3].ChildNodes[1].ChildNodes.Count == 3) ? "" : item.ChildNodes[3].ChildNodes[1].ChildNodes[3].ChildNodes[1].InnerText.Replace("&nbsp;", "")
                });
                id++;
            }
            return _dishes;
        }

        public DishModel LoadDishModelById(int shopId, string categoryId, string dishId)
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