namespace EpiPlanTool.Models {
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using DGO = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

  [Table("EPT_BOOKED_ORDERS_V")]   
  public class BookedOrder {

    public BookedOrder() {
      Lots = new List<AllocLot>();
    }

    [Key]
    public long BookedOrderID { get; set; }
    public string OrderID { get; set; }
    public string OrdNum { get; set; }
    public string OrdItem { get; set; }
    public string OrdType { get; set; }
    public string MAPL { get; set; }
    public int BookQty { get; set; }
    public DateTime BookDate { get; set; }
    public DateTime? MasterSchedDate { get; set; }
    public string CW { get; set; }
    public int AllocQty { get; set; }
    public int RemToProd { get; set; }
    public int? ReqForProd { get; set; }
    [Column("SHORT_QTY")]
    public int? SHORT { get; set; }
    [Column("TRANS_QTY")]
    public int? TRANS { get; set; }
    [Column("BIN_QTY")]
    public int? BIN { get; set; }
    [Column("LC_QTY")]
    public int? LC { get; set; }
    [Column("MWS_QTY")]
    public int? MWS { get; set; }
    [Column("WC02_QTY")]
    public int? WC02 { get; set; }
    [Column("WC03_QTY")]
    public int? WC03 { get; set; }
    [Column("WC04_QTY")]
    public int? WC04 { get; set; }
    [Column("HTQ_QTY")]
    public int? HTQ { get; set; }
    [Column("WC05_QTY")]
    public int? WC05 { get; set; }
    [Column("WC06_QTY")]
    public int? WC06 { get; set; }
    [Column("WC07_QTY")]
    public int? WC07 { get; set; }
    [Column("WC08_QTY")]
    public int? WC08 { get; set; }
    [Column("WC09_QTY")]
    public int? WC09 { get; set; }
    public int? PostQty { get; set; }
    [Column("MG_QTY")]
    public int? MG { get; set; }
    [Column("FG_QTY")]
    public int? FG { get; set; }
    [Column("WIP_QTY")]
    public int? WIP { get; set; }
    public int? FinQty { get; set; }
    public int? CP { get; set; }
    public int? RW { get; set; }
    public int? PP { get; set; }
    public int? ES { get; set; }
    [Column("ASM_WPD")]
    public decimal? AsmWPD { get; set; }
    public string AsmRecipe1 { get; set; }
    [Column("CEN_WPD")]
    public double? CenWPD { get; set; }
    public string CenRecipe1 { get; set; }
    public string CustShortName { get; set; }
    public string CustSpecNickName { get; set; }
    public string Dopant { get; set; }
    [Column("EPI1_DOPANT")]
    public string Dopant1 { get; set; }
    [Column("EPI1_RES_TGT")]
    public Nullable<decimal> ResTgt1 { get; set; }
    [Column("EPI1_THICK_TGT")]
    public decimal? ThickTgt1 { get; set; }
    public string ReactQual { get; set; }
    public string ReactType { get; set; }
    [Column("EPI_LAYER_COUNT_TGT")]
    public int? Layers { get; set; }
    [Column("CV_LINE")]
    public string CV1 { get; set; }
    [Column("CV2_LINE")]
    public string CV2 { get; set; }
    public int? CV_FREQ { get; set; }
    public bool DualQual { get; set; }
    [Column("WS_PRODUCT")]
    public string Product { get; set; }

    public virtual List<AllocLot> Lots { get; set; }

  }
}
