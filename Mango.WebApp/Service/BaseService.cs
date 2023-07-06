using Mango.WebApp.Models;
using Mango.WebApp.Service.IService;
using Newtonsoft.Json;
using System.Text;
using static Mango.WebApp.Models.Utility.SD;

namespace Mango.WebApp.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                //when we are making a call, we are providing and configuring options on that message
                HttpRequestMessage message = new();
                //THIS SAYS THAT WE ARE ACCEPTING APPLICATION/JSON
                message.Headers.Add("Accept", "application/json");
                //token
                //we specify url to which we make calls
                message.RequestUri = new Uri(requestDTO.Url);
                //if it is a post/put request we need to serialize that data and then add it to message.content
                
                if(requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }

                //we have a request message and when we send it we get a response:
                HttpResponseMessage? apiResponse = null;
                switch (requestDTO.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }


                //now we send this and get reposne back and we are getting responseDTO and we can serialize that
                apiResponse = await client.SendAsync(message);

            
                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access deniedd" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        //retrieve the content from apiResponse
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                        return apiResponseDTO;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDTO
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
            
        }
    }
}
