using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TikTok.Marketing.Api.Sdk.Models;

namespace TikTok.Marketing.Api.Sdk
{
    public  class TikTokClient
    {
        private readonly EnvEnum _envEnum;

        public TikTokClient(EnvEnum envEnum)
        {
            _envEnum = envEnum;
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

        public Task<K> GetRequestAsync<T,K>(BaseRequest<T,K> request)
        {
            try
            {
                var host = GetApiBaseUrl();

                var queryStr = ExtractCanonicalQueryString(request);

                return $"{host}{request.Url}{queryStr}".WithHeader("Access-Token",  request.Token)
                    .GetAsync()
                    .ReceiveJson<K>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<K> PostRequestAsync<T, K>(BaseRequest<T, K> request)
        {
            try
            {
                var host = GetApiBaseUrl();

                return $"{host}{request.Url}".WithHeader("Access-Token", request.Token).WithHeader("Content-Type", "application/json")
                    .PostJsonAsync(request.Param)
                    .ReceiveJson<K>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
