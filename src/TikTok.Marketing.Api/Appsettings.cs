using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TikTok.Marketing.Api
{
    public class Appsettings
    {
        public TikTokConfig TikTokConfig { get; set; }
    }

    public class TikTokConfig
    {
        public bool IsDev { get; set; }
        public string AppId { get; set; }
        public string Secret { get; set; }
        public string RedirectUri { get; set; }
        public string Token { get; set; }
    }

}
