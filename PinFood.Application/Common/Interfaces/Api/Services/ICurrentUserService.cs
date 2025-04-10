namespace PinFood.Application.Common.Interfaces.Api.Services;

public interface ICurrentUserService
{
	Guid Id { get; set; }
	string Email { get; set; }
}