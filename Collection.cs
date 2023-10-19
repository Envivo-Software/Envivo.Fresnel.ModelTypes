// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;

namespace Envivo.Fresnel.ModelTypes
{
    /// <summary>
    /// A collection of Domain Objects
    /// </summary>
    public partial class Collection<T> : IList<T>,
                                         IListAdapter<T>,
                                         INotifyCollectionChanges<T>,
                                         INotifyCollectionChanged,
                                         IDisposable
        where T : class
    {
        private Guid _Id = Guid.NewGuid();
        private List<T> _Items = new List<T>();
        private IList<T> _InnerList = new List<T>();

        private ReaderWriterLock _Lock = new ReaderWriterLock();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Collection()
            : base()
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Collection(int capacity)
            : base()
        {
            _Items = new List<T>(capacity);
            _InnerList = new List<T>(capacity);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Collection(IEnumerable<T> collection)
            : base()
        {
            this.InnerList = new List<T>(collection);
        }

        /// <summary>
        /// A unique identifier used to track this object
        /// </summary>
        public virtual Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public virtual long Version { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual IList<T> InnerList
        {
            get { return _InnerList; }

            set
            {
                // Add all of the items *without* invoking any events:
                _InnerList = value;
                _Items.Clear();

                if (_InnerList != null)
                {
                    _Lock.AcquireWriterLock(Timeout.Infinite);
                    for (int i = 0; i < value.Count; i++)
                    {
                        _Items.Add(_InnerList[i]);
                    }
                    _Lock.ReleaseWriterLock();
                }
            }
        }

        public virtual event NotifyCollectionChangesEventHandler<T> Adding;

        public virtual event NotifyCollectionChangesEventHandler<T> Added;

        public virtual event NotifyCollectionChangesEventHandler<T> Replacing;

        public virtual event NotifyCollectionChangesEventHandler<T> Replaced;

        public virtual event NotifyCollectionChangesEventHandler<T> Removing;

        public virtual event NotifyCollectionChangesEventHandler<T> Removed;

        public virtual event NotifyCollectionChangesEventHandler<T> Clearing;

        public virtual event NotifyCollectionChangesEventHandler<T> Cleared;

        public virtual event NotifyCollectionChangesEventHandler<T> Changing;

        public virtual event NotifyCollectionChangesEventHandler<T> Changed;

        public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual T this[int index]
        {
            get
            {
                _Lock.AcquireReaderLock(Timeout.Infinite);
                var item = _Items[index];
                _Lock.ReleaseReaderLock();
                return item;
            }

            set
            {
                var e = new CollectionChangeEventArgs<T>()
                {
                    Action = NotifyCollectionChangedAction.Replace,
                    Item = this[index],
                    Index = index,
                };

                this.OnReplacingItem(e);
                if (e.IsCancelled)
                    return;

                _Lock.AcquireWriterLock(Timeout.Infinite);
                this.InnerList[index] = value;
                _Items[index] = value;
                _Lock.ReleaseWriterLock();

                this.OnReplacedItem(e);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void Add(T item)
        {
            var e = new CollectionChangeEventArgs<T>()
            {
                Action = NotifyCollectionChangedAction.Add,
                Item = item,
                Index = _Items.Count,
            };

            this.OnInsertingItem(e);
            if (e.IsCancelled)
                return;

            _Lock.AcquireWriterLock(Timeout.Infinite);
            this.InnerList.Add(item);
            _Items.Add(item);
            _Lock.ReleaseWriterLock();

            this.OnInsertedItem(e);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void Clear()
        {
            var e = new CollectionChangeEventArgs<T>()
            {
                Action = NotifyCollectionChangedAction.Reset,
                Index = -1,
            };

            this.OnClearing(e);
            if (e.IsCancelled)
                return;

            _Lock.AcquireWriterLock(Timeout.Infinite);
            while (this.Count > 0)
            {
                this.RemoveAt(0);
            }
            _Items.Clear();
            _Lock.ReleaseWriterLock();

            this.OnCleared(e);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual bool Contains(T item)
        {
            _Lock.AcquireReaderLock(Timeout.Infinite);
            var result = _Items.Contains(item);
            _Lock.ReleaseReaderLock();
            return result;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void CopyTo(T[] array)
        {
            _Lock.AcquireReaderLock(Timeout.Infinite);
            _Items.CopyTo(array);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            _Lock.AcquireReaderLock(Timeout.Infinite);
            _Items.CopyTo(array, arrayIndex);
            _Lock.ReleaseReaderLock();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            _Lock.AcquireReaderLock(Timeout.Infinite);
            _Items.CopyTo(index, array, arrayIndex, count);
            _Lock.ReleaseReaderLock();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual int Count
        {
            get
            {
                _Lock.AcquireReaderLock(Timeout.Infinite);
                var result = _Items.Count;
                _Lock.ReleaseReaderLock();
                return result;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual int IndexOf(T item)
        {
            _Lock.AcquireReaderLock(Timeout.Infinite);
            var result = _Items.IndexOf(item);
            _Lock.ReleaseReaderLock();
            return result;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual int IndexOf(T item, int index)
        {
            _Lock.AcquireReaderLock(Timeout.Infinite);
            var result = _Items.IndexOf(item, index);
            _Lock.ReleaseReaderLock();
            return result;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual int IndexOf(T item, int index, int count)
        {
            _Lock.AcquireReaderLock(Timeout.Infinite);
            var result = _Items.IndexOf(item, index, count);
            _Lock.ReleaseReaderLock();
            return result;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void Insert(int index, T item)
        {
            _Lock.AcquireWriterLock(Timeout.Infinite);
            _Items.Insert(index, item);
            _Lock.ReleaseWriterLock();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual bool Remove(T item)
        {
            var e = new CollectionChangeEventArgs<T>()
            {
                Action = NotifyCollectionChangedAction.Remove,
                Item = item,
                Index = _Items.IndexOf(item),
            };

            this.OnRemovingItem(e);
            if (e.IsCancelled)
                return false;

            _Lock.AcquireWriterLock(Timeout.Infinite);
            this.InnerList.Remove(item);
            _Items.Remove(item);
            _Lock.ReleaseWriterLock();

            this.OnRemovedItem(e);

            return true;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void RemoveAt(int index)
        {
            var e = new CollectionChangeEventArgs<T>()
            {
                Action = NotifyCollectionChangedAction.Remove,
                Item = _Items[index],
                Index = index,
            };

            this.OnRemovingItem(e);
            if (e.IsCancelled)
                return;

            _Lock.AcquireWriterLock(Timeout.Infinite);
            this.InnerList.RemoveAt(index);
            _Items.RemoveAt(index);
            _Lock.ReleaseWriterLock();

            this.OnRemovedItem(e);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual IEnumerator<T> GetEnumerator()
        {
            _Lock.AcquireReaderLock(Timeout.Infinite);
            var result = _Items.GetEnumerator();
            _Lock.ReleaseReaderLock();
            return result;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            _Lock.AcquireReaderLock(Timeout.Infinite);
            var result = _Items.GetEnumerator();
            _Lock.ReleaseReaderLock();
            return result;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        private NotifyCollectionChangedEventArgs CreateCollectionChangedArgsFrom(ICollectionChangeEventArgs<T> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    return new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, e.Item, e.Index);

                case NotifyCollectionChangedAction.Remove:
                    return new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, e.Item, e.Index);

                case NotifyCollectionChangedAction.Move:
                    return new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, e.Item, e.Index);

                case NotifyCollectionChangedAction.Replace:
                    return new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, e.Item);

                default:
                    return new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            }
        }

        /// <summary>
        /// Raised just before an item is inserted/added to the Collection. Use the event arguments to cancel the operation.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnInsertingItem(ICollectionChangeEventArgs<T> e)
        {
            if (this.Adding != null)
                this.Adding(this, e);

            if (this.Changing != null)
                this.Changing(this, e);
        }

        protected virtual void OnInsertedItem(ICollectionChangeEventArgs<T> e)
        {
            if (this.Added != null)
                this.Added(this, e);

            if (this.Changed != null)
                this.Changed(this, e);

            if (this.CollectionChanged != null)
                this.CollectionChanged(this, this.CreateCollectionChangedArgsFrom(e));
        }

        /// <summary>
        /// Raised just before an item is removed from the Collection. Use the event arguments to cancel the operation.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRemovingItem(ICollectionChangeEventArgs<T> e)
        {
            if (this.Removing != null)
                this.Removing(this, e);

            if (this.Changing != null)
                this.Changing(this, e);
        }

        protected virtual void OnRemovedItem(ICollectionChangeEventArgs<T> e)
        {
            if (this.Removed != null)
                this.Removed(this, e);

            if (this.Changed != null)
                this.Changed(this, e);

            if (this.CollectionChanged != null)
                this.CollectionChanged(this, this.CreateCollectionChangedArgsFrom(e));
        }

        /// <summary>
        /// Raised just before an item is set in the Collection. Use the event arguments to cancel the operation.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnReplacingItem(ICollectionChangeEventArgs<T> e)
        {
            if (this.Replacing != null)
                this.Replacing(this, e);

            if (this.Changing != null)
                this.Changing(this, e);
        }

        protected virtual void OnReplacedItem(ICollectionChangeEventArgs<T> e)
        {
            if (this.Replaced != null)
                this.Replaced(this, e);

            if (this.Changed != null)
                this.Changed(this, e);

            if (this.CollectionChanged != null)
                this.CollectionChanged(this, this.CreateCollectionChangedArgsFrom(e));
        }

        /// <summary>
        /// Raised just before the Collection is cleared. Use the event arguments to cancel the operation.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnClearing(ICollectionChangeEventArgs<T> e)
        {
            if (this.Clearing != null)
                this.Clearing(this, e);

            if (this.Changing != null)
                this.Changing(this, e);
        }

        protected virtual void OnCleared(ICollectionChangeEventArgs<T> e)
        {
            if (this.Cleared != null)
                this.Cleared(this, e);

            if (this.Changed != null)
                this.Changed(this, e);

            if (this.CollectionChanged != null)
                this.CollectionChanged(this, this.CreateCollectionChangedArgsFrom(e));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj, o => o.Id);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode(o => o.Id);
        }

        public virtual void Dispose()
        {
            _InnerList.Clear();
            _Items.Clear();
        }

        public override string ToString()
        {
            return _Items.ToString();
        }
    }
}