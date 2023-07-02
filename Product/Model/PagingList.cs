namespace Products.Model
{
    public class PagingList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
        public PagingList(List<T> items, int count, int pageIndex, int pageSize) 
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static PagingList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();

            return new PagingList<T>(items, count, pageIndex, pageSize);
        }
    }
}
