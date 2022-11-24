//namespace OptionsTest;

using Microsoft.Extensions.Options;

public class TestOptions
{
    public string Name { get; set; }
}

public class TestOptionsSetup : IConfigureOptions<TestOptions>
{
    public void Configure(TestOptions options)
    {
        options.Name = "12334";
    }
}
