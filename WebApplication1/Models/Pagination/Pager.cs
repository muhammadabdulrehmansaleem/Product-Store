namespace WebApplication1.Models.Pagination
{
    public class Pager
    {
        public int TotalItems { get;private set; }
        public int TotalPages { get; private set;}
        public int CurrentPage { get; private set;}
        public int PageSize { get; private set;}
        public int StartPage { get;private set;}
        public int EndPage { get; private set;}
        public Pager()
        {

        }
        public Pager(int totalItems,int page,int pageSize=10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems/(decimal)pageSize);
            int currentPage = page;
            int startPgae = currentPage - 5;
            int endPage = currentPage + 4;
            if(startPgae<=0)
            {
                endPage = endPage - (startPgae - 1);
                startPgae = 1;
            }
            if(endPage>totalPages)
            {
                endPage = totalPages;
            }
            TotalPages = totalPages;
            StartPage = startPgae;
            EndPage = endPage;
            CurrentPage=currentPage;
            PageSize=pageSize;
            TotalItems=totalItems;
        }
        
    }
}
