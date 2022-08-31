using Microsoft.AspNetCore.Mvc;

namespace FromServiceTest.Code
{
    public class TestService
    {
        public TestService()
        {

        }

        public void Show()
        {
            var accessor = Helper.ServiceProvider.GetService<IHttpContextAccessor>();
            var query = accessor.HttpContext.Request.QueryString;

            Console.WriteLine("show");
        }
    }
}
