using ShiftsLogger.Data.DTOs;
using ShiftsLogger.Data.DTOs.Contracts;
using ShiftsLogger.Data.Entities;

namespace ShiftsLogger.API.Mappers;

public static class WorkerMapper
{
    public static WorkerDto ToWorkerDto(this Worker worker) => new(worker.Name);

    public static void MapToEntity(this IWorkerDto workerDto, Worker entity)
    {
        entity.Name = workerDto.Name;
    }
}
