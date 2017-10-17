namespace Wechat.Util.Messages.Reply
{
    public class Text
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public long CreateTime { get; set; }
        public string MsgType { get; set; }
        public string Content { get; set; }
    }
}
