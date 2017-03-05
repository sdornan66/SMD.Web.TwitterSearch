using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.Web.TwitterSearch
{

    public class SMDTweet
    {
        public ulong TweetID { get; set; }
        public string TweetText { get; set; }
        public string TweetLink { get; set; }
        public string Author { get; set; }
        public string ScreenName { get; set; }
        public string ProfilePicUrl { get; set; }
        public DateTime Created { get; set; }
        
        public int? Retweets { get; set; }
        public int? Likes { get; set; }
    }
}