using System;

namespace Monolith.Core.Domain.Entities;

public interface ITrackable
{
    byte[] RowVersion { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsRemoved { get; set; }
}
