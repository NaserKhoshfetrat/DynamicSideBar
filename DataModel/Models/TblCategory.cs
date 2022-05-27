using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class TblCategory
    {
        public int Id { get; set; }
        public int ParentCategoryId { get; set; }
        public string Title { get; set; } = null!;
        public int Priority { get; set; }
        public bool Deleted { get; set; }
    }
}
