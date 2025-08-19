using ShiftsLogger.API.Exceptions;
using ShiftsLogger.API.Mappers;
using ShiftsLogger.API.Services.Contracts;
using ShiftsLogger.Data.Context;
using ShiftsLogger.Data.DTOs;
using ShiftsLogger.Data.Entities;

namespace ShiftsLogger.API.Services;

public class WorkerService : IWorkerService
{
    private ShiftsDbContext _dbContext;
    public WorkerService(ShiftsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void AddWorker(CreateWorkerDto workerDto)
    {
        var worker = new Worker();
        workerDto.MapToEntity(worker);
        _dbContext.Workers.Add(worker);
        _dbContext.SaveChanges();
    }

    public void DeleteWorker(int workerId)
    {
        var workerToDelete = _dbContext.Workers.FirstOrDefault(w => w.Id == workerId);
        if (workerToDelete is not null)
        {
            _dbContext.Remove(workerToDelete);
            _dbContext.SaveChanges();
            return;
        }
        throw new WorkerNotFoundException("No worker with this ID exists.");
    }

    public List<WorkerDto> GetAllWorkers()
    {
        return _dbContext.Workers.Select(w => w.ToWorkerDto()).ToList();
    }

    public void UpdateWorker(int workerId, CreateWorkerDto newWorkerDto)
    {
        var workerToUpdate = _dbContext.Workers.FirstOrDefault(w => w.Id == workerId);
        if (workerToUpdate is not null)
        {
            newWorkerDto.MapToEntity(workerToUpdate);
            _dbContext.SaveChanges();
            return;
        }
        throw new WorkerNotFoundException("No worker with this ID exists.");
    }
}
