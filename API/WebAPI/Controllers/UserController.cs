using Business.Contracts;
using Business.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync()
    {

        /*    unitOfWork.BeginTransaction();
            var userInserted = await unitOfWork.UserRepository.AddAsync(user);
            unitOfWork.CommitAndCloseConnection();
            return userInserted ? Ok() : BadRequest();*/
        unitOfWork.BeginTransaction();
        var user = await unitOfWork.UserRepository.GetByColumn("email", "sdfoihbsdiu@gmail.com");
        return Ok();
    }
}
