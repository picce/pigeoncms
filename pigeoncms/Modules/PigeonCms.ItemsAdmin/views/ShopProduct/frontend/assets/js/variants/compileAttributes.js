var $ = require('jquery');
var _ = require('lodash');
var emitter = require('../modules/emitter');
var templates = require('../modules/templates');
var container;

var compileAttributes = function(attachTo) {

	//set global container
	container = attachTo;
	var itemId = $(container).data('itemid');

	GetAttributesForVariants(getAttributesForVariantsSuccess, getAttributesForVariantsFailed, parseInt(itemId));

};

$(document).on('change', '#selectLinkAttribute', function(){
	
	var $this = $(this),
		value = $this.val();

	var itemId = $(container).data('itemid');

	if(value > 0) {
		GetAttributeValuesForVariants(GetAttributeValuesForVariantsSuccess, GetAttributeValuesForVariantsFailed, parseInt(value), parseInt(itemId))
	}

});

$(document).on('change', '#selectLinkValue', function(){

	var $this = $(this),
		value = $this.val();

	// var itemId = $(container).data('itemid');

	if(value > 0) {

		//GetAttributeValuesForVariants(GetAttributeValuesForVariantsSuccess, GetAttributeValuesForVariantsFailed, value, parseInt(itemIdSuccess))
		emitter.emit('valsChanged');
			
	}

});

function getAttributesForVariantsSuccess(result) {

	console.log(result);

	//select of attributes, generated in ajax
	var storedAttributes = result;
	var parsedAttributes = $.parseJSON(storedAttributes);

	//generate template
	var selectAttrs = templates.variantsAttributes;

	//render attrs
	var attrs = selectAttrs({
		parsedAttributes: parsedAttributes
	}); 

	var baseValSel = templates.variantsValues;

	//useful logs
	// console.log('json :' + parseAttributes );
	// console.log('html : ' + html);

	//append to element
	$(container).append(attrs + baseValSel);
}

function getAttributesForVariantsFailed(result) {
	console.log(result);
}

function GetAttributeValuesForVariantsSuccess(result) {

	console.log(result);
	var attributesValues = result;
	var parsedValues = $.parseJSON(attributesValues);

	emitter.emit('attrsChanged', parsedValues);
}

function GetAttributeValuesForVariantsFailed(result) {
	console.log(result);
}


//export compileAttributes
module.exports.compileAttributes = compileAttributes;