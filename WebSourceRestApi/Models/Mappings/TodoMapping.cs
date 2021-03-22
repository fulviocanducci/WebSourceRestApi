using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebSourceRestApi.Models.Mappings
{
    public class TodoMapping : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("todos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Active)
                .HasColumnName("active")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("createdAt")
                .IsRequired();

            builder.Property(x => x.UpdateAt)
                .HasColumnName("updatedAt")
                .IsRequired();
        }
    }
}
