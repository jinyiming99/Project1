using GameFrameWork.Network;
using GameFrameWork.Pool;

namespace GameFrameWork.Network.MessageBase
{
    public class MessageBase
    {
        private static int Count = 0;
        public static int GetSeq() => Count++;
        
        
        public short m_messageID ;
        public short m_cmd ;
        public int m_ack;
        public int m_length;
        public byte[] m_data;

        public MessageBase()
        {
            m_ack = GetSeq();
        }

        public DataSegment GetData()
        {
            var data = new DataSegment();
            int length = 0;
            data.ClearPos();
            data.Write(m_cmd);
            data.Write(m_ack);
            data.Write(m_messageID);
            data.Write(m_data,m_length);
            return data;
        }

        public static bool TryGetMessage(DataSegment data,out MessageBase msg)
        {
            var off = data.Length;
            if (data.TryReadShort(out var cmd))
            {
                switch ((MessageCommand)cmd)
                {
                    case MessageCommand.Ack:
                    {
                        if (data.TryReadInt(out var ack))
                        {
                            msg = new MessageBase()
                            {
                                m_ack = ack,
                            };
                            return true;
                        }
                        break;
                    }
                    case MessageCommand.ProtoBuf_Message:
                    {
                        if (data.TryReadInt(out var ack) &&
                            data.TryReadShort(out var messageID) &&
                            data.TryReadDatas(out var arr, out var length))
                        {
                            msg = new MessageBase()
                            {
                                m_ack = ack,
                                m_messageID = messageID,
                                m_cmd = cmd,
                                m_length = length,
                                m_data = arr,
                            };
                            return true;
                        }
                        break;
                    }
                    case MessageCommand.Heart:
                        break;
                }
            }
            

            msg = null;
            data.Length = off;
            return false;
        }

        public void ClearData()
        {
            m_data = null;
        }
    }
}