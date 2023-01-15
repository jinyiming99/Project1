using System.Collections.Generic;
using GameFrameWork.Pool;
using CodedInputStream = Google.Protobuf.CodedInputStream;

namespace GameFrameWork.Network.MessageBase
{
    public abstract class MessageProcessor<T> : IMessageProcessor where T : Google.Protobuf.IMessage,new()
    {
        public class MsgSturct
        {
            public T message;
            public int ack;

            public MsgSturct()
            {
                message = new T();
                ack = 0;
            }
        }
        protected List<MsgSturct> m_msgList;
        protected Pool.StackPool<MsgSturct> m_pool = new StackPool<MsgSturct>();
        public MessageProcessor()
        {
            m_msgList = new List<MsgSturct>();
        }

        public abstract int MessageID { get; }

        public void CreateMessage(MessageBase msgBase)
        {
            var t = m_pool.Pop();
            if (t == null)
                t = new MsgSturct();
            t.message.MergeFrom(new CodedInputStream(msgBase.m_data));
            t.ack = msgBase.m_ack;
            m_msgList.Add(t);
        }

        public void Sort()
        {
            if (m_msgList.Count > 1)
            {
                m_msgList.Sort((t1,t2) =>
                {
                    return -t1.ack.CompareTo(t2.ack) ;
                });
            }
        }
        public abstract void Processor();

        public void ReleaseMessage()
        {
            foreach (var msg in m_msgList)
            {
                m_pool.Push(msg);
            }
        }
    }
}