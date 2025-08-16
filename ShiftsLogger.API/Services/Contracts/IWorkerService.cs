using ShiftsLogger.API.DTOs;

namespace ShiftsLogger.API.Services.Contracts;

public interface IWorkerService
{
    void AddWorker(WorkerDto worker);
    void DeleteWorker(int workerId);
    void UpdateWorker(int workerId, WorkerDto newWorker);
    List<WorkerDto> GetAllWorkers();
}
