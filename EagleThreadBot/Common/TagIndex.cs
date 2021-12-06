using System;
namespace EagleThreadBot.Common
{
    public class TagIndex
    {
        // Get the tag index
        public Index[] index { get; set; }
    }
    public class Index
    {
        // Get the tag identifier from index
        public String identifier { get; set; }

        // Get the tag aliases from index
        public String[] aliases { get; set; }

        // Get the tag url from index
        public String url { get; set; }
    }
}