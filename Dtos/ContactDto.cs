namespace ContactsManagerAPI.Dtos
{
    public record ContactDto
    {
        public Guid ContactId { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string PostAddress { get; set; }
        public string PostCode { get; set; }
    }
}
