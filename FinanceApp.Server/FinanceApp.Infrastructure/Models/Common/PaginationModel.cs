using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Models.Common
{
    public class PaginationModel
    {
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
        public string sortBy { get; set; }
        public string sortDirection { get; set; }
    }
}
