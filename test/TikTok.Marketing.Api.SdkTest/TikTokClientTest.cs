using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TikTok.Marketing.Api.Sdk;
using TikTok.Marketing.Api.Sdk.Models.Terms;
using Xunit;

namespace TikTok.Marketing.Api.SdkTest
{
    public class TikTokClientTest
    {
        private readonly TikTokClient _client;
        public TikTokClientTest()
        {
            _client = new TikTokClient(EnvEnum.Dev);
        }

        [Fact]
        public async Task TermGet()
        {

            TermGetRequestModel model = new TermGetRequestModel() { 
            
            
            };
            var res = _client.GetRequestAsync(new TermGetRequest("", model));

            Assert.Equal("", res.Result.message);
        }

        [Fact]
        public async Task TermConfirm()
        {

            TermConfirmRequestModel model = new TermConfirmRequestModel()
            {


            };
            var res = _client.PostRequestAsync(new TermConfirmRequest("", model));

            Assert.Equal("", res.Result.message);
        }

    }
}
