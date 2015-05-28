var $ = require('jquery');
var _ = require('lodash');
var emitter = require('../modules/emitter');
var templates = require('../modules/templates');
var container;

var compileAttributes = function(attachTo) {

	//set global container
	container = attachTo;
	var itemId = $(container).data('itemid');

	GetAttributeValuesForVariants(getAttributeValuesForVariantsSuccess, getAttributeValuesForVariantsFailed, parseInt(itemId));

};

// $(document).on('change', '#selectLinkValue', function(){

// 	var $this = $(this),
// 		value = $this.val();

// 	var itemId = $(container).data('itemid'),
// 		attrVal = $('#selectLinkAttribute').val();

// 	if(value > 0) {

// 		//GetAttributeValuesForVariants(GetAttributeValuesForVariantsSuccess, GetAttributeValuesForVariantsFailed, value, parseInt(itemIdSuccess))
// 		//emitter.emit('valsChanged');
// 		GetLinkVariants(getLinkVariantsSuccess, getLinkVariantsFailed, parseInt(itemId), parseInt(attrVal), parseInt(value));
			
// 	}

// });

$(document).on('click', '#addVariant', function() {

	var itemId = $(container).data('itemid');
	GetLinkVariants(getLinkVariantsSuccess, getLinkVariantsFailed, parseInt(itemId), 0, 0);

});


$(document).on('click', '#linkAll', function() {

	var itemId = $(container).data('itemid');
	GetLinkVariants(getLinkVariantsSuccess, getLinkVariantsFailed, parseInt(itemId), 0, 0);

});

function getAttributeValuesForVariantsSuccess(result) {

	console.log(result);
	var attributesValues = result;
	var parsedValues = $.parseJSON(attributesValues);

	//debugger;

	for(var key in parsedValues) {

		var info = parsedValues[key].pop();

		var tmpl = templates.variantsValues;

		var selectBox = tmpl({
			info: info,
			parsedValues: parsedValues[key]
		}); 

		$('#variantsBoxes').append(selectBox);
	}

}

function getAttributeValuesForVariantsFailed(result) {
	console.log(result);
}

function getLinkVariantsSuccess(result) {

	var values = result,
		parsedValues = $.parseJSON(values);

	var ids = parsedValues.Listids,
		values = parsedValues.ListValues;

	for(var key in ids) {
		emitter.emit('generateInputs', ids[key], values[key]);
	}

	// for(var key in parsedValues) {
	// 	emitter.emit('valsChanged', parsedValues[key]);
	// }
	
}

function getLinkVariantsFailed(result) {
	console.log(result);
}

//export compileAttributes
module.exports.compileAttributes = compileAttributes;