<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaOrden.aspx.cs" Inherits="SolucionBase.Taller.NuevaOrden" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">

        <!-- TÍTULO PRINCIPAL SOLO -->
        <div class="row mb-3">
            <div class="col-12">
                <h3 class="m-0 font-weight-bold text-dark">
                    <i class="fas fa-tools mr-2 text-primary"></i>Ingreso de Equipo / Nueva Orden
                </h3>
            </div>
        </div>

        <asp:UpdatePanel ID="upNuevaOrden" runat="server">
            <ContentTemplate>

                <!-- BOX OPCIONAL: ALTA DE CLIENTE (Panel desplegable) -->
                <asp:UpdatePanel ID="upBoxCliente" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlBoxCliente" runat="server" CssClass="card card-outline card-primary shadow-sm mb-4" Visible="false">
                            <div class="card-header bg-primary text-white">
                                <h3 class="card-title mb-0 font-weight-bold">
                                    <i class="fas fa-user-plus mr-2"></i>
                                    <asp:Literal ID="ltrTituloModal" runat="server" Text="Alta de Nuevo Cliente"></asp:Literal>
                                </h3>
                                <div class="card-tools">
                                    <asp:LinkButton ID="btnCerrarBox" runat="server" CssClass="btn btn-tool text-white" OnClick="btnCancelar_Click" CausesValidation="false">
                                        <i class="fas fa-times"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <div class="card-body">
                                <asp:HiddenField ID="hfClienteId" runat="server" Value="0" />

                                <div class="row">
                                    <div class="col-md-8 form-group mb-3">
                                        <label>Nombre y apellido <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="TB_NombreApellido" runat="server" CssClass="form-control" Placeholder="Ej: Juan Pérez"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 form-group mb-3">
                                        <label>DNI / Documento</label>
                                        <asp:TextBox ID="TB_DNI" runat="server" CssClass="form-control" Placeholder="Ej: 12345678"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6 form-group mb-3">
                                        <label>Dirección</label>
                                        <asp:TextBox ID="TB_Direccion" runat="server" CssClass="form-control" Placeholder="Ej: Calle 123, Ciudad"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 form-group mb-3">
                                        <label>Teléfono Principal <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="TB_Telefono" runat="server" CssClass="form-control" Placeholder="Ej: 123-456-7890"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 form-group mb-3">
                                        <label>Teléfono Secundario</label>
                                        <asp:TextBox ID="TB_TelefonoSecundario" runat="server" CssClass="form-control" Placeholder="Ej: 123-456-7890"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12 form-group mb-0">
                                        <label>Correo Electrónico (Opcional)</label>
                                        <asp:TextBox ID="TB_Correo" runat="server" CssClass="form-control" Placeholder="Ej: juan.perez@example.com"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="card-footer text-right bg-light">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary mr-2" OnClick="btnCancelar_Click" CausesValidation="false" />
                                <asp:Button ID="btnGuardarCliente" runat="server" Text="Guardar Cliente" CssClass="btn btn-primary" OnClick="btnGuardarCliente_Click" />
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <!-- SECCIÓN 1: DATOS DEL CLIENTE (CON COLLAPSE) -->
                <div class="card card-default card-outline border-left-primary shadow-sm mb-4">
                    <div class="card-header">
                        <h3 class="card-title text-primary font-weight-bold">
                            <i class="fas fa-user mr-2"></i>1. Datos del Cliente
                        </h3>
                        <div class="card-tools">
                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                <i class="fas fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-8 form-group mb-0">
                                <label>Cliente <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 form-group mb-0 d-flex align-items-end">
                                <asp:LinkButton ID="btnNuevoCliente" runat="server" CssClass="btn btn-outline-success btn-block mt-2" OnClick="btnNuevoCliente_Click" CausesValidation="false">
                                    <i class="fas fa-user-plus mr-1"></i>Nuevo Cliente
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- SECCIÓN 2: DATOS DEL EQUIPO (CON COLLAPSE) -->
                <div class="card card-default card-outline border-left-primary shadow-sm mb-4">
                    <div class="card-header">
                        <h3 class="card-title text-primary font-weight-bold">
                            <i class="fas fa-mobile-alt mr-2"></i>2. Selección del Equipo
                        </h3>
                        <div class="card-tools">
                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                <i class="fas fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4 form-group">
                                <label>Categoría <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control select2"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 form-group">
                                <label>Marca <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control select2"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 form-group">
                                <label>Modelo <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlModelo" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 form-group mb-0">
                                <asp:LinkButton ID="LNK_NuevoModelo" runat="server" CssClass="btn btn-outline-success btn-block" OnClick="LNK_NuevoModelo_Click" CausesValidation="false">
                                <i class="fas fa-plus mr-1"></i>Nuevo Modelo
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- MODAL PARA ALTA RÁPIDA DE MODELO -->
                <div class="modal fade" id="modalAltaModelo" tabindex="-1" role="dialog" aria-labelledby="modalAltaModeloLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <asp:UpdatePanel ID="upModalModelo" runat="server">
                                <ContentTemplate>
                                    <div class="modal-header bg-success text-white">
                                        <h5 class="modal-title" id="modalAltaModeloLabel">
                                            <i class="fas fa-plus-circle mr-1"></i>Nuevo Modelo
                                        </h5>
                                        <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>

                                    <div class="modal-body">
                                        <!-- Banner con la Categoría y Marca seleccionadas -->
                                        <div class="alert alert-info py-2 mb-3 small">
                                            <i class="fas fa-info-circle mr-1"></i>
                                            Asignando a: <strong>
                                                <asp:Label ID="lblContextoCatMarca" runat="server" Text=""></asp:Label></strong>
                                        </div>

                                        <div class="form-group mb-3">
                                            <label>Nombre del Modelo <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtModalNombreModelo" runat="server" CssClass="form-control" Placeholder="Ej: Galaxy S21, iPhone 13"></asp:TextBox>
                                        </div>
                                        <div class="form-group mb-0">
                                            <label>Número Técnico / Código OEM (Opcional)</label>
                                            <asp:TextBox ID="txtModalTecnicoModelo" runat="server" CssClass="form-control" Placeholder="Ej: SM-G991B, A2633"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="modal-footer bg-light">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                                        <asp:Button ID="BTN_GuardarModelo" runat="server" Text="Guardar Modelo" CssClass="btn btn-success" OnClick="BTN_GuardarModelo_Click" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>


                <!-- SECCIÓN 3: DIAGNÓSTICO E INGRESO (CON COLLAPSE) -->
                <div class="card card-default card-outline border-left-primary shadow-sm mb-4">
                    <div class="card-header">
                        <h3 class="card-title text-primary font-weight-bold">
                            <i class="fas fa-clipboard-list mr-2"></i>3. Diagnóstico Inicial y Recepción
                        </h3>
                        <div class="card-tools">
                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                <i class="fas fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6 form-group mb-3">
                                <label>Tipo de Operación</label>
                                <asp:DropDownList ID="ddlTipoOperacion" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6 form-group mb-3">
                                <label>Estado Inicial</label>
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-12 form-group mb-0">
                                <label>Falla Reportada / Observaciones del Ingreso</label>
                                <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Rows="3"
                                    CssClass="form-control" Placeholder="Ej: Pantalla rota, no enciende, mojado..."></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- BOTONES GLOBALES DE LA ORDEN -->
                <div class="row mb-4">
                    <div class="col-12 text-right">
                        <a href="Default.aspx" class="btn btn-default mr-2">Cancelar</a>
                        <asp:Button ID="btnGuardarOrden" runat="server" Text="Guardar Orden"
                            CssClass="btn btn-primary" OnClick="btnGuardarOrden_Click" />
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
