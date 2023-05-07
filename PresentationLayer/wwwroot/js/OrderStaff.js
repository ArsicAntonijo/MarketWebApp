var Itemlist;
var URL = "https://localhost:7122/api/Sales/Order";
const createItemForm = document.querySelector('#createItemForm');
const deleteOrderForm = document.querySelector('#deleteOrderForm');
const table = document.querySelector('#tableBody');
const table2 = document.querySelector('#tableDetails');
const indexP = document.querySelector('#index');

window.onload = function () {
    if (table != null) getOrders();
    if (table2 != null) getOrderItems();
    if (deleteOrderForm != null) deleteOrderForm.onsubmit = DeleteOrder;
    console.log("onload");
}

function getOrders() {
    $.getJSON(URL, function (data) {
        console.log(data);
        processData(data);
    });
}

function processData(data) {

    table.innerHTML = "";
    for (var i = 0; i < data.length; i++) {

        table.innerHTML += "<tr>" +
            "<td>" + data[i].confirmed + "</td>" +
            "<td>" + data[i].customer.name + "</td>" +
            "<td>" +
            "<a href='./Order/Details/" + data[i].orderId + "'>Details</a>   " +
            " <a href='./Order/Delete/" + data[i].orderId + "'>Delete</a>" +
            "</td>" +
            "</tr>";
    }
}
// not used
function CreateItem(e) {
    console.log("Submited");
    // stops convetional submit
    e.preventDefault();

    var body = {
        "name": "'" + createItemForm.elements["Name"].value + "'",
        "price": parseFloat(createItemForm.elements["Price"].value),
        "amauntAvailable": parseFloat(createItemForm.elements["AmauntAvailable"].value),
        "measurementUnit": "'" + createItemForm.elements["MeasurementUnit"].value + "'",
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

function DeleteOrder(e) {
    console.log("Deleting ...");
    // stops convetional submit
    e.preventDefault();

    var id = deleteOrderForm.elements["OrderIdToDelete"].value;

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

function getOrderItems() {
    console.log("We are in details - " + indexP.value);
    $.getJSON(URL + "/" + indexP.value, function (data) {
        console.log(data);

        table2.innerHTML = "";
        for (var i = 0; i < data.orderedItems.length; i++) {

            table2.innerHTML += "<tr>" +
                "<td>" + data.orderedItems[i].itemId + "</td>" +
                "<td>" + data.orderedItems[i].amaunt + "</td>" +
                "</tr>";
        }
    });
}
