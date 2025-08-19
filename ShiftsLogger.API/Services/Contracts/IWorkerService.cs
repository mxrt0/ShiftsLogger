using ShiftsLogger.Data.DTOs;

namespace ShiftsLogger.API.Services.Contracts;

public interface IWorkerService
{
    void AddWorker(CreateWorkerDto worker);
    void DeleteWorker(int workerId);
    void UpdateWorker(int workerId, CreateWorkerDto newWorker);
    List<WorkerDto> GetAllWorkers();
}
