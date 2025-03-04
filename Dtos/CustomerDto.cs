using ContactsManagerAPI.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsManagerAPI.Dtos
{
    public record CustomerDto
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }

        public Collection<Contact> Contacts { get; set; }   
    }
}
