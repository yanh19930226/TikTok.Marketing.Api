using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TikTok.Marketing.Api.Sdk;
using TikTok.Marketing.Api.Sdk.Models.Audiences;
using TikTok.Marketing.Api.Sdk.Models.Catalogs;
using TikTok.Marketing.Api.Sdk.Models.Reports;
using TikTok.Marketing.Api.Sdk.Models.Terms;
using TikTok.Marketing.Api.Sdk.Models.Users;
using Xunit;

namespace TikTok.Marketing.Api.SdkTest
{
    public class TikTokClientTest
    {
        private readonly TikTokClient _client;

        public TikTokClientTest()
        {
            var httpClient = new HttpClient();
            _client = new TikTokClient(EnvEnum.Prod,httpClient);
        }

        [Fact]
        public async Task TermGet()
        {

            TermGetRequestModel model = new TermGetRequestModel() {
                advertiser_id = 6968333440944996354,
                lang = "EN",
                term_type= "LeadAds"

            };
            var res = _client.Get(new TermGetRequest("a4f186b706df6cd49570b3a31e34982ec9d042a2", model));

            Assert.Equal("", res.message);
        }

        [Fact]
        public async Task TermConfirm()
        {

            TermConfirmRequestModel model = new TermConfirmRequestModel()
            {
                advertiser_id = 6968333440944996354,
                term_type = "LeadAds"
            };

            var res = _client.PostRequestAsync(new TermConfirmRequest("a4f186b706df6cd49570b3a31e34982ec9d042a2", model));

            Assert.Equal("", res.message);
        }


        [Fact]
        public async Task CatalogDelete()
        {

            CatalogDeleteRequestModel model = new CatalogDeleteRequestModel()
            {
                advertiser_id = 6968333440944996354,
                catalog_id=1111
            };

            var res = _client.Post(new CatalogDeleteRequest("a4f186b706df6cd49570b3a31e34982ec9d042a2", model));

            Assert.Equal("", res.message);
        }


        [Fact]
        public async Task AudienceCreate()
        {

            AudienceCreateRequestModel model = new AudienceCreateRequestModel()
            {
                advertiser_id = 6968333440944996354,
                custom_audience_name = "ttttt",
                file_paths=null,
                calculate_type=""
            };

            var res = _client.PostRequestAsync(new AudienceCreateRequest("a4f186b706df6cd49570b3a31e34982ec9d042a2", model));

            Assert.Equal("", res.message);
        }

        [Fact]
        public async Task IntegratedGet()
        {

            IntegratedGetRequestModel model = new IntegratedGetRequestModel()
            {
                advertiser_id = 6962451729342300162,
                service_type = "AUCTION",
                report_type = "BASIC",
                data_level= "AUCTION_ADVERTISER",
                dimensions=new List<string>() { "advertiser_id" },
                metrics = new List<string>() { "spend", "impressions", "cpc", "cpm", "clicks" },
                start_date="2021-01-06",
                end_date= "2021-06-06"
            };

            var res = _client.Get(new IntegratedGetRequest("a919c8840122a0585de25f3100436c861d23a8a9", model));

            Assert.Equal("", res.message);
        }

        [Fact]
        public async Task UserInfo()
        {

            UserInfoRequestModel model = new UserInfoRequestModel()
            {
            };

            var res = _client.Get(new UserInfoRequest("a919c8840122a0585de25f3100436c861d23a8a9", model));

            Assert.Equal("", res.message);
        }

    }
}
