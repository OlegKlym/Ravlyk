using HtmlAgilityPack;
using Ravlyk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ravlyk.Services
{
    public class DataService
    {
        public async Task<ShopViewModel> LoadShopAsync(string url)
        {
            var root = await LoadHtml(url);
            var shopList = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("col-sm-3 col-sm-offset-0 col-xs-10 col-xs-offset-1 main-category")).Single();
            var shop = new ShopViewModel()
            {
                ImagePath = shopList.ChildNodes[1].GetAttributeValue("src", ""),
                Title = (shopList.ChildNodes.Count == 7) ? "" : shopList.ChildNodes[3].InnerText,
                Address = (shopList.ChildNodes.Count == 7) ? shopList.ChildNodes[3].InnerText : shopList.ChildNodes[5].InnerText,
                WorkTime = (shopList.ChildNodes.Count == 7) ? shopList.ChildNodes[5].InnerText : shopList.ChildNodes[6].InnerText,
                Type = (shopList.ChildNodes.Count == 7) ? "" : shopList.ChildNodes[7].InnerText,
                Description = (shopList.ChildNodes.Count == 7) ? "" : shopList.ChildNodes[8].InnerText,
                Categories = await getCategories(root)

            };

            return shop;
        }

        public async static Task<HtmlNode> LoadHtml(string url)
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



        public async static Task<List<CategoryViewModel>> getCategories(HtmlNode root)
        {

            List<CategoryViewModel> _categories = new List<CategoryViewModel>() { };
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

                        _categories.Add(new CategoryViewModel()
                        {
                            ImagePath = item.ChildNodes[1].GetAttributeValue("src", ""),
                            Title = item.ChildNodes[3].InnerText,
                            Dishes = await getDishes(url)
                        });
                    }
                }
            }
            return _categories;
        }


        public static async Task<List<DishViewModel>> getDishes(string url)
        {
            var root = await LoadHtml(url);
            var divList = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("product-thumb"));
            List<DishViewModel> _dishes = new List<DishViewModel>() { };
            foreach (var item in divList)
            {
                _dishes.Add(new DishViewModel()
                {
                    ImagePath = item.ChildNodes[1].ChildNodes[0].GetAttributeValue("src", ""),
                    Title = (item.ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerText.Contains("&quot;")) ?
                        item.ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerText.Replace("&quot;", "''") : item.ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerText,
                    Price = item.ChildNodes[3].ChildNodes[3].ChildNodes[1].InnerText,
                    Description = (item.ChildNodes[3].ChildNodes[1].ChildNodes.Count == 3) ? "" : item.ChildNodes[3].ChildNodes[1].ChildNodes[3].ChildNodes[1].InnerText.Replace("&nbsp;", "")
                });

            }
            return _dishes;
        }

      
    }
}
