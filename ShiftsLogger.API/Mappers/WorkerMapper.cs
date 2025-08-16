using ShiftsLogger.Data.DTOs;
using ShiftsLogger.Data.Entities;

namespace ShiftsLogger.API.Mappers;

public static class WorkerMapper
{
    public static CreateWorkerDto ToWorkerDto(this Worker worker) => new(worker.Name);

    public static void ToEntity(CreateWorkerDto workerDto, Worker entity)
    {
        entity.Name = workerDto.Name;
    }
}
