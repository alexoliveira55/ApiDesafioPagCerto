using System;
using System.Collections.Generic;

namespace api.Models.EntityModel
{
    public class AnticipationRequest : EntityBase
    {
        public AnticipationRequest()
        {
            RequestDate = DateTime.Now;
            ResultAnticipationRequest = null;
        }

        public long Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? DateStartAnalyze { get; set; }
        public DateTime? DateEndAnalyze { get; set; }
        public int? ResultAnticipationRequest { get; set; }
        public decimal ValueAnticipationRequest { get; set; }
        public decimal? AnticipatedValue { get; set; }
        public List<TransactionAnticipationRequest> TransactionAnticipationRequests { get; set; }

    }

}
