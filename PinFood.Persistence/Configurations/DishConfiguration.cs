using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PinFood.Domain.Entities;

namespace PinFood.Persistence.Configurations;

public class DishConfiguration : IEntityTypeConfiguration<Dish>
{
	public void Configure(EntityTypeBuilder<Dish> builder)
	{
		builder.HasKey(d => d.Id);
		builder.Property(d => d.Id).ValueGeneratedNever();
		
		builder.Property(d => d.DishName)
			.IsRequired()
			.HasMaxLength(256);

		builder.Property(d => d.Description)
			.IsRequired()
			.HasMaxLength(1000);

		builder.HasMany(d => d.RecipeSteps)
			.WithOne(r => r.Dish)
			.HasForeignKey(r => r.DishId)
			.OnDelete(DeleteBehavior.Cascade);
		
		builder.HasMany(d => d.DishImages)
			.WithOne(r => r.Dish)
			.HasForeignKey(r => r.DishId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}