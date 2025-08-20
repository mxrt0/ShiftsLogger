using ShiftsLogger.Data.DTOs;
using ShiftsLogger.Data.Entities;

namespace ShiftsLogger.API.Mappers;

public static class ShiftMapper
{
    public static ShiftDto ToShiftDto(this Shift shift)
    {
        ShiftDto dto = new(shift.Date, shift.Start, shift.End, shift.WorkerId);
        dto.Id = shift.Id;
        return dto;
    }

    public static void MapToEntity(this ShiftDto dto, Shift entity)
    {
        entity.Date = dto.Date;
        entity.Start = dto.Start;
        entity.End = dto.End;
        entity.CalculateDuration();
        entity.WorkerId = dto.WorkerId;
    }
}
