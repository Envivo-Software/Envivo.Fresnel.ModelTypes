// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;
using System.Collections.Specialized;

namespace Envivo.Fresnel.ModelTypes
{
    /// <summary>
    /// The event args when a collection is modified
    /// </summary>
    public class CollectionChangeEventArgs<T> : EventArgs, ICollectionChangeEventArgs<T>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public NotifyCollectionChangedAction Action { get; internal set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public T Item { get; internal set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int Index { get; internal set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}