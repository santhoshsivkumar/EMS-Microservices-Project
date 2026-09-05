using EMS.Application.DTOs;
using EMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {

        public readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            if (pageNumber <= 1)
            {
                pageNumber = 1;
            }
            const int maxPageSize = 50;

            if (pageSize <= 0)
            {
                pageSize = 10;
            }
                
            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

            var result = await _service.GetAllAsync(pageNumber, pageSize, search);

            var response = new ApiResponse<PaginatedResult<EmployeeDto>>
            (
                true,
                "Employees retrieved successfully",
                result
            );

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(new ApiResponse<string>(false, "Employee not found", null));
            }
            return Ok(new ApiResponse<EmployeeDto>(true, "Employee fetched successfully", result));

        }

        [Authorize(Roles = "Admin,HR")]
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<EmployeeDto>(true, "Employee created successfully", result));
        }

        [Authorize(Roles = "Admin,HR")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, EmployeeDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid data", null));
            }

            var updated = await _service.UpdateAsync(id, dto);
            if (!updated)
            {
                return NotFound(new ApiResponse<string>(false, "Employee not found", null));
            }
            return Ok(new ApiResponse<string>(true, "Employee updated successfully",null));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new ApiResponse<string>(false, "Employee not found", null));
            }
            return Ok(new ApiResponse<string>(true, "Employee deleted successfully", null));
        }
    }
}
