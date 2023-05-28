import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'concatBeforeIfNotURLOnline'
})
export class ImgURLCustomizerPipe implements PipeTransform {

  transform(value:string): string {
    if(!value.includes('http')){
      return `../../assets/${value}`
    }
    return value;
  }

}
