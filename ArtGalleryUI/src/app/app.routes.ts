import { Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { LoginComponent } from './features/auth/login/login.component';
import { authGuard } from './features/auth/guards/auth.guard';
import { RegisterComponent } from './features/auth/register/register.component';
import { ProductListComponent } from './features/products/product-list/product-list.component';
import { AddAddressComponent } from './features/checkout/add-address/add-address.component';
import { UpdateAddressComponent } from './features/checkout/update-address/update-address.component';
import { ProductDetailComponent } from './features/products/product-detail/product-detail.component';
import { CartListComponent } from './features/cart/cart-list/cart-list.component';
import { UpdateProfileComponent } from './features/profile/update-profile/update-profile.component';


export const routes: Routes = [
  {
    path: '',
    component: ProductListComponent,
  },
  {
    path: 'products/:productId',
    component: ProductDetailComponent,
  },
  {
    path: 'cart/:userId',
    component: CartListComponent
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'admin/categories',
    component: CategoryListComponent,
    canActivate: [authGuard],
  },
  {
    path: 'admin/categories/add',
    component: AddCategoryComponent,
    canActivate: [authGuard],
  },
  {
    path: 'admin/categories/edit/:categoryId',
    component: EditCategoryComponent,
    canActivate: [authGuard],
  },
  {
    path: 'user/address/add',
    component: AddAddressComponent,
  },
  {
    path: 'user/address/update/:addressId',
    component: UpdateAddressComponent,
  },
  {
    path: 'user/update-profile/:userId',
    component: UpdateProfileComponent,
  },
];
