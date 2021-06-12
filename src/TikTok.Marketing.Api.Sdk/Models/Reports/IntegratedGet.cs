using System;
using System.Collections.Generic;
using System.Text;

namespace TikTok.Marketing.Api.Sdk.Models.Reports
{
    public class IntegratedGetRequest : BaseRequest<IntegratedGetRequestModel, IntegratedGetResponse>
    {
        public IntegratedGetRequest(string token, IntegratedGetRequestModel param) : base(token, param)
        {

        }
        public override string Url => "/reports/integrated/get/";
    }
    public class IntegratedGetRequestModel
    {
        public long advertiser_id { get; set; }
        public string service_type { get; set; }
        public string report_type { get; set; }
        public string data_level { get; set; }
        public List<Filter> filters { get; set; }
        public List<string> dimensions { get; set; }
        public List<string> metrics { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public bool lifetime { get; set; } = false;
        public string order_field { get; set; }
        public string order_type { get; set; }
        public long page { get; set; } = 1;
        public long page_size { get; set; } = 1000;
    }
    public class Filter
    {
        public string field_name { get; set; }
        public string filter_type { get; set; }
        public string filter_value { get; set; }
    }

    public class Dimension
    {
        public long advertiser_id { get; set; }
        public long campaign_id { get; set; }
        public long adgroup_id { get; set; }
        public long ad_id { get; set; }
        public string stat_time_day { get; set; }
        public string stat_time_hour { get; set; }
        public string ac { get; set; }
        public string age { get; set; }
        public string country_code { get; set; }
        public string interest_category { get; set; }
        public string interest_category_v2 { get; set; }
        public string gender { get; set; }
        public string language { get; set; }
        public string placement { get; set; }
        public string platform { get; set; }

    }

    public class Metric
    {
        public string spend { get; set; }
        public string impressions { get; set; }
        public string cpc { get; set; }
        public string cpm { get; set; }
        public string clicks { get; set; }
    }

    public class ListData
    {
        public Dimension dimensions { get; set; }

        public Metric metrics { get; set; }
    }
    public class IntegratedGetResponse : BaseResponse<IntegratedGetResponseModel>
    {

    }

    public class IntegratedGetResponseModel
    {
        public List<ListData> list { get; set; }
        public PageInfo page_info { get; set; }
    }
}
