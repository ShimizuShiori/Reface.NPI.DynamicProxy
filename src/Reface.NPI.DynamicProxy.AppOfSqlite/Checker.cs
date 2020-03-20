namespace Reface.NPI.DynamicProxy.AppOfSqlite
{
    class Checker
    {
        public static void IsNotNull(object value)
        {
            if (value == null)
                throw new CheckException("为 null");
        }

        public static void IsNull(object value)
        {
            if (value != null)
                throw new CheckException("不为 null");
        }

        public static void Equals(object value1, object value2)
        {
            if (value1 == null && value2 == null) return;
            if (value1.Equals(value2)) return;
            throw new CheckException($"不相等 : [{value1}],[{value2}]");
        }
    }
}
