export class TopSellingProductDto {
    productId: string;
    name: string;
    totalSales: number = 0; // Initialize with a default value

    constructor(productId: string, name: string, totalSales: number = 0) {
        this.productId = productId;
        this.name = name;
        this.totalSales = totalSales; // Can also be set in the constructor
    }
}
