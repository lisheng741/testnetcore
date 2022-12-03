import { TestBed } from '@angular/core/testing';
import { ModuleTestService } from './services/module-test.service';
import { RestService } from '@abp/ng.core';

describe('ModuleTestService', () => {
  let service: ModuleTestService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(ModuleTestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
