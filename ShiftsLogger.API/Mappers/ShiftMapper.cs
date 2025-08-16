using ShiftsLogger.Data.DTOs;
using ShiftsLogger.Data.Entities;

namespace ShiftsLogger.API.Mappers;

public static class ShiftMapper
{
    public static ShiftDto ToShiftDto(this Shift shift) => new(shift.Date, shift.Start, shift.End, shift.WorkerId);
    public static void ToEntity(ShiftDto dto, Shift entity)
    {
        entity.Date = dto.Date;
        entity.Start = dto.Start;
        entity.End = dto.End;
        entity.CalculateDuration();
        entity.WorkerId = dto.WorkerId;
    }
}
