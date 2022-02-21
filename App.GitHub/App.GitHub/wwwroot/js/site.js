function DetalhesDoUsuario() {
    $(document).ready(function () {

        // Insere modal
        $(document).on("click", ".btnModal", function () {

            let login = $(this).attr("data-login");

            let loading = $(this).children("span");

            loading.removeClass("fa fa-search").addClass("fas fa-spinner fa-pulse");
            $(".btnModal").attr("disabled", true);

            // Carrega detalhes do usuario
            $.ajax({
                url: "detalhes-do-usuario/" + login,
                type: "GET",
                dataType: "html",
                success: function (data) {
                    $(data).insertAfter("header");
                    $("#exampleModal").modal('show');
                    loading.removeClass("fas fa-spinner fa-pulse").addClass("fa fa-search");
                    $(".btnModal").attr("disabled", false);
                }
            });

        });

        // Remove modal
        $(document).on('hidden.bs.modal', '.modal', function () {
            $("#exampleModal").remove();
            $(".modal-dialog").remove();
        });

    });
}

function CarregarMaisUsuarios() {
    $(document).ready(function () {

        $("#btnCarregaUsuarios").click(function () {

            let id = $(".table tr:last").attr("data-id");

            $("#btnCarregaUsuarios").hide();
            $("#loading").fadeIn();

            $.ajax({
                url: "carregar-mais-usuarios/" + id,
                type: "GET",
                dataType: "html",
                success: function (data) {
                    $(".table tbody").append(data);
                    $("#loading").hide();
                    $("#btnCarregaUsuarios").fadeIn();
                }
            });

        });

    });
}