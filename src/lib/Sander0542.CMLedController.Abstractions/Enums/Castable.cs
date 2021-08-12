using System.Collections.Generic;

namespace Sander0542.CMLedController.Abstractions.Enums
{
    public abstract class Castable<T>
    {
        private readonly T _value;

        protected Castable(T value)
        {
            _value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is Castable<T> castable ? Equals(castable) : obj.Equals(this);
        }

        private bool Equals(Castable<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_value, other._value);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(_value);
        }

        public static implicit operator T(Castable<T> mode) => mode._value;

        public override string ToString()
        {
            return _value?.ToString() ?? "null";
        }
    }
}
