using System;
using System.Collections.Generic;
using System.Linq;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Used as an adapter to a persistence data store
    /// </summary>
    public interface IPersistenceService : IDomainDependency, IDisposable
    {
        bool IsTypeRecognised(Type entityType);

        bool IsPersistent(Guid id, object entity);

        object? CreateInstance(Type entityType, object constructorArg = null);

        object? Load(Type entityType, Guid id, IEnumerable<string> propertiesToInclude = null);

        IQueryable GetAll(Type entityType, IEnumerable<string> propertiesToInclude = null);

        void LoadProperty(object entity, string propertyName);

        void Refresh(object staleEntity);

        int SaveChanges(IEnumerable<object> newEntities, IEnumerable<object> modifiedEntities, IEnumerable<object> deletedEntities);

        T? Load<T>(Guid id)
            where T : class;

        IQueryable<T> GetAll<T>()
            where T : class;
    }
}