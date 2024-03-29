﻿using System.Threading.Tasks;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Provides pessimistic locking for Aggregates used in concurrent scenarios
    /// </summary>
    public interface IAggregateLockService<TAggregateRoot> : IDomainDependency
    {
        /// <summary>
        /// Locks the Aggregate Root, and prevents other users from changing its contents while it is locked
        /// </summary>
        /// <param name="aggregateRoot"></param>
        Task<IAggregateLock?> LockAsync(TAggregateRoot aggregateRoot);

        /// <summary>
        /// Unlocks the Aggregate Root, and allows other users to change its contents
        /// </summary>
        /// <param name="aggregateRoot"></param>
        Task UnlockAsync(TAggregateRoot aggregateRoot);
    }
}
