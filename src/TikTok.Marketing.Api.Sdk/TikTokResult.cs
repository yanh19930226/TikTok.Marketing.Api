using System;
using System.Collections.Generic;
using System.Text;

namespace TikTok.Marketing.Api.Sdk
{
    /// <summary>
    /// 响应实体
    /// </summary>
    public class TikTokResult
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public TikTokResultCode Code { get; set; }
        /// <summary>
        /// 响应信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 成功
        /// </summary>
        public bool IsSuccess => Code == TikTokResultCode.Succeed;
        /// <summary>
        /// 时间戳(毫秒)
        /// </summary>
        public long Timestamp { get; } = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void Success(string message = "")
        {
            Message = message;
            Code = TikTokResultCode.Succeed;
        }
        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void Failed(string message = "")
        {
            Message = message;
            Code = TikTokResultCode.Failed;
        }
        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="exexception></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void Failed(Exception exception)
        {
            Message = exception.InnerException?.StackTrace;
            Code = TikTokResultCode.Failed;
        }
    }
    /// <summary>
    /// 响应实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TikTokResult<T> : TikTokResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public void Success(T result = default(T), string message = "")
        {
            Message = message;
            Code = TikTokResultCode.Succeed;
            Result = result;
        }
    }
    public enum TikTokResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 1,
        /// <summary>
        /// 失败
        /// </summary>
        Failed = 0
    }
}
