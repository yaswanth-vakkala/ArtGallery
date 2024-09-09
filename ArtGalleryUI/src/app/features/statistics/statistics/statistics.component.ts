import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { StatisticsService } from '../service/statistics.service';
import {
  Chart,
  ChartType,
  ChartOptions,
  registerables,
} from 'chart.js';
import { TopSellingProductDto } from '../models/TopSellingProductDto';
import { SalesOverTimeDto } from '../models/SalesOverTimeDto';

interface MonthData {
  [key: string]: number; // Assumes keys are month names or numbers and values are sales counts
}

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css']
})
export class StatisticsComponent implements OnInit {
  totalSales: number = 0;
  categoryOrderCounts: any;
  ordersByCustomer: number=0;
  monthlySales: any;
  totalProductsSold: number = 0;
  topSellingProducts: TopSellingProductDto[] = [];
  ordersInDateRange: SalesOverTimeDto[] = [];
  
  private totalSalesChart!: Chart;
  private categoryOrderCountsChart!: Chart;
  private ordersCountByCustomerChart!: Chart;
  private monthlySalesChart!: Chart;
  private totalProductsSoldChart!: Chart;
  private topSellingProductsChart!: Chart;
  private ordersInDateRangeChart!: Chart;
 

  constructor(private statisticsService: StatisticsService) {
    Chart.register(...registerables); // Register all chart types
  }

  ngOnInit(): void {
    this.getTotalSales();
    this.getCategoryOrderCounts();
    // this.getOrdersByCustomerIdMonthWise();
    this.getMonthlySales();
    this.getTotalProductsSold();
    this.getTopSellingProducts(5); // Get top 5 selling products
    this.getOrdersInDateRange(new Date('2024-01-01'), new Date('2024-12-31')); // Example date range
  }

  getTotalSales(): void {
    this.statisticsService.getTotalSales().subscribe(total => {
      this.totalSales = total;
      this.updateTotalSalesChart();
    });
  }

  getCategoryOrderCounts(): void {
    this.statisticsService.getCategoryOrderCounts().subscribe(counts => {
      this.categoryOrderCounts = counts;
      this.updateCategoryOrderCountsChart();
    });
  }

  // getOrdersByCustomerIdMonthWise(): void {
  //   this.statisticsService.getOrdersByCustomerIdMonthWise().subscribe(counts => {
  //     this.ordersByCustomer=counts;
  //     // this.updateOrdersByCustomerIdMonthWiseChart();
  //   });
  // }

  getMonthlySales(): void {
    this.statisticsService.getMonthlySales().subscribe(sales => {
      console.log('Monthly Sales Data:', sales); // Log the response
      this.monthlySales = sales;
      this.updateMonthlySalesChart(sales);
    });
}


  getTotalProductsSold(): void {
    this.statisticsService.getTotalProductsSold().subscribe(total => {
      this.totalProductsSold = total;
      this.updateTotalProductsSoldChart();
    });
  }

  getTopSellingProducts(topN: number): void {
    this.statisticsService.getTopSellingProducts(topN).subscribe(products => {
      this.topSellingProducts = products;
      this.updateTopSellingProductsChart();
    });
  }

  getOrdersInDateRange(startDate: Date, endDate: Date): void {
    this.statisticsService.getOrdersInDateRange(startDate, endDate).subscribe(orders => {
      this.ordersInDateRange = orders;
      this.updateOrdersInDateRangeChart();
    });
  }

  // Update total sales chart
updateTotalSalesChart(): void {
  this.destroyChart(this.totalSalesChart);
  const ctx = document.getElementById('totalSalesChart') as HTMLCanvasElement;
  this.totalSalesChart = new Chart(ctx, {
      type: 'bar' as ChartType,
      data: {
          labels: ['Total Sales'],
          datasets: [{
              label: 'Sales Count',
              data: [this.totalSales],
              backgroundColor: 'rgba(75, 192, 192, 0.6)',
              barThickness: 20, // Adjust thickness here
          }]
      },
      options: this.getDefaultOptions({
          scales: {
              x: {
                  stacked: true, // Optional: If you want stacked bars
                  grid: {
                      display: false, // Hide grid lines if desired
                  }
              },
              y: {
                  beginAtZero: true
              }
          }
      })
  });
}

// Update category order counts chart
updateCategoryOrderCountsChart(): void {
  this.destroyChart(this.categoryOrderCountsChart);
  const ctx = document.getElementById('categoryOrderCountsChart') as HTMLCanvasElement;
  this.categoryOrderCountsChart = new Chart(ctx, {
      type: 'pie' as ChartType,
      data: {
          labels: Object.keys(this.categoryOrderCounts),
          datasets: [{
              label: 'Category Order Counts',
              data: Object.values(this.categoryOrderCounts),
              backgroundColor: Object.keys(this.categoryOrderCounts).map((_, index) =>
                  `rgba(${(index * 30) % 255}, ${(index * 50) % 255}, 200, 0.6)`)
          }]
      },
      options: this.getDefaultOptions()
  });
}

// updateOrdersByCustomerIdMonthWiseChart(): void{
//   this.destroyChart(this.ordersCountByCustomerChart);
//   const ctx = document.getElementById('ordersByCustomer') as HTMLCanvasElement;
//   this.ordersCountByCustomerChart = new Chart(ctx, {
//       type: 'bar' as ChartType,
//       data: {
//           labels: ['Total Orders'],
//           datasets: [{
//               label: 'Orders Count',
//               data: [this.ordersByCustomer],
//               backgroundColor: 'rgba(75, 192, 192, 0.6)',
//               barThickness: 20, // Adjust thickness here
//           }]
//       },
//       options: this.getDefaultOptions({
//           scales: {
//               x: {
//                   stacked: true, // Optional: If you want stacked bars
//                   grid: {
//                       display: false, // Hide grid lines if desired
//                   }
//               },
//               y: {
//                   beginAtZero: true
//               }
//           }
//       })
//   });
// }

// Update monthly sales chart
updateMonthlySalesChart(monthData: { [year: string]: { [month: string]: number } }): void {
  this.destroyChart(this.monthlySalesChart);
  const ctx = document.getElementById('monthlySalesChart') as HTMLCanvasElement;

  const year = Object.keys(monthData)[0]; 
  const monthlyData = monthData[year];
  const labels = Object.keys(monthlyData).map(month => new Date(0, Number(month) - 1).toLocaleString('default', { month: 'long' }));
  const values = Object.values(monthlyData);

  if (values.length === 0 || labels.length === 0) {
      console.warn('No data to display for monthly sales chart.');
      return;
  }

  this.monthlySalesChart = new Chart(ctx, {
      type: 'bar' as ChartType,
      data: {
          labels: labels,
          datasets: [{
              label: 'Monthly Sales',
              data: values,
              backgroundColor: 'rgba(75, 192, 192, 0.6)',
              barThickness: 40, // Adjust thickness here
              maxBarThickness: 40, // Max thickness
              barPercentage: 0.5, // Control spacing between bars
              categoryPercentage: 0.5 // Control category spacing
          }]
      },
      options: this.getDefaultOptions()
  });
}

// Update total products sold chart
updateTotalProductsSoldChart(): void {
  this.destroyChart(this.totalProductsSoldChart);
  const ctx = document.getElementById('totalProductsSoldChart') as HTMLCanvasElement;
  this.totalProductsSoldChart = new Chart(ctx, {
      type: 'doughnut' as ChartType,
      data: {
          labels: ['Total Products Sold'],
          datasets: [{
              data: [this.totalProductsSold, 100 - this.totalProductsSold],
              backgroundColor: ['rgba(255, 159, 64, 0.6)', 'rgba(201, 203, 207, 0.6)']
          }]
      },
      options: this.getDefaultOptions()
  });
}

// Update top selling products chart
updateTopSellingProductsChart(): void {
  this.destroyChart(this.topSellingProductsChart);
  const ctx = document.getElementById('topSellingProductsChart') as HTMLCanvasElement;
  this.topSellingProductsChart = new Chart(ctx, {
      type: 'bar' as ChartType,
      data: {
          labels: this.topSellingProducts.map(product => product.name),
          datasets: [{
              label: 'Top Selling Products',
              data: this.topSellingProducts.map(product => product.totalSales),
              backgroundColor: 'rgba(153, 102, 255, 0.6)',
              barThickness: 40, // Adjust thickness here
          }]
      },
      options: this.getDefaultOptions()
  });
}

// Update orders in date range chart
updateOrdersInDateRangeChart(): void {
  this.destroyChart(this.ordersInDateRangeChart);
  const ctx = document.getElementById('ordersInDateRangeChart') as HTMLCanvasElement;
  this.ordersInDateRangeChart = new Chart(ctx, {
      type: 'bar' as ChartType,
      data: {
          labels: this.ordersInDateRange.map(order => order.formattedDate),
          datasets: [{
              label: 'Orders Over Time',
              data: this.ordersInDateRange.map(order => order.orderCount),
              backgroundColor: 'rgba(255, 99, 132, 0.6)',
              barThickness: 40, // Adjust thickness here
          }]
      },
      options: this.getDefaultOptions()
  });
}


  // Helper to destroy existing chart instances
  private destroyChart(chart: Chart | undefined): void {
    if (chart) {
      chart.destroy();
    }
  }

  // Helper to get default options
  private getDefaultOptions(extraOptions: ChartOptions = {}): ChartOptions {
    return {
      scales: {
        y: {
          beginAtZero: true
        }
      },
      responsive: true
    };
  }
}
