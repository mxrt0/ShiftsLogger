using ShiftsLogger.Data.DTOs;

namespace ShiftsLogger.API.Services.Contracts;

public interface IShiftsService
{
    void AddShift(ShiftDto shift);
    void DeleteShift(int shiftId);
    void UpdateShift(int shiftId, ShiftDto newShift);
    ShiftDto GetShift(int workerId, DateTime date);
    List<ShiftDto> GetAllShifts();
}
