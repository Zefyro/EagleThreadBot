namespace EagleThreadBot.Common
{
    public class TagIndex
    {
        public Index[] index { get; set; }
    }
    public class Index
    {
        public string identifier { get; set; }
        public string[] aliases { get; set; }
        public string url { get; set; }
    }
}