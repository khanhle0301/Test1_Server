using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Data.Entities
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public int? ParentId { get; set; }

        // more...
    }
}