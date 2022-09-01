﻿// SPDX-FileCopyrightText: Copyright (c) 2022 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using System.Collections.Generic;

namespace Envivo.Fresnel.ModelTypes.Interfaces
{
    /// <summary>
    /// Provides a wrapper for a generic list. Commonly used to wrap proxies generated by an ORM (e.g. Nhibernate)
    /// </summary>
    public interface IListAdapter<T> : IList<T>
        where T : class
    {
        /// <summary>
        /// The original list that is being wrapped
        /// </summary>
        IList<T> InnerList { get; set; }
    }
}