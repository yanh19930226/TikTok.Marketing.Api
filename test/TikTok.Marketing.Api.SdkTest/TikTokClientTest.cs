using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TikTok.Marketing.Api.Sdk;
using TikTok.Marketing.Api.Sdk.Models.Audiences;
using TikTok.Marketing.Api.Sdk.Models.Catalogs;
using TikTok.Marketing.Api.Sdk.Models.Terms;
using Xunit;

namespace TikTok.Marketing.Api.SdkTest
{
    public class TikTokClientTest
    {
        private readonly TikTokClient _client;


        public TikTokClientTest()
        {
            var httpClient = new HttpClient();
            _client = new TikTokClient(EnvEnum.Dev,httpClient);
        }

        [Fact]
        public async Task TermGet()
        {

            TermGetRequestModel model = new TermGetRequestModel() {
                advertiser_id = 6969504072827191298,
                lang = "EN",
                term_type= "LeadAds"

            };
            var res = _client.GetRequestAsync(new TermGetRequest("fd7a075680aee6505fb0206d983eb848f345077b", model));

            Assert.Equal("", res.message);
        }

        [Fact]
        public async Task TermConfirm()
        {

            TermConfirmRequestModel model = new TermConfirmRequestModel()
            {
                advertiser_id = 6969504072827191298,
                term_type = "LeadAds"
            };

            var res = _client.PostRequestAsync(new TermConfirmRequest("fd7a075680aee6505fb0206d983eb848f345077b", model));

            Assert.Equal("", res.message);
        }


        [Fact]
        public async Task CatalogDelete()
        {

            CatalogDeleteRequestModel model = new CatalogDeleteRequestModel()
            {
                advertiser_id = 6969504072827191298,
                catalog_id=1111
            };

            var res = _client.PostRequestAsync(new CatalogDeleteRequest("fd7a075680aee6505fb0206d983eb848f345077b", model));

            Assert.Equal("", res.message);
        }


        [Fact]
        public async Task AudienceCreate()
        {

            AudienceCreateRequestModel model = new AudienceCreateRequestModel()
            {
                advertiser_id = 6969504072827191298,
                custom_audience_name = "",
                file_paths=null,
                calculate_type=""
            };

            var res = _client.PostRequestAsync(new AudienceCreateRequest("fd7a075680aee6505fb0206d983eb848f345077b", model));

            Assert.Equal("", res.message);
        }

    }
}
