namespace GameFrameWork.Pool
{
    public interface IPool<T>
    {
        int GetLength();


        T Pop();


        void Push(T t);

        void Release();

    }
}