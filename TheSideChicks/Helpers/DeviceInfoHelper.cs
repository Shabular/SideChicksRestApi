using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicks.Helpers
{
    public static class DeviceInfoHelper
    {
        public static string BaseAddress =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5126" : "http://localhost:5126";

        public static string BaseUrl = $"{BaseAddress}/api";
    }
}
