var $ = require('jquery');

var bodyContent = function (content) {
	var $p = $('<p />');
	$p.text(content);
	$('body').append($p)
};


module.exports.bodyContent = bodyContent;