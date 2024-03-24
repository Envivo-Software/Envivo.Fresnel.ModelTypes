// SPDX-FileCopyrightText: Copyright (c) 2022-2024 Envivo Software
// SPDX-License-Identifier: Apache-2.0
namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    public delegate void NotifyCollectionChangesEventHandler<T>(object sender, ICollectionChangeEventArgs<T> e)
        where T : class;

    /// <summary>
    /// Implemented by collections that should raise events when their contents are modified
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INotifyCollectionChanges<T>
        where T : class
    {
        /// <summary>
        /// Raised just before the collection's contents are changed
        /// </summary>
        event NotifyCollectionChangesEventHandler<T> Changing;

        /// <summary>
        /// Raised after the collection's contents are changed
        /// </summary>
        event NotifyCollectionChangesEventHandler<T> Changed;
    }
}