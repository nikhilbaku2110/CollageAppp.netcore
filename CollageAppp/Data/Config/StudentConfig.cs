using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollageAppp.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("students");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.StudentName).IsRequired();
            builder.Property(n => n.StudentName).HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);

            builder.HasData(new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    StudentName = "venkat",
                    Address = "india",
                    Email = "venkat@gmail.com",
                    DOB = new DateTime(2022,12,12)
                },
                new Student
                {
                    Id = 2,
                    StudentName = "nikhil",
                    Address = "india",
                    Email = "nikhl@gmail.com",
                    DOB = new DateTime(2022,12,12)
                },
            });
        }
    }
}
