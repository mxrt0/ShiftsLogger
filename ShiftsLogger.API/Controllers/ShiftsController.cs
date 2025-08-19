using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.API.Exceptions;
using ShiftsLogger.API.Services.Contracts;
using ShiftsLogger.Data.DTOs;

namespace ShiftsLogger.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftsController : ControllerBase
{
    private IShiftsService _shiftsService;
    public ShiftsController(IShiftsService shiftsService)
    {
        _shiftsService = shiftsService;
    }

    [HttpPost("add")]
    public IActionResult AddShift([FromBody] ShiftDto shift)
    {
        try
        {
            _shiftsService.AddShift(shift);
            return Ok(new { Success = new[] { "Successfully inserted new shift record." } });
        }
        catch (Exception)
        {
            return StatusCode(500, new { Errors = new[] { "Something went wrong while adding the shift." } });
        }
    }

    [HttpGet("search")]
    public IActionResult GetShift([FromQuery] int workerId, [FromQuery] DateTime date)
    {
        try
        {
            var shift = _shiftsService.GetShift(workerId, date);
            return Ok(shift);
        }
        catch (ShiftNotFoundException ex)
        {
            return NotFound(new { Errors = new[] { ex.Message } });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Errors = new[] { $"An unexpected error occurred: {ex.Message}" } });
        }
    }

    [HttpGet("all")]
    public IActionResult GetAllShifts()
    {
        try
        {
            var allShifts = _shiftsService.GetAllShifts();
            return Ok(allShifts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Errors = new[] { $"An unexpected error occurred: {ex.Message}" } });
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateShift(int id, [FromBody] ShiftDto newShift)
    {
        try
        {
            _shiftsService.UpdateShift(id, newShift);
            return Ok(new { Success = new[] { $"Successfully updated shift with ID {id}." } });
        }
        catch (ShiftNotFoundException ex)
        {
            return NotFound(new { Errors = new[] { ex.Message } });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Errors = new[] { $"An unexpected error occurred: {ex.Message}" } });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteShift(int id)
    {
        try
        {
            _shiftsService.DeleteShift(id);
            return Ok(new { Success = new[] { $"Successfully deleted shift with ID {id}." } });
        }
        catch (ShiftNotFoundException ex)
        {
            return NotFound(new { Errors = new[] { ex.Message } });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Errors = new[] { $"An unexpected error occurred: {ex.Message}" } });
        }
    }

}