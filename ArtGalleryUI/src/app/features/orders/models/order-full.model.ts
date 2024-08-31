import { OrderItemFull } from "./orderItem-full.model";

export interface OrderFull{
    orderId: string,
    addressId: string,
    paymentId: string,
    appUserId: string,
    createdAt: Date,
    orderItems: OrderItemFull[]
}