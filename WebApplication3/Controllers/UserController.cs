using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

using Amazon.S3.Transfer;
using Task_Novin_Teck.DTO;
using Task_Novin_Teck.Repository;
using Amazon.S3;


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
            _userRepository.InsertUser(user);
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
            var user = _userRepository.GetUserById(id);
            if (user == null) {
                return NotFound("We dont have user with this id");
            }
            return Ok(user);
        }
  
   
        
    }
}
