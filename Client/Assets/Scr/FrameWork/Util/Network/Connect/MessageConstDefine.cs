namespace GameFrameWork.Network
{
    public enum ConnectProtoType
    {
        None,
        TCP,
        KCP,
    }
    public static class MessageConstDefine
    {
        public static readonly int MESSAGE_ID_SIZE = 2;
        public static readonly int MESSAGE_CMD_SIZE = 2;
        public static readonly int MESSAGE_INDEX_SIZE = 4;
        public static readonly int MESSAGE_LENGTH_SIZE = 4;
        
        
    }
}