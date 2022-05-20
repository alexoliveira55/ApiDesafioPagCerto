using api.Models.EntityModel;
using api.Models.ResultModels;
using AutoMapper;

namespace api.Infrastructure.AutoMapper
{
    public class AutoMapperConfig:  Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AnticipationRequest, ResultAnticipationRequest>().ReverseMap();
            CreateMap<TransactionCard, ResultTransactionCard>().ReverseMap();
            CreateMap<TransactionAnticipationRequest, ResultTransactionAnticipationRequest>().ReverseMap();
            CreateMap<InstallmentTransaction, ResultInstallmentTransaction>().ReverseMap();
        }
    }
}
