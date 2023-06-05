export class Page<T> {
  private items:T[]
  private pageIndex:number
  private pageCount:number

  constructor(items:T[], pageIndex:number, pageCount:number){
    this.items = items
    this.pageIndex = pageIndex
    this.pageCount = pageCount
  }
    get Items():T[]{
      return this.items
    }

    get PageIndex(){
      return this.pageIndex
    }

    get nextIndex(){
      if(this.pageIndex+1 >this.pageCount){
        return null
      }
      return this.pageIndex+1;
    }

    get PageCount(){
      return this.pageCount
    }

    get previousIndex(){
      if(this.pageIndex-1<1){
        return null
      }
      return this.pageIndex-1;
    }

    set PageIndex(index:number){
      this.pageIndex = index
    }

    getNextPageIndexes():number[]|null{
      let start = this.pageIndex+1;
      let end = this.pageIndex+3;
      if(start>this.pageCount){
        return null
      }
      if(end>this.pageCount){
        while(end>this.pageCount){
          end --
        }
      }
      return this.createNumberArray(start,end)
    }

    getPreviousPageIndexes():number[]|null{
      let end = this.pageIndex-1;
      let start = this.pageIndex-3;
      if(end<1){
        return null
      }
      if(start<1){
        while(start<1){
          start ++
        }
      }
      return this.createNumberArray(start,end)
    }



  private createNumberArray(start:number, end:number){
    return Array.from({length:end-start+1},(_,index)=>start+index)
  }

}
