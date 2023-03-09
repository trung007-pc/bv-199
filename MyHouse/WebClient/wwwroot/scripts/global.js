function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}


function  printAsFile(elementId){
    
    var iframe = document.getElementById(elementId);
    var iframeWindow = iframe.contentWindow;
    iframeWindow.print();
}


function downloadURI(uri, name)
{
    var link = document.createElement("a");
    // If you don't know the name or want to use
    // the webserver default set name = ''
    link.setAttribute('download', name);
    link.href = uri;
    document.body.appendChild(link);
    link.click();
    link.remove();
}


// fetch("https://localhost:7093/StaticFiles/Document-Files/26-02-2023/20230226155548_20230222145519_Hieu-Minh-Le-Java-Full-stack-Engineer.pdf", {
//     method: 'GET',
//     headers: { accept: 'application/json' }
//
// }).then(resp => resp.blob())
//     .then(blob => {
//         const url = window.URL.createObjectURL(blob);
//         const a = document.createElement('a');
//         a.style.display = 'none';
//         a.href = url;
//         a.download = "name"; // the filename you want
//         document.body.appendChild(a);
//         a.click();
//         window.URL.revokeObjectURL(url);
//     })
//
// fetch("https://localhost:7093/StaticFiles/Document-Files/26-02-2023/20230226155548_20230222145519_Hieu-Minh-Le-Java-Full-stack-Engineer.pdf")
//     .then((response) => {console.log(response)})