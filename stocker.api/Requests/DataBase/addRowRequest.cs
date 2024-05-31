namespace stocker.api.Requests.DataBase;

public class AddRowRequest : Request {
    public required string [] row { get; set; }
}
