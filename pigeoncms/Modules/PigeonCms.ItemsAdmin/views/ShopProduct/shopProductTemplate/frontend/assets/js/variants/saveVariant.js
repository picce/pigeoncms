var $ = require('jquery');
var _ = require('lodash');
require('fancybox')($);
var emitter = require('../modules/emitter');
var templates = require('../modules/templates');
var validator = require('../modules/formValidator');

var saveVariant = function(parsedAttributesValues, text, attachTo) {
	
    //set global container
    container = attachTo;
    //itemId = $(container).data('itemid');

	//generate template
	var box = templates.boxAttributes;

	//render template
	var html = box({
        //ReferredId: itemId,
		parsedAttributesValues: parsedAttributesValues,
		text: text
	}); 

	//useful logs
	// console.log('json :' + parsedAttributesValues );
	// console.log('html : ' + html);

	//append to element
	$(attachTo).append(html);
		
};

emitter.on('saveVariant', saveVariant);