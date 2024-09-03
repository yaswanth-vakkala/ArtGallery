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
import { AddressListComponent } from './features/checkout/address-list/address-list.component';
import { OrdersListComponent } from './features/orders/orders-list/orders-list.component';
import { OrderItemDetailComponent } from './features/orderItem/order-item-detail/order-item-detail.component';
import { StatisticsComponent } from './features/statistics/statistics/statistics.component';
import { CreateProductComponent } from './features/products/create-product/create-product.component';
import { UserListComponent } from './features/user/user-list/user-list.component';
import { AddUserComponent } from './features/user/add-user/add-user.component';
import { EditUserComponent } from './features/user/edit-user/edit-user.component';


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
    path: 'myorders/:userId',
    component: OrdersListComponent
  },
  {
    path: 'orderItems/:orderItemId',
    component: OrderItemDetailComponent
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
    path: 'admin/users',
    component : UserListComponent,
  },
  {
    path: 'admin/users/add',
    component: AddUserComponent
  },
  {
    path: 'admin/users/edit/:id',
    component: EditUserComponent
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
    path:'admin/statistics',
    component:StatisticsComponent,
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
  {
    path: 'user/address/:userId',
    component: AddressListComponent,
  },
  {
    path:'admin/product/add',
    component:CreateProductComponent,
  }
];
