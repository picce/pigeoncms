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

	ShowVariants(showVariantsSuccess, showVariantsFailed, parseInt(itemId));

};

$(document).on('click', '#linkAll', function() {

	var $variants = $(container),
		$forms = $variants.find('.form-variant'),
		itemId = $variants.data('itemid');
	debugger;
	_.each($forms, function(form){
		var $form = $(form),
			variantId = $form.find('.saveVariant').data('variantid');
		if(variantId == 0) {
			$form.parent().remove();
		}
	});

	GetLinkVariants(getLinkVariantsSuccess, getLinkVariantsFailed, parseInt(itemId), 0, 0);

});

function getAttributeValuesForVariantsSuccess(result) {

	console.log(result);
	var attributesValues = result;
	var parsedValues = $.parseJSON(attributesValues);

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

function showVariantsSuccess(result) {
	console.log(result);
	var variants = $.parseJSON(result);

	_.each(variants, function(variant){
		console.log(variant.Info);
		console.log(variant.Product);
		emitter.emit('generateInputs', variant.Info.ListIds, variant.Info.ListValues, variant.Product);
	});
}

function showVariantsFailed(result) {
	console.log(result);
}

function getLinkVariantsSuccess(result) {

	var values = result,
		parsedValues = $.parseJSON(values);

	var ids = parsedValues.ListIds,
		values = parsedValues.ListValues;

	//debugger;

	var Product = {
		Id: '0',
		ProductCode: '',
		Availability: '',
		RegularPrice: '',
		SalePrice: '',
		Weight: ''
	};

	for(var key in ids) {
		emitter.emit('generateInputs', ids[key], values[key], Product);
	}

}

function getLinkVariantsFailed(result) {
	console.log(result);
}

//export compileAttributes
module.exports.compileAttributes = compileAttributes;