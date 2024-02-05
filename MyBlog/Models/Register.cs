using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
	public class Register
	{
		[Required]
		public string Username { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

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
