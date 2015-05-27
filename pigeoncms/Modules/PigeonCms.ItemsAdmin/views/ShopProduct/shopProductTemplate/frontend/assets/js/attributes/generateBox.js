var $ = require('jquery');
var _ = require('lodash');
var templates = require('../modules/templates');
var emitter = require('../modules/emitter');
var container;

var generateBox = function(parsedAttributesValues, text, attachTo) {
	
    //set global container
    container = attachTo;

	//generate template
	var box = templates.boxAttributes;

	//render template
	var html = box({
		parsedAttributesValues: parsedAttributesValues,
		text: text
	}); 

	//useful logs
	// console.log('json :' + parsedAttributesValues );
	// console.log('html : ' + html);

	//append to element
	$(attachTo).append(html);
		
};

emitter.on('generateBox', generateBox);

//trigger select all click !
$(document).on('click', '.select', function (e) {
	//prevent postback
    e.preventDefault();

    //store the current panel
    var $self = $(this),
		$panel = $self.parents('.panel').eq(0);

    $panel.find('input').each(function () {
    	this.checked = true;
    });

});

//trigger unselect all click !
$(document).on('click', '.unselect', function (e) {
	//prevent postback
    e.preventDefault();

    //store the current panel
    var $self = $(this),
		$panel = $self.parents('.panel').eq(0);

    $panel.find('input').each(function () {
    	this.checked = false;
    });

});

//trigger save click !
$(document).on('click', '#updateValues', function (e) {
	//prevent postback
    e.preventDefault();

    //get itemId (if any)
    var jsonArr = [],
        itemId = $('#attributesForm').data('itemid');

    //find all input in container
    $('#attributesForm').find('input').each(function (e) {
        var self = $(this),
            attributeId = self.parents('.checkbox').data('attributeid'),
            checkval = self.val(),
            itemIdValueId = checkval.split(',');
            
        if (self.is(':checked')) {
            jsonArr.push({
                ItemId: parseInt(itemIdValueId[1]),
                AttributeId: parseInt(attributeId),
                AttributeValueId: parseInt(itemIdValueId[0]),
                Referred: parseInt(itemId)
            });
        }
    });
    //Ajax call on save
    SaveAttributeValues(saveValuesSuccess, saveValueValuesFailure, JSON.stringify(jsonArr), parseInt(itemId));
    console.log(JSON.stringify(jsonArr));
});


function saveValuesSuccess(result) {

    var parsedResult = $.parseJSON(result);
    var res = '';
    
    if (!parsedResult.success) {
        res = '<div class="alert alert-danger alert-dismissable">' +
                    '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>' +
                     parsedResult.message + '<br></div>';
    } else {
        res = '<div class="alert alert-success alert-dismissable">' +
                    '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>' + 
                    parsedResult.message + '</div>';
    }

    $('#spanResult').css('display', 'block').append(res);
    
}

function saveValueValuesFailure(result) {
    console.log(result);
}

