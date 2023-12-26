using System;
using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels.Category
{
	public class CategoryCreateVM
	{
		[Required(ErrorMessage = "Başlıq daxil edilməlidir")]
		public string Title { get; set; }

	}
}

