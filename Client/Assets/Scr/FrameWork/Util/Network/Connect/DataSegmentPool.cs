using GameFrameWork.Pool;

namespace GameFrameWork.Network
{
    public class DataSegmentPoolInstance : SingleInstance.SingleInstance<DataSegmentPool> { }

    public class DataSegmentPool :StackPool<DataSegment>
    {
        
    }
}