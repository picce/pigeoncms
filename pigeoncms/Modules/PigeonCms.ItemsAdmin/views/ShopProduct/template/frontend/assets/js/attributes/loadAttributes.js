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
		parse = $.parseJSON(values),
		boxes = parse.boxes,
		spans = parse.spans;

	debugger;

	for(var key in boxes) {
		var text = boxes[key][0];
		var parsedAttributesValues = boxes[key].splice(1, boxes[key].length);
		var box = templates.boxAttributes;
		var html = box({
			parsedAttributesValues: parsedAttributesValues,
			text: text
		});
		$(container).append(html);
	}

	for(var key in spans) {
		var text = spans[key][0];
		var value = spans[key][1];
		var span = templates.spanAttributes;
	    var html = span({
	        value: value,
	        text: text
	    });
		$(container).append(html);
	}

}

function compileAttributesFailure(result) {
	console.log(result);
}

module.exports.loadAttributes = loadAttributes;