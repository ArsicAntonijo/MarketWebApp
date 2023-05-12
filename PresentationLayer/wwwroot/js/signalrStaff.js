// create connection
var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7122/hubs/itemHub").build();

//connect methods  that hub invokes
connection.on("updateItemTable", (value) => {
    var Span = document.getElementById("counter");
    Span.innerHTML = value;
});

// invoke hub methods
function newWindowLoaded() {
    connection.send("NewItemCreated");
}

//start connection

function fullfiled() {
    console.log("success");
    //newWindowLoaded();
}
function issue() {
    console.log("error");
}
connection.start().then(fullfiled, issue);

function doIt() {
    console.log("button clicked");
    newWindowLoaded();
}


// ********* for home page **************
////connect methods  that hub invokes
//connection.on("updateItemTable", (value) => {
//    var Span = document.getElementById("counter");
//    Span.innerHTML = value;
//});

//// invoke hub methods
//function newWindowLoaded() {
//    connection.send("NewItemCreated");
//}

////start connection

//function fullfiled() {
//    console.log("success");
//    //newWindowLoaded();
//}
//function issue() {
//    console.log("error");
//}
//connection.start().then(fullfiled, issue);

//function doIt() {
//    console.log("button clicked");
//    newWindowLoaded();
//}