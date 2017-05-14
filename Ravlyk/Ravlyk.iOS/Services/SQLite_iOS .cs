using System;
using System.IO;
using Xamarin.Forms;
using Ravlyk.iOS;

[assembly: Dependency(typeof(SQLiteService))]
namespace Ravlyk.iOS
{
    public class SQLiteService : ISQLiteService
    {

        public string GetDatabasePath()
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "shops.db");
            return path;
        }
    }
}