namespace stocker.api.Requests.DataBase;

public class UpdateRequest : Request {
    public required string key { get; set; }
    public required string value { get; set; }
    public required string newInfo { get; set; }
}