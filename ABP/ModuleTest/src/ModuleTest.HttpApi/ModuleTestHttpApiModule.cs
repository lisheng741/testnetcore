﻿using Localization.Resources.AbpUi;
using ModuleTest.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace ModuleTest;

[DependsOn(
    typeof(ModuleTestApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class ModuleTestHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(ModuleTestHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<ModuleTestResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
