import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecruiterApplicantsComponent } from './recruiter-applicants.component';

describe('RecruiterApplicantsComponent', () => {
  let component: RecruiterApplicantsComponent;
  let fixture: ComponentFixture<RecruiterApplicantsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecruiterApplicantsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RecruiterApplicantsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
