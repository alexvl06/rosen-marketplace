import { Page } from "./page.model";

fdescribe('PageModel', () => {

  describe('Only one page is available', ()=>{
    const page = new Page([1], 1, 1)
    it('next page indexes getter should return null',()=>{
      const nextIndexes = page.getNextPageIndexes()
      expect(nextIndexes).toBeNull()
    })

    it('previous page indexes getter should return null',()=>{
      const nextIndexes = page.getPreviousPageIndexes()
      expect(nextIndexes).toBeNull()
    })

    it('page count should return one',()=>{
      const count = page.PageCount
      expect(count).toEqual(1)
    })

    it('index should be one',()=>{
      const index = page.PageIndex
      expect(index).toEqual(1)
    })

    it('next index should be null',()=>{
      const index = page.nextIndex
      expect(index).toBeNull()
    })

    it('previous index should be null',()=>{
      const index = page.previousIndex
      expect(index).toBeNull()
    })
  })


  describe('10 pages are available and the index is in the first page', ()=>{
    const page = new Page([], 1, 10)

    it('next page indexes getter should return [2, 3, 4]',()=>{
      const nextIndexes = page.getNextPageIndexes()
      expect(nextIndexes).toEqual([2,3,4])
    })

    it('previous page indexes getter should return null',()=>{
      const nextIndexes = page.getPreviousPageIndexes()
      expect(nextIndexes).toBeNull()
    })

    it('page count should return ten',()=>{
      const count = page.PageCount
      expect(count).toEqual(10)
    })

    it('index should be one',()=>{
      const index = page.PageIndex
      expect(index).toEqual(1)
    })

    it('next index should be 2',()=>{
      const index = page.nextIndex
      expect(index).toEqual(2)
    })

    it('previous index should be null',()=>{
      const index = page.previousIndex
      expect(index).toBeNull()
    })
  })

  describe('10 pages are available and the index is in the second page', ()=>{
    const page = new Page([], 2, 10)

    it('next page indexes getter should return [3, 4, 5]',()=>{
      const nextIndexes = page.getNextPageIndexes()
      expect(nextIndexes).toEqual([3,4,5])
    })

    it('previous page indexes getter should return [1]',()=>{
      const nextIndexes = page.getPreviousPageIndexes()
      expect(nextIndexes).toEqual([1])
    })

    it('page count should return ten',()=>{
      const count = page.PageCount
      expect(count).toEqual(10)
    })

    it('index should be 2',()=>{
      const index = page.PageIndex
      expect(index).toEqual(2)
    })

    it('next index should be 2',()=>{
      const index = page.nextIndex
      expect(index).toEqual(3)
    })

    it('previous index should be 1',()=>{
      const index = page.previousIndex
      expect(index).toEqual(1)
    })
  })

  describe('10 pages are available and the index is in the third page', ()=>{
    const page = new Page([], 3, 10)

    it('next page indexes getter should return [4, 5, 6]',()=>{
      const nextIndexes = page.getNextPageIndexes()
      expect(nextIndexes).toEqual([4,5,6])
    })

    it('previous page indexes getter should return [1, 2]',()=>{
      const nextIndexes = page.getPreviousPageIndexes()
      expect(nextIndexes).toEqual([1, 2])
    })

    it('page count should return ten',()=>{
      const count = page.PageCount
      expect(count).toEqual(10)
    })

    it('index should be 3',()=>{
      const index = page.PageIndex
      expect(index).toEqual(3)
    })

    it('next index should be 4',()=>{
      const index = page.nextIndex
      expect(index).toEqual(4)
    })

    it('previous index should be 2',()=>{
      const index = page.previousIndex
      expect(index).toEqual(2)
    })
  })

  describe('10 pages are available and the index is in the fifth page', ()=>{
    const page = new Page([], 5, 10)

    it('next page indexes getter should return [6, 7, 8]',()=>{
      const nextIndexes = page.getNextPageIndexes()
      expect(nextIndexes).toEqual([6,7,8])
    })

    it('previous page indexes getter should return [2, 3, 4]',()=>{
      const nextIndexes = page.getPreviousPageIndexes()
      expect(nextIndexes).toEqual([2, 3, 4])
    })

    it('page count should return ten',()=>{
      const count = page.PageCount
      expect(count).toEqual(10)
    })

    it('index should be 5',()=>{
      const index = page.PageIndex
      expect(index).toEqual(5)
    })

    it('next index should be 6',()=>{
      const index = page.nextIndex
      expect(index).toEqual(6)
    })

    it('previous index should be 4',()=>{
      const index = page.previousIndex
      expect(index).toEqual(4)
    })
  })

  describe('10 pages are available and the index is in the eighth page', ()=>{
    const page = new Page([], 8, 10)

    it('next page indexes getter should return [ 9, 10]',()=>{
      const nextIndexes = page.getNextPageIndexes()
      expect(nextIndexes).toEqual([9,10])
    })

    it('previous page indexes getter should return [5, 6, 7]',()=>{
      const nextIndexes = page.getPreviousPageIndexes()
      expect(nextIndexes).toEqual([5, 6, 7])
    })

    it('page count should return ten',()=>{
      const count = page.PageCount
      expect(count).toEqual(10)
    })

    it('index should be 8',()=>{
      const index = page.PageIndex
      expect(index).toEqual(8)
    })

    it('next index should be 9',()=>{
      const index = page.nextIndex
      expect(index).toEqual(9)
    })

    it('previous index should be 7',()=>{
      const index = page.previousIndex
      expect(index).toEqual(7)
    })
  })

  describe('10 pages are available and the index is in the nineth page', ()=>{
    const page = new Page([], 9, 10)

    it('next page indexes getter should return [10]',()=>{
      const nextIndexes = page.getNextPageIndexes()
      expect(nextIndexes).toEqual([10])
    })

    it('previous page indexes getter should return [ 6, 7, 8]',()=>{
      const nextIndexes = page.getPreviousPageIndexes()
      expect(nextIndexes).toEqual([ 6, 7,  8])
    })

    it('page count should return ten',()=>{
      const count = page.PageCount
      expect(count).toEqual(10)
    })

    it('index should be 9',()=>{
      const index = page.PageIndex
      expect(index).toEqual(9)
    })

    it('next index should be 10',()=>{
      const index = page.nextIndex
      expect(index).toEqual(10)
    })

    it('previous index should be 8',()=>{
      const index = page.previousIndex
      expect(index).toEqual(8)
    })
  })

  describe('10 pages are available and the index is in the last page', ()=>{
    const page = new Page([], 10, 10)

    it('next page indexes getter should return null',()=>{
      const nextIndexes = page.getNextPageIndexes()
      expect(nextIndexes).toBeNull()
    })

    it('previous page indexes getter should return [ 7, 8, 9]',()=>{
      const nextIndexes = page.getPreviousPageIndexes()
      expect(nextIndexes).toEqual([ 7,  8, 9])
    })

    it('page count should return ten',()=>{
      const count = page.PageCount
      expect(count).toEqual(10)
    })

    it('index should be 10',()=>{
      const index = page.PageIndex
      expect(index).toEqual(10)
    })

    it('next index should null',()=>{
      const index = page.nextIndex
      expect(index).toBeNull()
    })

    it('previous index should be 9',()=>{
      const index = page.previousIndex
      expect(index).toEqual(9)
    })
  })
});
