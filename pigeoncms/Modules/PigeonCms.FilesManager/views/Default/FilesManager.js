/**
* @version		FilesManager.js 2011-04-15 p-ice
* @package		PigeonCms
*/

var ImageManager = {
    initialize: function () {
        this.fields = new Object();
        this.fields.url = "";
        this.fields.alt = "";
    },

    onok: function () {
        var extra = '';
        var tag = '';
        var extension = '';
        if (this.fields.url != '') {
            extension = this.fields.url.split('.').pop().toLowerCase();
            switch (extension) {
            case "jpg":
            case "jpeg":
            case "gif":
            case "png":
                if (this.fields.alt != '') {
                    extra = extra + 'alt="' + this.fields.alt + '" ';
                } else {
                    extra = extra + 'alt="" ';
                }
                tag = "<img src=\"" + this.fields.url + "\" " + extra + "/>";
                break;
            default:
                tag = "<a href=\"" + this.fields.url + "\">"+ this.fields.alt +"</a>";
                break;
            }

        }

        window.parent.insertEditorText(tag);
        return false;
    },

    populateFields: function (file, title) {
        this.fields.url = file;
        this.fields.alt = title;
    },

    parseQuery: function (query) {
        var params = new Object();
        if (!query) {
            return params;
        }
        var pairs = query.split(/[;&]/);
        for (var i = 0; i < pairs.length; i++) {
            var KeyVal = pairs[i].split('=');
            if (!KeyVal || KeyVal.length != 2) {
                continue;
            }
            var key = unescape(KeyVal[0]);
            var val = unescape(KeyVal[1]).replace(/\+ /g, ' ');
            params[key] = val;
        }
        return params;
    }
};

$(document).ready(function () {
    ImageManager.initialize();
});