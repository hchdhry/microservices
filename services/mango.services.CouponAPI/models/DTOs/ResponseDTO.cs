using System;

namespace mango.services.models.DTOs;

public class ResponseDTO
{
    public object Result  { get;set; }
    public bool isSuccess { get;set; }
    public string Message { get;set; }
}
