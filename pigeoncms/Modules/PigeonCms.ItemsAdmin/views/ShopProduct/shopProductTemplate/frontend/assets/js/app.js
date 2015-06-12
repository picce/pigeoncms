// require for attributes
var attributes = require('./attributes/compileAttributes');
var loadAttributes = require('./attributes/loadAttributes');
require('./attributes/generateBox');

//require for variants
var variants = require('./variants/compileAttributes.js');
require('./variants/compileValues');
require('./variants/generateInputs');

var related = require('./related/autocompile.js');

require('./modules/variantsButton');

$(document).on('show.bs.tab', 'a[data-toggle="tab"]', function (e) {

	if($(this).text() == 'Attributes' && !$('#attributesForm').hasClass('init')) {
		var containerAttributes = document.getElementById('attributesForm');
		attributes.compileAttributes(containerAttributes);
		loadAttributes.loadAttributes(containerAttributes);
		$(containerAttributes).addClass('init');
	} else if ($(this).text() == 'Related' && !$('#relatedForm').hasClass('init')) {
	    console.log('enter');
	    var containerRelated = document.getElementById('relatedForm');
	    related.autocompile(containerRelated);
	    $(containerRelated).addClass('init');
	} else if ($(this).text() == 'Variants' && !$('#variantsForm').hasClass('init')) {
	    var containerVariants = document.getElementById('variantsForm');
	    variants.compileAttributes(containerVariants);
	    $(containerVariants).addClass('init');
	}

});



