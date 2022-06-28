import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisclientsComponent } from './regisclients.component';

describe('RegisclientsComponent', () => {
  let component: RegisclientsComponent;
  let fixture: ComponentFixture<RegisclientsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegisclientsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisclientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
