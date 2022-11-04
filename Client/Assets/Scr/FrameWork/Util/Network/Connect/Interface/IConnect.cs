namespace GameFrameWork.Network.Interface
{
    public interface IConnect
    {
        ConnectErrorCallBack FailedCallBack { set; }
        ReveiceDataCallBack ReveiceCallback { set; }
        bool IsConnected { get; }

        void BeginReceive();
        void SendAsync(byte[] data,int length,int offer);

        void Dispose();

    }
}