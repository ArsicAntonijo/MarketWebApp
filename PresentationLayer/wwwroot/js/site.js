//// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.
//const createItemForm = document.querySelector('#createItemForm');
//var URL = "https://localhost:7122/api/Sales";

//window.onload = function () {
//    if(createItemForm != null) createItemForm.onsubmit = CreateItem;
//}

//function CreateItem(e) {
//    console.log("Submited");
//    // stops convetional submit
//    e.preventDefault();

//    var body = {
//        name: createItemForm.elements["Name"].value,
//        price: parseFloat(createItemForm.elements["Price"].value),
//        amauntAvailable: parseFloat(createItemForm.elements["AmauntAvailable"].value),
//        measurementUnit: createItemForm.elements["MeasurementUnit"].value,
//        pricePerUnit: parseFloat(createItemForm.elements["PricePerUnit"].value),
//        tax: parseFloat(createItemForm.elements["Tax"].value),
//        priceWithTax: parseFloat(createItemForm.elements["PriceWithTax"].value)
//    };

//    console.log(body);

//    $.ajax({
//        url: URL + '/Item',
//        type: 'post',
//        data: body,
//        success: function (response) {
//            console.log(response);
//        },
//        error: function (e) {
//            console.log(e);
//        }
//    });   
//}