using System;
using System.Collections.Generic;

namespace api.Models.ResultModels
{
    public class ResultTransactionCard
    {
        public long Nsu { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? FailureDate { get; set; }
        public bool Anticipated { get; set; }
        public bool ConfirmationAcquirer { get; set; }
        public decimal ValueGross { get; set; }
        public decimal ValueLiquid { get; set; }
        public decimal RateTransaction { get; set; }
        public int NumberOfInstallments { get; set; }
        public int LastNumberCar { get; set; }
        public List<ResultInstallmentTransaction> InstallmentTransactions { get; set; }
    }
}
