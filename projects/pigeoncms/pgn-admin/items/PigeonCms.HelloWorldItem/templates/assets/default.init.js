Type.registerNamespace("PigeonCms.ItemTemplates.PigeonCms_HelloWorld_Default");


//js resource for default item template
PigeonCms.ItemTemplates.PigeonCms_HelloWorld_Default = function () {

    this.init = function () {

        //add attr
        $(".label-CustomProp1_TextSimple").attr("title", "sample title here");

        $(".btnSave").click(function () {
            alert("btnSave click");
        });

        $(".btnCancel").click(function () {
            alert("btnCancel click");
        });
    };

}

$(document).ready(function () {

    new PigeonCms.ItemTemplates.PigeonCms_HelloWorld_Default().init();

});