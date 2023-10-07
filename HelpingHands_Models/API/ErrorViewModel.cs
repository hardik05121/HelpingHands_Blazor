using HelpingHands_Common;
using static HelpingHands_Common.SD;

namespace HelpingHands_Models.API
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}