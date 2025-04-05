using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PinFood.Domain.Entities;

namespace PinFood.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
	public void Configure(EntityTypeBuilder<RefreshToken> builder)
	{
		builder.HasKey(rt => rt.Id);
		
		builder.Property(rt => rt.Id).ValueGeneratedNever();

		builder.Property(rt => rt.UserId)
			.IsRequired();
		
		builder.Property(rt => rt.Token)
			.IsRequired()
			.HasMaxLength(256);
		
		builder.HasIndex(rt => rt.Token)
			.IsUnique();

		builder.Property(rt => rt.ExpiresTime)
			.IsRequired();

		builder.HasOne(rt => rt.User)
			.WithMany()
			.HasForeignKey(rt => rt.UserId);
	}
}