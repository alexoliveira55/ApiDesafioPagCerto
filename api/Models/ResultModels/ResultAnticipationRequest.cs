using System;

namespace api.Models.ResultModels
{
    public class ResultAnticipationRequest 
    {
        public long Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? DateStartAnalyze { get; set; }
        public DateTime? DateEndAnalyze { get; set; }
        public int? EnResultAnticipationRequest { get; set; }
        public decimal ValueAnticipationRequest { get; set; }
        public decimal? AnticipatedValue { get; set; }

    }
}
