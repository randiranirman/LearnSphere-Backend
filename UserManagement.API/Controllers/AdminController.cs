using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Domain;
using UserManagement.Infrastructure.Services;

namespace UserManagement.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController(IAdminService adminService) : ControllerBase
    {

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAdminDetailsAsync( int id , Admin request)
        {
            var result = await adminService.EditAdminDetails(id, request);

            if (!result)
            {
                return NotFound();
            }

            else return Ok();
        }


        [HttpGet]
        public async Task<IEnumerable<Admin>> GetAllAdmins()
        {


            var admins = await adminService.GetAllAdmins();

            if (admins == null)
            {
                return null;
            }

            return admins;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdmin(int id)
        {
            var admin = await adminService.GetAdminDetails(id);
            if (admin == null)
            {
                return NotFound(new { message = "Admin not found " });
            }

            return Ok(admin);

        }


    }
}
