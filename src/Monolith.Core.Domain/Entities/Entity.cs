using System;
using System.ComponentModel.DataAnnotations;

namespace Monolith.Core.Domain.Entities;

public abstract class Entity<Guid> : ITrackable
{
    public Guid Id { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }

    public DateTimeOffset CreatedDateTime { get; set; }
    public DateTimeOffset? UpdatedDateTime { get; set; }
    
    
}
