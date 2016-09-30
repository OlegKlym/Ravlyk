using Caliburn.Micro;
using System;

namespace Ravlyk.Helpers
{
    public class Helper :Screen
    {
        public long GetGMTInMS()
        {
            var unixTime = DateTime.Now.ToUniversalTime() -
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)unixTime.TotalMilliseconds;
        }
    }
}
