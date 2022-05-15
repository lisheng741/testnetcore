using AutoMapper;
using AutoMapperTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoMapperTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMapper _mapper;

        public TestController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public void GetUser()
        {
            User user = new User(Guid.NewGuid(), "test", null);
            UserDto dto = _mapper.Map<User, UserDto>(user);
            dto.Age = 12;
            dto.Id = Guid.NewGuid();
            dto.Name = "test2";

            User user2 = new User();
            user2.Id = Guid.NewGuid();
            user2.Name = "test3";
            User user3 = user2;
            user2 = _mapper.Map<UserDto, User>(dto);
        }
    }
}
