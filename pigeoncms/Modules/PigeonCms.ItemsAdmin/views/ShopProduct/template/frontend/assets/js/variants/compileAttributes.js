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

	emitter.emit('variantsButton');

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
		emitter.emit('generateInputs', variant.Info.ListIds, variant.Info.ListValues, variant.Product, variant.CustomFields);
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

	var itemId = $(container).data('itemid');

	//debugger;

	var dims = {
	   DimL: "",
	   DimW: "",
	   DimH: ""
	}

	var Product = {
		Id: '0',
		ProductCode: '',
		Availability: '',
		RegularPrice: '',
		SalePrice: '',
		Weight: '',
		Dimensions: dims
	};

	var proxy = { values: values, ids: ids, Product: Product}

	GetCustomValues($.proxy(getCustomSuccess, proxy), getCustomFailed, parseInt(itemId));

}

function getLinkVariantsFailed(result) {
	console.log(result);
}


function getCustomSuccess(result) {

	var customAttributes = $.parseJSON(result);

	console.log(this);

	console.log(customAttributes);

	for(var key in this.ids) {
		emitter.emit('generateInputs', this.ids[key], this.values[key], this.Product, customAttributes);
	}

}

function getCustomFailed(result) {
    console.log(result);
}

//export compileAttributes
module.exports.compileAttributes = compileAttributes;