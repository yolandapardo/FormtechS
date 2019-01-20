using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FormtechS.Models
{
    public class Description
    {
        public int Id { get; set; }
        [StringLength(5)]
        [Required]
        [Index("IX_LotAndBlockAndPlatAndPage",1,IsUnique =true)]
        public string Lot { get; set; }
        [StringLength(5)]
        [Index("IX_LotAndBlockAndPlatAndPage", 2, IsUnique = true)]
        public string Block { get; set; }
        [StringLength(5)]
        [Index("IX_LotAndBlockAndPlatAndPage", 3, IsUnique = true)]
        public string Plat { get; set; }
        [StringLength(5)]
        [Index("IX_LotAndBlockAndPlatAndPage", 4, IsUnique = true)]
        public string Page { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}