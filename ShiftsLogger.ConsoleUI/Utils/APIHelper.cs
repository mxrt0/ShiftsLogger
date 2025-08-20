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
        try
        {
            var response = await _client.GetAsync("Workers");
            var jsonWorkers = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<WorkerDto>>(jsonWorkers) ?? new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<string> PostShiftAsync(ShiftDto shift)
    {
        var payload = JsonConvert.SerializeObject(shift);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        try
        {
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
        catch (Exception ex)
        {
            return $"Unexpected error while making request: {ex.Message}";
        }

    }

    public async Task<string> PostWorkerAsync(CreateWorkerDto worker)
    {
        var payload = JsonConvert.SerializeObject(worker);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        try
        {
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
        catch (Exception ex)
        {
            return $"Unexpected error while making request: {ex.Message}";
        }

    }

    public async Task<(string, ShiftDto?)> GetShiftAsync(int workerId, DateTime startDate)
    {
        var normalizedDate = startDate.ToString("yyyy-MM-dd");
        var url = $"Shifts/search?workerId={workerId}&date={normalizedDate}";

        try
        {
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
        catch (Exception ex)
        {
            return ($"Unexpected error while making request: {ex.Message}", null);
        }
    }

    public async Task<(string, List<ShiftDto>?)> GetAllShiftsAsync()
    {
        try
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
        catch (Exception ex)
        {
            return ($"Unexpected error while making request: {ex.Message}", null);
        }

    }

    public async Task<string> UpdateShift(int shiftId, ShiftDto updatedShift)
    {
        var payload = JsonConvert.SerializeObject(updatedShift);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var url = $"Shifts/{shiftId}";

        try
        {
            var response = await _client.PutAsync(url, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse)?.Errors[0] ?? string.Empty;
            }
            return JsonConvert.DeserializeObject<SuccessResponse>(jsonResponse)?.Success[0] ?? string.Empty;
        }
        catch (Exception ex)
        {
            return $"Unexpected error while making request: {ex.Message}";
        }

    }

    public async Task<string> UpdateWorker(int workerId, CreateWorkerDto newWorker)
    {
        var payload = JsonConvert.SerializeObject(newWorker);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var url = $"Workers/{workerId}";

        try
        {
            var response = await _client.PutAsync(url, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse)?.Errors[0] ?? string.Empty;
            }
            return JsonConvert.DeserializeObject<SuccessResponse>(jsonResponse)?.Success[0] ?? string.Empty;
        }
        catch (Exception ex)
        {
            return $"Unexpected error while making request: {ex.Message}";
        }
    }

    public async Task<string> DeleteShift(int shiftId)
    {
        var url = $"Shifts/{shiftId}";

        try
        {
            var response = await _client.DeleteAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse)?.Errors[0] ?? string.Empty;
            }
            return JsonConvert.DeserializeObject<SuccessResponse>(jsonResponse)?.Success[0] ?? string.Empty;
        }
        catch (Exception ex)
        {
            return $"Unexpected error while making request: {ex.Message}";
        }

    }

    public async Task<string> DeleteWorker(int workerId)
    {
        var url = $"Workers/{workerId}";

        try
        {
            var response = await _client.DeleteAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse)?.Errors[0] ?? string.Empty;
            }
            return JsonConvert.DeserializeObject<SuccessResponse>(jsonResponse)?.Success[0] ?? string.Empty;
        }
        catch (Exception ex)
        {
            return $"Unexpected error while making request: {ex.Message}";
        }

    }
}
