using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.API.Exceptions;
using ShiftsLogger.API.Services.Contracts;
using ShiftsLogger.Data.DTOs;

namespace ShiftsLogger.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkersController : ControllerBase
    {
        private IWorkerService _workerService;

        public WorkersController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpPost("add")]
        public IActionResult AddWorker([FromBody] CreateWorkerDto worker)
        {
            try
            {
                _workerService.AddWorker(worker);
                return Ok(new { Success = new[] { "Successfully inserted new worker." } });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Errors = new[] { "Something went wrong while adding the worker." } });
            }
        }

        [HttpGet]
        public IActionResult GetAllWorkers()
        {
            try
            {
                var workers = _workerService.GetAllWorkers();
                return Ok(workers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Errors = new[] { ex.Message } });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWorker(int id, CreateWorkerDto newWorkerDto)
        {
            try
            {
                _workerService.UpdateWorker(id, newWorkerDto);
                return Ok(new { Success = new[] { $"Successfully updated worker with ID {id}." } });

            }
            catch (WorkerNotFoundException ex)
            {
                return NotFound(new { Errors = new[] { ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Errors = new[] { ex.Message } });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWorker(int id)
        {
            try
            {
                _workerService.DeleteWorker(id);
                return Ok(new { Success = new[] { $"Successfully deleted worker with ID {id}." } });
            }
            catch (WorkerNotFoundException ex)
            {
                return NotFound(new { Errors = new[] { ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Errors = new[] { ex.Message } });
            }
        }

    }
}
