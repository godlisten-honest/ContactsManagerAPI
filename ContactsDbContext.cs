using ContactsManagerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsManagerAPI
{
    public class ContactsDbContext:DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //m ---- Customer Mapping ----
            // Customer Mapping
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerId)
                .HasColumnName("customer_id")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>()
                .Property(c => c.Name).HasColumnName("organization_name");
            modelBuilder.Entity<Customer>()
                .Property(c => c.CreatedBy).HasColumnName("created_by");
            modelBuilder.Entity<Customer>()
                .Property(c => c.CreatedAt).HasColumnName("created_at")
                .HasDefaultValueSql("now()");
            modelBuilder.Entity<Customer>()
                .Property(c => c.LastModifiedBy).HasColumnName("last_modified_by");
            modelBuilder.Entity<Customer>()
                .Property(c => c.LastModifiedAt).HasColumnName("last_modified_at")
                .HasDefaultValueSql("now()");


            //m ---- Contact Mapping ----
            modelBuilder.Entity<Contact>().ToTable("Contacts");
                modelBuilder.Entity<Contact>()
                        .Property(c => c.ContactId)
                        .HasColumnName("contact_id")
                        .ValueGeneratedOnAdd();
            modelBuilder.Entity<Contact>()
                        .Property(c => c.Name).HasColumnName("name");
            modelBuilder.Entity<Contact>()
                       .Property(c => c.Title).HasColumnName("title");
            modelBuilder.Entity<Contact>()
                       .Property(c => c.CustomerId).HasColumnName("customer_id");
            modelBuilder.Entity<Contact>()
                        .Property(c => c.PostCode).HasColumnName("post_code");
            modelBuilder.Entity<Contact>()
                        .Property(c => c.EmailAddress).HasColumnName("email_adddress");
            modelBuilder.Entity<Contact>()
                        .Property(c => c.PhoneNo).HasColumnName("phone_no");
            modelBuilder.Entity<Contact>()
                        .Property(c => c.PostAddress).HasColumnName("post_address");
            modelBuilder.Entity<Contact>()
                        .Property(c => c.CreatedBy).HasColumnName("created_by");
            modelBuilder.Entity<Contact>()
                        .Property(c => c.CreatedAt).HasColumnName("created_at")
                        .HasDefaultValueSql("now()");
            modelBuilder.Entity<Contact>()
                        .Property(c => c.LastModifiedBy).HasColumnName("last_modified_by");
            modelBuilder.Entity<Contact>()
                        .Property(c => c.LastModifiedAt).HasColumnName("last_modified_at")
                        .HasDefaultValueSql("now()");

            //modelBuilder.Entity<Customer>()
            //   .HasMany(c => c.Contacts)
            //   .WithOne(c => c.Customer);


            // Relationship Mapping: One-to-Many relationship (Customer - Contact)
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Customer)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); 

        }

    }
}
