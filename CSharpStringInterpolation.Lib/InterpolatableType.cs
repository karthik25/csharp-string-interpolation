namespace CSharpStringInterpolation.Lib
{
    public enum InterpolatableType
    {
        /*
         * SimpleProperty, SimpleProperty1, SimpleProperty2:
         * 
         * public string Name { get;set; }
         * public int Id { get;set; }
         * 
         * public Point Point { get; set; }
         * 
         * where 
         * 
         * Point is a class with properties X and Y and overridden ToString()
         * 
         * */

        /* #{SimpleProperty} */
        Simple,
        /* #{SimpleProperty[0]} */
        Array,
        /* #{SimpleProperty1 + SimpleProperty2} */
        Expression,
        Unknown
    }
}