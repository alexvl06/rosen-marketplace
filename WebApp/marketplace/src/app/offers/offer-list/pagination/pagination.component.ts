import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit {
  @Input() totalPages:number = 0;
  @Output() pageIndex = new EventEmitter<number>();
  counter = Array
  index = 1
  btnIndexes:number[]
  partialStart:number = 1;
  partialEnd:number =7
  ngOnInit(){
    this.createNumberArray(this.partialStart, this.partialEnd)
  }

  updatePage(index:number){
    this.index = index;
    this.pageIndex.next(index)
  }

  nextIndex(){
    if(this.index <this.totalPages){
      this.index ++
      this.pageIndex.next(this.index)
    }
    if(this.index > this.btnIndexes[this.btnIndexes.length-1]){
      this.partialStart++
      this.partialEnd++
      this.createNumberArray(this.partialStart,this.partialEnd)
    }
  }

  preIndex(){
    if(this.index >1){
      this.index --
      this.pageIndex.next(this.index)
    }
    if(this.index < this.btnIndexes[0]){
      this.partialStart--
      this.partialEnd--
      this.createNumberArray(this.partialStart,this.partialEnd)
    }
  }

  createNumberArray(start:number, end:number){
    this.btnIndexes = Array.from({length:end-start+1},(_,index)=>start+index)
  }
}
