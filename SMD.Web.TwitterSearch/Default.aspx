<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SMD.Web.TwitterSearch._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Twitter Hashtag Search</h1>
        <p class="lead">This will search Twitter feeds for posts relevenat to the entered HashTag</p>
    </div>

    <div class="row">
        <div >
            <h2>Enter the Hashtag to Search for (# prefix not required)</h2>
            <p>
                <asp:TextBox ID="SearchFor" runat="server" Width="288px"></asp:TextBox>
                <asp:Button ID="TwitterGo" runat="server" OnClick="TwitterGo_Click" Text="Go" />
            </p>
            <p>
                <asp:DropDownList ID="Responses" runat="server">
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem Selected="True">100</asp:ListItem>
                    <asp:ListItem>200</asp:ListItem>
                </asp:DropDownList>
&nbsp;(Number of results to return)</p>
            <p>
                </p>
            <asp:Panel ID="ResultPanel" runat="server">
                <br />
                <asp:GridView ID="ResultsGrid" runat="server" AllowPaging="True" AllowSorting="True" EnableSortingAndPagingCallbacks="True" PageSize="25" AutoGenerateColumns="False">
                    <Columns>
                        <asp:ImageField DataImageUrlField="ProfilePicUrl" HeaderText="Image">
                        </asp:ImageField>
                        <asp:BoundField DataField="Author" HeaderText="Author" />
                        <asp:BoundField DataField="ScreenName" HeaderText="Screen Name" />
                        <asp:BoundField DataField="TweetText" HeaderText="Tweet" />
                        <asp:BoundField DataField="Created" HeaderText="Time Stamp" />
                        <asp:BoundField DataField="Likes" HeaderText="Likes" />
                        <asp:BoundField DataField="Retweets" HeaderText="Retweets" />
                        <asp:HyperLinkField DataNavigateUrlFields="TweetLink" HeaderText="Link to Tweet" Text="Click Here" />
                    </Columns>
                    <PagerSettings LastPageText="Prev" NextPageText="Next" PageButtonCount="5" Position="TopAndBottom" />
                </asp:GridView>
                <br />
            </asp:Panel>
            <p>
                &nbsp;</p>
        </div>
    </div>

</asp:Content>
