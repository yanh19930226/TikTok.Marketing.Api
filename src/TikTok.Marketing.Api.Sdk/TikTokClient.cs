using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TikTok.Marketing.Api.Sdk.Models;

namespace TikTok.Marketing.Api.Sdk
{
    public  class TikTokClient
    {
        private readonly EnvEnum _envEnum;
        private HttpClient _client { get; }

        public TikTokClient(EnvEnum envEnum, HttpClient client)
        {
            _envEnum = envEnum;
            _client = client;
        }
        private class JsonContent : StringContent
        {
            public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            { }
        }

        private string GetApiBaseUrl()
        {
            switch (_envEnum)
            {
                case EnvEnum.Dev:
                    return "https://sandbox-ads.tiktok.com/open_api/v1.2";
                case EnvEnum.Prod:
                    return "https://ads.tiktok.com/open_api/v1.2";
                default:
                    return "https://ads.tiktok.com/open_api/v1.2";
            }
        }

        /// <summary>
        /// 构造字典
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Dictionary<string, string> ToDictionary(object obj)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            Type t = obj.GetType(); // 获取对象对应的类， 对应的类型

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance); // 获取当前type公共属性

            foreach (PropertyInfo p in pi)
            {
                MethodInfo m = p.GetGetMethod();

                if (m != null && m.IsPublic)
                {
                    // 进行判NULL处理 
                    if (m.Invoke(obj, new object[] { }) != null)
                    {
                        if (m.ReturnType == typeof(string))
                        {
                            map.Add(p.Name, (string)m.Invoke(obj, new object[] { })); // 向字典添加元素
                        }
                        else
                        {
                            map.Add(p.Name, JsonConvert.SerializeObject(m.Invoke(obj, new object[] { }))); // 向字典添加元素
                        }
                    }
                }
            }

            return map.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);
        }

        /// <summary>
        ///构造查询字符串
        /// </summary>
        /// <param name="request">RestRequest</param>
        /// <returns>Query parameters in canonical order with URL encoding</returns>
        private static string ExtractCanonicalQueryString<T, K>(BaseRequest<T, K> request)
        {
            var sortedqueryParameters = ToDictionary(request.Param);

            StringBuilder canonicalQueryString = new StringBuilder();

            foreach (var key in sortedqueryParameters.Keys)
            {
                if (canonicalQueryString.Length > 0)
                {
                    canonicalQueryString.Append("&");
                }
                canonicalQueryString.AppendFormat("{0}={1}",
                   key,
                   sortedqueryParameters[key]);
            }

            return canonicalQueryString.ToString();

        }

        public K GetRequestAsync<T,K>(BaseRequest<T,K> request)
        {

            var host = GetApiBaseUrl();

            var queryStr = ExtractCanonicalQueryString(request);

            var uri = $"{host}{request.Url}?{queryStr}";

            _client.DefaultRequestHeaders.Clear();

            _client.DefaultRequestHeaders.Add("Access-Token",  request.Token);

            try
            {
               var res = _client.GetAsync(uri).Result;

                var content =  res.Content.ReadAsStringAsync().Result;

                K obj =  JsonConvert.DeserializeObject<K>(content);

                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public K PostRequestAsync<T, K>(BaseRequest<T, K> request)
        {

            var host = GetApiBaseUrl();

            var uri = $"{host}{request.Url}";

            //_client.DefaultRequestHeaders.Clear();

            //_client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            _client.DefaultRequestHeaders.Add("Access-Token", request.Token);


            //HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, uri);
            //msg.Headers.Authorization = new AuthenticationHeaderValue("Access-Token", request.Token);
            //msg.Content = new StringContent(JsonConvert.SerializeObject(request.Param), Encoding.UTF8, "application/json");

            //HttpResponseMessage response =  _client.SendAsync(msg).Result;

            //response.EnsureSuccessStatusCode();

            //string json = response.Content.ReadAsStringAsync().Result;

            try
            {
                var res = _client.PostAsync(uri, new JsonContent(new { request.Param })).Result;

                var content = res.Content.ReadAsStringAsync().Result;

                K obj = JsonConvert.DeserializeObject<K>(content);

                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
