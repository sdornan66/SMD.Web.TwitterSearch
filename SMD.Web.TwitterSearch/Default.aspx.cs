using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using LinqToTwitter;

namespace SMD.Web.TwitterSearch
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false) ResultPanel.Visible = false;
        }

        protected void TwitterGo_Click(object sender, EventArgs e)
        {
            var results = SearchTwitter( SearchFor.Text);
            
            ResultPanel.Visible = true;
            ResultsGrid.DataSource = results;
            ResultsGrid.DataBind();
        }
      
        private List<SMDTweet> SearchTwitter(string searchTerm)
        {
            var ret = new List<SMDTweet>();
            
            var twitterContext = new TwitterContext(GetAuthorizer());
            int rCount = 0;
            int.TryParse(Responses.Text, out rCount);
            var srch =
               Enumerable.SingleOrDefault((from search in
                  twitterContext.Search
                                           where search.Type == SearchType.Search &&
                                     search.Query == searchTerm &&
                                     search.Count == rCount && 
                                     search.IncludeEntities == true &&
                                     search.ResultType == ResultType.Popular
                                           select search));
            if (srch != null && srch.Statuses.Count > 0)
            {
                foreach(Status s in srch.Statuses)
                {
                    var t = new SMDTweet();
                    t.Author = s.User.Name;
                    t.ScreenName = s.User.ScreenNameResponse;
                    t.Likes = s.FavoriteCount;
                    t.ProfilePicUrl = s.User.ProfileImageUrl;
                    t.Retweets = s.RetweetCount;
                    t.TweetID = s.StatusID;
                    t.TweetText = s.Text;
                    t.TweetLink = "https://twitter.com/" + t.ScreenName + "/status/" + t.TweetID.ToString();
                    //https://twitter.com/SandbachDrama/status/838459262326145024
                    ret.Add(t);

                }
                return ret; ;
            }

            return new List<SMDTweet>();
        }

        private SingleUserAuthorizer GetAuthorizer()
        {
            return new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = "icn846h9F1FmGrcPOB7E8Vs0x",
                    ConsumerSecret = "Q2q8aNBQXrTAkxLe8DZn5ZCyHOEzVQI1xMuCKCCHcx6VuWjWWZ",
                    AccessToken = "838471865211498500-LTrdsGuAFNEAMQ6ylYqkbxpdv8MS8cd",
                    AccessTokenSecret = "AoSB2EwuXj9Er4HoQDFoUefcMfAC75ZTuDj1Vqw2UxqtA"
                }
            };
        }
    }
}