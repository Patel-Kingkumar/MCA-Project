using Api.Repository.AdminRepository;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _AdminRepository;
        public AdminController(IAdminRepository AdminRepository)
        {
            _AdminRepository = AdminRepository;
        }
        // GET: AdminController
        [HttpGet]
        public async Task<ActionResult> GetAdmins()
        {
            try
            {
                return Ok(await _AdminRepository.GetAdmins());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Admins>> GetAdmins(int id)
        {
            try
            {
                var result = await _AdminRepository.GetAdmins(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddAdmins(Admins admin)
        {
            try
            {
                if (admin == null)
                {
                    return BadRequest();
                }
                var CreateAdmin = await _AdminRepository.AddAdmins(admin);
                return CreatedAtAction(nameof(AddAdmins), new { id = CreateAdmin.Id }, CreateAdmin);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Admins>> UpdateAdmins(int id,Admins admin)
        {
            try
            {
                if (id != admin.Id)
                {
                    return BadRequest("Id is not match");
                }
                var adminUpdated = await _AdminRepository.GetAdmins(id);
                if (adminUpdated == null)
                {
                    return NotFound($"Admin {id} is not found");
                }
                var UpdatedAdmin = await _AdminRepository.UpdateAdmins(admin);
                return UpdatedAdmin;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Admins>> DeleteAdmins(int id)
        {
            try
            {

                var adminDelete = await _AdminRepository.GetAdmins(id);

                if (adminDelete != null)
                {
                    var admin = await _AdminRepository.DeleteAdmins(id);
                    return admin;
                }
                return null;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
        [HttpGet("{Search}")]
        public async Task<ActionResult<IEnumerable<Admins>>> Search(string Name)
        {
            try
            {
                var result = await _AdminRepository.Search(Name);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from database");
            }
        }
    }
}
