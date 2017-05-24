<%@ Page Title="" Language="C#" MasterPageFile="~/pgn-content/masterpages/puppets.master" AutoEventWireup="true" CodeFile="elements.aspx.cs" Inherits="contents_elements" %>
<%@ Register Assembly="PigeonCms.Core" Namespace="PigeonCms.Controls" TagPrefix="pgn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" runat="Server">

	<div class="o-container">

		<div class="o-row o-row--small u-table u-table--full u-pad-tb--5 u-border--bottom">

			<div class="o-title o-title--small o-col o-col--20 u-table-cell">
				Labels and images
			</div>

			<div class="o-title o-subtitle--big o-col o-col--80 u-table-cell">
				Add dynamic resources in your page. Resources are automatically cached. On first method call, if not present, the resources are created in backend labels admin area.
			</div>

		</div>

		<div class="o-row o-row--small u-table u-table--full u-pad-tb--5 u-border--bottom">

			<div class="o-title o-title--small o-col o-col--20 u-table-cell o-title--align-text">
				Labels
			</div>

			<div class="o-text o-col o-col--80 u-table-cell">
				You can show dynamic and localized texts in your page using the following ways. Each method has the following parameters:
				<div class="o-list-commands">

					<div class="o-list-commands--single o-row">
						<div class="o-col o-col--30 u-no-pad o-code--cont_small">
							<code class="o-code o-code--strong">string resourceSet</code>
						</div>
						<div class="o-col o-col--70 u-no-pad">
                            used to group resources of the same type (example: page name)
						</div>
					</div>

					<div class="o-list-commands--single o-row">
						<div class="o-col o-col--30 u-no-pad o-code--cont_small">
							<code class="o-code o-code--strong">string resourceId</code>
						</div>
						<div class="o-col o-col--70 u-no-pad">
                            the resource identifier
						</div>
					</div>

					<div class="o-list-commands--single o-row">
						<div class="o-col o-col--30 u-no-pad o-code--cont_small">
							<code class="o-code o-code--strong">string defaultValue </code>
						</div>
						<div class="o-col o-col--70 u-no-pad">
                            the default value of the label
						</div>
					</div>
				</div>
			</div>

		</div>

		<div class="o-row u-table u-table--full u-pad-t--5">

			<div class="o-col o-col--100 u-table-cell">
				<div class="o-title o-title--big">
					Sample 1
				</div>
				<div class="o-text">
					Using <code class="o-code o-code--strong">GetLabel</code> method.
                    <br />
                    <br />
				</div>
			</div>

			<div class="o-col o-col--100 u-table-cell">

				<div class="o-wrapper-pre">
					<pre>
						<code class="cs hljs">
&lt;% =GetLabel("Page1", "Sample1", "Using GetLabel method") %&gt;
						</code>
					</pre>
				</div>

			</div>

		</div>

		<div class="o-row u-table u-table--full u-pad-t--5">

			<div class="o-col o-col--100 u-table-cell">
				<div class="o-title o-title--big">
					Sample 2
				</div>
				<div class="o-text">
					Using <code class="o-code o-code--strong">&lt;pgn:Label&gt;</code> server control with <code class="o-code o-code--strong">TextMode="Text"</code>.
                    This is useful to include big data in plain text.
                    <br>
                    <br>
				</div>
			</div>

			<div class="o-col o-col--100 u-table-cell">

				<div class="o-wrapper-pre">
					<pre>
						<code class="xml hljs">
&lt;pgn:Label runat="server" ResourceSet="Page1" ResourceId="Sample2" TextMode="Text"&gt;
	Your simple plain text content
&lt;/pgn:Label&gt;
						</code>
					</pre>
				</div>

			</div>

		</div>

		<div class="o-row u-table u-table--full u-pad-t--5">

			<div class="o-col o-col--100 u-table-cell">
				<div class="o-title o-title--big">
					Sample 3
				</div>
				<div class="o-text">
					Using <code class="o-code o-code--strong">&lt;pgn:Label&gt;</code> server control with <code class="o-code o-code--strong">TextMode="BasicHtml"</code>.
                    This is useful to include big data with basic html 
                    <br>
                    <br>
				</div>
			</div>

			<div class="o-col o-col--100 u-table-cell">

				<div class="o-wrapper-pre">
					<pre>
						<code class="xml hljs">
&lt;pgn:Label runat="server" ResourceSet="Page1" ResourceId="Sample3" TextMode="BasicHtml"&gt;
	Your &lt;em&gt;simple&lt;/em&gt; html content.&lt;br&gt;
&lt;/pgn:Label&gt;
						</code>
					</pre>
				</div>

			</div>

		</div>

		<div class="o-row u-table u-table--full u-pad-t--5">

			<div class="o-col o-col--100 u-table-cell">
				<div class="o-title o-title--big">
					Sample 4
				</div>
				<div class="o-text">
					Using <code class="o-code o-code--strong">&lt;pgn:Label&gt;</code> server control with <code class="o-code o-code--strong">TextMode="Html"</code>.
                    This is useful to include big data with complex html content. In the backend the user could change the content with a full WYSIWYG editor.
                    <br>
                    <br>
				</div>
			</div>

			<div class="o-col o-col--100 u-table-cell">

				<div class="o-wrapper-pre">
					<pre>
						<code class="xml hljs">
&lt;pgn:Label runat="server" ResourceSet="Page1" ResourceId="Sample4" TextMode="Html"&gt;
	&lt;p&gt;
	Your &lt;em&gt;complex&lt;/em&gt; html content.&lt;br&gt;
	This is a &lt;a href="http://www.google.com"&gt;link&lt;/a&gt;
	&lt;/p&gt;
&lt;/pgn:Label&gt;
						</code>
					</pre>
				</div>

			</div>

		</div>

		<div class="o-row u-table u-table--full u-pad-t--5">

			<div class="o-col o-col--100 u-table-cell">
				<div class="o-title o-title--big">
					Sample 5
				</div>
				<div class="o-text">
					Using <code class="o-code o-code--strong">GetLabel</code> method in code behind.
                    <br>
                    <br>
				</div>
			</div>

			<div class="o-col o-col--100 u-table-cell">

				<div class="o-wrapper-pre">
					<pre>
						<code class="xml hljs">
&lt;asp:Literal runat="server" ID="LitSample5"&gt;&lt;/asp:Literal&gt;
						</code>
					</pre>
				</div>

				<div class="o-wrapper-pre">
					<pre>
						<code class="cs hljs">
//code behind
LitSample5.Text = GetLabel("Page1", "Sample5", "Label value calling method in code behind");
						</code>
					</pre>
				</div>

			</div>

		</div>

		<div class="o-row o-row--small u-table u-table--full u-pad-b--5 u-border--bottom u-no-pad">
			&nbsp;
		</div>

		<div class="o-row o-row--small u-table u-table--full u-pad-tb--5 u-border--bottom">

			<div class="o-title o-title--small o-col o-col--20 o-title--align-text u-table-cell">
				images
			</div>

			<div class="o-text o-col o-col--80 u-table-cell">
				You can add localized image resources in your page using the following ways. All resources are then available in backed to be edited.
			</div>

		</div>

		<div class="o-row u-table u-table--full u-pad-t--5">

			<div class="o-col o-col--50 u-table-cell">
				<div class="o-title o-title--big">
					Sample image 1
				</div>
				<div class="o-text">
					Using <code class="o-code o-code--strong">&lt;pgn:Image&gt;</code> user control 
					with a simple <code class="o-code o-code--strong">&lt;img&gt;</code> tag.
					<pgn:Image runat="server" ResourceSet="Page1" ResourceId="Image1" Allowed="jpg|png|gif" MaxSize="1024">
						<img class="o-image o-image--shadow u-margin-t--5" src="/assets/images/elements-img1.jpg" alt="pigeon flying" />
					</pgn:Image>
				</div>
			</div>

			<div class="o-col o-col--50 u-table-cell">

				<div class="o-title o-title--big">
					&nbsp;
				</div>

				<div class="o-wrapper-pre o-triangle">
					<pre>
						<code class="xml hljs">
&lt;pgn:Image runat="server" ResourceSet="Page1" ResourceId="Image1"&gt;
	&lt;img src="/assets/images/elements-img1.jpg"  alt='' /&gt;
&lt;/pgn:Image&gt;
						</code>
					</pre>
				</div>

			</div>

		</div>

		<div class="o-row u-table u-table--full u-pad-tb--5 o-row--float-reverse">

			<div class="o-col o-col--50 u-table-cell">
				<div class="o-title o-title--big">
					Sample image 2
				</div>
				<div class="o-text">
					Use the resource as <code class="o-code o-code--strong">div</code> background
					<pgn:Image runat="server" ResourceSet="Page1" ResourceId="Image2">
						<div style="width:480px; height:280px; background-image:url(/assets/images/elements-img1.jpg);"></div>
					</pgn:Image>
				</div>
			</div>

			<div class="o-col o-col--50 u-table-cell">

				<div class="o-title o-title--big">
					&nbsp;
				</div>

				<div class="o-wrapper-pre o-triangle">
					<pre>
						<code class="xml hljs">
&lt;pgn:Image runat="server" ResourceSet="Page1" ResourceId="Image1"&gt;
	&lt;div style="background-image:url(/assets/images/elements-img1.jpg);"&gt;&lt;/div&gt;
&lt;/pgn:Image&gt;
						</code>
					</pre>
				</div>

			</div>

		</div>

		<div class="o-row u-table u-table--full u-pad-t--5">

			<div class="o-col o-col--50 u-table-cell">
				<div class="o-title o-title--big">
					Sample image 3
				</div>
				<div class="o-text">
					Use the resource in a html <code class="o-code o-code--strong">data-</code> attribute.<br />
					<code class="o-code o-code--strong">&lt;span data-image='/assets/images/elements-img1.jpg'&gt;&lt;/span&gt;</code>
				</div>
			</div>

			<div class="o-col o-col--50 u-table-cell">

				<div class="o-title o-title--big">
					&nbsp;
				</div>

				<div class="o-wrapper-pre o-triangle">
					<pre>
						<code class="xml hljs">
&lt;pgn:Image runat="server" ResourceSet="Page1" ResourceId="Image3" SrcAttr="data-image"&gt;
	&lt;span data-image='/assets/images/elements-img1.jpg'&gt;&lt;/span&gt;
&lt;/pgn:Image&gt;
						</code>
					</pre>
				</div>

			</div>

		</div>

	</div>

</asp:Content>
