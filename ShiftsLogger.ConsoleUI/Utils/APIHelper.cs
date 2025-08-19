using Newtonsoft.Json;
using ShiftsLogger.Data.DTOs;
using System.Text;
namespace ShiftsLogger.ConsoleUI.Utils;

public class APIHelper
{
    private HttpClient _client;
    public APIHelper(HttpClient httpClient)
    {
        _client = httpClient;
    }
    public async Task<List<WorkerDto>> FetchWorkersAsync()
    {
        var response = await _client.GetAsync("Workers");
        var jsonWorkers = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<WorkerDto>>(jsonWorkers) ?? new();
    }

    public async Task<string> PostShiftAsync(ShiftDto shift)
    {
        var payload = JsonConvert.SerializeObject(shift);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("Shifts/add", content);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        string responseMessage;
        if (!response.IsSuccessStatusCode)
        {
            responseMessage = JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse)?.Errors[0] ?? string.Empty;
            return responseMessage;
        }
        return JsonConvert.DeserializeObject<SuccessResponse>(jsonResponse)?.Success[0] ?? string.Empty;
    }

    public async Task<string> PostWorkerAsync(CreateWorkerDto worker)
    {
        var payload = JsonConvert.SerializeObject(worker);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("Workers/add", content);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        string responseMessage;
        if (!response.IsSuccessStatusCode)
        {
            responseMessage = JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse)?.Errors[0] ?? string.Empty;
            return responseMessage;
        }
        return JsonConvert.DeserializeObject<SuccessResponse>(jsonResponse)?.Success[0] ?? string.Empty;
    }
}
