using EMS.Application.DTOs;
using EMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentsController : ControllerBase
    {

        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<DepartmentDto>>(true, "Departments retrieved successfully", result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new ApiResponse<string>(false, "Department not found", null));
            }

            return Ok(new ApiResponse<DepartmentDto>(true, "Department fetched successfully", result));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<DepartmentDto>(true, "Department created successfully", result));
        }

        [Authorize(Roles = "Admin,HR")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DepartmentDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated)
            {
                return NotFound(new ApiResponse<string>(false, "Department not found", null));
            }
            return Ok(new ApiResponse<string>(true, "Department updated successfully", null));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new ApiResponse<string>(false, "Department not found", null));
            }
            return Ok(new ApiResponse<string>(true, "Department deleted successfully", null));
        }
    }
}
