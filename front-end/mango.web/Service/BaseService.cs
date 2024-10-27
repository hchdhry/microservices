using System;
using System.Text;
using System.Net;
using mango.web.models.DTO;
using mango.web.Models.DTO;
using mango.web.Service.IService;
using mango.web.Utilities;
using Newtonsoft.Json;
namespace mango.web.Service;


public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public BaseService(IHttpClientFactory clientFactory)
    {
        _httpClientFactory = clientFactory;
    }
    public async Task<ResponseDTO> SendAsync(RequestDTO requestDTO)
    {
        HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
        HttpRequestMessage message = new HttpRequestMessage();
        message.Headers.Add("Accept","application/json");
        message.RequestUri = new Uri(requestDTO.Url);
        
        if (requestDTO.Data != null)
        {
            message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");

        }
        HttpResponseMessage apiResponse = new HttpResponseMessage();
        switch (requestDTO.APItype)
        {
            case SD.APIType.POST:
                message.Method = HttpMethod.Post;
                break;
            case SD.APIType.PUT:
                message.Method = HttpMethod.Put;
                break;
            case SD.APIType.DELETE:
                message.Method = HttpMethod.Delete;
                break;
            default:
                message.Method = HttpMethod.Get;
                break;
        }

       apiResponse = await client.SendAsync(message);

        switch (apiResponse.StatusCode)
        {
            case HttpStatusCode.Unauthorized:
                return new ResponseDTO { Message = "Please Login", isSuccess = false, Result = null };
            case HttpStatusCode.Forbidden:
                return new ResponseDTO { Message = "You are not authorized to perform this action", isSuccess = false, Result = null };
            case HttpStatusCode.NotFound:
                return new ResponseDTO { Message = "Resource not found", isSuccess = false, Result = null };
            case HttpStatusCode.BadRequest:
                return new ResponseDTO { Message = "Please provide the required information", isSuccess = false, Result = null };
            default:
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
          
                Console.WriteLine($"Response Content: {apiContent}");
               
                var responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                if (responseDTO != null)
                {
                    responseDTO.isSuccess = apiResponse.IsSuccessStatusCode;
                    responseDTO.Message = apiResponse.IsSuccessStatusCode ? "Success" : "Error";
                }
                return responseDTO;  

        }


    }
}
