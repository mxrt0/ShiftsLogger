using ShiftsLogger.Data.DTOs;
using ShiftsLogger.Data.Entities;

namespace ShiftsLogger.API.Mappers;

public static class WorkerMapper
{
    public static WorkerDto ToWorkerDto(this Worker worker) => new(worker.Name, worker.Id);

    public static void MapToEntity(this WorkerDto workerDto, Worker entity)
    {
        entity.Name = workerDto.Name;
    }
}
