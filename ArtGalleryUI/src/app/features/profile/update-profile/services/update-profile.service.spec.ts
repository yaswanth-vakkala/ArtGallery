import { TestBed } from '@angular/core/testing';

import { UpdateProfileService } from './update-profile.service';

describe('UpdateProfileService', () => {
  let service: UpdateProfileService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UpdateProfileService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
