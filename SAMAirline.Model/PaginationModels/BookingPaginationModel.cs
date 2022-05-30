using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Model.PaginationModels
{
    public class BookingPaginationModel
    {
        public int? UserId { get; set; }
        public int RowsCount { get; set; }
        public int PagesCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public IEnumerable BookingList { get; set; }
        public string Message { get; set; }

        public BookingPaginationModel()
        {

        }
        public BookingPaginationModel(int userId, int rowsCount, int pagesCount, int pageSize, int pageNumber, IEnumerable list, string message)
        {
            this.UserId = userId;
            this.RowsCount = rowsCount;
            this.PagesCount = pagesCount;
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.BookingList = list;
            this.Message = message;
        }
    }
}
