namespace Sander0542.CMLedController.Abstractions.Enums
{
    public abstract class Castable<T>
    {
        private readonly T _value;

        protected Castable(T value)
        {
            _value = value;
        }
        
        public static implicit operator T(Castable<T> mode) => mode._value;
    }
}
