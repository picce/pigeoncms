var $ = require('jquery');
var _ = require('lodash');
var emitter = require('../modules/emitter');
var templates = require('../modules/templates');
var container;

var compileAttributes = function(attachTo) {
	
	container = attachTo;

	var itemId = $(container).data('itemid');

	GetAttributes(getAttributesSuccess, getAttributesFailure, parseInt(itemId));
	
};

function getAttributesSuccess(result) {

 	var attributes = result;

	//parse the json
	var parsedAttributes = $.parseJSON(attributes);

	//generate template
	var select = templates.attributesAttributes;

	//render template
	var html = select({
		parsedAttributes: parsedAttributes
	}); 

	//useful logs
	// console.log('json :' + parsedAttributes );
	// console.log('html : ' + html);

	//append to element
	$(container).append(html);

}

function getAttributesFailure(result) {
    console.log(result);
}

$(document).on('change', '#selectAttributes', function(e) {

	//store value
	var value = $(this).val();

	//don't use the default empty value
	if (value > 0) {

		//call ajax method
		GetAttributeValues(getAttributeValuesSuccess, getAttributeValuesFailure, parseInt(value));

	}

});

function getAttributeValuesSuccess(result) {

	//assign result
	var attributesValues = result;

    //parse json 
	var parsedAttributesValues = $.parseJSON(attributesValues);

	var text = $('#selectAttributes').find("option:selected").text();
		value = $('#selectAttributes').find("option:selected").val();
	
	//build box with emitter
	emitter.emit('generateBox', parsedAttributesValues, text, container);

	//remove from select the option clicked
	$('#selectAttributes option[value="' + value + '"]').remove();

	//disable select only if present the default empty value
	if( $('#selectAttributes option').length == 1 ) $('#selectAttributes').prop('disabled', 'disabled');

}

function getAttributeValuesFailure(result) {
    console.log(result);
}

//export dropMyAttributes
module.exports.compileAttributes = compileAttributes;

