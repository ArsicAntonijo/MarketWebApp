var URL = "https://localhost:7122/api/Storage/";
const createStorageForm = document.querySelector('#createStorageForm');
const deleteCustomerForm = document.querySelector('#deleteCustomerForm');
const table = document.querySelector('#tableBody');

window.onload = function () {
    if (table != null) getReceipts();
    if (createStorageForm != null) createStorageForm.onsubmit = CreateStorage;
    if (deleteCustomerForm != null) deleteCustomerForm.onsubmit = DeleteCustomer;
    console.log("onload");
}

function getReceipts() {
    $.getJSON(URL, function (data) {
        console.log(data);
        processData(data);
        Itemlist = data;
    });
}

function processData(data) {

    table.innerHTML = "";
    for (var i = 0; i < data.length; i++) {

        table.innerHTML += "<tr>" +
            "<td>" + data[i].distributerName + "</td>" +
            "<td>" + data[i].distributerFirm + "</td>" +
            "<td>" + data[i].dateOfReceipt + "</td>" +
            "<td>" +
            "<a href='./Storage/Details?stringItems=" + data[i].stringItems + "'>Details</a>   " +
            "</td>" +
            "</tr>";
    }
}

function CreateStorage(e) {
    console.log("Submited");
    // stops convetional submit
    e.preventDefault();

    var body = {
        "distributerName": createStorageForm.elements["DistributerName"].value,
        "distributerFirm": createStorageForm.elements["DistributerFirm"].value,
        "dateOfReceipt": createStorageForm.elements["Date"].value,
        "stringItems": createStorageForm.elements["Items"].value
    };

    console.log(body);

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        crossDomain: true,
        url: URL,
        type: 'POST',
        data: JSON.stringify(body),
        success: function (response) {
            console.log(response);
        },
        error: function (e) {
            console.log(e);
        }
    });
}

function DeleteCustomer(e) {
    console.log("Deleting ...");
    // stops convetional submit
    e.preventDefault();

    var id = deleteCustomerForm.elements["CustomerIdToDelete"].value;

    fetch(URL + "/" + id, {
        method: "DELETE",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then((res) => { console.log(res); })
        .catch((error) => { confirm.log(error); });
}