<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication.Ejemplos.Errores.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header with-border">
            <h2 class="card-title">Ejemplo de manejo de errores</h2>
        </div>
        <div class="card-body">
            <br />            
            <asp:Button CssClass="btn btn-default" runat="server" ID="btn_error" Text="Errores sin Ajax" OnClick="btn_error_Click" />
            <br />
            <br />        
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <asp:Button CssClass="btn btn-default" runat="server" ID="btn_errorAjax" Text="Errores con Ajax" OnClick="btn_errorAjax_Click" />
            </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <asp:Button CssClass="btn btn-default" runat="server" ID="btnBloqueo" Text="Prueba bloqueo de Ajax" OnClick="btnBloqueo_Click" />
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
