export interface Payment{
    paymentId: string,
    amount: number,
    paymentDate: Date,
    cardNumber: string,
    cardHolderName: string, 
    expiryDate: Date
}