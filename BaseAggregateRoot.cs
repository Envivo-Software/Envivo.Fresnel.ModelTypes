using Envivo.Fresnel.ModelTypes.Interfaces;

namespace Envivo.Fresnel.ModelTypes
{
    /// <summary>
    /// Groups closely related objects, and provides a transactional boundary around them.
    /// </summary>
    public abstract partial class BaseAggregateRoot : BaseDomainObject, IAggregateRoot
    {
    }
}