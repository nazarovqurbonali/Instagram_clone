using System.Net;
using AutoMapper;
using Domain.Dtos.LocationDto;
using Domain.Entities;
using Domain.Filters.LocationFilter;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Services.LocationDto;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.LocationService;

public class LocationService : ILocationService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public LocationService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PagedResponse<List<GetLocationDto>>> GetLocations(LocationFilter filter)
    {
        try
        {
            var locations = _context.Locations.AsQueryable();
            if (!string.IsNullOrEmpty(filter.City))
                locations = locations.Where(l =>
                    l.City.ToLower().Contains(filter.City.ToLower()));
            if (!string.IsNullOrEmpty(filter.Country))
                locations = locations.Where(l =>
                    l.City.ToLower().Contains(filter.Country.ToLower()));
            if (!string.IsNullOrEmpty(filter.ZipCode))
                locations = locations.Where(l =>
                    l.City.ToLower().Contains(filter.ZipCode.ToLower()));
            if (!string.IsNullOrEmpty(filter.State))
                locations = locations.Where(l =>
                    l.City.ToLower().Contains(filter.State.ToLower()));
            var response = await locations
                .Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var mapped = _mapper.Map<List<GetLocationDto>>(response);
            var totalRecord = locations.Count();
            return new PagedResponse<List<GetLocationDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetLocationDto>>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<GetLocationDto>> GetLocationById(int id)
    {
        try
        {
            var location = await _context.Locations.FindAsync(id);
            var mapped = _mapper.Map<GetLocationDto>(location);
            return new Response<GetLocationDto>(mapped);
            
        }
        catch (Exception e)
        {
            return new Response<GetLocationDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<GetLocationDto>> AddLocation(AddLocationDto addLocation)
    {
        try
        {
            var location = _mapper.Map<Location>(addLocation);
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
            var mapped = _mapper.Map<GetLocationDto>(location);
            return new Response<GetLocationDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetLocationDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<GetLocationDto>> UpdateLocation(UpdateLocationDto addLocation)
    {
        try
        {
            var location = _mapper.Map<Location>(addLocation);
            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
            var mapped = _mapper.Map<GetLocationDto>(location);
            return new Response<GetLocationDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetLocationDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteLocation(int id)
    {
        try
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null) return new Response<bool>(HttpStatusCode.BadRequest, "Location not found");
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.BadRequest, e.Message);
        }
    }
}