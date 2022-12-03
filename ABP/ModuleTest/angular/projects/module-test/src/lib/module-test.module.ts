import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { ModuleTestComponent } from './components/module-test.component';
import { ModuleTestRoutingModule } from './module-test-routing.module';

@NgModule({
  declarations: [ModuleTestComponent],
  imports: [CoreModule, ThemeSharedModule, ModuleTestRoutingModule],
  exports: [ModuleTestComponent],
})
export class ModuleTestModule {
  static forChild(): ModuleWithProviders<ModuleTestModule> {
    return {
      ngModule: ModuleTestModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<ModuleTestModule> {
    return new LazyModuleFactory(ModuleTestModule.forChild());
  }
}
