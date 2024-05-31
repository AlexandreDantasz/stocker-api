namespace stocker.api.Requests.DataBase;

public class DeleteRowRequest : Request {
    public required string [] rowToDelete { get; set; }
}
