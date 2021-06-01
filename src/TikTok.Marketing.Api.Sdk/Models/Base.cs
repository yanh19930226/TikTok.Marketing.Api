using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TikTok.Marketing.Api.Sdk.Models
{
    public abstract class BaseRequest<T,K>
    {
        public BaseRequest(string token,T parm)
        {
            this.Token = token;
            this.Param = parm;
        }

        public T Param { get; set; }
        public string Token { get; set; }

        public abstract string Url { get; }
    }
    public class BaseResponse<T>
    {
        /// <summary>
        /// message
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// code
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// data
        /// </summary>
        [JsonConverter(typeof(DataConvert))]
        public T data { get; set; }
        /// <summary>
        /// request_id
        /// </summary>
        public string request_id { get; set; }
    }

    public class DataConvert : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var result = serializer.Deserialize(reader);

                if (result.ToString() == "[]")
                    return null;

                return JsonConvert.DeserializeObject(result.ToString(), objectType);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Console.WriteLine(111);
        }
    }
}
