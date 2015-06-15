var $ = require('jquery');
var emitter = require('../modules/emitter');

var variantsButton = function() {
    isPopulated = $('#variantsForm').find('.form-variant').length;
    console.log(isPopulated);
    if (isPopulated == 0) {
        $('#bulkActionButton').addClass('disabled');
        $('#saveAll').addClass('disabled');
        $('#deleteAll').addClass('disabled');
    } else {
        $('#bulkActionButton').removeClass('disabled');
        $('#saveAll').removeClass('disabled');
        $('#deleteAll').removeClass('disabled');
    }
}

emitter.on('variantsButton', variantsButton);

////export compileAttributes
//module.exports.buttonManager = buttonManager;