using System;
using System.Collections.Generic;
using System.Text;

namespace TikTok.Marketing.Api.Sdk.Models.Users
{
    public class UserInfoRequest : BaseRequest<UserInfoRequestModel, UserInfoResponse>
    {
        public UserInfoRequest(string token, UserInfoRequestModel param) : base(token, param)
        {

        }
        public override string Url => "/user/info/";
    }

    public class UserInfoRequestModel
    {
       
    }

    public class UserInfoResponse : BaseResponse<UserInfoResponseModel>
    {

    }

    public class UserInfoResponseModel
    {
        public string display_name { get; set; }
        public string email { get; set; }
        public long id { get; set; }
    }
}
