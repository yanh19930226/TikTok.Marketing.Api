using Flurl.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        /// <summary>
        /// 获取登入TikTok链接
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTikTokLoginUrl")]
        public TikTokResult<string> GetTikTokLoginUrl()
        {

            var res = new TikTokResult<string>();

            var guid = Guid.NewGuid().ToString();

            var uri = "https://ads.tiktok.com/marketing_api/auth?app_id=" + _settings.TikTokConfig.AppId+ "&state="+ guid + "&redirect_uri=" + _settings.TikTokConfig.RedirectUri;

            res.Success(uri, TikTokResultCode.Succeed.ToString());

            return res;
        }

        /// <summary>
        /// 根据授权码获取长期AccessToken
        /// </summary>
        /// <param name="code">授权码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTokenUserAsync")]
        public async Task<TikTokResult<AccessTokenResponse>> GetTokenUserAsync(string code)
        {
            var res = new TikTokResult<AccessTokenResponse>();

            AccessTokenRequest req = new AccessTokenRequest() { 
                app_id= _settings.TikTokConfig.AppId,
                auth_code = code,
                secret  = _settings.TikTokConfig.Secret,
            };

            var host = "";
            if (_settings.TikTokConfig.IsDev)
            {
                host = "https://sandbox-ads.tiktok.com/open_api/v1.2";
            }
            else
            {
                host = "https://ads.tiktok.com/open_api/v1.2";
            }
            var uri = "/oauth2/access_token/";
            try
            {
                var result= $"{host}{uri}".WithHeader("Content-Type", "application/json")
                    .PostJsonAsync(req)
                    .ReceiveJson<AccessTokenResponse>().Result;
                res.Result = result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return res;

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
