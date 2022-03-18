namespace Common.Basic.UMVC
{
    public interface INavigator
    {
        void Push(INavigatable navigated, bool onStashFirst = false);
        void PushLast();
        void Pop(bool unstashPrevious = true);

        INavigatable GetLast();
    }
}
