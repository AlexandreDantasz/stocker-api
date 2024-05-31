namespace stocker.api.Responses;
public class Response {
    private int Code = 200;
    public Dictionary<int, List<String>>? Data { get; set; }
    public string? Message { get; set;} 
    public Response(Dictionary<int, List<String>>? data, string? message, int code = 200) {
        Code = code;
        Data = data;
        Message = message;
    }
}