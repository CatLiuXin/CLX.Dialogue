namespace CLX
{
    public interface IPool<T>
    {
        T Allocate();
        void Recycle(T value);
        int Count { get; }
    }
}
