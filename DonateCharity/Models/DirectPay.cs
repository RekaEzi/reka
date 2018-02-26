using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DonateCharity.Models
{
    public class DirectPay
    {
        public string Charity { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        [DisplayName("Card Name")]
        [Required(ErrorMessage = "Card Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string CardName { get; set; }
        [DisplayName("Card Number")]
        [Required(ErrorMessage = "Card Number is required")]
        [StringLength(16)]
        public string CardNumber { get; set; }
        public string CardExpiryMonth { get; set; }
        public string CardExpiryYear { get; set; }
        [DisplayName("CVV")]
        [Required(ErrorMessage = "CVV is required")]
        [StringLength(3)]
        public string CVN { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        [Range(1, 10000000)]
        public int TotalAmount { get; set; }
        public string TransactionType { get; set; }
       
        
       
    }
}
