$(function () {
    function parseJsonDateToTimeStamp(value) {
        return parseInt(value.substr(6), 10) + 25200000;  //The 6 is for trimming '/Date(' 25200000 là GTM + 7 cộng thêm 1 ngày
        }    
    $.getJSON('https://cungphim.com/TickerPrice/GetStockPriChart?chart=' + $('#stockHidenPage').val(), function (data) {
        // split the data set into ohlc and volume
        var ohlc = [],
            volume = [],
            dataLength = data.length,
            // set the allowed units for data grouping
            groupingUnits = [[
                'week',                         // unit name
                [1]                             // allowed multiples
            ], [
                'month',
                [1, 2, 3, 4, 6]
            ]],

            i = 0;

        /*    for (i; i < dataLength; i += 1) {
                var item =
                ohlc.push([
                    data[i][0], // the date
                    data[i][1], // open
                    data[i][2], // high
                    data[i][3], // low
                    data[i][4] // close
                ]);

                volume.push([
                    data[i][0], // the date
                    data[i][5] // the volume
                ]);
            }  */

        $(data).each(function (index, item) {
            ohlc.push([
              parseJsonDateToTimeStamp(item.t), // the date
               item.o, // open
               item.h, // high
               item.l, // low
               item.c // close
            ]);

            volume.push([
                parseJsonDateToTimeStamp(item.t), // the date
                 item.s // the volume
            ]);
        })


        // create the chart
        $('#container').highcharts('StockChart', {
            chart: {
                height: 300, width: 278, style: { fontFamily: "'Helvetica Neue', Helvetica, Arial, sans-serif'" }, /*plotBackgroundImage: "/assets/web2/270x163.png", */ animation: !1, panning: !1
            }, navigator: {
                enabled: !1
            }, scrollbar: {
                enabled: !1
            }, plotOptions: {
                candlestick: { color: "#db4c3c", upColor: "#00B746", lineColor: "#db4c3c", upLineColor: "#00B746" }
            }, navigation: {
                buttonOptions: { enabled: !1 }
            }, rangeSelector: {
                //enabled: !1,
                inputEnabled: !1
            }, exporting: {
                chartOptions: { rangeSelector: { enabled: !1 } }
            },
            credits: {
                enabled: false
            },

            yAxis: [{
                labels: {
                    align: 'right',
                    x: -3
                },

                height: '60%',
                lineWidth: 2
            }, {
                labels: {
                    align: 'right',
                    x: -3
                },
                dateTimeLabelFormats: {
                    day: '%e of %b'
                },
                top: '65%',
                height: '35%',
                offset: 0,
                lineWidth: 2
            }],

            series: [{
                type: 'candlestick',
                name: $('#stockHidenPage').val(),
                data: ohlc,
                dataGrouping: {
                    units: groupingUnits
                }
            }, {
                type: 'column',
                name: 'Volume',
                data: volume,
                yAxis: 1,
                dataGrouping: {
                    units: groupingUnits
                }
            }]
        });
    });
});