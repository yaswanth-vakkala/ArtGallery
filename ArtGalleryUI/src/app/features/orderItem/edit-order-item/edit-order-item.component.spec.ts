import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditOrderItemComponent } from './edit-order-item.component';

describe('EditOrderItemComponent', () => {
  let component: EditOrderItemComponent;
  let fixture: ComponentFixture<EditOrderItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditOrderItemComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditOrderItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
