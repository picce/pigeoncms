<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detail.aspx.cs" Inherits="_detail" MasterPageFile="~/pgn-content/masterpages/puppets.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" Runat="Server">

    <h1>
        <%=GetLabel("AQ_detail", "Title", "Prodotto Singolo")%>    
    </h1>

    <script src="//code.jquery.com/jquery-1.11.3.min.js"></script>
    <script type="text/javascript">

        (function ($) {

            $(document).on('ready', function () {

                function nextAttributeValues(values) {
                    return $.ajax({
                        url: '/pgn-content/contents/ajax.aspx/GetNextValues',
                        type: 'POST',
                        dataType: 'json',
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ values: values.join(','), productId: parseInt($('#PanelDropVariants').attr('data-product-id'))})
                    }).then(function (response) {
                        return response.d;
                    });

                }

                var $selects = $('select[data-select-id]');
                var $last = $('select[data-last]');

                $selects.each(function (i, select) {

                    var $select = $(select);
                    var id = $select.data('select-id');
                    var parents = $select.attr('data-select-parent').split(',');


                    var $parents = $selects.filter(function (i, el) {
                        var selectId = $.attr(el, 'data-select-id');
                        return jQuery.inArray(selectId, parents) !== -1;
                    });


                    $parents.on('change', function (e) {

                        var values = $.map($parents.get(), function (el) {
                            return $(el).val();
                        });

                        values = $.grep(values, function (v) { return !!v; });

                        var completed = values.length === $parents.length;

                        if (values.length === $parents.length) {
  
                            select.innerHTML = '';
                            $.when(nextAttributeValues(values)).then(function (options) {

                                var html = $.map(options, function (el) {
                                    return '<option value="' + el.value + '">' + el.label + '</option>';
                                });
                                $select.html('<option></option>' + html.join(''));

                            });
                        }

                    });

                });


                $last.on('change', function (e) {

                    var values = $.map($selects.get(), function (select) {
                        return $(select).val();
                    });

                    $.ajax({
                        url: '/pgn-content/contents/ajax.aspx/GetThread',
                        type: 'POST',
                        dataType: 'json',
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ values: values.join(','), productId: parseInt($('#PanelDropVariants').attr('data-product-id')) })
                    }).then(function (response) {
                        console.log(response.d);
                        $('#productTitle').html(response.d.title);
                        $('#productDescription').html(response.d.description);
                        $('#productRegPrice').html(response.d.regPrice);
                        $('#productSalePrice').html(response.d.salePrice);
                    });

                    return false;
                });

            });

        })(jQuery);


    </script>


    <div class="product-box">
        <h2 id="productTitle"><asp:Literal ID="LitTitle" runat="server" /></h2>
        <p id="productDescription"><asp:Literal ID="LitDescription" runat="server" /></p>
        <p id="productRegPrice"><asp:Literal ID="LitRegPrice" runat="server" /></p>
        <p id="productSalePrice"><asp:Literal ID="LitSalePrice" runat="server" /></p>
    </div>

    <asp:Literal ID="LitVariants" runat="server" Visible="false"></asp:Literal>

    <asp:Panel ID="PanelDropVariants" runat="server" ClientIDMode="Static"></asp:Panel>

</asp:Content>
