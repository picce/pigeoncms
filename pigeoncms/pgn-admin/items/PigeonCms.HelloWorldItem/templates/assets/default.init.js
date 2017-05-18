Type.registerNamespace("PigeonCms.ItemTemplates.PigeonCms_HelloWorld_Default");


//js resource for default item template
PigeonCms.ItemTemplates.PigeonCms_HelloWorld_Default = function () {

    this.init = function () {

        var pgn_helloworldItem_sample = 3;

    };

}

$(document).ready(function () {

    template = new PigeonCms.ItemTemplates.PigeonCms_HelloWorld_Default();
    template.init();

});