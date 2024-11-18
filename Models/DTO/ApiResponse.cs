using System.Net;

namespace Models.DTO;

public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; }  //  200, 400, 500
    
    public bool isSuccessfull { get; set; }

    public string Message { get; set; }

    public object Result { get; set; }  //  List, Entity
}