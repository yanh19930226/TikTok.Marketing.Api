using System;
using System.Collections.Generic;
using System.Text;

namespace TikTok.Marketing.Api.Sdk.Models.Terms
{
    public class TermGetRequest : BaseRequest<TermGetRequestModel,TermGetResponse>
    {
        public TermGetRequest(string token, TermGetRequestModel param) :base(token, param)
        {

        }
        public override string Url => "/term/get";
    }


    public class TermGetRequestModel
    {
         public long advertiser_id { get; set; }
        public string lang { get; set; }
        public string term_type { get; set; }
    }

    public class TermGetResponse:BaseResponse<TermGetResponseModel>
    {
        
    }

    public class TermGetResponseModel
    {
        public string[] terms { get; set; }
    }
}
