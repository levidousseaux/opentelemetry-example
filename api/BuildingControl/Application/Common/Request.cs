using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace Application.Common;

public abstract class Request
{
    [JsonIgnore]
    public List<Notification> Errors { get; set; } = new List<Notification>();

    public abstract bool Validate();

    public void AddNotifications(ValidationResult validation)
    {
        Errors.AddRange(validation.Errors.Select(x => new Notification(x.PropertyName, x.ErrorMessage)));
    }
}
