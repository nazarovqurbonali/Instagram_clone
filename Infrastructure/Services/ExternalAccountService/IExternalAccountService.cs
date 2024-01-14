using Domain.Dtos.ExternalAccountDto;
using Domain.Filters.ExternalAccountFilter;
using Domain.Responses;

namespace Infrastructure.Services.ExternalAccountService;

public interface IExternalAccountService
{
    Task<PagedResponse<List<ExternalAccountDto>>> GetExternalAccountsByName(ExternalAccountFilter filter);
    Task<Response<ExternalAccountDto>> GetExternalAccountById(int id);
    Task<Response<ExternalAccountDto>> AddExternalAccount(ExternalAccountDto ExternalAccount);
    Task<Response<ExternalAccountDto>> UpdateExternalAccount(ExternalAccountDto ExternalAccount);
    Task<Response<bool>> DeleteExternalAccount(int id);
}