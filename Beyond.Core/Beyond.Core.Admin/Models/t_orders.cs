using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Beyond.Core.Admin.Models
{
    [Table("t_orders", Schema="dbo")]
    public class t_orders
    {
        [Key]
        public int id { get; set; }

        [MaxLength(30), Required(AllowEmptyStrings = false, ErrorMessage = "订单编号不能为空")]
        [Column(TypeName = "nvarchar")]
        public string order_no { get; set; }
    }
}