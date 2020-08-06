using AutoMapper;

using YnabReportsAPI.Transactions.Models;
using YnabReportsAPI.Transactions.ViewModels;

namespace YnabReportsAPI.Profiles
{
    public class YnabReportsProfiles : Profile
    {
        public YnabReportsProfiles()
        {
            CreateMap<Transaction, TransactionViewModel>();
        }
    }
}
