import { OrderItem } from "./orderItem.model";

export interface OrderFull{
    orderId: string,
    addressId: string,
    paymentId: string,
    appUserId: string,
    orderItems: OrderItem[]
}