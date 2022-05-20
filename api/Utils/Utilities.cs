namespace api.Utils
{
    public class Utilities
    {
        public const decimal FixedRate = 0.9M;
        public const string PrefixeDesapproveTransaction = "5999";
        public const decimal PercentageAnticipation = 0.038M;

        public static decimal DiscountFixedRate(decimal value)
        {
            if (value <= FixedRate) return 0;

            return value - FixedRate;
        }
        public static decimal DiscountPercentageRate(decimal value)
        {
            if (value == 0) return 0;

            return value - (PercentageAnticipation * value);
        }

        public enum EnResultAnalizeAnticipationRequest
        {
            Disapproved,
            Approved,
            ApprovedPartially
        }
        public enum EnStatusAnticipationRequest
        {
            Pending,
            UnderAnalysis,
            Finished
        }

    }
}
