import { Category } from '../../category/models/category.model';

export interface EditProduct {
  name: string;
  description: string;
  imageUrl: string;
  price: number;
  status: string;
  modifiedAt: Date;
  categoryId: string;
}
