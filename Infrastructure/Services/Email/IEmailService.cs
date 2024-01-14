using Domain.Dtos;
using Domain.Dtos.MessagesDto;
using MimeKit.Text;

namespace Infrastructure.Services;

public interface IEmailService
{
    void SendEmail(MessagesDto model,TextFormat format);
}