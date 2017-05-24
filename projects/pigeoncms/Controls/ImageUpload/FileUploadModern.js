Type.registerNamespace("AQuest.PigeonCMS.Control.FileUploadModern");

AQuest.PigeonCMS.Control.FileUploadModern = function () {

    this.sendFileToServer = function (formData, status, $container) {
        var me = this;
        var uploadURL = "/Controls/ImageUpload/FileUploadModernHandler.ashx";
        var extraData = {};
        var jqXHR = $.ajax({

            xhr: function () {
                var xhrobj = $.ajaxSettings.xhr();
                if (xhrobj.upload) {
                    xhrobj.upload.addEventListener('progress', function (event) {
                        var percent = 0;
                        var position = event.loaded || event.position;
                        var total = event.total;
                        if (event.lengthComputable) {
                            percent = Math.ceil(position / total * 100);
                            status.setProgress(percent);
                        }
                    }, false);
                }
                return xhrobj;
            },
            url: uploadURL,
            type: "POST",
            dataType: 'json',
            contentType: false,
            processData: false,
            cache: false,
            data: formData,
            complete: function (msg) {
                
                var data = msg.responseJSON;
                var $boxInsert = $container.find(".box-insert");

                if (data.status) {
                    status.setProgress(100);                    
                    var previewUrl = uploadURL + "?action=preview&parameters=" + formData.get("parameters") + "&ts=" + new Date().getTime();
                    $container.find(".box-preview").find(".preview-link").text(data.fileName);
                    $container.find(".box-preview").find(".preview-link").attr("href", previewUrl);
                    $container.find(".box-preview").css("opacity", "1");
                }
                else {
                    openMessage(data.message);
                }

                me.handleDropZoneElement($boxInsert, true);
            },
            error: function (e) {
                var $boxInsert = $container.find(".box-insert");
                me.handleDropZoneElement($boxInsert, false);
                openMessage("An error occurred during upload");
            }
        });

        status.setAbort(jqXHR);
    };

    createStatusbar = function ($container) {
        var me = this;

        this.progressBar = $container.find(".progress-bar");

        this.setProgress = function (progress) {
            me.progressBar.css('opacity', '1');
            var progressBarWidth = progress * me.progressBar.width() / 100;
            if (progress <= 0)
                me.progressBar.find('div').css("width", 0);
            me.progressBar.find('div').animate({ width: progressBarWidth }, 100);

            if (progress >= 100) {
                setTimeout(function () {
                    me.progressBar.css('opacity', '0');
                }, 1000);
            }
        }

        this.setAbort = function (jqxhr) {

        }
    };

    this.handleFileUpload = function (files, obj, maxSize) {
        if (files && files.length > 0) {
            var fd = new FormData();
            fd.append('file', files[0]);
            fd.append('parameters', $(obj).closest(".box-container").find("input[type=hidden]").val());
            this.startUpload(fd, obj, maxSize);
        }
    };

    this.startUpload = function (formData, obj, maxSize) {
        var file = formData.get("file");

        if (file) {
            if (file.size > (maxSize * 1024)) {
                openMessage($(obj).closest(".box-container").data("lbl-filetoobig"));
                return false;
            }

            var acceptTypes = $(obj).closest(".box-container").find("input[type=file]").attr("accept");
            acceptTypes = acceptTypes.split(",");
            if (acceptTypes.indexOf("*") < 0 && acceptTypes.indexOf(file.type) < 0) {
                openMessage($(obj).closest(".box-container").data("lbl-filenotallowed"));
                return false;
            }

            var status = new createStatusbar($(obj).closest(".box-container"));
            status.setProgress(0);
            this.sendFileToServer(formData, status, $(obj).closest(".box-container"));
        }
        else {
            return false;
        }
    };

    this.log = function (message) {
        if (console && console.log)
            console.log(message);
    };

    this.handleDropZoneElement = function ($container, visible) {
        if (visible) {
            $container.removeClass('box-insert--dragenter');
            $container.find(".drop-label").removeClass('drop-label--dragenter');
        }
        else {
            $container.removeClass('box-insert--dragenter');
            $container.find(".drop-label").addClass('drop-label--dragenter');
        }
    };

    this.bindUploadFiles = function () {
        var me = this,
            containerPrefix = '.pgn-fileUploadSingle';

        // dragenter event
        $(document).off('dragenter', containerPrefix + ' .dragandrophandler');
        $(document).on('dragenter', containerPrefix + ' dragandrophandler', function (e) {
            e.stopPropagation();
            e.preventDefault();
            var $container = $(this).closest(".box-insert");
            me.handleDropZoneElement($container, true);
        });

        // dragover event
        $(document).off('dragover', containerPrefix + ' .dragandrophandler');
        $(document).on('dragover', containerPrefix + ' .dragandrophandler', function (e) {
            e.stopPropagation();
            e.preventDefault();
        });

        // drop event
        $(document).off('drop', containerPrefix + ' .dragandrophandler');
        $(document).on('drop', containerPrefix + ' .dragandrophandler', function (e) {
            e.preventDefault();
            var files = e.originalEvent.dataTransfer.files;
            var maxSize = $(e.currentTarget).closest('.dragandrophandler').data("max-file-size");
            me.handleFileUpload(files, $(e.currentTarget), maxSize);
        });

        // select file with fileupload event
        $(document).off('change', containerPrefix + ' .pgn-fileupload-input');
        $(document).on('change', containerPrefix + ' .pgn-fileupload-input', function (e) {
            var fileuploaded = $(this).val();
            var maxSize = $(this).data("max-file-size");

            if (fileuploaded) {
                var file = this.files[0];
                var fd = new FormData();
                var $container = $(this).closest(".box-container");

                fd.append('file', file);
                fd.append('parameters', $container.find("input[type=hidden]").val());

                me.handleDropZoneElement($container.find(".box-insert"), false);
                me.startUpload(fd, $(this), maxSize);
            }
        });

        // insert new file event
        $(document).off("click", containerPrefix + ' .box-insert');
        $(document).on("click", containerPrefix + ' .box-insert', function () {
            $(this).closest(".box-container").find('.pgn-fileupload-input').trigger('click');
        });
    };
}

$(document).ready(function () {
    FileUploadModern = new AQuest.PigeonCMS.Control.FileUploadModern();
    FileUploadModern.bindUploadFiles();
});