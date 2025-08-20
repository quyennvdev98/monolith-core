using System;
using System.ComponentModel.DataAnnotations;

namespace Monolith.Core.Domain.Entities;

public abstract class Entity<Guid> : ITrackable
{
    [Key]
    public Guid Id { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsRemoved { get; set; }
}
