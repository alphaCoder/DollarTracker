app.factory('imageUpload', function () {

    var imageUploadService = {};

    function readImageFile(e, onImageSelected) {
        if (e.target.files.length != 1) return; 
        var reader = new FileReader();
        var file = e.target.files[0];
        reader.onload = function (evt) {
            var img = new Image();
            img.onload = function () {
                onImageSelected(img, file.name);
            }
            img.src = evt.target.result;
        }
        reader.readAsDataURL(file);
    }

    imageUploadService.initFileButton = function (jqueryObj, onImageSelected) {
        if (document.getElementById('__hiddenFileInput')) {
            document.body.removeChild(document.getElementById('__hiddenFileInput'));
        }
        var hiddenFileInput = document.createElement('INPUT');
        hiddenFileInput.type = 'file';
        hiddenFileInput.id = '__hiddenFileInput';
        hiddenFileInput.style.display = 'none';
        document.body.appendChild(hiddenFileInput);
        $('#__hiddenFileInput').on('change', function (e) {
            readImageFile(e, onImageSelected);
            $('#__hiddenFileInput').val(''); 
        });
        jqueryObj.on('click', function () {
            $('#__hiddenFileInput').trigger('click');
        });

    }

    imageUploadService.scale = function (img, maxWidth, maxHeight, func) {
        var ratioX = maxWidth / img.width;
        var ratioY = maxHeight / img.height;
        var ratio = Math.min(ratioX, ratioY);
        if (ratio > 1) {
            
            ratio = 1;
        }
        var w = (img.width * ratio);
        var h = (img.height * ratio);
        var canvas = document.createElement('canvas');
        canvas.width = w;
        canvas.height = h;
        var ctx = canvas.getContext("2d");
        ctx.drawImage(img, 0, 0, w, h);
        var outputImg = new Image();
        outputImg.onload = function () {
            func(outputImg);
        };
        outputImg.src = canvas.toDataURL();
    }

    return imageUploadService;

});