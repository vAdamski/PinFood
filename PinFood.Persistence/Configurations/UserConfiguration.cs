using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PinFood.Domain.Entities;

namespace PinFood.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(u => u.Id);
		
		builder.Property(u => u.Id).ValueGeneratedNever();

		builder.Property(u => u.FirstName)
			.IsRequired()
			.HasMaxLength(256);

		builder.Property(u => u.LastName)
			.IsRequired()
			.HasMaxLength(256);

		builder.Property(u => u.Email)
			.IsRequired()
			.HasMaxLength(256);

		builder.Property(u => u.PasswordHash)
			.IsRequired()
			.HasMaxLength(256);
	}
}