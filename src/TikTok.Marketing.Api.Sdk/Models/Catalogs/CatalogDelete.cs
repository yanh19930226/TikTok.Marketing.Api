using System;
using System.Collections.Generic;
using System.Text;

namespace TikTok.Marketing.Api.Sdk.Models.Catalogs
{
    public class CatalogDeleteRequest : BaseRequest<CatalogDeleteRequestModel, CatalogDeleteResponse>
    {

        public CatalogDeleteRequest(string token, CatalogDeleteRequestModel param) : base(token, param)
        {

        }
        public override string Url => "/catalog/delete";
    }

    public class CatalogDeleteRequestModel 
    {
        public long advertiser_id { get; set; }
        public long catalog_id { get; set; }
    }

    public class CatalogDeleteResponse : BaseResponse<CatalogDeleteResponseModel>
    {
       
    }

    public class CatalogDeleteResponseModel
    {
        public long catalog_id { get; set; }
    }
}
