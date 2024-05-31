namespace stocker.api.Requests.DataBase;

public class SearchRequest : Request {
    public required string key { get; set; }
    public required string value { get; set; }
}