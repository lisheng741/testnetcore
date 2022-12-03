import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class ModuleTestService {
  apiName = 'ModuleTest';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/ModuleTest/sample' },
      { apiName: this.apiName }
    );
  }
}
