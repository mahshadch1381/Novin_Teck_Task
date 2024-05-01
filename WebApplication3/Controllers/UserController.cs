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
       private readonly IAmazonS3 _s3Client;

        public UserController(UserRepository userRepository, IAmazonS3 s3Client)
        {
            _userRepository = userRepository;
            _s3Client = s3Client;
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

        //[HttpPost("UploadFile")]
       // public async Task<IActionResult> UploadFile(IFormFile file)
       // {
           // try
           // {
                //if (file == null || file.Length <= 0)
                   // return BadRequest("Invalid file");

               // Create a unique file name to avoid conflicts
               // string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
               //  string bucketName = "Mybucket";
               // using (var memoryStream = new MemoryStream())
               // {
                 //   await file.CopyToAsync(memoryStream);
                 //   memoryStream.Position = 0;  Reset memory stream position

                    /// Upload file to S3 bucket
                    //var fileTransferUtility = new TransferUtility(_s3Client);
                  //  await fileTransferUtility.UploadAsync(memoryStream, bucketName, fileName);
              //  }

               
             //   string s3Url = $"https://s3.amazonaws.com/{bucketName}/{fileName}";
             //   return Ok($"File uploaded successfully! S3 URL: {s3Url}");
         //   }
         //   catch (Exception ex)
          //  {
              //  return StatusCode(500, $"Error uploading file: {ex.Message}");
          //  }
      //  }
    }
}
