import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'percentageChange'
})
export class PercentageChangePipe implements PipeTransform {
  transform(previousValue: number, currentValue: number): string {
    const change = currentValue - previousValue;

    if (change > 0) {
      return `+${((change / previousValue) * 100).toFixed(2)}%`;
    } else if (change < 0) {
      return `-${((Math.abs(change) / previousValue) * 100).toFixed(2)}%`;
    } else {
      return '0%';
    }
  }
}
