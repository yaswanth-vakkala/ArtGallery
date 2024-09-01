export class SalesOverTimeDto {
    formattedDate: string;
    orderCount: number; // Initialize with a default value

    constructor(formattedDate: string, orderCount: number = 0) {
        this.formattedDate = formattedDate;
        this.orderCount = orderCount; // Can also be set in the constructor
    }
}
