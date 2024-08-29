import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAddressComponent } from './add-address.component';

describe('AddressformComponent', () => {
  let component: AddAddressComponent;
  let fixture: ComponentFixture<AddAddressComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddAddressComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddAddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
