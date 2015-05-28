var _ = require('lodash');

var templates = {
	
	boxAttributes : _.template(require('../templates/box_attributes.html')),
	inputVariants : _.template(require('../templates/variants_form.html')),
	optionValues : _.template('<% _.each(parsedValues, function (vals) { %><option value="<%= vals.Id %>"><%= vals.Value %></option><% }); %>'),
	variantsAttributes: _.template(require('../templates/variants_attributes.html')),
	attributesAttributes: _.template(require('../templates/attributes_attributes.html')),
	variantsValues: _.template(require('../templates/variants_values.html'))

};

module.exports = templates;