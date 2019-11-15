using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pgh.Common.Common
{
    public class PagedList<T> : List<T>
    {
        
        public int CurrentPage { get; private set; }

        public  int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => (CurrentPage > 1);

        public bool HasNext => (CurrentPage < TotalPages);


        


        public PagedList(List<T> items, int count, int pageSize, int pageNumber)
        {
            
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            //add all the items to the list of T
            AddRange(items);
        }
        
        public  static async Task<PagedList<T>> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber == 0 || pageNumber == 1)
            {
                pageNumber = 1;
            }
            
            var count = source.Count();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();//.ToList();
            return  new  PagedList<T>(items, count, pageSize, pageNumber);
        }
        
    }
}
