using UnityEngine.Rendering;

namespace GameFrameWork.Network
{
    public class DataSegment
    {
        
        private const int DefLength = 1024;
        public byte[] m_data;

        private int m_pos = 0;

        public void ClearPos()
        {
            m_pos = 0;
        }

        public DataSegment()
        {
            m_data = new byte[DefLength];
        }
        
        public DataSegment(int size)
        {
            m_data = new byte[size];
        }

        public int Length
        {
            get => m_pos;
            set => m_pos = value;
        }
        

        public void ResetSize(int length)
        {
            if (m_data.Length < length)
            {
                int l = DefLength;
                while (true)
                {
                    l *= 2;
                    if (l >= length)
                    {
                        var newData = new byte[l];
                        if (m_pos > 0)
                        {
                            System.Buffer.BlockCopy(m_data, 0, newData, 0, m_pos);
                        }
                        m_data = newData;
                        return;
                    }
                }
            }
        }

        public void Write(int data)
        {
            ResetSize(m_pos + 4);
            m_data[m_pos] = (byte)((data >> 24) & 0xFF);
            m_data[m_pos + 1] = (byte)((data >> 16) & 0xFF);
            m_data[m_pos + 2] = (byte)((data >> 8) & 0xFF);
            m_data[m_pos + 3] = (byte)(data & 0xFF);
            m_pos += 4;
        }

        public void Write(short data)
        {
            ResetSize(m_pos + 2);
            m_data[m_pos] = (byte)((data >> 8) & 0xFF);
            m_data[m_pos + 1] = (byte)(data & 0xFF);
            m_pos += 2;
        }

        public void Write(byte data)
        {
            ResetSize(m_pos + 1);
            m_data[m_pos] = (byte)(data & 0xFF);
            m_pos += 1;
        }

        public void Write(byte[] data, int length)
        {
            if (length > 0)
            {
                Write(length);
                ResetSize(m_pos + length);
                System.Buffer.BlockCopy(m_data, m_pos, data, 0, length);
                m_pos += length;
            }
        }

        public bool TryReadInt(out int o)
        {
            var b = (m_pos + 4) < Length;
            if (b)
            {
                o = (int) (m_data[m_pos + 3] |
               (int) (m_data[m_pos + 2] << 8) |
               (int) (m_data[m_pos + 1] << 16) |
               (int) (m_data[m_pos] << 24));
                m_pos += 4;
            }
            else
            {
                o = 0;
            }
            return b;
        }
        
        public bool TryReadByte(out byte o)
        {
            var b = (m_pos + 1) < Length;
            if (b)
            {
                o = m_data[m_pos];
                m_pos++;
            }
            else
            {
                o = default;
            }
            
            return b;
        }
        public bool TryReadShort(out short o)
        {
            var b = (m_pos + 2) < Length;
            if (b)
            {
                o = (short) (m_data[m_pos + 1] | (m_data[m_pos] << 8));
                m_pos += 2;
            }
            else o = default;
            
            return b;
        }

        public bool TryReadDatas(int l,out byte[] data)
        {
            var b = (m_pos + l) < Length;
            if (b)
            {
                data = new byte[l];
                System.Buffer.BlockCopy(data, 0, m_data, m_pos, l);
                m_pos += l;
            }
            else
            {
                data = null;
            }

            return b;
        }
        
        public bool TryReadDatas(out byte[] data,out int length)
        {
            if (TryReadInt(out length))
            {
                if (TryReadDatas(length, out data))
                {
                    return true;
                }
            }

            data = null;
            return false;
        }
    }
}