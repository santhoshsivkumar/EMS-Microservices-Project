using EMS.Application.DTOs;
using EMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<RoleDto>>(true, "Roles retrieved successfully", result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(new ApiResponse<string>(false, "Role not found", null));
            }
            return Ok(new ApiResponse<RoleDto>(true, "Role fetched successfully", result));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(RoleDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<RoleDto>(true, "Role created successfully", result));
        }

        [Authorize(Roles = "Admin,HR")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, RoleDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated)
            {
                return NotFound(new ApiResponse<string>(false, "Role not found", null));
            }
            return Ok(new ApiResponse<string>(true, "Role updated successfully", null));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new ApiResponse<string>(false, "Role not found", null));
            }
            return Ok(new ApiResponse<string>(true, "Role deleted successfully", null));
        }
    }
}
