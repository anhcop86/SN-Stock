var viewModel = {

    CompanyID: ko.observable(),
    FinishPrice: ko.observable(),
    Diff: ko.observable(),
    DiffRate: ko.observable(),

};

//var listStock =
viewModel.ColourChange = ko.pureComputed(function () {
    return this.Diff() === 0 ? '<em><color style="color:yellow">YELLOW </color></em>'
        : this.Diff() > 0 ? 'GREEN'
        : 'RED';
}, viewModel);
// This makes Knockout get to work


$(function () {
    var ticker = $.connection.StockRealTimeHub;

    function init() {

        ticker.server.getStock(@ViewBag.StockCode).done(function (stock) {

            viewModel.CompanyID(stock.CompanyID).Diff(stock.Diff).DiffRate(stock.DiffRate).FinishPrice(stock.FinishPrice);
            ko.applyBindings(viewModel, document.getElementById("containB")); // knockout javascript framework bindings

        });
        ticker.server.joinRoom($('#stockcode').html().toUpperCase());
    }

    ticker.client.updateStockPrice = function (stock) {
        viewModel.CompanyID(stock.CompanyID).Diff(stock.Diff).DiffRate(stock.DiffRate).FinishPrice(stock.FinishPrice);

    }

    $.connection.hub.start().done(init);
    $.connection.hub.logging = true;

});

