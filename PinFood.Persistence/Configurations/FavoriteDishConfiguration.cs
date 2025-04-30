using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PinFood.Domain.Entities;

namespace PinFood.Persistence.Configurations;

public class FavoriteDishConfiguration : IEntityTypeConfiguration<FavoriteDish>
{
	public void Configure(EntityTypeBuilder<FavoriteDish> builder)
	{
		builder.HasKey(fd => fd.Id);
		builder.Property(fd => fd.Id)
			.ValueGeneratedNever();

		builder.Property(fd => fd.DishId)
			.IsRequired();

		builder.Property(fd => fd.Email)
			.IsRequired()
			.HasMaxLength(256);

		builder.HasOne(fd => fd.Dish)
			.WithMany()
			.HasForeignKey(fd => fd.DishId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}