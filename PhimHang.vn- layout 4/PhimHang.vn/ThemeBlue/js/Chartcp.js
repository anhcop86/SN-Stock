function HighChart(e, n) {
    {
        var r = this, a = e.data("symbol");
        moment().format("MM/DD/YYYY")
    }
    if (this.formatPrice = function (t) {
        return t.toFixed(2)
    }, this.parseChartData = function (e) {
        for (P = [], V = [], i = 0; i < e.length; i++) d = e[i], "1d" == s ? (t = new Date(d.EndDate + " " + d.EndTime).getTime(), c = d.Close) : (t = new Date(d.Date).getTime(), c = d.Last), isNaN(t) || (13965084e5 > t && ("GOOG" == a || "GOOGL" == a) && (c /= 2, d.Open = d.Open / 2, d.High = d.High / 2, d.Low = d.Low / 2), P.push([t, d.Open, d.High, d.Low, c]), V.push([t, d.Volume])); "1d" != s && (P = P.reverse(), V = V.reverse()), chartData[a] = {}, chartData[a][s] = { prices: P, volumes: V }, r.doChart()
    }, this.doChart = function () {
        P = chartData[a][s].prices, V = chartData[a][s].volumes, n ? r.renderHighChart(P, V) : r.renderSymbolChart(P, V)
    }, this.renderHighChart = function (t, n) {
        var i; i = "1d" != s ? t[0][0] : null,

        e.highcharts("StockChart",
        {
            chart: { height: 215, width: 295, style: { fontFamily: "'Helvetica Neue', Helvetica, Arial, sans-serif'" }, plotBackgroundImage: "/assets/web2/270x163.png", animation: !1, panning: !1 }, navigator: { enabled: !1 }, scrollbar: { enabled: !1 }, plotOptions: { candlestick: { color: "#db4c3c", upColor: "#00B746", lineColor: "#db4c3c", upLineColor: "#00B746" } }, xAxis: { type: "datetime", dateTimeLabelFormats: { hour: "%l:%M", day: "%b %e", week: "%b %e" }, tickInterval: interval, min: i }, yAxis: [{ opposite: !1, labels: { enabled: !1 }, height: 40, top: 138, gridLineColor: "white" }, { labels: { formatter: function () { return r.formatPrice(this.value) } } }], series: [{ type: "candlestick", name: a, data: t, yAxis: 1, zIndex: 9, dataGrouping: grouping }, { type: "column", name: "Volume", data: n, color: "#DDD", yAxis: 0 }], navigation: { buttonOptions: { enabled: !1 } }, rangeSelector: { enabled: !1 }, exporting: { chartOptions: { rangeSelector: { enabled: !1 } } }, tooltip: { borderColor: "white", formatter: function () { var t = "<b>" + Highcharts.dateFormat("%A, %b %e, %Y", this.x) + "</b><br/>"; return $.each(this.points, function (e, n) { n.point.open && (t += "Open : " + r.formatPrice(n.point.open) + "<br/>", t += "High : " + r.formatPrice(n.point.high) + "<br/>", t += "Low : " + r.formatPrice(n.point.low) + "<br/>", t += "Close : " + r.formatPrice(n.point.close) + "<br/>") }), t } }, credits: { text: "", href: "http://stocktwits.com" }
        })
    }, this.renderSymbolChart = function (t, n)
    {
        e.highcharts("StockChart",
        {
            chart: { height: 180, width: 303, style: { fontFamily: "'Helvetica Neue', Helvetica, Arial, sans-serif'" }, plotBackgroundImage: "/assets/web2/283x142.png", animation: !1 }, navigator: { enabled: !1 }, scrollbar: { enabled: !1 }, yAxis: [{ opposite: !1, labels: { enabled: !1 }, height: 40, top: 111, gridLineColor: "white" }, { opposite: !0 }], series: [{ type: "candlestick", name: a, data: t, yAxis: 1, zIndex: 9 }, { type: "column", name: "Volume", data: n, color: "#DDD", yAxis: 0 }], plotOptions: { candlestick: { color: "#db4c3c", upColor: "#00B746", lineColor: "#db4c3c", upLineColor: "#00B746" } }, navigation: { buttonOptions: { enabled: !1, theme: { "stroke-width": 1, stroke: "silver", r: 0, states: { hover: { fill: "#f2f2f2", stroke: "white" }, select: { stroke: "#333", fill: "#f2f2f2" } } } } }, rangeSelector: { enabled: !1, buttonTheme: { fill: "none", stroke: "none", "stroke-width": 0, r: 8, style: { color: "#333", fontWeight: "bold" }, states: { hover: { fill: "#89130a", style: { color: "white" } }, select: { fill: "#333", style: { color: "white" } } } }, inputEnabled: e.width() > 480, selected: 1 }, exporting: { chartOptions: { rangeSelector: { enabled: !1 } } }, tooltip: { borderColor: "white", formatter: function () { var t = "<b>" + Highcharts.dateFormat("%A, %b %e, %Y", this.x) + "</b><br/>"; return $.each(this.points, function (e, n) { n.point.open && (t += "Open : " + r.formatPrice(n.point.open) + "<br/>", t += "High : " + r.formatPrice(n.point.high) + "<br/>", t += "Low : " + r.formatPrice(n.point.low) + "<br/>", t += "Close : " + r.formatPrice(n.point.close) + "<br/>") }), t } }, credits: { text: "", href: "http://stocktwits.com" }
        })
    }, a) {
        var s = $(".chart-zoom .selected").text(); n || (s = "3m"), "1d" == s && n ? (url = "http://quotes.stocktwits.com/intraday?symbol=" + a, interval = 72e5, grouping =
            {
                enabled: !0
            }) : (interval = null, grouping = { enabled: !1 }, url = "http://quotes.stocktwits.com/chart?symbol=" + a + "&zoom=" + s), void 0 == chartData[s] ?
        $.getJSON(url, function (t) {
            "Error" == t.status ? ($("#price .chart-highchart .loading").remove(), $("#price .chart-highchart").append("<div class='error'>No data available for this timeframe.</div>")) : r.parseChartData(t)
        }) : r.doChart()
    }
}



