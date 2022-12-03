import { Component, OnInit } from '@angular/core';
import { ModuleTestService } from '../services/module-test.service';

@Component({
  selector: 'lib-module-test',
  template: ` <p>module-test works!</p> `,
  styles: [],
})
export class ModuleTestComponent implements OnInit {
  constructor(private service: ModuleTestService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
