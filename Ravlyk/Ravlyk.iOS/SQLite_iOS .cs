using System;
using Xamarin.Forms;
using System.IO;
using Ravlyk.iOS;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace Ravlyk.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            // ?????????? ???? ? ??
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // ????? ??????????
            var path = Path.Combine(libraryPath, sqliteFilename);

            return path;
        }
    }
}