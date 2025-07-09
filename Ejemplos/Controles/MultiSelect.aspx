<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MultiSelect.aspx.cs" Inherits="WebApplication.Ejemplos.Controles.MultiSelect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <link rel="stylesheet" href="../../bower_components/bootstrap4-duallistbox/bootstrap-duallistbox.min.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header with-border">
            <h3 class="card-title">Ejemplo de MultiSelect</h3>
        </div>
        <div class="card-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <label for="TBNombreBusqueda" class="col-sm-1 control-label">Empresa</label>
                    <div class="col-sm-12">
                        <asp:ListBox ID="LB_Prueba" runat="server" SelectionMode="Multiple" CssClass="form-control dual_select"></asp:ListBox>
                    </div>
                </div>

            </div>
        </div>
        <!-- /.box-body -->
    </div>


    <script src="../../bower_components/bootstrap4-duallistbox/jquery.bootstrap-duallistbox.js"></script>
    <script type="text/javascript">
        var multiselect;
        jQuery(function ($) {

            $('.dual_select').bootstrapDualListbox();

        });
    </script>
</asp:Content>
