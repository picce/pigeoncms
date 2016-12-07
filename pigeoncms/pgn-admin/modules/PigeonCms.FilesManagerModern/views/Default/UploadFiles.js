
    function sendFileToServer(formData, status) {

    	log("sendFileToServer()");

    	var uploadURL = $('#hidCurrViewPath').val() + "UploadFile.ashx";
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

            	log("sendFileToServer():complete");

            	var data = msg.responseJSON;

            	if (data.status) {
            		status.setProgress(100);
            	}
            	else {
            		openMessage(data.message);

            		$(".box-insert").removeClass('box-insert--dragenter');
            		$(".drop-label").removeClass('drop-label--dragenter');
            		$(".icon-image--hover").removeClass('icon-image--hover--dragenter');
            	}
            },
			error: function (e) {

				log("sendFileToServer():error");

            	$(".box-insert").removeClass('box-insert--dragenter');
            	$(".drop-label").removeClass('drop-label--dragenter');
            	$(".icon-image--hover").removeClass('icon-image--hover--dragenter');

            	//openMessage("an error occurred during upload");
            }
        });

        status.setAbort(jqXHR);
    }

    var rowCount = 0;
    function createStatusbar(obj) {
        rowCount++;
        var row = "odd";
        if (rowCount % 2 == 0) row = "even";
        this.progressBar = $(".progress-bar");
        obj.after(this.statusbar);

        this.setProgress = function (progress) {

        	this.progressBar.css('opacity', '1');
            var progressBarWidth = progress * this.progressBar.width() / 100;
            this.progressBar.find('div').animate({ width: progressBarWidth }, 2000, function () {

				//in module code
            	ReloadGrid();

            });
            if (parseInt(progress) >= 100) {

            }
        }

        this.setAbort = function (jqxhr) {

        }
    }

    function handleFileUpload(files, obj) {

    	log("handleFileUpload()");
        for (var i = 0; i < files.length; i++) {
            var fd = new FormData();
            fd.append('file', files[i]);

            insertInformation(fd, obj);
        }
    }

    function insertInformation(formData, obj) {

    	log("insertInformation()");

		formData.append('moduleid',	  $('#hidMuduleId').val() );
		formData.append('foldername', $('#hidFolderName').val());

    	var status = new createStatusbar(obj);
    	sendFileToServer(formData, status);
    }

    function insertMedia(filename, fileurl, filetitle) {

    	var extra = "";
    	var tag = "";
    	var extension = "";
    	if (fileurl != "") {

    		extension =fileurl.split('.').pop().toLowerCase();
    		switch (extension) {
    			case "jpg":
    			case "jpeg":
    			case "gif":
    			case "png":
    				if (filetitle != "") {
    					extra = extra + 'alt="' + filetitle + '" ';
    				} else {
    					extra = extra + 'alt="" ';
    				}
    				tag = "<img src=\"" + fileurl + "\" " + extra + "/>";
    				break;
    			default:
    				tag = "<a href=\"" + fileurl + "\">" + filetitle + "</a>";
    				break;
    		}
    	}

    	window.parent.insertEditorText(tag);
    	return false;
    }

    function RenameFile_Success() {

    	log("RenameFile_Success");
    	window.location.reload();

    }

    function Generic_Success(result) {

    	koConfirm();
    }

    function NavigateFolder_Success(parentfolder) {

    	koConfirm();
    }

    function Generic_Failure(result) {

    	log("Generic_Failure:result=" + result);

    }

    $(document).ready(function () {

    	log("document.ready()");

    	bindUploadFiles();

    });
	

function doUploadFile() {

	log("doUploadFile()");

	var fileuploaded = $("#FileUpload1").val();

	if (fileuploaded) {

		var file = document.getElementById('FileUpload1').files[0];
		var fd = new FormData();
		fd.append('file', file);

		sendFileToServer(fd, status);
	}
}


function bindUploadFiles() {

	log("bindUploadFiles()");

	// dragenter event
	$('#dragandrophandler').off('dragenter');
	$('#dragandrophandler').on('dragenter', function (e) {
		e.stopPropagation();
		e.preventDefault();

		$(".box-insert").addClass('box-insert--dragenter');
		$(".drop-label").addClass('drop-label--dragenter');
		$(".icon-image--hover").addClass('icon-image--hover--dragenter');
	});

	// dragover event
	$('#dragandrophandler').off('dragover');
	$('#dragandrophandler').on('dragover', function (e) {
		e.stopPropagation();
		e.preventDefault();
	});

	// drop event
	$('#dragandrophandler').off('drop');
	$('#dragandrophandler').on('drop', function (e) {
		log("#dragandrophandler:drop");
		e.preventDefault();
		var files = e.originalEvent.dataTransfer.files;
		handleFileUpload(files, $("#dragandrophandler"));

	});

	// select file with fileupload event
	$('#FileUpload1').off('change');
	$('#FileUpload1').on('change', function (e) {

		log("#FileUpload1:change");
		var fileuploaded = $(this).val();

		if (fileuploaded) {

			var file = document.getElementById('FileUpload1').files[0];
			var fd = new FormData();
			fd.append('file', file);

			$(".box-insert").addClass('box-insert--dragenter');
			$(".drop-label").addClass('drop-label--dragenter');
			$(".icon-image--hover").addClass('icon-image--hover--dragenter');

			insertInformation(fd, $(this));
		}
	});

	// insert new file event
	$(".box-insert").off("click");
	$(".box-insert").on("click", function () {

		log(".box-insert:click");
		$(this).next().find('input[type=file]').trigger('click');

	});

	// rename file
	$(".js-txtname").off();
	$(".js-txtname").on("focus", function (e) {

		var self = $(this);
		self.addClass("image-name--editmode");
		return false;

	}).blur(function (e) {

		var self = $(this);
		self.removeClass("image-name--editmode");

	}).change(function (e) {

		var $this = $(this);
		var encPath = $("#hidPath").val();
		var sourceFileName = $this.data('filename');
		var destFileName = $this.val();

		if (sourceFileName !== destFileName) {
			log(".js-txtname:change>RenameFile(" + sourceFileName + ", " + destFileName + ")");
			RenameFile(RenameFile_Success, Generic_Failure, encPath, sourceFileName, destFileName);
		}
		return false;

	}).keydown(function (e) {

		//if (e.keyCode == 13) {
		//	e.preventDefault();
		//	$(this).trigger("change");
		//}

	});

	$(".js-selectfile").off("click");
	$(".js-selectfile").on("click", function (e) {

		var $this = $(this);
		var encPath = $("#hidPath").val();
		var filename = $this.data('filename');
		var fileurl = $this.data('fileurl');
		var filetitle = $this.data('filetitle');

		log(".js-selectfile:click(" + filename + ")");

		if (filename !== undefined) {

			insertMedia(filename, fileurl, filetitle);
			closeFancy();
		}

		return false;

	});


	// navigate folder event TODO
	$(".js-btn-navigatefolder").off("click");
	$(".js-btn-navigatefolder").on("click", function (e) {

		//var $this = $(this);
		//var encPath = $("#hidPath").val();
		//var foldername = $this.data('foldername');

		log(".js-btn-navigatefolder:click");

		$(this).next(".js-btn-navigatefolder-trigger").trigger("click");

		return false;

	});
}


function log(message) {
	var tolog = true;
	if (tolog)
		console.log(message);
}