var Itemlist;
var URL = "https://localhost:7122/api/Sales";
const createItemForm = document.querySelector('#createItemForm');
const deleteItemForm = document.querySelector('#deleteItemForm');
const table = document.querySelector('#tableBody');

window.onload = function () {
    if (table != null) getCustomers();
    if (createItemForm != null) createItemForm.onsubmit = CreateItem;
    if (deleteItemForm != null) deleteItemForm.onsubmit = DeleteItem;
    console.log("onload");
}

function getCustomers() {
    $.getJSON(URL + "/Item", function (data) {
        console.log(data);
        processData(data);
        Itemlist = data;
    });
}

function processData(data) {

    table.innerHTML = "";
    for (var i = 0; i < data.length; i++) {

        //    "<a href='./Item/Edit/" + data[i].itemId + "'>Edit</a>   " +
        //    " <a href='./Item/Delete/" + data[i].itemId + "'>Delete</a>" +
        table.innerHTML += "<tr>" +
            "<td>" + data[i].name + "</td>" +
            "<td>" + data[i].price + "</td>" +
            "<td>" + data[i].amauntAvailable + "</td>" +
            "<td>" + data[i].measurementUnit + "</td>" +
            "<td>" + data[i].pricePerUnit + "</td>" +
            "<td>" + data[i].tax + "</td>" +
            "<td>" + data[i].priceWithTax + "</td>" +
            "<td>" +
            " <a href=\"./Item/Order?id=" + data[i].itemId + "&Name=" + data[i].name + "\">Order</a>" +
            "</td>" +
            "</tr>";
    }
}

function CreateItem(e) {
    console.log("Submited");
    // stops convetional submit
    e.preventDefault();

    var body = {
        "name": createItemForm.elements["Name"].value,
        "price": parseFloat(createItemForm.elements["Price"].value),
        "amauntAvailable": parseFloat(createItemForm.elements["AmauntAvailable"].value),
        "measurementUnit": createItemForm.elements["MeasurementUnit"].value,
        "pricePerUnit": parseFloat(createItemForm.elements["PricePerUnit"].value),
        "tax": parseFloat(createItemForm.elements["Tax"].value),
        "priceWithTax": parseFloat(createItemForm.elements["PriceWithTax"].value)
    };

    console.log(body);

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        crossDomain: true,
        url: URL + '/Item',
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

function DeleteItem(e) {
    console.log("Deleting ...");
    // stops convetional submit
    e.preventDefault();

    var id = deleteItemForm.elements["ItemIdToDelete"].value;

    fetch(URL + "/Item/" + id, {
        method: "DELETE",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then((res) => { console.log(res); })
        .catch((error) => { confirm.log(error); });
}