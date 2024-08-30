import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddressListComponent } from './address-list.component';

describe('AddressListComponent', () => {
  let component: AddressListComponent;
  let fixture: ComponentFixture<AddressListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddressListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddressListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
