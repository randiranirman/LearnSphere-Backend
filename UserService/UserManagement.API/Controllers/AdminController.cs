    using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Domain;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Services;

namespace UserManagement.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController(IAdminService adminService, ILogger<UserController> logger, IAuthService authService, UserDbContext context) : ControllerBase
    {

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAdminDetailsAsync(int id, Admin request)
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

        [HttpPost("upload-csv")]
        [Authorize(Roles ="admin")]
        
        public async Task<ActionResult> ImportUserFromCSV(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No  file  uploaded");
                     
            }

            if(!file.FileName.EndsWith("CSV", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(" file must be a csv");
            }



            try
            {

                using  ( var stream = file.OpenReadStream())
                {
                    var users = await  adminService.ReadCsvFile(stream);
                    // process each user 
                    foreach (var user in users)
                    {
                        // check if the user already eixits
                        if( await context.Users.AnyAsync( u => u.Username == user.Username)){
                            // skip eixting users
                            continue;
                        }
                           // registering the each users 
                        var registerUsers =  await authService.RegisterAsync(user);
                       


                      

                    }

                    return Ok(" users register succeffyly");

                }
            }catch(ApplicationException  ex)
            {
                return BadRequest("error importing csv file" +  ex.Message);
            }
        }
        










    }

}

