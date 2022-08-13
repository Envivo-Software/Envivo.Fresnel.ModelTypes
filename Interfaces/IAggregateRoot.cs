namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Groups closely related objects. Aggregates Roots are used to
    /// (1) Provide a access route for Domain Objects within the cluster
    /// (2) Enforce rules/consistency for the entire cluster of objects
    /// (3) Provide a suitable locking point for Domain Objects in a mult-user environment
    /// </summary>
    public interface IAggregateRoot : IEntity
    {
    }
}