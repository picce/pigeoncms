var $ = require('jquery');
var _ = require('lodash');
var emitter = require('../modules/emitter');
var templates = require('../modules/templates');

var generateInputs = function(ids, values) {

	var form = templates.inputVariants;

	// var attrId = $('#selectLinkAttribute').find("option:selected").val(),
	//     attrValId = $('#selectLinkValue').find("option:selected").val(),
	//     attribute = $('#selectLinkAttribute').find("option:selected").text(),
	//     attributeValue = $('#selectLinkValue').find("option:selected").text();

	var compiled = form({
		// attrId: parsedValues.attrId,
		// attrValId: parsedValues.attrValId,
		// attribute: parsedValues.attribute,
		// attributeValue: parsedValues.attributeValue
		values: values,
		ids: ids
	});

	$('#variantsForm').append(compiled);
};

emitter.on('generateInputs', generateInputs);

