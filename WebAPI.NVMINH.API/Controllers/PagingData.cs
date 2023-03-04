using WebAPI.NVMINH.API.Entities;

namespace WebAPI.NVMINH.API.Controllers
{
    internal class PagingData<T>
    {
        public PagingData()
        {
        }

        public List<Employees> Data { get; set; }
        public long TotalCount { get; set; }
    }
}