using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTest.SQL.DBModels
{
    [Table("Payments")]
    public class Payment : BaseEntity
    {
        [Column("CreditCardNumber")]
        [Required]
        [StringLength(50)]
        public string CreditCardNumber { get; set; }

        [Column("CardHolder")]
        [Required]
        [StringLength(100)]
        public string CardHolder { get; set; }

        [Column("ExpirationDate")]
        [Required]
        public DateTime ExpirationDate { get; set; }

        [Column("SecurityCode")]
        [Required]
        [StringLength(3)]
        public string SecurityCode { get; set; }

        [Column("Amount")]
        [Required]
        public decimal Amount { get; set; }

        [Column("GateWayType")]
        [Required]
        [StringLength(20)]
        public string GateWayType { get; set; }

        [Column("Status")]
        [Required]
        [StringLength(50)]
        public string Status { get; set; }

    }
}
