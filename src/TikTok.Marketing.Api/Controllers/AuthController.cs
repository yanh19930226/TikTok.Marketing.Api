using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TikTok.Marketing.Api.Sdk;
using TikTok.Marketing.Api.Sdk.Models.Tokens;

namespace TikTok.Marketing.Api.Controllers
{
    /// <summary>
    /// 权限认证管理
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("Api/Auth")]
    public class AuthController : Controller
    {
        private readonly Appsettings _settings;

        public AuthController(IOptions<Appsettings> options)
        {
            _settings = options.Value;
        }
        private class JsonContent : StringContent
        {
            public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            { }
        }
        /// <summary>
        /// 获取登入TikTok链接
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTikTokLoginUrl")]
        public TikTokResult<string> GetTikTokLoginUrl()
        {

            var res = new TikTokResult<string>();

            var url = "https://ads.tiktok.com/marketing_api/auth?app_id=6969066684241952770&state=your_custom_params&redirect_uri=https%3A%2F%2Fyanh.fun%2FAuth%2FCode&rid=c6z9spimr96";

            res.Success(url, TikTokResultCode.Succeed.ToString());

            return res;
        }

        /// <summary>
        /// 根据授权码获取长期AccessToken
        /// </summary>
        /// <param name="code">授权码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTokenUserAsync")]
        public  AccessTokenResponse GetTokenUserAsync(string code)
        {

            AccessTokenRequest req = new AccessTokenRequest() { 
                app_id= _settings.TikTokConfig.AppId,
                auth_code = code,
                secret = _settings.TikTokConfig.Secret,
            };

            var url = "";

            if (_settings.TikTokConfig.IsDev)
            {
                url = "https://sandbox-ads.tiktok.com/open_api/v1.2/oauth2/access_token/";
            }
            else
            {
                url = "https://ads.tiktok.com/open_api/v1.2/oauth2/access_token/";
            }
            
            try
            {
                HttpClient httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

                HttpContent httpContent= new StringContent(JsonConvert.SerializeObject(req));

                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var result = httpClient.PostAsync(url, httpContent).Result;

                var content = result.Content.ReadAsStringAsync().Result;

                AccessTokenResponse obj = JsonConvert.DeserializeObject<AccessTokenResponse>(content);

                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new AccessTokenResponse();
        }

        //    /// <summary>
        //    /// 用户短期Token换取用户长期Token
        //    /// </summary>
        //    /// <param name="userToken">用户短期Token</param>
        //    /// <returns></returns>
        //    [HttpGet]
        //    [Route("GetLongTokenUserAsync")]
        //    public async Task<TikTokResult<UserAuthTokenResponse>> GetLongTokenUserAsync(string userToken)
        //    {

        //        var _client = new HttpClient();

        //        var res = new TikTokResult<UserAuthTokenResponse>();

        //        var uri = "https://graph.facebook.com/v9.0/oauth/access_token?grant_type=fb_exchange_token&client_id=" + _settings.Facebook.ClientId + "&client_secret=" + _settings.Facebook.ClientSecret + "&fb_exchange_token=" + userToken;

        //        var httpResponse = await _client.GetAsync(uri);

        //        var content = await httpResponse.Content.ReadAsStringAsync();

        //        UserAuthTokenResponse obj = JsonConvert.DeserializeObject<UserAuthTokenResponse>(content);

        //        if (httpResponse.StatusCode != HttpStatusCode.OK)
        //        {
        //            res.Failed(httpResponse.StatusCode.ToString());
        //        }
        //        else
        //        {
        //            res.Success(httpResponse.StatusCode.ToString());
        //        }
        //        res.Result = obj;

        //        return res;

        //    }
    }
}
