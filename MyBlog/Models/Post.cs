using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Models;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Judul harus diisi.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Konten harus diisi.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "Tanggal dibuat harus diisi.")]
    public DateTime CreatedDate { get; set; }

    [Required(ErrorMessage = "Jumlah Likes harus diisi.")]
    public int Likes { get; set; }
}

