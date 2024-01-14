using System.Net;
using AutoMapper;
using Domain.Dtos.ExternalAccountDto;
using Domain.Entities.User;
using Domain.Filters.ExternalAccountFilter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ExternalAccountService;

public class ExternalAccountService : IExternalAccountService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ExternalAccountService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResponse<List<ExternalAccountDto>>> GetExternalAccountsByName(ExternalAccountFilter filter)
    {
        try
        {
            var externalAccounts = _context.ExternalAccounts.AsQueryable();
            if (!string.IsNullOrEmpty(filter.AccountName))
                externalAccounts = externalAccounts.Where(e =>
                    e.FacebookEmail.ToLower().Contains(filter.AccountName.ToLower()) ||
                    e.TwitterUsername.ToLower().Contains(filter.AccountName.ToLower()));
            var response = await externalAccounts
                .Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var mapped = _mapper.Map<List<ExternalAccountDto>>(response);
            var totalRecord = externalAccounts.Count();
            return new PagedResponse<List<ExternalAccountDto>>(mapped, filter.PageNumber, filter.PageSize,
                totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<ExternalAccountDto>>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<ExternalAccountDto>> GetExternalAccountById(int id)
    {
        try
        {
            var externalAccount = await _context.ExternalAccounts.FindAsync(id);
            var mapped = _mapper.Map<ExternalAccountDto>(externalAccount);
            return new Response<ExternalAccountDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<ExternalAccountDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<ExternalAccountDto>> AddExternalAccount(ExternalAccountDto externalAccountDto)
    {
        try
        {
            var externalAccount = _mapper.Map<ExternalAccount>(externalAccountDto);
            await _context.ExternalAccounts.AddAsync(externalAccount);
            await _context.SaveChangesAsync();
            var mapped = _mapper.Map<ExternalAccountDto>(externalAccount);
            return new Response<ExternalAccountDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<ExternalAccountDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<ExternalAccountDto>> UpdateExternalAccount(ExternalAccountDto externalAccountDto)
    {
        try
        {
            var externalAccount = _mapper.Map<ExternalAccount>(externalAccountDto);
            _context.ExternalAccounts.Update(externalAccount);
            await _context.SaveChangesAsync();
            var mapped = _mapper.Map<ExternalAccountDto>(externalAccount);
            return new Response<ExternalAccountDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<ExternalAccountDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteExternalAccount(int id)
    {
        try
        {
            var externalAccount = await _context.ExternalAccounts.FindAsync(id);
            if (externalAccount == null)
                return new Response<bool>(HttpStatusCode.BadRequest, "ExternalAccount not found");
            _context.ExternalAccounts.Remove(externalAccount);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.BadRequest, e.Message);
        }
    }
}