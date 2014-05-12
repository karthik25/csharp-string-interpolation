namespace CSharpStringInterpolation.Lib.Concrete
{
    public class Interpolatable<T>
        where T:class 
    {
        public string Item { get; set; }
        public InterpolatableType Type { get; set; }
        public T Instance { get; set; }

        public string Value
        {
            get { return ValueFactory.GetValue(this); }
        }
    }
}