using System;
using System.Collections.Generic;
using System.Text;

namespace TikTok.Marketing.Api.Sdk.Models.Audiences
{
    public class AudienceCreateRequest : BaseRequest<AudienceCreateRequestModel, AudienceCreateResponse>
    {

        public AudienceCreateRequest(string token, AudienceCreateRequestModel param) : base(token, param)
        {

        }
        public override string Url => "/dmp/custom_audience/create";
    }

    public class AudienceCreateRequestModel
    {
        public long advertiser_id { get; set; }
        public string custom_audience_name { get; set; }

        public string[] file_paths { get; set; }
        public string calculate_type { get; set; }
    }

    public class AudienceCreateResponse : BaseResponse<AudienceCreateResponseModel>
    {

    }

    public class AudienceCreateResponseModel
    {
        
    }
}
