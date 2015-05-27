var $ = require('jquery');
var _ = require('lodash');
var emitter = require('../modules/emitter');
var templates = require('../modules/templates');

var generateInputs = function() {

	var form = templates.inputVariants;

	var attrId = $('#selectLinkAttribute').find("option:selected").val(),
	    attrValId = $('#selectLinkValue').find("option:selected").val(),
	    attribute = $('#selectLinkAttribute').find("option:selected").text(),
	    attributeValue = $('#selectLinkValue').find("option:selected").text();

	var compiled = form({
		attrId: attrId,
		attrValId: attrValId,
		attribute: attribute,
		attributeValue, attributeValue
	});

	$('#variantsForm').append(compiled);
};

emitter.on('valsChanged', generateInputs);

