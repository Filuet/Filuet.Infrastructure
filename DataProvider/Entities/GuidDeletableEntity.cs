using Filuet.Infrastructure.DataProvider.Interfaces;
using System;

namespace Filuet.Infrastructure.DataProvider.Entities
{
    public abstract class GuidDeletableEntity<T> : EntityDeletable<T>, IGuidable
    {
        public string UID { get; protected set; } = Guid.NewGuid().ToString();

        public bool IsSaved => !string.IsNullOrWhiteSpace(UID);
    }
}