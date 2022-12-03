import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ModuleTestComponent } from './components/module-test.component';
import { ModuleTestService } from '@module-test';
import { of } from 'rxjs';

describe('ModuleTestComponent', () => {
  let component: ModuleTestComponent;
  let fixture: ComponentFixture<ModuleTestComponent>;
  const mockModuleTestService = jasmine.createSpyObj('ModuleTestService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ModuleTestComponent],
      providers: [
        {
          provide: ModuleTestService,
          useValue: mockModuleTestService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModuleTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
