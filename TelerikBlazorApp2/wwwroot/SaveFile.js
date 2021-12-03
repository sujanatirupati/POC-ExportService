(function () {
    window.saveFile = async function (bytesBase64, mimeType, fileName) {
        var response = await fetch(`data:${mimeType};base64,${bytesBase64}`);
        var blob = await response.blob();
        var link = document.createElement("a");
        link.href = URL.createObjectURL(blob, { type: mimeType });
        link.download = fileName;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }
})();
