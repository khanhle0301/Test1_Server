using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.Services.ViewModels
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        [StringLength(250)]
        [Required]
        public string Name { get; set; }

        public int? ParentId { get; set; }

        public List<MenuViewModel> Children { get; set; }
    }
}