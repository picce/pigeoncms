var $ = require('jquery');
var _ = require('lodash');
require('jquery-ui/autocomplete');
var templates = require('../modules/templates');

window.container;

var autocompile = function(attachTo) {
	
    window.container = attachTo;

    var itemId = $(attachTo).data('itemid');

    GetRelatedById(getByIdSuccess, getByIdFailed, parseInt(itemId));

	//var itemId = $(container).data('itemid');
	document.getElementById("typeProducts").onkeypress=function(e){
		var e=window.event || e
		var keyunicode=e.charCode || e.keyCode
		//Allow alphabetical keys, plus BACKSPACE and SPACE
		console.log(keyunicode);
		if (keyunicode>=65 && keyunicode<=122 || keyunicode==8 || keyunicode==32) {
			GetRelatedProductSearch(getRelatedSuccess, getRelatedFailed, this.value, parseInt(itemId));
		} else if(keyunicode==13 ) {

			e.preventDefault();

		} else {
			return false;
		}
	}
	
};

$(document).on('click', '.deleteRelated', function (e) {
    e.preventDefault();
    var $this = $(this),
        relatedId = $this.data('id'),
        itemId = $(window.container).data('itemid');
    DeleteRelated($.proxy(deleteSuccess, this), deleteFailed, itemId, relatedId);
});

function getRelatedSuccess(result) {
	//console.log(result);
	var tags = $.parseJSON(result);
	$("#typeProducts").autocomplete({
	    source: tags,
	    select: function (event, ui) {
	        event.preventDefault();
	        document.getElementById("typeProducts").value = ui.item.label;
	        var related = templates.relatedItem,
				itemId = $(window.container).data('itemid');
	        //render template
	        var html = related({
	            id: ui.item.value,
	            name: ui.item.label
	        });

	        document.getElementById("typeProducts").value = '';

	        SetRelatedProducts(setRelatedSuccess, setRelatedFailed, parseInt(itemId), parseInt(ui.item.value));

	        $(window.container).append(html);

	    },
	    focus: function (event, ui) {
	        event.preventDefault();
	        document.getElementById("typeProducts").value = ui.item.label;
	    }
	}).data("ui-autocomplete")._renderItem = function (ul, item) {
	    ul.addClass('dropdown-menu');
	    console.log('worked');
	    return $("<li></li>")
			.data("item.autocomplete", item)
			.append("<a>" + item.label + "</a>")
			.appendTo(ul);
	};
}

function getRelatedFailed(result) {
	console.log(result);
}

function getByIdSuccess(result) {

    var items = $.parseJSON(result);

    var related = templates.relatedItem;
    //render template

    _.each(items, function (item) {
        var html = related({
            id: item.id,
            name: item.name
        });

        $(window.container).append(html);
    });

}

function getByIdFailed(result) {}

function setRelatedSuccess(result) { }
function setRelatedFailed(result) { }

function deleteSuccess(result) {
    var $well = $(this).parents('.well');

    $well.parent().fadeOut(800);

}

function deleteFailed(result) {

}

//export dropMyAttributes
module.exports.autocompile = autocompile;
