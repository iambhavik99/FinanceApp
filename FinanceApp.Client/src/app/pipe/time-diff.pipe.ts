import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeDiff'
})
export class TimeDiffPipe implements PipeTransform {

  transform(value: number): string {
    const currentTime = Date.now();
    const givenTime = value; // value should be a Unix timestamp in milliseconds
    const diffMillis = currentTime - givenTime;

    const diffSeconds = Math.floor(diffMillis / 1000);
    const diffMinutes = Math.floor(diffSeconds / 60);
    const diffHours = Math.floor(diffMinutes / 60);
    const diffDays = Math.floor(diffHours / 24);

    if (diffDays > 0) {
      return `${diffDays} day(s) ago`;
    } else if (diffHours > 0) {
      return `${diffHours} hour(s) ago`;
    } else if (diffMinutes > 0) {
      return `${diffMinutes} minute(s) ago`;
    } else {
      return `${diffSeconds} second(s) ago`;
    }
  }

}
