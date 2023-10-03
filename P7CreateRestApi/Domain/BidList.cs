using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Domain
{
    public class BidList
    {
        public int BidListId { get; set; }

        [Required(ErrorMessage = "Le champ Account est requis.")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Le champ BidType est requis.")]
        public string BidType { get; set; }

        public double? BidQuantity { get; set; }

        public double? AskQuantity { get; set; }

        public double? Bidamount { get; set; }

        public double? Ask { get; set; }

        public string Benchmark { get; set; }

        [Required(ErrorMessage = "La BidListDate est requise.")]
        public DateTime? BidListDate { get; set; }

        [Required(ErrorMessage = "Le champ Commentary est requis.")]
        public string Commentary { get; set; }

        [Required(ErrorMessage = "Le champ BidSecurity est requis.")]
        public string BidSecurity { get; set; }

        [Required(ErrorMessage = "Le champ BidStatus est requis.")]
        public string BidStatus { get; set; }

        [Required(ErrorMessage = "Le champ Trader est requis.")]
        public string Trader { get; set; }

        [Required(ErrorMessage = "Le champ Book est requis.")]
        public string Book { get; set; }

        [Required(ErrorMessage = "Le champ CreationName est requis.")]
        public string CreationName { get; set; }

        [Required(ErrorMessage = "La CreationDate est requise.")]
        public DateTime? CreationDate { get; set; }

        [Required(ErrorMessage = "Le champ RevisionName est requis.")]
        public string RevisionName { get; set; }

        [Required(ErrorMessage = "La RevisionDate est requise.")]
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
}
