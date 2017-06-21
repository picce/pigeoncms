Type.registerNamespace("AQuest.PigeonCMS.Control.ImageUpload");

AQuest.PigeonCMS.Control.ImageUpload = {

    OnUploadError: function (sender, args) {
        alert("Si è verificato un errore");
    },

    OnUploadComplete: function (sender, args) {
        var $ctrlID = $("#" + sender.get_inputFile().id);
        var $container = $ctrlID.closest(".pgn-ajax-image-upload-container");
        var $thumbContainer = $container.find(".pgn-ajax-image-upload-thumb-container");
        console.log(window.location.href + "?action=ImageUploadGetPreview&uploadId=" + $container.data("uploader-id"));
        $thumbContainer.find(".pgn-ajax-image-upload-thumb").attr("src", window.location.href + "?action=ImageUploadGetPreview&uploadId=" + $container.data("uploader-id"));
    }
};
