using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace SMD.Web.TwitterSearch
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DO not shpow the results panel on initial page load
            if (IsPostBack == false) ResultPanel.Visible = false;
        }

        protected void TwitterGo_Click(object sender, EventArgs e)
        {
            //Make sure they actually entered a search term
            if (SearchFor.Text == null || SearchFor.Text.Length <=0)
            {
                Response.Write("<script>alert('You must enter a hashtag to search for......');</script>");
            }
            //If the user forgot the #hashtag symbol, add it in for them
            if (SearchFor.Text.Contains("#") == false)
                SearchFor.Text = "#" + SearchFor.Text;

            var results = SearchTwitter( SearchFor.Text);
            

            //If no results came back....
            if(results == null || results.Count <= 0)
            {
                Response.Write("<script>alert('Your search for " + SearchFor.Text + " yielded no results. Please try a different search term.');</script>");
                return;
            }
            ResultPanel.Visible = true;
            ResultsGrid.DataSource = results;
            ResultsGrid.DataBind();
        }

        private List<SMDTweet> SearchTwitter(string searchTerm)
        {
            //Declare the return object and other needed variables.
            var ret = new List<SMDTweet>();
            var twitterContext = new TwitterContext(GetAuthorizer());
            int rCount = 0;
            int.TryParse(Responses.Text, out rCount);
            Search srch = new Search();
            try
            {
                srch = Enumerable.SingleOrDefault((from search in
                    twitterContext.Search
                                                    where search.Type == SearchType.Search &&
                                                search.Query == searchTerm &&
                                                search.Count == rCount &&
                                                search.IncludeEntities == true &&
                                                search.ResultType == ResultType.Popular
                                                    select search));
            }
            catch (Exception ex)
            {
                //There must have been an error connecitng to the twitter api
                //just pop a quick alert.....
                Response.Write("<script>alert('There was an error attempting the search against the Twitter API. Please retry your request at a later time');</script>");
                return new List<SMDTweet>();
            }

            //dont wast time trying to parse the results of they do not contain a result set.
            if (srch != null && srch.Statuses.Count > 0)
            {
                foreach (Status s in srch.Statuses)
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
                 
                    ret.Add(t);

                }
        
            }

            return ret;
        }

        /// <summary>
        /// This private method resurn a credential object for authenticating an application on twitter
        /// </summary>
        /// <returns></returns>
        private SingleUserAuthorizer GetAuthorizer()
        {
            return new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    //NOTE: These are MY personal Twitter keys.... DO NOT SHARE
                    ConsumerKey = "icn846h9F1FmGrcPOB7E8Vs0x",
                    ConsumerSecret = "Q2q8aNBQXrTAkxLe8DZn5ZCyHOEzVQI1xMuCKCCHcx6VuWjWWZ",
                    AccessToken = "838471865211498500-LTrdsGuAFNEAMQ6ylYqkbxpdv8MS8cd",
                    AccessTokenSecret = "AoSB2EwuXj9Er4HoQDFoUefcMfAC75ZTuDj1Vqw2UxqtA"
                }
            };
        }
    }
}