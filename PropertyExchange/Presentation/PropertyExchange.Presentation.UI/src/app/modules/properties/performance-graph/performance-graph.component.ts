import { formatDate } from '@angular/common';
import { SimpleChanges } from '@angular/core';
import { Component, Input } from '@angular/core';
import { Property, PropertyTradeRecords, PropertyValuationMetrics } from 'src/app/shared/models/property.model';

@Component({
  selector: 'app-performance-graph',
  templateUrl: './performance-graph.component.html',
  styleUrl: './performance-graph.component.css'
})
export class PerformanceGraphComponent {
    @Input() tradeRecords!: PropertyTradeRecords[];
    @Input() valuationMetrics!: PropertyValuationMetrics[];


  data: any;
  options: any;

  //ngOnChanges(changes: SimpleChanges) {
   // if (this.refresh) {
      // Handle refresh event
     // this.loadData(); // Reload data in child component
    //}
  //}

  ngOnInit() {
    this.loadData();
}

loadData(){
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');
    if(this.tradeRecords != null){
    this.data = {
        labels: this.tradeRecords.map(x => formatDate(x.CreatedDate, 'dd-MMM-yyyy', 'en-US')), 
        datasets: [
            {
                label: 'Trade',
                data: this.tradeRecords.map(x => x.CurrentTokenPrice),
                fill: true,
                borderColor: documentStyle.getPropertyValue('--green-500'),
                tension: 0.4,
                backgroundColor: 'rgb(0, 128, 0, 0.2)'
            }
        ]
    };
    }
    if(this.valuationMetrics != null){
    this.data = {
        labels: this.valuationMetrics.map(x => formatDate(x.CreatedDate, 'dd-MMM-yyyy', 'en-US')), 
        datasets: [
            {
                label: 'Valuation - Price per Sft',
                data: this.valuationMetrics.map(x => x.PricePerSft),
                fill: true,
                borderColor: documentStyle.getPropertyValue('--green-500'),
                tension: 0.4,
                backgroundColor: 'rgb(0, 128, 0, 0.2)'
            }
        ]
    };
}
    this.options = {
        responsive: true,
        maintainAspectRatio: false,
        aspectRatio: 0.6,
        plugins: {
            legend: {
                labels: {
                    color: textColor
                }
            }
        },
        scales: {
            x: {
                ticks: {
                    color: textColorSecondary,
                    display: false 
                },
                grid: {
                    color: surfaceBorder
                }
            },
            y: {
                ticks: {
                    color: textColorSecondary
                },
                grid: {
                    color: surfaceBorder
                }
            }
        }
    };
}
}
