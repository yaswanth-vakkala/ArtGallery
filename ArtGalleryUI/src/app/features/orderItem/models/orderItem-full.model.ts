import { Product } from '../../products/models/product.model';

export interface OrderItemFull {
  orderItemId: string;
  status: string;
  productCost: number;
  taxCost: number;
  shippingCost: number;
  orderId: string;
  product: Product;
}
