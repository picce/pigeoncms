<%@ Page Title="" Language="C#" MasterPageFile="~/pgn-content/masterpages/puppets.master" AutoEventWireup="true" CodeFile="detail.aspx.cs" Inherits="contents_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" runat="Server">

    <div class="o-container">

        <div class="o-row o-row--small u-table u-table--full u-pad-tb--5">

            <div class="o-title o-title--small o-col o-col--35 u-table-cell">
                detail
            </div>

            <div class="o-title o-subtitle--big o-col o-col--50 u-table-cell">
                Learn how to use and extend items. Example using <code class="o-code">asp:Repeater</code>
            </div>

        </div>

        <div class="o-row o-row--medium u-table u-table--full u-pad-b--5">

            <div class="o-col o-col--50 u-table-cell o-detail-blog">

                <div class="o-title o-title--big">Detail</div>

                <%--<div class="o-text">
                    Using <code class="o-code o-code--strong">getLabel</code> method.
                    <br />
                    <br />
                    Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa.
                </div>--%>

                <div class="c-slider">

                    <div class="owl-carousel owl-theme">
                        <%  foreach (var image in SingleItem.Images) {    %>
                                <div class="item o-media-fullscreen" style="background-image: url('<%=image.FileFullUrl %>');"></div>      
                        <%  }   %>
                    </div>

                </div>

                <div class="o-detail-blog--category u-pad-t--5">
                    <%=SingleItem.Category.Title  %>
                </div>

                <h2 class="o-title o-detail-blog--title u-pad-tb--5"><%=SingleItem.Title  %></h2>

                <div class="o-text o-detail-blog--text">
                    <%=DescriptionItem  %>
                </div>

                <div class="u-table u-table--full o-detail-blog--attachments u-pad-tb--5">

                    <%  foreach (var attachment in SingleItem.Files) {    %>
                            <div class="u-table-cell o-detail-blog--attachments-wrapper">
                                <a href="<%=attachment.FileFullUrl  %>" target="_blank" class="o-detail-blog--attachments-single">
                                    <span><%=attachment.FileExtension   %></span>
                                    <span class="o-icon-arrow"></span>
                                    <div class="o-detail-blog--attachments-download">download now</div>
                                </a>
                            </div>
                    <%  }   %>

                </div>

            </div>

            <div class="o-col o-col--50 u-table-cell o-code--cont">

                <div class="o-title o-title--big">
                    &nbsp;
                </div>

                <div class="o-wrapper-pre o-triangle">
                    <pre>
                        <code class="cs html hljs">
                            <%=CodeSource %>
                        </code>
                    </pre>
                </div>

            </div>

        </div>

    </div>

</asp:Content>
