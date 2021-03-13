using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTest.SQL.DBModels
{
    public class BaseEntity
    {
        [Key]
        [Column("UniqueId")]
        [Required]
        public Guid UniqueId { get; set; }

        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }
      
        [Column("IsActive")]
        public bool? IsActive { get; set; }

        [Column("IsDeleted")]
        public bool? IsDeleted { get; set; }
    }
}
