import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { OffersModule } from '../../offers.module';
import { Page } from 'src/app/core/models/page.model';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit {
  @Input() totalPages:number = 0;
  @Output() pageIndex = new EventEmitter<number>();
  index = 1
  nextIndexes:number[] = [];
  previousIndexes:number[] = []
  page:Page<OffersModule>;
  ngOnInit(){
    this.page = new Page([],this.index,this.totalPages)
  }

  updatePage(index:number){
    this.index = index;
    this.page.PageIndex = this.index
    this.pageIndex.next(index)
  }

  nextIndex(){
    if(this.page.nextIndex){
      this.index++
      this.page.PageIndex = this.index
      this.pageIndex.next(this.index)
    }
  }

  preIndex(){
    if(this.index >1){
      this.index--
      this.page.PageIndex = this.index
      this.pageIndex.next(this.index)
    }
  }
}
