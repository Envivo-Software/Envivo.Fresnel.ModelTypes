// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System.Collections.Specialized;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// The event args when a collection is modified
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionChangeEventArgs<T>
    {
        /// <summary>
        /// The underlying action that triggered this event
        /// </summary>
        NotifyCollectionChangedAction Action { get; }

        /// <summary>
        /// The item that is being added/removed from the Collection
        /// </summary>
        T Item { get; }

        /// <summary>
        /// The index of the item being added/removed from the Collection
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Determines if the action should be cancelled
        /// </summary>
        bool IsCancelled { get; set; }
    }
}