namespace stocker.api.Requests;

public abstract class Request {
    public required string emailUser { get; set; } = string.Empty;
    public required string password { get; set; } = string.Empty;
}