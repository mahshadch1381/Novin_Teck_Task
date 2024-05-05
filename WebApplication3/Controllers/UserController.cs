using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

using Amazon.S3.Transfer;
using Task_Novin_Teck.DTO;
using Task_Novin_Teck.Repository;
using Amazon.S3;
using Task_Novin_Teck.ReqClasses;


namespace Task_Novin_TeckControllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
      //  private readonly IAmazonS3 _s3Client;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }

        [HttpPost]
        public IActionResult CreateUser(UserDto user)
        {
            var command = new CreateUserCommand
            {
                Name = user.Name,
                Email = user.Email,
                Age = user.Age
            };
            _userRepository.InsertUser(command);
            return Ok("User inserted successfully!");
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetUser(int id)
        {
            var query = new GetUserByIdQuery { UserId = id };
            var user = _userRepository.GetUserById(query);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
  
   
        
    }
}
