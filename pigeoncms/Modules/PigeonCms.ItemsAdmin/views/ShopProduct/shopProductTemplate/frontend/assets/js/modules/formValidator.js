(function () {

    module.exports.name = 'validator';

    var _ = require('lodash');

    // --- --- --- --- 
    // module obj
    // --- --- --- --- 
    var validator = {};



    // --- --- --- --- 
    // START private vars and functions declaration
    // --- --- --- --- 

    var _stripTags = function (val) {
        return val.replace(/<\/?[^>]+(>|$)/g, '');
    };

    var _isValidEmail = function (email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    };

    // eventualmente sarebbe possibile fare un controllo sulla presenza di errorClass
    // se non è presente la si va a leggera dal modulo config
    var _validateField = function (field, errorClass) {

        var val = _stripTags(field.val());
        var validField = true;

        if (field.is("[required]") && _.isEmpty(val)) {
            validField = false;
            field.parent().addClass(errorClass);
        }
        if (field.is("[type='email']")) {
            if (!_isValidEmail(val)) {
                validField = false;
                field.parent().addClass(errorClass);
            }
        }

        if (field.is("[data-type='file']") && field.is("[data-required]") && _.isEmpty(val)) {

            console.log("|" + val + "|");

            validField = false;
            $("[data-file-name='" + field.attr("name") + "']").parent().addClass(errorClass);
        }

        if (validField) {
            field.parent().removeClass(errorClass);
        }

        return validField;
    };

    var _validate = function (formSelector, errorClass) {

        var valid = true,
            form = $(formSelector),
            input = form.find("input");

        _.each(input, function (elem) {
            var validField = _validateField($(elem), errorClass);
            valid = valid ? validField : valid;
        });

        return valid;
    };


    // --- --- --- --- 
    // END private vars and functions declaration
    // --- --- --- --- 



    // --- --- --- --- 
    // START exported functions
    // --- --- --- --- 

    validator.validateField = _validateField;
    validator.validate = _validate;

    // --- --- --- --- 
    // END exported functions
    // --- --- --- --- 



    // --- --- --- --- 
    // exports module obj
    // --- --- --- --- 
    module.exports = validator;

} ());