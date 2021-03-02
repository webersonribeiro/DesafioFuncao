$('#addBeneficiarios').on('shown.bs.modal', function () {
    if (document.getElementById("gridBeneficiario")) {
        $('#gridBeneficiario').jtable({
            title: 'Beneficiários',
            paging: true, //Enable paging
            pageSize: 5, //Set page size (default: 10)
            sorting: true, //Enable sorting
            defaultSorting: 'Nome ASC', //Set default sorting
            actions: {
            },
            fields: {
                CPF: {
                    title: 'CPF',
                    width: '35%'
                },
                Nome: {
                    title: 'Nome',
                    width: '50%'
                },
                Alterar: {
                    title: '',
                    display: function (data) {
                        return '<button onclick="buscarBeneficiario(\'' + data.record.CPF + '\')" class="btn btn-primary btn-sm">Alterar</button>';
                    }
                },
                Excluir: {
                    title: '',
                    display: function (data) {
                        return '<button onclick="excluirBeneficiario(\'' + data.record.CPF + '\')" class="btn btn-danger btn-sm">Excluir</button>';
                    }
                }
            }
        });
        if (!beneficiarios) {
            $.ajax({
                url: urlBeneficiarioListar,
                method: "POST",
                data: {
                    "IdCliente": obj.Id
                },
                error:
                    function (r) {
                        if (r.status == 400)
                            ModalDialog("Ocorreu um erro", r.responseJSON);
                        else if (r.status == 500)
                            ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                    },
                success:
                    function (r) {

                        beneficiarios = r;
                        $("#formBeneficiario")[0].reset();
                        $('#gridBeneficiario').jtable('addRecordsToTable', beneficiarios.Records);
                    }
            });
        }

    }

    $('#btnBeneficiarioIncluir').unbind().click(function (e) {
        e.preventDefault();

        var item = $.grep(beneficiarios.Records, function (e) { return e.CPF == $("#CPFBeneficiario").val(); });

        var maxId = 0;
        if (beneficiarios.Records.length > 0) {
            var array = $.grep(beneficiarios.Records, function (e) { return e; });
            maxId = Math.max.apply(null, array.map(x => x.Id));
        }

        if (!isFormValid("#formBeneficiario")) {
            $("#formBeneficiario").find("#submit-hidden").click();
            return false;
        }
        if (!ValidarCPF($("#CPFBeneficiario").val())) {
            showPopover("#CPFBeneficiario", "CPF Inválido", 3000);
            return false;
        }

        if (item.length == 1 && $("#IdBeneficiario").val() != item[0].Id) {
            showPopover("#CPFBeneficiario", "CPF Já existe na lista", 3000);
            return false;
        }
        if (item.length == 0) {
            item[0] = {
                "Id": maxId + 1,
                "CPF": $("#CPFBeneficiario").val(),
                "Nome": $("#NomeBeneficiario").val(),
                "IdCliente": $("#Id").val(),
            };
            beneficiarios.Records = $.grep(beneficiarios.Records, function (e) { return e.CPF != item[0].CPF });
            beneficiarios.Records.push(item[0]);
            showPopover("#btnBeneficiarioIncluir", "Beneficiário Incluído", 3000);
        }
        if ($("#IdBeneficiario").val() == item[0].Id) {

            item[0].Nome = $("#NomeBeneficiario").val();
            item[0].CPF = $("#CPFBeneficiario").val();
            var index = beneficiarios.Records.indexOf(item[0]);
            beneficiarios.Records[index] = item[0];

            $("#CPFBeneficiario").removeAttr("disabled");
            showPopover("#btnBeneficiarioIncluir", "Beneficiário Alterado", 3000);
        }
        $("#formBeneficiario")[0].reset();


        beneficiarios.TotalRecordCount = beneficiarios.Records.length;
        $('#gridBeneficiario').jtable('removeAllRows');
        $('#gridBeneficiario').jtable('addRecordsToTable', beneficiarios.Records);

    });
});


function buscarBeneficiario(CPF) {

    var beneficiario = $.grep(beneficiarios.Records, function (e) { return e.CPF == CPF; });
    $("#CPFBeneficiario").val(beneficiario[0].CPF);
    $("#NomeBeneficiario").val(beneficiario[0].Nome);
    $("#IdBeneficiario").val(beneficiario[0].Id);
    $("#IdCliente").val(beneficiario[0].IdCliente);

    $("#CPFBeneficiario").attr("disabled", "disabled")

}


function excluirBeneficiario(CPF) {
    beneficiarios.Records = $.grep(beneficiarios.Records, function (e) { return e.CPF != CPF; });
    $('#gridBeneficiario').jtable('removeAllRows');
    $('#gridBeneficiario').jtable('addRecordsToTable', beneficiarios.Records);
}

function ValidarCPF(strCPF) {
    var Soma;
    var Resto;
    Soma = 0;

    strCPF = strCPF.replace('.', '').replace('.', '').replace('-', '');

    if (strCPF == "00000000000") return false;

    for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(9, 10))) return false;

    Soma = 0;
    for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(10, 11))) return false;
    return true;
}

function isFormValid(formName) {
    return $(formName)[0].checkValidity()
}

function showPopover(element, message, timeOut) {
    $(element).popover({
        content: message, placement: "bottom"
    });
    $(element).popover('show');

    setTimeout(function () {
        $(element).popover('destroy');
    }, timeOut);
}