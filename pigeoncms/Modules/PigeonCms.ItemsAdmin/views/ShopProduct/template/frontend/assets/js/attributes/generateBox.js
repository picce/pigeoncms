var $ = require('jquery');
var _ = require('lodash');
var templates = require('../modules/templates');
var emitter = require('../modules/emitter');
var container;

var generateBox = function(parsedAttributesValues, text, attachTo) {

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

emitter.on('generateBox', generateBox);


var generateSpan = function(value, text, attachTo) {

    //set global container
    container = attachTo;
    //itemId = $(container).data('itemid');

    //generate template
    var span = templates.spanAttributes;

    //render template
    var html = span({
        //ReferredId: itemId,
        value: value,
        text: text
    });

    // //useful logs
    // console.log('value :' + value );
    // console.log('html : ' + html);

    //append to element
    $(attachTo).append(html);

};

emitter.on('generateSpan', generateSpan);

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

//trigger remove custom all click !
$(document).on('click', '.removeCustom', function (e) {

    debugger;

    //prevent postback
    e.preventDefault();

    //store the current panel
    var itemId = $('#attributesForm').data('itemid'),
        $self = $(this),
        $panel = $self.parents('.panel'),
        id = $panel.data('customid');

    RemoveCustom($.proxy(removeCustomSuccess, $panel), removeCustomFailed, parseInt(id), parseInt(itemId));

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
            valueId = checkval;

        if (self.is(':checked')) {
            jsonArr.push({
                ItemId: 0,
                AttributeId: parseInt(attributeId),
                AttributeValueId: parseInt(valueId),
                Referred: parseInt(itemId)
            });
        }
    });

    $('#attributesForm').find('*[data-customid]').each(function(e){
        var self = $(this),
            attributeId = self.data('customid'),
            valueId = 0;
            jsonArr.push({
                ItemId: 0,
                AttributeId: parseInt(attributeId),
                AttributeValueId: parseInt(valueId),
                Referred: parseInt(itemId)
            });
    });

    //Ajax call on save
    SaveAttributeValues(saveValuesSuccess, saveValueValuesFailure, JSON.stringify(jsonArr), parseInt(itemId));

    //console.log(JSON.stringify(jsonArr));
});


function saveValuesSuccess(result) {

    var parsedResult = $.parseJSON(result);
    var res = '';

    itemId = $('#attributesForm').data('itemid');

    if (!parsedResult.success) {
        res = '<div class="alert alert-danger alert-dismissable">' +
                    '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>' +
                     parsedResult.message + '<br></div>';
    } else {
        res = '<div class="alert alert-success alert-dismissable">' +
                    '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>' +
                    parsedResult.message + '</div>';
    }

    CompileAttributes(refreshSuccess, refreshFailure, parseInt(itemId));

    $('#spanResult').css('display', 'block').append(res);

    $('#variantsBoxes').removeClass('init').empty();

}

function saveValueValuesFailure(result) {
    console.log(result);
}

function refreshSuccess(result) {
    // console.log(result);

    // var values = result,
    //     parse = $.parseJSON(values);

    $('#attributesForm').find('.panel').fadeOut(800).delay(800).remove();
    // for(var key in parse) {
    //     var text = parse[key][0];
    //     //parse = parse.splice(0, 1);
    //     var parsedAttributesValues = parse[key].splice(1, parse[key].length);
    //     //debugger;
    //     var box = templates.boxAttributes;
    //     var html = box({
    //         parsedAttributesValues: parsedAttributesValues,
    //         text: text
    //     });

    var values = result,
        parse = $.parseJSON(values),
        boxes = parse.boxes,
        spans = parse.spans;

   //     debugger;

    for(var key in boxes) {
        var text = boxes[key][0];
        var parsedAttributesValues = boxes[key].splice(1, boxes[key].length);
        var box = templates.boxAttributes;
        var html = box({
            parsedAttributesValues: parsedAttributesValues,
            text: text
        });
        $('#attributesForm').append(html);
    }

    for(var key in spans) {
        var text = spans[key][0];
        var value = spans[key][1];
        var span = templates.spanAttributes;
        var html = span({
            value: value,
            text: text
        });
        $('#attributesForm').append(html);
    }

}

function refreshFailure(result) {
    console.log(result);
}

function removeCustomSuccess(result) {

    debugger;

    var parsedResult = $.parseJSON(result);
    var res = '';

    itemId = $('#attributesForm').data('itemid');

    if (!parsedResult.success) {
        res = '<div class="alert alert-danger alert-dismissable">' +
                    '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>' +
                     parsedResult.message + '<br></div>';
    } else {
        res = '<div class="alert alert-success alert-dismissable">' +
                    '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>' +
                    parsedResult.message + '</div>';

        $(this).fadeOut(800).delay(800).remove();
    }

    $('#spanResult').css('display', 'block').append(res);
}

function removeCustomFailed(result) {

}

