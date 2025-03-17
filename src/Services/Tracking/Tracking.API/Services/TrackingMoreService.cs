using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Tracking.API.Dtos;
using Microsoft.Extensions.Logging;

namespace Tracking.API.Services;

public class TrackingMoreService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<TrackingMoreService> _logger;
    private const string TrackingMoreApiUrl = "https://api.trackingmore.com/v4/trackings";

    public TrackingMoreService(HttpClient httpClient, ILogger<TrackingMoreService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _httpClient.DefaultRequestHeaders.Add("Tracking-Api-Key", Environment.GetEnvironmentVariable("TRACKINGMORE_API_KEY") ?? "kk6fbzw5-2fra-ogto-tim4-ooaxf66npfy8");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    // ✅ Fetch Tracking Information
    public async Task<string> CreateTrackingOrder(OrderStatusDto orderStatusDto)
    {
        try
        {
            _logger.LogInformation($"Creating tracking order for {orderStatusDto.TrackingId.ToString()} in TrackingMore");

            var requestBody = new
            {
                tracking_number = orderStatusDto.TrackingId.ToString(), 
                courier_code = orderStatusDto.Courier,  
                order_number = orderStatusDto.OrderId.ToString(),  
                customer_name = orderStatusDto.CustomerName,
                customer_email = orderStatusDto.EmailAddress,
                recipient_postcode = orderStatusDto.ZipCode
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var requestUrl = $"{TrackingMoreApiUrl}/create"; 
            var response = await _httpClient.PostAsync(requestUrl, jsonContent);
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating tracking in TrackingMore: {ex.Message}");
            throw new Exception("Failed to create tracking.");
        }
    }

    public async Task<string> GetTrackingOrder(string trackingNumber)
    {
        try
        {

            var requestUrl = $"{TrackingMoreApiUrl}/get?tracking_numbers={trackingNumber}";

            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching tracking details: {ex.Message}");
            throw new Exception("Failed to get tracking info.");
        }
    }

    public async Task<string> DeleteTrackingOrder(string trackingNumber)
    {
        try
        {
            var requestUrl = $"{TrackingMoreApiUrl}/delete/{trackingNumber}";

            var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);

            _logger.LogInformation($"Deleting tracking with ID: {trackingNumber}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"TrackingMore Response: {responseContent}");

            return responseContent;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting tracking: {ex.Message}");
            throw new Exception("Failed to delete tracking.");
        }
    }

    public async Task<string> UpdateTrackingOrder(string trackingNumber, UpdateTrackingDto updateTrackingDto)
    {
        try
        {
            var requestUrl = $"{TrackingMoreApiUrl}/update/{trackingNumber}";

            // Create the JSON payload with the updated tracking information
            var requestBody = new
            {
                customer_name = updateTrackingDto.FirstName  + " " + updateTrackingDto.LastName,
                customer_sms = updateTrackingDto.CustomerPhoneNumber
                // Include other fields as necessary
            };

            // Serialize the request body to JSON
            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            // Create the PUT request
            var request = new HttpRequestMessage(HttpMethod.Put, requestUrl)
            {
                Content = jsonContent
            };


            _logger.LogInformation($"Updating tracking with ID: {trackingNumber}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"TrackingMore Response: {responseContent}");

            return responseContent;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Updating tracking: {ex.Message}");
            throw new Exception("Failed to update tracking.");
        }
    }
}
