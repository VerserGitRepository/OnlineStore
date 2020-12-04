$.validator.unobtrusive.adapters.addSingleVal("Required", "string");
$.validator.addMethod("Required", function (value, element, required) {
    return true;
});  