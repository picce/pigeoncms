var $ = require('jquery');
var _ = require('lodash');
require('fancybox')($);
var emitter = require('../modules/emitter');
var templates = require('../modules/templates');
var validator = require('../modules/formValidator');

window.$buttonSave;
window.$buttonDelete;
window.$boxDelete;
window.$boxSave;

var generateInputs = function(ids, values, product) {

	var form = templates.inputVariants;

	var compiled = form({
		values: values,
		ids: ids,
		Product: product
	});

	$('#variantsForm').append(compiled);
};

emitter.on('generateInputs', generateInputs);

$(document).on('click', '.saveVariant', function(e){

	e.preventDefault();
	var $this = $(this),
		itemId = $('#variantsForm').data('itemid'),
		attributesValuesId = $this.data('saveids').substring(0, $this.data('saveids').length - 1),
		eachIds = attributesValuesId.split(','),
		$form = $this.parents('.form-variant'),
		variantId = $this.data('variantid'),
		$boxes = $('#variantsBoxes');
		defaults = "";

	window.$buttonSave = $this;
	window.$buttonDelete = $form.find('.deleteVariant');
	window.$boxSave = $form.parent();

	//debugger;

	var formArray = [];

	_.each($boxes.find('select'), function (elem){
		var _self = elem,
			value = _self.options[_self.selectedIndex].value;

		defaults += value + ',';

	});

	//debugger;

	var isValidForm = validator.validate($form, "has-error");

	if(isValidForm) {

		var tail = '';

		_.each(eachIds, function(id) {
			tail += '_' + id;
		});
		
		var formobj = {};

		formobj['ProductCode'] = $form.find('#ProductCode' + tail).val();
		formobj['Availability'] = parseInt($form.find('#Availability' + tail).val());
		formobj['RegularPrice'] = parseFloat($form.find('#RegularPrice' + tail).val());
		formobj['SalePrice'] = parseFloat($form.find('#SalePrice' + tail).val());
		formobj['Weight'] = parseFloat($form.find('#Weight' + tail).val());
		formobj['Dimensions'] = $form.find('#DimL' + tail).val() + "," + $form.find('#DimW' + tail).val() + "," + $form.find('#DimH' + tail).val();

		formArray.push(formobj);
		SaveVariant(saveVariantSuccess, saveVariantFailed, parseInt(itemId), attributesValuesId, defaults.substring(0, defaults.length - 1), JSON.stringify(formArray), parseInt(variantId));
	}

});

$(document).on('click', '.deleteVariant', function(e){

	e.preventDefault();
	var $this = $(this),
		itemId = $('#variantsForm').data('itemid'),
		attributesValuesId = $this.data('deleteids').substring(0, $this.data('deleteids').length - 1),
		eachIds = attributesValuesId.split(','),
		$form = $this.parents('.form-variant'),
		variantId = $this.data('variantid');

	window.$boxDelete = $form.parent();

	DeleteVariant(deleteVariantSuccess, deleteVariantFailed, parseInt(itemId), attributesValuesId, parseInt(variantId));

});

$(document).on('click', '.uploadImage', function(e){

	e.preventDefault();

	var $this = $(this),
		itemId = $this.data('variantid'),
		$form = $this.parents('.form-variant'),
		attributesValuesId = $this.data('itemattributes').substring(0, $this.data('itemattributes').length - 1),
		eachIds = attributesValuesId.split(',');

	if(itemId > 0) {

		var tail = '';

		_.each(eachIds, function(id) {
			tail += '_' + id;
		});

		$('<a href="/admin/images-upload.aspx?type=items&amp;id=' + itemId + '"></a>').fancybox({
		    'width': '80%',
		    'height': '80%',
		    'type': 'iframe',
		    'hideOnContentClick': false,
		    beforeClose: function () { 
		    	console.log('close');
		    	window.$boxSave = $form.find('#gallery' + tail);
		    	RefreshGalleryById(refreshGallerySuccess, refreshGalleryFailed, parseInt(itemId));
		    }
		}).click();

	} else {

		alert('save variant before upload gallery!');
		return;

	}

});

function saveVariantSuccess(result) {
	console.log(result);
	//debugger;
	window.$buttonSave.data('variantid', result);
	window.$buttonDelete.data('variantid', result);
	window.$boxSave.removeClass('panel-default').addClass('panel-green');
}

function saveVariantFailed(result) {
	console.log(result);
}

function deleteVariantSuccess(result) {
	// console.log(result);
	// var success = Boolean(result);
	// console.log(success);
	if(result == "true") {
		window.$boxDelete.fadeOut(800);
	} else {
		alert('can\'t delete unassigned variant');
	}
	
}

function deleteVariantFailed(result) {

}

function refreshGallerySuccess(result) {
	var images = $.parseJSON(result);
	//debugger;

	var form = 	_.template('<% _.each(Images, function (pics) { %> <img src="<%= pics.FileUrl %>?width=120&height=120&bgcolor=Ffffff&scale=both" /> <% }); %>');

		var compiled = form({
			Images: images
		});


	window.$boxSave.empty().append(compiled);
}

function refreshGalleryFailed(result) {
	console.log(result);
}