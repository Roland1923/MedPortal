import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPatientProfileComponent } from './edit-patient-profile.component';

describe('EditPatientProfileComponent', () => {
  let component: EditPatientProfileComponent;
  let fixture: ComponentFixture<EditPatientProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditPatientProfileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPatientProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
