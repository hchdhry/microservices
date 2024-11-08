using System;

namespace Mango.Services.ProductAPI.Models.DTOs;

public class ResponseDTO
{
    public object Result  { get;set; }
    public bool isSuccess { get;set; }
    public string Message { get;set; }
}
