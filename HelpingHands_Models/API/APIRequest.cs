
using HelpingHands_Common;
using static HelpingHands_Common.SD;

namespace HelpingHands_Models.API
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
