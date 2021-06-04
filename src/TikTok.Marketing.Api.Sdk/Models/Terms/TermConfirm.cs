using System;
using System.Collections.Generic;
using System.Text;

namespace TikTok.Marketing.Api.Sdk.Models.Terms
{
    public class TermConfirmRequest : BaseRequest<TermConfirmRequestModel,TermConfirmResponse>
    {
        public TermConfirmRequest(string token, TermConfirmRequestModel param) :base(token, param)
        {

        }
        public override string Url => "/term/confirm";
    }

    public class TermConfirmRequestModel 
    {
        public long advertiser_id { get; set; }
        public string term_type { get; set; }
    }

    public class TermConfirmResponse:BaseResponse<TermConfirmResponseModel>
    {

    }
    public class TermConfirmResponseModel
    {

    }
}
