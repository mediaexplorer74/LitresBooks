//

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace LitresBooks
{
   
    [Table("Quote")]
    public partial class Quote
    {
        public int QuoteID { get; set; }

        [Required]
        public string QuoteText { get; set; }

        public int BookID { get; set; }

        public virtual Book Book { get; set; }
    }
}
