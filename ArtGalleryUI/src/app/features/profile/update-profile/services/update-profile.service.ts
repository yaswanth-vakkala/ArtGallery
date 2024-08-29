import { HttpClient } from '@angular/common/http';
import { Injectable, model } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { User } from '../../../auth/models/user.model';
import { UpdateProfile } from '../models/update-profile.model';
import { environment } from '../../../../../environments/environment.development';
import { UserProfile } from '../models/user-profile.model';

@Injectable({
  providedIn: 'root'
})
export class UpdateProfileService {

   constructor(private http: HttpClient,private cookieService:CookieService) {}
  // updateProfile(userId: string, model: UpdateProfile):Observable<UpdateProfile>{
  //   return this.http.put<UpdateProfile>(
  //     `${environment.apiBaseUrl}/api/update-profile/${userId}?addAuth=true`,

  //   )
  // }
  getuserById(userId: string): Observable<UserProfile> {
    return this.http.get<UserProfile>(
      `${environment.apiBaseUrl}/api/AppUser/${userId}`
    );
  }

  updateProfile(userId: string, model: UpdateProfile): Observable<UserProfile>{
    return this.http.put<UserProfile>(
      `${environment.apiBaseUrl}/api/AppUser/${userId}?addAuth=true`,
      model
    );
  }
}
