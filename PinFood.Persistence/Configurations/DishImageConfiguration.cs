using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PinFood.Domain.Entities;

namespace PinFood.Persistence.Configurations;

public class DishImageConfiguration : IEntityTypeConfiguration<DishImage>
{
	public void Configure(EntityTypeBuilder<DishImage> builder)
	{
		builder.HasKey(x => x.Id);
		
		builder.Property(x => x.Id).ValueGeneratedNever();
		builder.Property(x => x.FilePath).IsRequired().HasMaxLength(256);
	}
}