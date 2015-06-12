var $ = require('jquery');

require('jquery-ui/autocomplete');
var templates = require('../modules/templates');

window.container;

var autocompile = function(attachTo) {
	
	window.container = attachTo;

	//var itemId = $(container).data('itemid');
	document.getElementById("typeProducts").onkeypress=function(e){
		var e=window.event || e
		var keyunicode=e.charCode || e.keyCode
		//Allow alphabetical keys, plus BACKSPACE and SPACE
		console.log(keyunicode);
		if (keyunicode>=65 && keyunicode<=122 || keyunicode==8 || keyunicode==32) {
			GetRelatedProductSearch(getRelatedSuccess, getRelatedFailed, this.value);
		} else if(keyunicode==13 ) {

			e.preventDefault();

		} else {
			return false;
		}
	}
	
};

function getRelatedSuccess(result) {
	//console.log(result);
	var tags = $.parseJSON(result);
	$("#typeProducts").autocomplete({
      	source: tags,
	 	select: function(event, ui) {
	        event.preventDefault();
	        document.getElementById("typeProducts").value = ui.item.label;
			var related = templates.relatedItem;

			//render template
			var html = related({
				id: ui.item.value,
				name: ui.item.label
			}); 

			document.getElementById("typeProducts").value = '';

			$(window.container).append(html);

	    },
	    focus: function(event, ui) {
	        event.preventDefault();
	        document.getElementById("typeProducts").value = ui.item.label;
	    }
    }).data( "ui-autocomplete" )._renderItem = function( ul, item ) {
		ul.addClass('dropdown-menu');
		console.log('worked');
		return $( "<li></li>" )
			.data("item.autocomplete", item)
			.append( "<a>" + item.label + "</a>" )
			.appendTo( ul );
	};
}

function getRelatedFailed(result) {
	console.log(result);
}

//export dropMyAttributes
module.exports.autocompile = autocompile;