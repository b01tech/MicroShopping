namespace MicroShopping.ProductAPI.Models;

public abstract class BaseEntity
{
    public long Id { get; protected set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
