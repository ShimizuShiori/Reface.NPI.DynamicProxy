namespace Reface.NPI.DynamicProxy
{
    public interface INPIImplementer
    {
        T Implement<T>() where T : class;
    }
}
