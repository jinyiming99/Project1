namespace Game.Characters
{
    public interface ICharacterController<T>
    {
        void Init();
        void BindObject(T t);

        void Release();
    }
}