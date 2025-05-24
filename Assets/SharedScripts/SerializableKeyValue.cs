using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SharedScripts
{
    [Serializable]
    public record SerializableKeyValue<TKey, T>
    {
        public SerializableKeyValue(TKey key, T value)
        {
            this.key   = key;
            this._value = value;
        }

        public virtual bool Equals(SerializableKeyValue<TKey, T> other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TKey>.Default.Equals(key, other.key);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TKey>.Default.GetHashCode(key);
        }

        public TKey Key => key;

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        [SerializeField] private TKey key;
        [SerializeField] private T    _value;
    }
}