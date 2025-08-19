using ShiftsLogger.API.Exceptions;
using ShiftsLogger.API.Mappers;
using ShiftsLogger.API.Services.Contracts;
using ShiftsLogger.Data.Context;
using ShiftsLogger.Data.DTOs;
using ShiftsLogger.Data.Entities;

namespace ShiftsLogger.API.Services;

public class ShiftsService : IShiftsService
{
    private ShiftsDbContext _dbContext;
    public ShiftsService(ShiftsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void AddShift(ShiftDto shiftDto)
    {
        Shift newShift = new Shift();
        shiftDto.MapToEntity(newShift);
        _dbContext.Shifts.Add(newShift);
        _dbContext.SaveChanges();
    }

    public void DeleteShift(int shiftId)
    {
        var shiftToDelete = _dbContext.Shifts.FirstOrDefault(s => s.Id == shiftId);
        if (shiftToDelete is not null)
        {
            _dbContext.Shifts.Remove(shiftToDelete);
            _dbContext.SaveChanges();
            return;
        }
        throw new ShiftNotFoundException("No shift with this ID exists.");
    }

    public List<ShiftDto> GetAllShifts()
    {
        return _dbContext.Shifts.Select(s => s.ToShiftDto()).ToList();
    }

    public ShiftDto GetShift(int workerId, DateTime date)
    {
        var shift = _dbContext.Shifts.FirstOrDefault(s => s.WorkerId == workerId && s.Date == date)
            ?? throw new ShiftNotFoundException("Shift not found.");

        return shift.ToShiftDto();
    }

    public void UpdateShift(int shiftId, ShiftDto newShift)
    {
        var shiftToUpdate = _dbContext.Shifts.FirstOrDefault(s => s.Id == shiftId);
        if (shiftToUpdate is not null)
        {
            ShiftMapper.MapToEntity(newShift, shiftToUpdate);
            _dbContext.SaveChanges();
            return;
        }
        throw new ShiftNotFoundException("No shift with this ID exists.");
    }
}
