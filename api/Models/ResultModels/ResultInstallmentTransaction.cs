using System;

namespace api.Models.ResultModels
{
    public class ResultInstallmentTransaction 
    {
        public long Id { get; set; }
        public long NsuTransaction { get; set; }
        public decimal ValueGross { get; set; }
        public decimal ValueLiquid { get; set; }
        public int InstallmentNumber { get; set; }
        public decimal? AnticipatedValue { get; set; }
        public DateTime ExpectedDateReceivement { get; set; }
        public DateTime? DateAdvancedPayment { get; set; }

    }
}
