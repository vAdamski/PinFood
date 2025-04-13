using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PinFood.Domain.Entities;

namespace PinFood.Persistence.Configurations;

public class RecipeStepConfiguration : IEntityTypeConfiguration<RecipeStep>
{
	public void Configure(EntityTypeBuilder<RecipeStep> builder)
	{
		builder.HasKey(r => r.Id);
		builder.Property(r => r.Id).ValueGeneratedNever();

		builder.Property(r => r.Name)
			.IsRequired()
			.HasMaxLength(256);

		builder.Property(r => r.Description)
			.IsRequired()
			.HasMaxLength(1000);

		builder.Property(r => r.Order)
			.IsRequired();

		builder.HasOne(r => r.Dish)
			.WithMany(d => d.RecipeSteps)
			.HasForeignKey(r => r.DishId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}