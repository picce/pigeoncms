// require for attributes
var attributes = require('./attributes/compileAttributes');
require('./attributes/generateBox');

//require for variants
var variants = require('./variants/compileAttributes.js');
require('./variants/compileValues');
require('./variants/generateInputs');

//fake json, substitute with ajax call
//var json = '[{"Id":5,"ItemType":"","Name":"Colore","AttributeType":0,"AllowCustomValue":false,"MeasureUnit":""},{"Id":6,"ItemType":"","Name":"Materiale","AttributeType":0,"AllowCustomValue":true,"MeasureUnit":""}]';

$(document).on('show.bs.tab', 'a[data-toggle="tab"]', function (e) {
	if($(this).text() == 'Attributes' && !$('#attributesForm').hasClass('init')) {
		var containerAttributes = document.getElementById('attributesForm');
		attributes.compileAttributes(containerAttributes);
		$(containerAttributes).addClass('init');
	}
});

//fake json with stored data
//var storedValues = '[{"ItemId":0,"AttributeId":5,"AttributeValueId":27},{"ItemId":0,"AttributeId":5,"AttributeValueId":28},{"ItemId":0,"AttributeId":5,"AttributeValueId":27}]';

$(document).on('show.bs.tab', 'a[data-toggle="tab"]', function (e) {
	if($(this).text() == 'Variants' && !$('#variantsForm').hasClass('init')) {
		var containerVariants = document.getElementById('variantsForm');
		variants.compileAttributes(containerVariants);
		$(containerVariants).addClass('init');
	}
});
