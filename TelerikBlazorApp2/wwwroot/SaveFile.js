////function FileSaveAs(filename, fileContent) {
////    var link = document.createElement('a');
////    link.download = filename;
////    link.href = "data:text/plain;charset=utf-8," + encodeURIComponent(fileContent)
////    document.body.appendChild(link);
////    link.click();
////    document.body.removeChild(link);
////}

////function FileSaveAs2(filename, url) {
////    var link = document.createElement('a');
////    link.download = filename;
////    link.href = url;
////    document.body.appendChild(link);
////    link.click();
////    document.body.removeChild(link);
////}

////function downloadFromUrl(options: { url: string, fileName?: string }): void {
////    const anchorElement = document.createElement('a');
////    anchorElement.href = options.url;
////    anchorElement.download = options.fileName ?? '';
////    anchorElement.click();
////    anchorElement.remove();
////}

////window.downloadInspectionPicture = function (p_strServerFilePath) {
////    var link = document.createElement('a');
////    link.href = this.encodeURIComponent(p_strServerFilePath);
////    document.body.appendChild(link);
////    link.click();
////    document.body.removeChild(link);
////}

    (function () {

        window.saveFile = function (bytesBase64, mimeType, fileName) {
            var fileUrl = "data:" + mimeType + ";base64," + bytesBase64;
            fetch(fileUrl)
                .then(response => response.blob())
                .then(blob => {
                    var link = window.document.createElement("a");
                    link.href = window.URL.createObjectURL(blob, { type: mimeType });
                    link.download = fileName;
                    document.body.appendChild(link);
                    link.click();
                    document.body.removeChild(link);
                });
        }

    })();
