using System;
using System.Collections.Generic;
using System.Text;

namespace TikTok.Marketing.Api.Sdk.Models.Tokens
{

    public class AccessTokenRequest
    {
       
        public string app_id { get; set; }
        
        public string auth_code { get; set; }
       
        public string secret { get; set; }
    }

    public class AccessTokenResponse:BaseResponse<AccessTokenResponseModel>
    {

    }

    public class AccessTokenResponseModel
    {
        /// <summary>
        /// access_token
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// scope
        /// </summary>
        public List<int> scope { get; set; }
        /// <summary>
        /// advertiser_ids
        /// </summary>
        public List<int> advertiser_ids { get; set; }

    }
}
