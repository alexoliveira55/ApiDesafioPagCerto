using api.Models.ViewModel;
using api.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.EntityModel
{
    public class TransactionCard : EntityBase
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

        public List<InstallmentTransaction> InstallmentTransactions { get; set; }
        public List<TransactionAnticipationRequest> TransactionAnticipationRequests{ get; set; }

        public static TransactionCard ParseViewModel(TransactionCardViewModel viewModel)
        {
            return new TransactionCard
            {
                Nsu = 0,
                TransactionDate = DateTime.Now,
                ValueGross = viewModel.ValueGross,
                ValueLiquid = Utilities.DiscountFixedRate(viewModel.ValueGross),
                Anticipated = false,
                InstallmentTransactions = null,
                TransactionAnticipationRequests = null,
                NumberOfInstallments = viewModel.NumberOfInstallments <= 0 ? 1 : viewModel.NumberOfInstallments,
                LastNumberCar = Convert.ToInt32(viewModel.NumberCard.Substring(viewModel.NumberCard.Length - 4, 4)),
                ConfirmationAcquirer = !viewModel.NumberCard.StartsWith(Utilities.PrefixeDesapproveTransaction)
            };
        }

        public void ApproveDisapproveTransaction()
        {
            if (ConfirmationAcquirer)
            {
                ApprovalDate = DateTime.Now;
                FailureDate = null;
                InstallmentTransactions = new List<InstallmentTransaction>();
            }
            else
            {
                ApprovalDate = null;
                FailureDate = DateTime.Now;
                InstallmentTransactions = null;
            }
        }
        [JsonIgnore]
        [NotMapped]
        public decimal ValueGrossForInstallment
        {
            get
            {
                return ValueGross / NumberOfInstallments;
            }
        }
        [JsonIgnore]
        [NotMapped]
        public decimal ValueLiquidForInstallment
        {
            get
            {
                return ValueLiquid / NumberOfInstallments;
            }
        }

    }
}
