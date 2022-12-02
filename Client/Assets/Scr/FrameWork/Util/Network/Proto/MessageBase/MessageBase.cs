using GameFrameWork.Network;
using GameFrameWork.Pool;

namespace GameFrameWork.Network.MessageBase
{
    public class MessageBase
    {
        public static int Count = 0;
        public static int GetSeq() => Count++;
        
        
        public short m_messageID;
        public short m_cmd;
        public int m_seq;
        public int m_length;
        public byte[] m_data;

        public MessageBase()
        {
            m_seq = GetSeq();
        }
        public void SetData(byte[] arr,int length)
        {
            m_data = arr;
            m_length = length;
        }

        public void SetMessageID(short id)
        {
            m_messageID = id;
        }

        public DataSegment GetData()
        {
            var data = new DataSegment();
            int length = 0;
            data.ClearPos();
            data.Write(m_messageID);
            data.Write(m_cmd);
            data.Write(m_seq);
            data.Write(m_length);
            data.Write(m_data,m_data.Length);
            return data;
        }

        public bool TrySetData(DataSegment data)
        {
            var off = data.Length;
            if (data.TryReadShort(out m_messageID) &&
                data.TryReadShort(out m_cmd) &&
                data.TryReadInt(out m_seq) &&
                data.TryReadInt(out m_length) &&
                data.TryReadDatas(out m_data))
            {
                return true;
            }

            data.Length = off;
            return false;
        }

        public void ClearData()
        {
            m_data = null;
        }
    }
}