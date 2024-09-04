export interface AddPayment{
    amount: number,
    paymentDate: Date,
    cardNumber: string,
    cardHolderName: string, 
    expiryDate: Date
}