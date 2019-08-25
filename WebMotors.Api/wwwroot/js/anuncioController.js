var anuncioController = {
    templateLoadingTable: "<tr><td colspan='6' class='text-center'><br><i class='fa fa-circle-o-notch fa-spin fa-3x fa-fw cor-vermelho'></td></tr>",
    editando: false ,
    marcaParaEdicao: "",
    modeloParaEdicao: "",
    versaoparaEdicao: "",
    adicionar: function () {
        anuncioController.editando = false;
        $("#titulo-formulario").text("Inserindo Anúncio");
        anuncioController.limparFormulario();
        anuncioController.carregarComboMarca();
        $("#modalForm").modal();
        $("#txtMarca").focus();
    },
    salvar: function () {

        if (!anuncioController.validarFormulario()) {
            return false;
        }

        var anuncioData = anuncioController.retornarFormComoObjeto();
        var verbHttp = 'POST';
        if (anuncioData.Id > 0)
            verbHttp = 'PUT';
        $.ajax({
            method: verbHttp,
            url: "/api/anuncio",
            contentType: "application/json; charset=UTF-8;",
            dataType: "json",
            data: JSON.stringify(anuncioData),
            success: function (data) {
                anuncioController.limparFormulario();
                $("#modalForm").modal("hide");
                anuncioController.listar();
            },
            error: function (erro) {
                anuncioController.erroBackEnd(erro);
            }
        });

    },
    listar: function () {

        $("#table-content").html(anuncioController.templateLoadingTable);

        $.ajax({
            method: "GET",
            url: "/api/anuncio",
            contentType: "application/json; charset=UTF-8;",
            dataType: "json",
            success: function (data) {
                $("#table-content").html("");
                var msg = "";
                $.each(data, function (index, item) {
                    msg = "<tr>";
                    msg += "<td>" + item.marca + "</td>";
                    msg += "<td>" + item.modelo + "</td>";
                    msg += "<td>" + item.versao + "</td>";
                    msg += "<td>" + item.ano + "</td>";
                    msg += "<td>" + item.quilometragem + "</td>";
                    msg += "<td class='text-right'>";
                    msg += "<button class='btn-small'><i class='fa fa-pencil' aria-hidden='true' onclick='anuncioController.editar(" + item.id + ")'></i></button> ";
                    msg += "<button class='btn-small'><i class='fa fa-trash' aria-hidden='true' onclick='anuncioController.excluir(" + item.id +")'></i></button>";
                    msg += "</td>";
                    msg += "</tr>";
                    $("#table-content").append(msg);
                });

                if (data.length == 0) {
                    msg = "<tr>";
                    msg += "<td colspan='6' class='text-center'><p class='text-sem-registros'>Sem registros, clique em adicionar</p></td>";
                    msg += "</tr>";
                    $("#table-content").append(msg);
                }

            },
            error: function (erro) {
                anuncioController.erroBackEnd(erro);
            }
        });
    },
    excluir: function (id) {
        var confirme = confirm("Deseja realmente excluir o anúncio?")
        if (confirme)
        {
            $.ajax({
                method: "DELETE",
                url: "/api/anuncio/" + id,
                contentType: "application/json; charset=UTF-8;",
                dataType: "json",
                success: function (data) {
                    anuncioController.listar();
                },
                error: function (erro) {
                    anuncioController.erroBackEnd(erro);
                }
            });
        }
    },
    editar: function (id) {
        anuncioController.editando = true;
        $("#titulo-formulario").text("Editando Anúncio");
        anuncioController.limparFormulario();
        $("#modalForm").modal();

        $("#formAnuncio").hide();
        $("#formAnuncioLoading").show();

        $.ajax({
            method: "GET",
            url: "/api/anuncio/" + id,
            contentType: "application/json; charset=UTF-8;",
            dataType: "json",
            success: function (data) {
                $("#formAnuncioLoading").hide();
                $("#formAnuncio").show();
                anuncioController.limparFormulario();
                anuncioController.preencherFormulario(data);
                console.log(data);
            },
            error: function (erro) {
                anuncioController.erroBackEnd(erro);
            }
        });
    },
    cancelar: function () {
        anuncioController.marcaParaEdicao = "";
        anuncioController.modeloParaEdicao = "";
        anuncioController.versaoparaEdicao = "";
        anuncioController.editando = false;
    },
    limparFormulario: function () {
        $("input").val("");
        $("textarea").val("");
        $("select").html("");
    },
    retornarFormComoObjeto: function () {
        var anuncio = {
            Id: $("#txtId").val(),
            Marca: $("#txtMarca option:selected").text(),
            Modelo: $("#txtModelo option:selected").text(),
            Versao: $("#txtVersao option:selected").text(),
            Ano: $("#txtAno").val(),
            Quilometragem: $("#txtKm").val(),
            Observacao: $("#txtObservacao").val(),
        };

        if (anuncio.Id == "")
            anuncio.Id = 0;
        console.log(anuncio);
        return anuncio;
    },
    preencherFormulario(model) {
        $("#txtId").val(model.id);

        anuncioController.marcaParaEdicao = model.marca;
        anuncioController.modeloParaEdicao = model.modelo;
        anuncioController.versaoparaEdicao = model.versao;
        anuncioController.carregarComboMarca();
        $("#txtAno").val(model.ano);
        $("#txtKm").val(model.quilometragem);
        $("#txtObservacao").val(model.observacao);
    },
    validarFormulario: function () {
        
        $("#formAnuncio input, #formAnuncio select, #formAnuncio textarea").each(function () {
            if ($(this).val() == "") {
                $(this).addClass("input-error");
            } else {
                $(this).removeClass("input-error");
            }
        });

        if ($("#formAnuncio .input-error").length == 0) {
            $("#formAnuncio .input-error:first").focus();
        }

        return ($("#formAnuncio .input-error").length == 0);
    },
    carregarComboMarca: function () {
        $("#txtMarca").addClass("combo-loading");

        $.ajax({
            method: "GET",
            url: "/api/webmotors/RetornarMarcas",
            contentType: "application/json; charset=UTF-8;",
            dataType: "json",
            success: function (data) {
                $("#txtMarca").empty();
                $.each(data, function () {
                    $("#txtMarca").append("<option value='" + this.id + "'>" + this.name + "</option>")
                });

                $("#txtMarca").removeClass("combo-loading");
                if (anuncioController.editando) {
                    $("#txtMarca option:contains('" + anuncioController.marcaParaEdicao + "')").attr('selected', 'selected');
                }
                $("#txtMarca").trigger('change');
            },
            error: function (erro) {
                anuncioController.erroBackEnd(erro);
            }
        });
    },
    carregarComboModelo: function (marcaID) {

        $("#txtModelo").addClass("combo-loading");

        $.ajax({
            method: "GET",
            url: "/api/webmotors/RetornarModelos/" + marcaID,
            contentType: "application/json; charset=UTF-8;",
            dataType: "json",
            success: function (data) {
                console.log(data);
                $("#txtModelo").empty();
                $.each(data, function () {
                    $("#txtModelo").append("<option value='" + this.id + "'>" + this.name + "</option>")
                });
                $("#txtModelo").removeClass("combo-loading");

                $("#txtMarca").removeClass("combo-loading");
                if (anuncioController.editando) {
                    $("#txtModelo option:contains('" + anuncioController.modeloParaEdicao + "')").attr('selected', 'selected');
                }
                $("#txtModelo").trigger('change');
            },
            error: function (erro) {
                anuncioController.erroBackEnd(erro);
            }
        });
    },
    carregarComboVersao: function (modeloID) {

        $("#txtVersao").addClass("combo-loading");

        $.ajax({
            method: "GET",
            url: "/api/webmotors/RetornarVersoes/" + modeloID,
            contentType: "application/json; charset=UTF-8;",
            dataType: "json",
            success: function (data) {
                $("#txtVersao").empty();
                $.each(data, function () {
                    $("#txtVersao").append("<option value='" + this.id + "'>" + this.name + "</option>")
                });
                $("#txtVersao").removeClass("combo-loading");
                if (anuncioController.editando) {
                    $("#txtVersao option:contains('" + anuncioController.versaoparaEdicao + "')").attr('selected', 'selected');
                }
            },
            error: function (erro) {
                anuncioController.erroBackEnd(erro);
            }
        });
    },
    aoSelecionarMarca: function () {
        anuncioController.carregarComboModelo($("#txtMarca").val());
        $("#txtVersao").html("");
        $("#txtModelo").focus();
    },
    aoSelecionarModelo: function () {
        anuncioController.carregarComboVersao($("#txtModelo").val());
        $("#txtVersao").focus();
    },
    erroBackEnd: function (erro) {
        console.log(erro);
        $("#modalErro .modal-erro-detalhe").html(erro.responseText);
        $("#modalErro").modal();
    }

};