namespace Common.Basic.UMVC
{
    public interface IView
    {
        T AsParent<T>() where T : class;
    }
}
