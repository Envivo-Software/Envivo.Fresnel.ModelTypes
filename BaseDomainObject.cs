// SPDX-FileCopyrightText: Copyright (c) 2022-2023 Envivo Software
// SPDX-License-Identifier: Apache-2.0
using Envivo.Fresnel.ModelTypes.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Envivo.Fresnel.ModelTypes
{
    /// <inheritdoc cref="IDomainObject" />
    [Serializable]
    public abstract partial class BaseDomainObject : IDomainObject,
                                                     INotifyPropertyChanged,
                                                     IValidatable,
                                                     IDisposable
    {
        public override bool Equals(object obj)
        {
            return this.Equals(obj, o => o.Id);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode(o => o.Id);
        }

        /// <inheritdoc/>
        public virtual Guid Id { get; set; } = Guid.NewGuid();

        /// <inheritdoc/>
        public virtual long Version { get; set; } = -1;

        /// <inheritdoc/>
        public virtual IAudit Audit { get; set; } = new Audit();

        public virtual event PropertyChangedEventHandler PropertyChanged;

        virtual protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged == null)
                return;

            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Helper method to set a property value and raise the PropertyChanged event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingField"></param>
        /// <param name="newValue"></param>
        /// <param name="propertyName"></param>
        /// <example>this.Set(ref _Name, value, "Name");
        /// </example>

        virtual protected bool Set<T>(ref T backingField, T newValue, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, newValue))
                return false;

            backingField = newValue;
            this.OnPropertyChanged(propertyName);

            return true;
        }

        private Dictionary<string, string> _ErrorMap = new Dictionary<string, string>();
        private string _ErrorMessage;

        private Dictionary<string, string> Errors
        {
            get { return _ErrorMap; }
        }

        string IDataErrorInfo.Error
        {
            get { return _ErrorMessage; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;
                this.Errors.TryGetValue(propertyName, out error);
                return error;
            }
        }

        protected virtual void SetError(string propertyName, string errorDescription)
        {
            var errors = this.Errors;
            this.Errors[propertyName] = errorDescription;
            this.UpdateErrorMessage();
        }

        private void UpdateErrorMessage()
        {
            switch (this.Errors.Count)
            {
                case 0:
                    _ErrorMessage = string.Empty;
                    break;

                case 1:
                    _ErrorMessage = this.Errors.ElementAt(0).Value;
                    break;

                default:
                    var sb = new StringBuilder("The following problems were found:\n");
                    foreach (var item in _ErrorMap.Where(p => string.IsNullOrEmpty(p.Value) == false))
                    {
                        sb.AppendLine($" {item.Key} : {item.Value}");
                    }
                    _ErrorMessage = sb.ToString();
                    break;
            }
        }

        public virtual bool IsValid()
        {
            return true;
        }

        public virtual void Dispose()
        {
            _ErrorMap = null;
        }
    }
}