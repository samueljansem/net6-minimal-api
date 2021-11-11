using Flunt.Notifications;
using Flunt.Validations;
using Minimal.API.Models;

namespace Minimal.API.ViewModels;

public class PersonViewModel : Notifiable<Notification>
{
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public Person MapTo(Guid id)
    {
        var contract = new Contract<Notification>()
            .Requires()
            .IsNotNull(FullName, "Full name field is required.")
            .IsNotNull(DateOfBirth, "Date of birth field is required.");

        AddNotifications(contract);

        return new Person(id == Guid.Empty ? Guid.NewGuid() : id, FullName, DateOfBirth);
    }

    public Person MapTo()
    {
        var contract = new Contract<Notification>()
            .Requires()
            .IsNotNull(FullName, "Full name field is required.")
            .IsNotNull(DateOfBirth, "Date of birth field is required.");

        AddNotifications(contract);

        return new Person(Guid.NewGuid(), FullName, DateOfBirth);
    }
}
