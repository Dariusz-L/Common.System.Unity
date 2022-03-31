namespace Common.Basic.UMVC
{
    public interface IView
    {
        void Show();
        void Hide();

        bool IsVisible { get; }

        IView AsParent();
        T GetParent<T>() where T : IView;
    }
}
