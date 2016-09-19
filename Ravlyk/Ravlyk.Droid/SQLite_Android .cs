using System;
using Android.OS;
using System.IO;
using Xamarin.Forms;
using Ravlyk.Droid;

[assembly: Dependency(typeof(SQLiteService))]
namespace Ravlyk.Droid
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