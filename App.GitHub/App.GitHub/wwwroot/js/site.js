$(document).ready(function () {

    // Insere modal
    $(".btnModal").click(function () {

        var id = $(this).attr("data-id");

        var loading = $(this).children("span");

        loading.removeClass("fa fa-search").addClass("fas fa-spinner fa-pulse");

        $(".btnModal").attr("disabled", true);

        // Carrega detalhes do usuario
        $.ajax({
            url: "detalhes-do-usuario/" + id,
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