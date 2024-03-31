using Microsoft.AspNetCore.Mvc;
using OpenStreatMap.Dal;
using OpenStreatMap.DTO;

using OpenStreatMap.Manager;

[ApiController]
[Route("[controller]")]
public class UserInputController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UserInputController(MongoDBContext mongoDbContext)
    {
        _userRepository = new UserRepository(mongoDbContext.User);
    }

    [HttpPost("addUserInput")]
    public IActionResult AddUserInput()
    {
        List<string> targets = new List<string> { "Destination A", "Destination B", "Destination C" };

        var userInput = UserManager.GetUserInput(targets);

        _userRepository.InsertUser(userInput);

        return Ok("User input inserted into MongoDB successfully.");
    }
}

