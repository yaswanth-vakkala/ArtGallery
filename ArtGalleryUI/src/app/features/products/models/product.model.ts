export interface Product {
  productId: string;
  name: string;
  description: string;
  imageUrl: string;
  price: number;
  status: string;
  createdAt: Date;
  modifiedAt: Date;
  modifiedBy: string;
  categoryId: string;
}
