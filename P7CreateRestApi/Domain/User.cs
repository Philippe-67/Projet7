using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Domain
{
    public class User
    {
        [Required(ErrorMessage = "Le champ Id est requis.")]
        public string Id { get; set; }

        public string? UserName { get; set; }

        [Required(ErrorMessage = "Le champ Password est requis.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le champ Fullname est requis.")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Le champ Role est requis.")]
        public string Role { get; set; }
    }
}
