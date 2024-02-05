using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
      

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Alamat { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string NoTelepon { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime TanggalLahir { get; set; }
    }
}
