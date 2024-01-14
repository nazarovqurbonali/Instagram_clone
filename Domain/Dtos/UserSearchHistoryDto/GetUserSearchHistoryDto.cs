using Domain.Dtos.UserDto;

namespace Domain.Dtos.UserSearchHistoryDto;

public class GetUserSearchHistoryDto : UserSearchHistoryDto
{
    public int Id { get; set; }
    public GetUserDto Users { get; set; } = null!;
}