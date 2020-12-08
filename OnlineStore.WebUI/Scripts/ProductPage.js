
function ShowProductDetailPage(ProductId) {
    // alert(ProductId)
    var theurl = "./ProductDetail";
    // alert(theurl);
    $.ajax({
        type: "GET",
        data: { "ProductId": ProductId },
        url: theurl,

        success: function (data) {
            // alert(data);
            $.confirm({
                boxWidth: '800px',
                useBootstrap: false,
                title: '',
                content: data,
                closeIcon: true,

                buttons: {
                    yes: { isHidden: true }
                },
            });
        }
    });
}