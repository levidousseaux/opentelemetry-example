namespace Application.Common;

public abstract class Result
{
    public IEnumerable<Notification> Errors { get; set; } = new List<Notification>();
    public IEnumerable<Notification> Notifications { get; set; } = new List<Notification>();
    public int? Status { get; set; }
}