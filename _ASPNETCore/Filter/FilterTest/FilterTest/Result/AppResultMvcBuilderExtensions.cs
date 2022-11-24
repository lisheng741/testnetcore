using Simple.Common.Filters;

namespace Microsoft.Extensions.DependencyInjection;

public static class AppResultMvcBuilderExtensions
{
    public static IMvcBuilder AddAppResult(this IMvcBuilder builder)
    {
        builder.AddMvcOptions(options =>
        {
            options.Filters.Add<AppResultExceptionFilter>();
        });

        return builder;
    }
}
