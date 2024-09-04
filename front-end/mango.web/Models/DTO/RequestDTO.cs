using System;
using static mango.web.Utilities.SD;
namespace mango.web.Models.DTO;

public class RequestDTO
{
    public APIType APItype {get;set;} = APIType.GET;
    public string Url {get;set;}    
    public object Data {get;set;}
    public string AccessToken{get;set;}

}
