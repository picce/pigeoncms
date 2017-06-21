<%@ Page Title="" Language="C#" MasterPageFile="~/pgn-content/masterpages/puppets.master" AutoEventWireup="true" CodeFile="blog.aspx.cs" Inherits="pgn_content_contents_blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" runat="Server">

    <div class="o-container" >

        <div class="o-row o-row--small u-table u-table--full u-pad-tb--5">

            <div class="o-title o-title--small o-col o-col--35 u-table-cell">
                blog
            </div>

            <div class="o-title o-subtitle--big o-col o-col--50 u-table-cell">
                Learn how to use and extend items. Example using <code class="o-code">asp:Repeater</code>
            </div>

        </div>

        <div class="o-row o-row--medium u-table u-table--full">

            <div class="o-code--cont">

                <pre>
                    <code class="cs hljs">
public static void getInfo() {
    string test = '';
}
                    </code>
                </pre>

            </div>

        </div>

        <div class="o-row o-row--medium u-table u-table--full u-pad-tb--5">

            <div class="o-title o-title--blog o-col o-col--100 u-table-cell">
                The Blog
            </div>

        </div>
        
        <div id="vue-app" class="c-blog-container o-row o-row--big u-table u-table--full">

            <div v-for="post in blogList">
                <div class="o-col o-col--100 u-table-cell o-blog-single {{post.CssClass}}">

                    <span class="o-blog-single--category">{{post.CategoryTitle}}</span>

                    <div class="o-blog-single--image o-media-fullscreen" 
                        :style="{BackgroundImage: 'url(' + post.DefaultImg + ')'}">
                        <div class="o-blog-single--title" v-text="post.Title">{{post.Title}}</div>
                    </div>

                    <div class="o-row o-row--full u-table u-table-full o-blog-single--wrapper">
                        <div class="o-col o-col--70 u-table-cell o-blog-single--abstract">
                            {{post.DescriptionAbstract}}
                        </div>
                        <div class="o-col o-col--30 u-table-cell o-blog-single--wrapper-link">
                            <a class="o-link-button o-link-button--blog" href="/contents/detail/{{post.Alias}}/{{post.PostId}}"><span>read more</span></a>
                        </div>
                    </div>

                </div>
            </div>

        </div>


    </div>

</asp:Content>