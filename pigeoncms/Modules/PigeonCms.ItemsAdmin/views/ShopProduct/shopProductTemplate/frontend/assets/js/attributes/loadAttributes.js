var $ = require('jquery');
var _ = require('lodash');
var emitter = require('../modules/emitter');
var templates = require('../modules/templates');
var container;

var loadAttributes = function(attachTo) {
	
	container = attachTo;
	var itemId = $(container).data('itemid');

	CompileAttributes(compileAttributesSuccess, compileAttributesFailure, parseInt(itemId));
	
};

function compileAttributesSuccess(result) {

	var values = result,
		parse = $.parseJSON(values);

	for(var key in parse) {
		var text = parse[key][0];
		//parse = parse.splice(0, 1);
		var parsedAttributesValues = parse[key].splice(1, parse[key].length);
		//debugger;
		var box = templates.boxAttributes;
		var html = box({
			parsedAttributesValues: parsedAttributesValues,
			text: text
		}); 
		$(container).append(html);
	}

}

function compileAttributesFailure(result) {
	console.log(result);
}

module.exports.loadAttributes = loadAttributes;