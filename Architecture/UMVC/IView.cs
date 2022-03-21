namespace Common.Basic.UMVC
{
    public interface IView
    {
        void Show();
        void Hide();

        bool IsVisible { get; }

        T AsParent<T>() where T : class;

    }
}
