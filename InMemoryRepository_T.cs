using Envivo.Fresnel.ModelTypes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Envivo.Fresnel.ModelTypes
{
    /// <summary>
    /// A simple repository that keeps it's contents in-memory.
    /// Used when designing domain models.
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public class InMemoryRepository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        private readonly List<TAggregateRoot> _Items = new();

        private Cloner<TAggregateRoot> _SimpleCloner = new();

        public InMemoryRepository()
        {
        }

        public InMemoryRepository(IEnumerable<TAggregateRoot> initialItems)
        {
            _Items.AddRange(initialItems);
        }

        public IQueryable<TAggregateRoot> GetAll()
        {
            return _Items.AsQueryable();
        }

        public TAggregateRoot? Load(Guid id)
        {
            return _Items.FirstOrDefault(p => p.Id == id);
        }

        public int Save(TAggregateRoot aggregateRoot, IEnumerable<object> newObjects, IEnumerable<object> modifiedObjects, IEnumerable<object> deletedObjects)
        {
            var newAggregates = newObjects.OfType<TAggregateRoot>();
            foreach (var ar in newAggregates)
            {
                Save(ar);
            }

            var modifiedAggregates = modifiedObjects.OfType<TAggregateRoot>();
            foreach (var ar in modifiedAggregates)
            {
                Save(ar);
            }

            return newAggregates.Count() + modifiedAggregates.Count();
        }

        private void Save(TAggregateRoot aggregateRoot)
        {
            Delete(aggregateRoot);

            var copy = _SimpleCloner.Clone(aggregateRoot);
            _Items.Add(copy);
        }

        public void Delete(TAggregateRoot aggregateRoot)
        {
            var match = _Items.FirstOrDefault(p => p.Id == aggregateRoot.Id);
            if (match != null)
            {
                _Items.Remove(match);
            }
        }

        public IAggregateLock Lock(TAggregateRoot aggregateRoot)
        {
            // Not applicable
            return null;
        }

        public void Unlock(TAggregateRoot aggregateRoot)
        {
            // Not applicable
        }

        private class Cloner<T>
        {
            private static Func<T, T> cloner = CreateCloner();

            private static Func<T, T> CreateCloner()
            {
                var cloneMethod = new DynamicMethod("CloneImplementation", typeof(T), new Type[] { typeof(T) }, true);
                var defaultCtor = typeof(T).GetConstructor(new Type[] { });

                var generator = cloneMethod.GetILGenerator();

                var loc1 = generator.DeclareLocal(typeof(T));

                generator.Emit(OpCodes.Newobj, defaultCtor);
                generator.Emit(OpCodes.Stloc, loc1);

                foreach (var field in typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    generator.Emit(OpCodes.Ldloc, loc1);
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Ldfld, field);
                    generator.Emit(OpCodes.Stfld, field);
                }

                generator.Emit(OpCodes.Ldloc, loc1);
                generator.Emit(OpCodes.Ret);

                return ((Func<T, T>)cloneMethod.CreateDelegate(typeof(Func<T, T>)));
            }

            public T Clone(T myObject)
            {
                return cloner(myObject);
            }
        }
    }
}