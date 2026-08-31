<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaOrden.aspx.cs" Inherits="SolucionBase.Taller.NuevaOrden" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="card card-dark">
            <div class="card-header">
                <h3 class="card-title"><i class="fas fa-tools mr-2"></i>Ingreso de Equipo / Nueva Orden</h3>
            </div>

            <asp:UpdatePanel ID="upNuevaOrden" runat="server">
                <ContentTemplate>
                    <div class="card-body">

                        <!-- SECCIÓN 1: DATOS DEL CLIENTE -->
                        <div class="row">
                            <div class="col-md-12">
                                <h5 class="text-primary border-bottom pb-2 mb-3"><i class="fas fa-user mr-1"></i>1. Datos del Cliente</h5>
                            </div>
                            <div class="col-md-8 form-group">
                                <label>Cliente <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 d-flex align-items-center mt-3">
                                <button type="button" class="btn btn-outline-success btn-block" data-toggle="modal" data-target="#modalCliente">
                                    <i class="fas fa-user-plus mr-1"></i>Registrar Nuevo Cliente
                               
                                </button>
                            </div>
                        </div>

                        <!-- SECCIÓN 2: DATOS DEL EQUIPO (CATÁLOGO EN CASCADA) -->
                        <div class="row mt-3">
                            <div class="col-md-12">
                                <h5 class="text-primary border-bottom pb-2 mb-3"><i class="fas fa-mobile-alt mr-1"></i>2. Selección del Equipo</h5>
                            </div>
                            <div class="col-md-4 form-group">
                                <label>Categoría <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 form-group">
                                <label>Marca <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 form-group">
                                <label>Modelo <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlModelo" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <!-- SECCIÓN 3: OPERACIÓN, ESTADO Y FALLA -->
                        <div class="row mt-3">
                            <div class="col-md-12">
                                <h5 class="text-primary border-bottom pb-2 mb-3"><i class="fas fa-clipboard-list mr-1"></i>3. Diagnóstico Inicial y Recepción</h5>
                            </div>
                            <div class="col-md-6 form-group">
                                <label>Tipo de Operación</label>
                                <asp:DropDownList ID="ddlTipoOperacion" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6 form-group">
                                <label>Estado Inicial</label>
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-12 form-group">
                                <label>Falla Reportada / Observaciones del Ingreso</label>
                                <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Rows="3"
                                    CssClass="form-control" Placeholder="Ej: Pantalla rota, no enciende, mojado..."></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <!-- BOTONES DE ACCIÓN -->
                    <div class="card-footer text-right">
                        <a href="Default.aspx" class="btn btn-default mr-2"><i class="fas fa-arrow-left mr-1"></i>Cancelar</a>
                        <asp:Button ID="btnGuardarOrden" runat="server" Text="Guardar Orden"
                            CssClass="btn btn-primary" OnClick="btnGuardarOrden_Click" />
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
