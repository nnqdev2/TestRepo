using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpiPlanTool.Models {

  [Table("EPT_ORDER")]
  public class OrderDetail {
    public OrderDetail() {  }

    [Key]
    public long BookedOrderID { get; set; }
    public DateTime? MasterSchedDate { get; set; }

//    [ForeignKey("BookedOrderID")]
    public BookedOrder BookedOrder { get; set; }

  }
}
