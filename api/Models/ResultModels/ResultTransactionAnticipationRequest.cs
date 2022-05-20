namespace api.Models.ResultModels
{
    public class ResultTransactionAnticipationRequest
    {
        public long IdAnticipationRequest { get; set; }
        public long NsuTransction { get; set; }
        public int StatusAnticipation { get; set; }
        public int? ResultAnalizeTransaction { get; set; }

        public ResultAnticipationRequest AnticipationRequest { get; set; }
        public ResultTransactionCard TransactionCard { get; set; }

    }
}
