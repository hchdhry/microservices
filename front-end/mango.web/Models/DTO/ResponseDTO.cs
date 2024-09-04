using System;

namespace mango.web.models.DTO;

public class ResponseDTO
{
    public object Result { get; set; }
    public bool isSuccess { get; set; }
    public string Message { get; set; }
}
