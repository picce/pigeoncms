var $ = require('jquery');
var _ = require('lodash');
var emitter = require('../modules/emitter');
var templates = require('../modules/templates');

var compileValues = function(parsedValues) {

	//generate template
	var selectAttributesValues = templates.optionValues;

	//render values
	var vals = selectAttributesValues({
		parsedValues: parsedValues
	}); 

	$('#selectLinkValue').empty();
	$('#selectLinkValue').append('<option selected> SLECT VALUES </option>');
	$('#selectLinkValue').append(vals);

};

emitter.on('attrsChanged', compileValues);

