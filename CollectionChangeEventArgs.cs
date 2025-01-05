// SPDX-FileCopyrightText: Copyright (c) 2022-2025 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;
using System.Collections.Specialized;

namespace Envivo.Fresnel.ModelTypes
{
    /// <inheritdoc cref="ICollectionChangeEventArgs{T}" />
    public class CollectionChangeEventArgs<T> : EventArgs, ICollectionChangeEventArgs<T>
    {
        /// <inheritdoc/>
        public NotifyCollectionChangedAction Action { get; internal set; }

        /// <inheritdoc/>
        public T Item { get; internal set; }

        /// <inheritdoc/>
        public int Index { get; internal set; }

        /// <inheritdoc/>
        public bool IsCancelled { get; set; }
    }
}