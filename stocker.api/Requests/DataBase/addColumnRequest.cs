namespace stocker.api.Requests.DataBase;

public class AddColumnRequest : Request {
    public required string column { get; set; } = string.Empty;
}
