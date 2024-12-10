using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetSkyApiPostgres.Models.Requests;
using DotnetSkyApiPostgres.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSkyApiPostgres.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUsersService personService, ILogger<UsersController> logger)
        {
            _usersService = personService;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUserAsync(CreateUserRequest request)
        {
            try
            {
                var person = await _usersService.AddUserAsync(request);

                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, UpdateUserRequest request)
        {
            if (id != request.UserId)
            {
                return BadRequest($"id in parameter and id in body is different. id in parameter: {id}, id in body: {request.UserId}");
            }
            try
            {
                GetUserRequest? data = await _usersService.GetUserByIdAsync(id);

                if (data == null)
                {
                    return NotFound();
                }

                await _usersService.UpdateUserAsync(request);

                // return NoContent();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUserByIdAsync(string id)
        {
            try
            {
                GetUserRequest? data = await _usersService.GetUserByIdAsync(id);

                if (data == null)
                {
                    return NotFound();
                }

                await _usersService.DeleteUserAsync(GetUserRequest.ToUsersModel(data));

                // return NoContent();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            try
            {
                GetUserRequest? person = await _usersService.GetUserByIdAsync(id);

                if (person == null)
                {
                    return NotFound();
                }
                
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetUsersAsync()
        {
            try
            {
                IEnumerable<GetUserRequest> list = await _usersService.GetUsersAsync();

                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}