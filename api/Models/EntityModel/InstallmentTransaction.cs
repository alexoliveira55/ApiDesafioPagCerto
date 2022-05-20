using System;

namespace api.Models.EntityModel
{
    public class InstallmentTransaction :EntityBase
    {
		public long Id { get; set; }
		public long NsuTransaction { get; set; }
		public decimal ValueGross { get; set; }
		public decimal ValueLiquid { get; set; }
		public int InstallmentNumber { get; set; }
		public decimal? AnticipatedValue { get; set; }
		public DateTime ExpectedDateReceivement { get; set; }
		public DateTime? DateAdvancedPayment { get; set; }

        public TransactionCard TransactionCard { get; set; }

		public static DateTime GetExpectedDate(DateTime transactionDate, int installmentNumber) 
		{
			return transactionDate.AddDays(30 * installmentNumber);
		}

    }
}
