import { ModuleWithProviders, NgModule } from '@angular/core';
import { MODULE_TEST_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class ModuleTestConfigModule {
  static forRoot(): ModuleWithProviders<ModuleTestConfigModule> {
    return {
      ngModule: ModuleTestConfigModule,
      providers: [MODULE_TEST_ROUTE_PROVIDERS],
    };
  }
}
