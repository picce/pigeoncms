var $ = require('jquery');
var _ = require('lodash');
var emitter = require('../modules/emitter');
var templates = require('../modules/templates');
var validator = require('../modules/formValidator');

var generateInputs = function(ids, values, product) {

	var form = templates.inputVariants;

	var compiled = form({
		values: values,
		ids: ids,
		Product: product
	});

	$('#variantsForm').append(compiled);

	emitter.emit('variantsButton');

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

	var formArray = [];

	_.each($boxes.find('select'), function (elem){
		var _self = elem,
			value = _self.options[_self.selectedIndex].value;

		defaults += value + ',';

	});

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
		var dimL = ($form.find('#DimL' + tail).val().length > 0) ? $form.find('#DimL' + tail).val() : 0;
		var dimW = ($form.find('#DimW' + tail).val().length > 0) ? $form.find('#DimW' + tail).val() : 0;
		var dimH = ($form.find('#DimH' + tail).val().length > 0) ? $form.find('#DimH' + tail).val() : 0;
		formobj['Dimensions'] = dimL + "," + dimW + "," + dimH;

		formArray.push(formobj);
		SaveVariant($.proxy(saveVariantSuccess, $this), saveVariantFailed, parseInt(itemId), attributesValuesId, defaults.substring(0, defaults.length - 1), JSON.stringify(formArray), parseInt(variantId));
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

	DeleteVariant($.proxy(deleteVariantSuccess, this), deleteVariantFailed, parseInt(itemId), attributesValuesId, parseInt(variantId));

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

		window.jQuery('<a href="/admin/images-upload.aspx?type=items&amp;id=' + itemId + '"></a>').fancybox({
		    'width': '80%',
		    'height': '80%',
		    'type': 'iframe',
		    'hideOnContentClick': false,
		    onClosed: function () { 
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

$(document).on('change', '#bulkActionSelect', function(e){
	e.preventDefault();

	var $modal = $('#myModal'),
		$select = $('#bulkActionSelect'),
		valueSelect = $select.val(),
		textSelect = $select.find("option:selected").text();

	$modal.empty();

	var $this = $(this),
		bulktemplate = templates.bulkActions;

	var compiled = bulktemplate({
		selectText: textSelect,
		selectValue: valueSelect
	});

	$modal.append(compiled);

});

$(document).on('click', '#bulkActionButton', function(e){
	e.preventDefault();
});

$(document).on('click', '#setBulk', function(e){
	e.preventDefault();
	var $this = $(this),
		value = document.getElementById('bulkValue').value,
		id = $this.data('id'),
		$form = $('#variantsForm');

	$(":input[id^='"+ id + "']").val(value);

	$('#closeBulk').click();

});

$(document).on('click', '#saveAll', function(e){
	e.preventDefault();
	var $form = $('#variantsForm'),
		$saveButton = $form.find('.saveVariant').eq(0);

	 $form.find('.saveVariant').each(function(){
	 	$(this).click();
	 });

	 $saveButton.click();

});

$(document).on('click', '#deleteAll', function(e){
	e.preventDefault();
	var $form = $('#variantsForm');

	$form.find('.deleteVariant').each(function(){
	    var $this = $(this),
            isSaved = $this.data('variantid');
	    if(isSaved > 0) {
	        $this.click();
	    }
        
	});

});


function saveVariantSuccess(result) {

    var $buttonClicked = $(this),
        $form = $buttonClicked.parents('.form-variant'),
        $boxSave = $form.parent(),
        $buttonDelete = $form.find('.deleteVariant'),
        $buttonGallery = $form.find('.uploadImage');

    $buttonClicked.data('variantid', result);
	$buttonDelete.data('variantid', result);
	$buttonGallery.data('variantid', result);
	$boxSave.find('.panel').removeClass('panel-default').addClass('panel-success');
}

function saveVariantFailed(result) {
	console.log(result);
}

function deleteVariantSuccess(result) {
    var $buttonDelete = $(this),
        $form = $buttonDelete.parents('.form-variant'),
        $boxDelete = $form.parent();

	if(result == "true") {
		$boxDelete.fadeOut(800);
	} else {
		alert('can\'t delete unassigned variant');
	}

	_.delay(800, emitter.emit('variantsButton'));
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

