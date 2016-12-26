using SQLite;


namespace Ravlyk.Services
{
    public interface IPlatformService
    {
        int GetAppVersion();
        string GetDeviceId();
		SQLiteConnection GetSQLiteConnection();
    }
}
