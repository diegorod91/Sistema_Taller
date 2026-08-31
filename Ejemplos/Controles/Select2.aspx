<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Select2.aspx.cs" Inherits="SolucionBase.Ejemplos.Controles.Select2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../bower_components/select2/dist/css/select2.css" rel="stylesheet" />
    <%--<link href="../../bower_components/select2/dist/css/select2-bootstrap.css" rel="stylesheet" />--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header with-border">
            <h3 class="card-title">Ejemplo de Select2</h3>
        </div>
        <div class="card-body">
            <br />
            <asp:DropDownList ID="DDL_empresa" runat="server" CssClass="select2 form-control"></asp:DropDownList>
            <br />
            <br />
            <asp:Button runat="server" ID="BTN_Buscar" Text="Buscar" OnClick="BTN_Buscar_Click" CssClass="btn btn-primary" />
        </div>
        <!-- /.card-body -->
    </div>

    <div class="card">
        <div class="card-header with-border">
            <h3 class="card-title">Ejemplo de Select2 Ajax</h3>
        </div>
        <div class="card-body">
            <br />
            <select id="DDL_Select2Ajax" class="form-control">
                <option value='-1' selected='selected'>Seleccione...</option>
            </select>
            <asp:HiddenField ID="HF_Select2Ajax" runat="server" />
            <br />
            <br />

        </div>
        <!-- /.card-body -->
    </div>

    <script type="text/javascript" src="../../bower_components/select2/dist/js/select2.full.js"></script>
    <script type="text/javascript" src="../../bower_components/select2/dist/js/i18n/es.js"></script>
    <script type='text/javascript'>
        jQuery(document).ready(function () {
            $(".select2").select2();
            $("#DDL_Select2Ajax").select2({

                maximumSelectionSize: 6,
                containerCssClass: ':all:',
                language: 'es',
                ajax: {
                    type: "POST",
                    url: "AjaxHandler.ashx/GetEmpleados",
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    delay: 600,
                    data: function (term, page) {
                        return `{'term': '${term.term}'}`;
                    },
                    processResults: function (data, params) {
                        return {
                            results: data
                        };
                    }
                },
                escapeMarkup: function (markup) { return markup; },
                minimumInputLength: 1,
                templateResult: formatRepo,
                templateSelection: formatRepoSelection
            });
            $("#DDL_Select2Ajax").on("select2:select", function (e) {
                $("#<%=HF_Select2Ajax.ClientID%>").val(e.params.data.text);
            });
        });
        function formatRepo(repo) {
            if (repo.loading) return repo.text;
            return repo.text;
        }
        function formatRepoSelection(repo) {
            return repo.full_name || repo.text;
        }
    </script>

</asp:Content>
