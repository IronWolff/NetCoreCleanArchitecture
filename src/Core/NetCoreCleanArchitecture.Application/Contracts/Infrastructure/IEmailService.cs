using NetCoreCleanArchitecture.Application.Models.Mail;

namespace NetCoreCleanArchitecture.Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}