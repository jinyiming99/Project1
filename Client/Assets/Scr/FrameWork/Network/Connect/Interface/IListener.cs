namespace GameFrameWork.Network.Interface
{
    public interface IListener
    {
        ListenerSocketCallBack Callback { set; }

        void StartAppept();
    }
}