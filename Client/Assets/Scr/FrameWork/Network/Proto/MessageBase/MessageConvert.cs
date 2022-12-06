using System.Collections.Generic;

namespace GameFrameWork.Network.MessageBase
{

    /// <summary>
    /// 消息处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MessageDistribute<T> :IMessageDistribute<T>
    {
        private static Dictionary<int, IMessageProcessor<T>> m_createrDic = new Dictionary<int, IMessageProcessor<T>>();
        
        public abstract void CreateMessageDic();
        bool IMessageDistribute<T>.Find(int index, out IMessageProcessor<T> creater)
        {
            return Find(index, out creater);
        }

        public static bool Find(int index,out IMessageProcessor<T> creater)
        {
            return m_createrDic.TryGetValue(index, out creater);
        }


        // public MessageType ConvertToMessage<MessageType>(DataSegment data)
        // {
        //     data.
        // }
    }
}