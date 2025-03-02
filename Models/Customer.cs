using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsManagerAPI.Models
{
    public class Customer
    {
        public Customer()
        {
            Contacts = new HashSet<Contact>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedAt { get; set; }

        [InverseProperty(nameof(Contact.Customer))]
        public virtual  ICollection<Contact> Contacts { get; set; }
    }
}
