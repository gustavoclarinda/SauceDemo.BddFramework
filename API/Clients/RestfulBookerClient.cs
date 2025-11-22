using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SauceDemo.BddFramework.Api.Models;

namespace SauceDemo.BddFramework.Api.Clients
{
    public class RestfulBookerClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        public RestfulBookerClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://restful-booker.herokuapp.com")
            };

            // Headers defaults
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> PingAsync()
        {
            // /ping retorn 201 if OK
            return await _httpClient.GetAsync("/ping");
        }

        public async Task<CreateBookingResponse> CreateBookingAsync(Booking booking)
        {
            var json = JsonConvert.SerializeObject(booking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/booking", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Create booking failed: {(int)response.StatusCode} - {responseBody}");
            }

            return JsonConvert.DeserializeObject<CreateBookingResponse>(responseBody);
        }

        public async Task<Booking> GetBookingAsync(int bookingId)
        {
            var response = await _httpClient.GetAsync($"/booking/{bookingId}");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Get booking failed: {(int)response.StatusCode} - {responseBody}");
            }

            return JsonConvert.DeserializeObject<Booking>(responseBody);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
