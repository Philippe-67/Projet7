using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P7CreateRestApi.Domain
{
    public class Rating
    {
    /// <summary>
    /// /   [Key]
    /// </summary>
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Le Champs MoodysRating est requis.")]

        public string MoodysRating { get; set; }

        [Required(ErrorMessage = "Le champ SandPrating est requis.")]
        public string SandPRating { get; set; }

        [Required(ErrorMessage = " Le champ FitchRating est requis.")]
        public string FitchRating { get; set; }
        public byte? OrderNumber { get; set; }
    }
}