function DrawStatistics() {

    var Draw = {
        init: function () {
            this.PieCategorie('@Url.Action("PieCategorie", new { id =       ViewBag.CompetId })',
               "Catégories",
               "Catégorie",
               "pieCate");
            this.PieCategorie('@Url.Action("PieSexe", new { id = ViewBag.CompetId })',
                    "Sexe",
                    "Sexe",
                    "pieSexe");
        },

        PieCategorie: function (url, titre, colName, targetDiv) {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: url,
                data: '{}',
                success: function (chartsdata) {

                    // Callback that creates and populates a data table,
                    // instantiates the pie chart, passes in the data and
                    // draws it.

                    var data = new google.visualization.DataTable();
                    data.addColumn('string', colName);
                    data.addColumn('number', 'Count');

                    for (var i = 0; i < chartsdata.length; i++) {
                        data.addRow([chartsdata[i].Categorie, chartsdata[i].Count]);
                    }

                    // Instantiate and draw our chart, passing in some options
                    var chart = new google.visualization.PieChart(document.getElementById(targetDiv));

                    chart.draw(data,
                      {
                          title: titre,
                          position: "top",
                          fontsize: "14px",
                          chartArea: { width: '100%' },
                      });
                },
                error: function () {
                    alert("Error lors du chargement des données.");
                }
            });
        },
    }

    Draw.init();
};