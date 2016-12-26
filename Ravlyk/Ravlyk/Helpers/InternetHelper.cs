using Plugin.Connectivity;
using System.Threading.Tasks;

namespace Ravlyk.Helpers
{
    public class InternetHelper
    {
        public static async Task<bool> HasConnection()
        {
            return CrossConnectivity.Current.IsConnected && await CrossConnectivity.Current.IsRemoteReachable("google.com");
        }
    }
}
