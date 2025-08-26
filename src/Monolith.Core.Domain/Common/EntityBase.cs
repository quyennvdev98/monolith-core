using System;
using System.ComponentModel.DataAnnotations;

namespace Monolith.Core.Domain;

public abstract class EntityBase 
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsRemoved { get; set; }
}
