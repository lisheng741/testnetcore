using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace ModuleTest.Pages;

public class IndexModel : ModuleTestPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
