using GameFrameWork.Network.MessageBase;

namespace GameFrameWork.Network
{
    public class NetworkKeeper
    {
        private MessageBase.MessageBase _messageBase;
        private FloatTimeCounter m_counter;
        private NetworkWorker m_worker;

        public NetworkKeeper()
        {
            m_counter = new FloatTimeCounter();
            _messageBase = new MessageBase.MessageBase()
            {
                m_cmd = (short) MessageCommand.Heart,
                m_ack = MessageBase.MessageBase.GetSeq(),
            };
        }
        public void Update()
        {
            if (m_counter.GetPassTime() > 20f)
            {
                //
                // DataSegment dataSegment = new DataSegment();
                // m_worker.SendAsync();
            }
        }
    }
}