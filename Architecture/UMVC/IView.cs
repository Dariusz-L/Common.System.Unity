namespace Common.Basic.UMVC
{
    public interface IView
    {
        void Show();
        void Hide();

        T AsParent<T>() where T : class;

    }
}
