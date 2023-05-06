var Itemlist;
var URL = "https://localhost:7122/api/Sales/Customer";
const createCustomerForm = document.querySelector('#createCustomerForm');
const deleteCustomerForm = document.querySelector('#deleteCustomerForm');
const table = document.querySelector('#tableBody');

window.onload = function () {
    if (table != null) getCustomers();
    if (createCustomerForm != null) createCustomerForm.onsubmit = CreateCustomer;
    if (deleteCustomerForm != null) deleteCustomerForm.onsubmit = DeleteCustomer;
    console.log("onload");
}

function getCustomers() {
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
            "<td>" + data[i].name + "</td>" +
            "<td>" + data[i].email + "</td>" +
            "<td>" +
            "<a href='#'>Edit</a>   " +
            " <a href='./Customer/Delete/" + data[i].id + "'>Delete</a>" +
            "</td>" +
            "</tr>";
    }
}

function CreateCustomer(e) {
    console.log("Submited");
    // stops convetional submit
    e.preventDefault();

    var body = {
        "name": createCustomerForm.elements["Name"].value,
        "email": createCustomerForm.elements["Email"].value,
        "password": createCustomerForm.elements["Password"].value
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