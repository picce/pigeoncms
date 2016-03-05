<%@ Page Title="" Language="C#" MasterPageFile="~/pgn-content/masterpages/puppets.master" AutoEventWireup="true" CodeFile="news.aspx.cs" Inherits="contents_news" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" runat="Server">

    <div class="o-container">

        <div class="o-row o-row--small u-table u-table--full u-pad-tb--5">

            <div class="o-title o-title--small o-col o-col--35 u-table-cell">
                news
            </div>

            <div class="o-title o-subtitle--big o-col o-col--50 u-table-cell">
                Load a list of news using items. Every item has its own security context.
            </div>

        </div>

        <div class="o-row o-row--medium u-table u-table--full">

            <div class="o-code--cont">

                <pre>
                    <code class="cs hljs">
//retrieve news list using user security context
var newsMan = new ItemsManager&lt;Item, ItemsFilter&gt;(true, false);
var filter = new ItemsFilter();
filter.SectionId = Acme.Settings.NewsSectionId;
filter.CategoryId = Acme.Settings.ArchiveCategoryId;
filter.Enabled = PigeonCms.Utility.TristateBool.True;
var ListNews = newsMan.GetByFilter(filter, "");
                    </code>
                </pre>

            </div>

        </div>

        <div class="o-row">

            <span class="o-title--hided">Our news</span>

            <div class="c-teaser-container o-col o-col--100 o-col--nopad">

                <div class="u-table u-table-full o-row o-row--full">

                    <%  foreach (var news in ListNews) {    %>
                            
                            <a href="/contents/detail/<%=news.Alias %>/<%=news.Id %>" 
                                class="o-col <%=news.CssClass %> u-table-cell o-teaser-single o-teaser-single--link 
                                    <%=(news.ReadAccessType == PigeonCms.MenuAccesstype.Registered) ? "o-teaser-single--private" : "" %>">

                                <div class="o-teaser-single--overlay">

                                    <%                 
                                        //default values                       
                                        var dimensions = "?w=360&h=240&scale=both";
                                        var image = "http://placehold.it/360x240";
                                        
                                        if (news.CssClass == "o-col--50") { 
                                            dimensions = "?w=550&h=320&scale=both"; image = "http://placehold.it/550x320"; }
                                        else if (news.CssClass.Contains("o-teaser-single--double-height")) { 
                                            dimensions = "?w=550&h=727&scale=both"; image = "http://placehold.it/550x727"; }
                                        else if (news.CssClass.Contains("o-teaser-single--half-height")) { 
                                            dimensions = "?w=550&h=250&scale=both"; image = "http://placehold.it/550x250"; }

                                        if (!String.IsNullOrEmpty(news.DefaultImage.FileFullUrl)) {
                                            image = news.DefaultImage.FileFullUrl + dimensions;
                                        }                                      
                                    %>

                                    <img alt="img" class="o-image" src="<%=image %>" />
                                    <div class="o-teaser-single--overlay-image">
                                        <span class="o-teaser-single--readmore">read more</span>
                                    </div>
                                    <img alt="private-ico" class="o-image--logo-news" src="/assets/images/logo-small.svg" />
                                </div>

                                <span class="o-teaser-single--category"><%=news.Category.Title %></span>
                                <div class="o-teaser-single--title"><%=news.Title %></div>
                                <div class="o-teaser-single--abstract">
                                    <%=PigeonCms.Utility.Html.StripTagsRegexCompiled(PigeonCms.Utility.Html.GetTextIntro(news.Description)) %>
                                </div>

                            </a>

                    <%  }   %>

                </div>
            </div>

        </div>

    </div>

</asp:Content>