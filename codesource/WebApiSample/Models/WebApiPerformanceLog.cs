using System;

namespace WebApiSample.Models
{
    public class WebApiPerformanceLog
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public DateTime RequestDateTime { get; set; }
        public string InterfaceName { get; set; }
        public string ParamStr { get; set; }
        public decimal InterfaceTotalElapsedMilliseconds { get; set; }
        public string SqlText { get; set; }
        public decimal SqlElapsedMilliseconds { get; set; }
        public decimal UiElapsedMilisenconds { get; set; }
        public string Type { get; set; }
    }
}