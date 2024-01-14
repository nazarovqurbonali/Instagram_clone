using Domain.Dtos.LocationDto;
using Domain.Filters.LocationFilter;
using Domain.Responses;

namespace Infrastructure.Services.LocationDto;

public interface ILocationService
{
    Task<PagedResponse<List<GetLocationDto>>> GetLocations(LocationFilter filter);
    Task<Response<GetLocationDto>> GetLocationById(int id);
    Task<Response<GetLocationDto>> AddLocation(AddLocationDto addLocation);
    Task<Response<GetLocationDto>> UpdateLocation(UpdateLocationDto addLocation);
    Task<Response<bool>> DeleteLocation(int id);
}