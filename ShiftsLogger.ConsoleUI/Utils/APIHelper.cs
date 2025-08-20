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

    public async Task<(string, ShiftDto?)> GetShiftAsync(int workerId, DateTime startDate)
    {
        var normalizedDate = startDate.ToString("yyyy-MM-dd");
        var url = $"Shifts/search?workerId={workerId}&date={normalizedDate}";
        var response = await _client.GetAsync(url);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var shift = JsonConvert.DeserializeObject<ShiftDto>(jsonResponse);

        if (!response.IsSuccessStatusCode || shift is null)
        {
            string responseMessage = JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse)?.Errors[0] ?? string.Empty;
            return (responseMessage, null);
        }

        return (string.Empty, shift);
    }

    public async Task<(string, List<ShiftDto>?)> GetAllShiftsAsync()
    {
        var response = await _client.GetAsync("Shifts/all");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var shifts = JsonConvert.DeserializeObject<List<ShiftDto>>(jsonResponse);
        if (!response.IsSuccessStatusCode || shifts is null)
        {
            string responseMessage = JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse)?.Errors[0] ?? string.Empty;
            return (responseMessage, null);
        }

        return (string.Empty, shifts);
    }

    public async Task<string> UpdateShift(int shiftId, ShiftDto updatedShift)
    {
        var payload = JsonConvert.SerializeObject(updatedShift);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var url = $"Shifts/{shiftId}";

        var response = await _client.PutAsync(url, content);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse)?.Errors[0] ?? string.Empty;
        }
        return JsonConvert.DeserializeObject<SuccessResponse>(jsonResponse)?.Success[0] ?? string.Empty;
    }

    public async Task<string> UpdateWorker(int workerId, CreateWorkerDto newWorker)
    {
        var payload = JsonConvert.SerializeObject(newWorker);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var url = $"Workers/{workerId}";

        var response = await _client.PutAsync(url, content);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse)?.Errors[0] ?? string.Empty;
        }
        return JsonConvert.DeserializeObject<SuccessResponse>(jsonResponse)?.Success[0] ?? string.Empty;
    }
}
