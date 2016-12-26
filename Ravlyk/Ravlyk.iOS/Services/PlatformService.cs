using Ravlyk.Services;
using System;
using System.IO;


namespace Ravlyk.iOS
{
	public class PlatformService : IPlatformService
	{
		public int GetAppVersion()
		{
			throw new NotImplementedException();
		}

		public string GetDeviceId()
		{
			throw new NotImplementedException();
		}

		public global::SQLite.SQLiteConnection GetSQLiteConnection()
		{
			var sqliteFilename = "db.db3";
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			// Create the connection
			var conn = new SQLite.SQLiteConnection(path);
			// Return the database connection
			return conn;
		}
	}
}
