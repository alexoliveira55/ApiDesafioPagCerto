using static api.Utils.Utilities;

namespace api.Models.EntityModel
{
    public class TransactionAnticipationRequest : EntityBase
    {
        public TransactionAnticipationRequest(){}
        public TransactionAnticipationRequest(TransactionCard transactionCard)
        {
            this.NsuTransction = transactionCard.Nsu;
            this.StatusAnticipation = EnStatusAnticipationRequest.Pending.GetHashCode();
        }
        public long IdAnticipationRequest { get; set; }
        public long NsuTransction { get; set; }
        public int StatusAnticipation { get; set; }
        public int? ResultAnalizeTransaction { get; set; }

        public AnticipationRequest AnticipationRequest { get; set; }
        public TransactionCard TransactionCard { get; set; }
    }

}
