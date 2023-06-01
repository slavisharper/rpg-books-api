namespace RpgBooks.Modules.Identity.Application.EventHandlers.OnUserRegistered;

using RpgBooks.Libraries.Module.Application.Services.Email;
using RpgBooks.Libraries.Module.Domain.Events;
using RpgBooks.Modules.Identity.Domain.Events;

using System.Threading;
using System.Threading.Tasks;

internal class SendRegisteredEmailHandler : IDomainEventHandler<UserRegisteredEvent>
{
    private readonly IEmailSender emailSender;

    public SendRegisteredEmailHandler(IEmailSender emailSender)
    {
        this.emailSender = emailSender;
    }

    public async Task HandleEvent(UserRegisteredEvent evnt, CancellationToken cancellationToken = default)
    {
        var bodyModel = new RegisteredEmailModel(evnt.Email, evnt.EmailConfirmationToken);
        var emailModel = EmailModel.Create<RegisteredEmailModel>(evnt.Email, "Welcome to RPB", bodyModel);

        await this.emailSender.SendEmailAsync(emailModel, cancellationToken);
    }
}
