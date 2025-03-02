using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsManagerAPI.Models
{
    // ---- Customer Model Class ----
   

    // ---- Contact Model Class ----
    public class Contact
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ContactId { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string PostAddress { get; set; }
        public string PostCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set;}
        public string LastModifiedBy { get; set;}
        public DateTime LastModifiedAt { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }
    }


}
