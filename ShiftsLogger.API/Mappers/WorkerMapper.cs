using ShiftsLogger.API.DTOs;
using ShiftsLogger.Data.Entities;

namespace ShiftsLogger.API.Mappers
{
    public static class WorkerMapper
    {
        public static WorkerDto ToWorkerDto(this Worker worker) => new(worker.Name);

        public static void ToEntity(WorkerDto workerDto, Worker entity)
        {
            entity.Name = workerDto.Name;
        }
    }
}
