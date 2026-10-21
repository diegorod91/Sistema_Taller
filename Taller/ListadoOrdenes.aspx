<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoOrdenes.aspx.cs" Inherits="SolucionBase.Taller.ListadoOrdenes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card card-outline card-primary">
        <div class="card-header">
            <h3 class="card-title">Listado de Órdenes de Reparación</h3>
            <div class="card-tools">
                <a href="NuevaOrden.aspx" class="btn btn-primary btn-sm">
                    <i class="fa fa-plus"></i>Nueva Orden
                </a>
            </div>
        </div>
        <div class="card-body">
            <asp:GridView ID="gvOrdenes" runat="server" CssClass="table table-bordered table-striped table-hover"
                AutoGenerateColumns="false" UseAccessibleHeader="true" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="IdOrden" HeaderText="N° Orden" DataFormatString="{0:D6}" />
                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:BoundField DataField="NombreCliente" HeaderText="Cliente" />
                    <asp:BoundField DataField="ModeloEquipo" HeaderText="Equipo / Modelo" />

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class="badge bg-info"><%# Eval("Estado") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-warning btn-sm"
                                CommandArgument='<%# Eval("IdOrden") %>' OnClick="btnEditar_Click" ToolTip="Editar / Ver Detalle">
                <i class="fa fa-pencil"></i>
                            </asp:LinkButton>

                            <a href='<%# "https://wa.me/" + Eval("TelefonoLimpio") + "?text=Hola%20" + Eval("NombreCliente") + ",%20te%20escribimos%20de%20RD%20solution's%20por%20tu%20orden%20N°%20" + Eval("IdOrden") %>'
                                target="_blank" class="btn btn-success btn-sm" title="Enviar WhatsApp">
                                <i class="fa fa-whatsapp"></i>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
