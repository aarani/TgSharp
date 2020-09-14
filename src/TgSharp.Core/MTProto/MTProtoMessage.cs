namespace TgSharp.Core.MtProto
{
    public class MTProtoMessage
    {
        public bool Authenticated { get; set; }
        public byte[] Payload { get; set; }
        public ulong RemoteSalt { get; set; }
        public ulong RemoteSessionId { get; set; }
        public ulong RemoteMessageId { get; set; }
        public int RemoteSequence { get; set; }
    }
}