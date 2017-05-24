Type.registerNamespace("AQuest.PigeonCMS.Control.ImageUploadModern");

AQuest.PigeonCMS.Control.ImageUploadModern = function () {

    this.sendFileToServer = function (formData, status, $container) {
        var me = this;
        var uploadURL = "/Controls/ImageUpload/ImageUploadModernHandler.ashx";
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
                    var width = -1;
                    var height = -1;
                    var bgSize = $container.find(".box-preview").css("background-size");
                    var re = /([0-9]+)px ([0-9]+)px/ig;
                    var bgSizes = re.exec($.trim(bgSize));
                    if (bgSizes)
                    {
                        width = bgSizes[1];
                        height = bgSizes[2];
                    }
                    var previewUrl = uploadURL + "?action=preview&parameters=" + formData.get("parameters") + "&ts=" + new Date().getTime();
                    $container.find(".box-actions__label--preview").data("preview-url", previewUrl);
                    $container.find(".box-preview").css("background-image", "url('" + previewUrl  + "&width=" + width + "&height=" + height + "')");
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
            if (acceptTypes.indexOf(file.type) < 0) {
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
            $container.find(".icon-image").removeClass('icon-image--dragenter');
            $container.find(".icon-image--hover").removeClass('icon-image--hover--dragenter');
        }
        else {
            $container.removeClass('box-insert--dragenter');
            $container.find(".drop-label").addClass('drop-label--dragenter');
            $container.find(".icon-image").addClass('icon-image--dragenter');
            $container.find(".icon-image--hover").addClass('icon-image--hover--dragenter');
        }
    };

    this.bindUploadFiles = function () {
        var me = this,
            containerPrefix = '.pgn-imageUploadSingle';

        // dragenter event
        $(document).off('dragenter', containerPrefix + ' .dragandrophandler');
        $(document).on('dragenter', containerPrefix + ' .dragandrophandler', function (e) {
            e.stopPropagation();
            e.preventDefault();
            var $container = $(this).closest(".box-insert");
            me.handleDropZoneElement($container, true);
        });

        // dragover event
        $(document).off('dragover', containerPrefix + ' dragandrophandler');
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
        $(document).off('change', containerPrefix + ' .pgn-imageupload-input');
        $(document).on('change', containerPrefix + ' .pgn-imageupload-input', function (e) {
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
            $(this).closest(".box-container").find('.pgn-imageupload-input').trigger('click');
        });

        // preview
        $(document).off("click", containerPrefix + ' .box-actions__label--preview');
        $(document).on("click", containerPrefix + ' .box-actions__label--preview', function (e) {
            var $this = $(e.currentTarget).closest(".box-actions__label--preview");
            if ($this.data("preview-url") == "")
                return;

            // TODO: use fancybox
            window.open($this.data("preview-url") + "&width=-1&height=-1");
            return;
        });
    };
}

$(document).ready(function () {
    ImageUploadModern = new AQuest.PigeonCMS.Control.ImageUploadModern();
    ImageUploadModern.bindUploadFiles();
});