using System;
using System.ComponentModel.DataAnnotations;

public class Trade
{
    public int TradeId { get; set; }

    [Required(ErrorMessage = "Le champ Account est requis.")]
    public string Account { get; set; }

    [Required(ErrorMessage = "Le champ AccountType est requis.")]
    public string AccountType { get; set; }

    [Required(ErrorMessage = "La quantité d'achat (BuyQuantity) est requise.")]
    public double? BuyQuantity { get; set; }

    [Required(ErrorMessage = "La quantité de vente (SellQuantity) est requise.")]
    public double? SellQuantity { get; set; }

    [Required(ErrorMessage = "Le prix d'achat (BuyPrice) est requis.")]
    public double? BuyPrice { get; set; }

    [Required(ErrorMessage = "Le prix de vente (SellPrice) est requis.")]
    public double? SellPrice { get; set; }

    [Required(ErrorMessage = "La date de la transaction (TradeDate) est requise.")]
    public DateTime? TradeDate { get; set; }

    [Required(ErrorMessage = "Le champ TradeSecurity est requis.")]
    public string TradeSecurity { get; set; }

    [Required(ErrorMessage = "Le champ TradeStatus est requis.")]
    public string TradeStatus { get; set; }

    [Required(ErrorMessage = "Le champ Trader est requis.")]
    public string Trader { get; set; }

    public string Benchmark { get; set; }

    [Required(ErrorMessage = "Le champ Book est requis.")]
    public string Book { get; set; }

    [Required(ErrorMessage = "Le champ CreationName est requis.")]
    public string CreationName { get; set; }

    [Required(ErrorMessage = "La date de création (CreationDate) est requise.")]
    public DateTime? CreationDate { get; set; }

    [Required(ErrorMessage = "Le champ RevisionName est requis.")]
    public string RevisionName { get; set; }

    [Required(ErrorMessage = "La date de révision (RevisionDate) est requise.")]
    public DateTime? RevisionDate { get; set; }

    [Required(ErrorMessage = "Le champ DealName est requis.")]
    public string DealName { get; set; }

    [Required(ErrorMessage = "Le champ DealType est requis.")]
    public string DealType { get; set; }

    [Required(ErrorMessage = "Le champ SourceListId est requis.")]
    public string SourceListId { get; set; }

    [Required(ErrorMessage = "Le champ Side est requis.")]
    public string Side { get; set; }
}
