namespace stocker.api.Requests.DataBase;

public class UpdateColumnRequest : Request {
    public required string oldName { get; set; } = string.Empty;
    public required string newName { get; set; } = string.Empty;
}