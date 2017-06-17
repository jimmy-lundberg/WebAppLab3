// Write your Javascript code.

window.onload = function () {
    $("#buy-price").change(function () {
        var buyPrice = $("#buy-price").val();
        var numberOfShares = $("#shares-to-buy").val();
        var totalPrice = buyPrice * numberOfShares;

        $("#total-buy-price").text("" + totalPrice + " SEK")
    });

    $("#shares-to-buy").change(function () {
        var buyPrice = $("#buy-price").val();
        var numberOfShares = $("#shares-to-buy").val();
        var totalPrice = buyPrice * numberOfShares;

        $("#total-buy-price").text("" + totalPrice + " SEK");
    });

    $("#sell-price").change(function () {
        var sellPrice = $("#sell-price").val();
        var numberOfShares = $("#shares-to-sell").val();
        var totalPrice = sellPrice * numberOfShares;

        $("#total-sell-price").text("" + totalPrice + " SEK")
    });

    $("#shares-to-sell").change(function () {
        var sellPrice = $("#sell-price").val();
        var numberOfShares = $("#shares-to-sell").val();
        var totalPrice = sellPrice * numberOfShares;

        $("#total-sell-price").text("" + totalPrice + " SEK");
    });    
}


