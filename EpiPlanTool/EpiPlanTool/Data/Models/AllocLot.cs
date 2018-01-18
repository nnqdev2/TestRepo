using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DGO = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace EpiPlanTool.Models {

  [Table("EPT_ALLOC_LOTS_V")]
  public class AllocLot {

    [Key, DatabaseGenerated(DGO.None)]
    public string AllocLotID { get; set; }
    public string OrderID { get; set; }
    public string LotNum { get; set; }
    public string PPN { get; set; }
    public string MAPL { get; set; }
    public string Product { get; set; }
    [Column("OPER_MVIN")]
    public string Oper { get; set; }
    [Column("WC2")]
    public string WC { get; set; }
    public int WorkcellIndex { get; set; }
    [Column("OUT_QTY")]
    public int? LotQty { get; set; }

    //[ForeignKey("BookedOrderID")]
    public long BookedOrderId { get; set; }
    public virtual BookedOrder BookedOrder { get; set; }

  }
}
