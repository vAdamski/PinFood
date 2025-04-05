using PinFood.Domain.Enums;
using PinFood.Domain.Primitives;

namespace PinFood.Domain.Common;

public class AuditableEntity : EntityEvent
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string CreatedBy { get; set; } = "";
	public DateTime Created { get; set; }
	public string ModifiedBy { get; set; } = "";
	public DateTime Modified { get; set; }
	public AuditableEntityStatus Status { get; set; }
	public string InactivatedBy { get; set; } = "";
	public DateTime? Inactivated { get; set; }
}