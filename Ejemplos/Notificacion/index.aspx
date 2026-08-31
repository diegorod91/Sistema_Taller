<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="SolucionBase.Ejemplos.Notificacion.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card card-default">
        <div class="card-header">
            <h3 class="card-title">Tipos de Notificaciones</h3>
        </div>
        <div class="card-body">
            <asp:Button runat='server' CssClass="btn btn-info" ID='btnSinNotificar' Text='Sin Notificar Servidor' OnClick="btnSinNotificar_Click" />
            <br />
            <br />
            <asp:Button runat='server' CssClass="btn btn-info" ID='btnNotificar' Text='Notificar Servidor' OnClick="btnNotificar_Click" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Button runat='server' CssClass="btn btn-info" ID='btnNotificarAjax' Text='Notificar Servidor Ajax' OnClick="btnNotificarAjax_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <button type="button" class="btn btn-info" data-dismiss="modal" onclick="javascript: notificar(2, 'Notificacion', 'Notificacion desde Javascript');">Notificar javascript</button>
        </div>

    </div>


</asp:Content>
