$(document).ready(function () {
    $.ajax({
        url: "/Relatorio/GetChartData",
        method: "GET",
        dataType: "json",
        success: function (data) {
            var ctx = document.getElementById("entradaSaidaChart").getContext("2d");

            var entradaSaidaChart = new Chart(ctx, {
                type: "line",
                data: {
                    labels: data.labels,
                    datasets: data.datasets
                },
                options: {
                    scales: {
                        x: {
                            type: 'category',
                            position: 'bottom'
                        },
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        }
    });
});
