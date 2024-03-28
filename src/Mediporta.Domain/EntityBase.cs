using System.ComponentModel.DataAnnotations;

namespace Mediporta.Domain;

public class EntityBase
{
    [Key]
    public Guid Id { get; set; }
}