<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMCatalogo.aspx.cs" Inherits="SolucionBase.Catalogo.ABMCatalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        /* 1. Fondo claro y texto oscuro para TODAS las pestañas inactivas */
        .nav-tabs .nav-link:not(.active) {
            background-color: #f8f9fa; /* Fondo gris claro */
            color: #333333; /* Texto oscuro bien visible */
            border-color: #dee2e6; /* Borde definido */
        }

            /* 2. Efecto al pasar el mouse por encima (Hover) */
            .nav-tabs .nav-link:not(.active):hover {
                background-color: #e2e6ea; /* Color de fondo al pasar el cursor */
                color: #0d6efd; /* Color del texto/ícono al pasar el cursor */
            }

        /* 3. Estilo para la pestaña que SÍ está activa */
        .nav-tabs .nav-link.active {
            background-color: #ffffff;
            color: #0d6efd;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">

        <!-- TARJETA PRINCIPAL CON PESTAÑAS -->
        <div class="card card-primary card-outline card-tabs">
            <div class="card-header p-0 pt-1 border-bottom-0">
                <ul class="nav nav-tabs" id="custom-content-below-tab" role="tablist">
                    <li class="nav-item">
                        <a class="nav-link active" id="tab-modelos-tab" data-toggle="tab" href="#tab-modelos" role="tab" aria-controls="tab-modelos" aria-selected="true">
                            <i class="fas fa-mobile-alt mr-1"></i>Modelos
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="tab-marcas-tab" data-toggle="tab" href="#tab-marcas" role="tab" aria-controls="tab-marcas" aria-selected="false">
                            <i class="fas fa-tag mr-1"></i>Marcas
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="tab-categorias-tab" data-toggle="tab" href="#tab-categorias" role="tab" aria-controls="tab-categorias" aria-selected="false">
                            <i class="fas fa-th-large mr-1"></i>Categorías
                        </a>
                    </li>
                </ul>
            </div>

            <div class="card-body">
                <asp:UpdatePanel ID="upCatalogo" runat="server">
                    <ContentTemplate>
                        <!-- Campo oculto para recordar la pestaña activa -->
                        <asp:HiddenField ID="hfTabActiva" runat="server" Value="#tab-modelos" />
                        <div class="tab-content" id="custom-tabs-two-tabContent">

                            <!-- PESTAÑA 1: MODELOS -->
                            <div class="tab-pane fade show active" id="tab-modelos" role="tabpanel">
                                <div class="row mb-3">
                                    <div class="col-md-3">
                                        <label>Filtrar Categoría:</label>
                                        <asp:DropDownList ID="DDL_FiltroCategoria" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDL_FiltroCategoria_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Filtrar Marca:</label>
                                        <asp:DropDownList ID="DDL_FiltroMarca" runat="server" CssClass="form-select form-control" AutoPostBack="true" OnSelectedIndexChanged="DDL_FiltroMarca_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6 text-right d-flex align-items-end justify-content-end">
                                        <asp:Button ID="btnNuevoModelo" runat="server" Text="+ Nuevo Modelo" CssClass="btn btn-success" OnClick="btnNuevoModelo_Click" />
                                    </div>
                                </div>

                                <div class="table-responsive">
                                    <asp:GridView ID="gvModelos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover" DataKeyNames="Id" OnRowCommand="gvModelos_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="#" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Modelo" />
                                            <asp:BoundField DataField="NroModeloTecnico" HeaderText="N° Técnico / OEM" />
                                            <asp:TemplateField HeaderText="Marca">
                                                <ItemTemplate>
                                                    <%# Eval("Marca.Nombre") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Categoría">
                                                <ItemTemplate>
                                                    <%# Eval("Categoria.Nombre") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="120px" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-info">
                                                        <i class="fas fa-edit"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Desea eliminar este modelo?');">
                                                        <i class="fas fa-trash"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <!-- PESTAÑA 2: MARCAS -->
                            <div class="tab-pane fade" id="tab-marcas" role="tabpanel">
                                <div class="row mb-3">
                                    <div class="col-md-12 text-right">
                                        <asp:Button ID="btnNuevaMarca" runat="server" Text="+ Nueva Marca" CssClass="btn btn-success" OnClick="btnNuevaMarca_Click" />
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvMarcas" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" DataKeyNames="Id">
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="#" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Marca" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <!-- PESTAÑA 3: CATEGORÍAS -->
                            <div class="tab-pane fade" id="tab-categorias" role="tabpanel">
                                <div class="row mb-3">
                                    <div class="col-md-12 text-right">
                                        <asp:Button ID="btnNuevaCategoria" runat="server" Text="+ Nueva Categoría" CssClass="btn btn-success" OnClick="btnNuevaCategoria_Click" />
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvCategorias" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" DataKeyNames="Id">
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="#" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Categoría" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- MODAL PARA ALTA / EDICIÓN DE MODELO -->
    <div class="modal fade" id="modalModelo" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <asp:UpdatePanel ID="upModalModelo" runat="server">
                    <ContentTemplate>
                        <div class="modal-header bg-primary text-white">
                            <h5 class="modal-title">
                                <asp:Literal ID="ltrTituloModal" runat="server" Text="Nuevo Modelo"></asp:Literal>
                            </h5>
                            <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hfModeloId" runat="server" Value="0" />

                            <div class="form-group">
                                <label>Categoría</label>
                                <asp:DropDownList ID="DDL_Categoria" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label>Marca</label>
                                <asp:DropDownList ID="DDL_Marca" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label>Nombre del Modelo</label>
                                <asp:TextBox ID="txtModalNombre" runat="server" CssClass="form-control" Placeholder="Ej: Galaxy S21, iPhone 13"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Número Técnico / OEM (Opcional)</label>
                                <asp:TextBox ID="txtModalTecnico" runat="server" CssClass="form-control" Placeholder="Ej: SM-G991B, A2633"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnGuardarModelo" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardarModelo_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>



    <!-- MODAL PARA ALTA / EDICIÓN DE Marca -->
    <div class="modal fade" id="modalMarca" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-header bg-primary text-white">
                            <h5 class="modal-title">
                                <asp:Literal ID="Literal1" runat="server" Text="Nueva Marca"></asp:Literal>
                            </h5>
                            <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="HF_Marca" runat="server" Value="0" />

                            <div class="form-group">
                                <label>Nombre Marca</label>
                                <asp:TextBox ID="TB_Marca" runat="server" CssClass="form-control" Placeholder="Ej: Samsung,Motorola,Xiaomi"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BTN_AltaMarca" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="BTN_AltaMarca_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <!-- MODAL PARA ALTA / EDICIÓN DE MODELO -->
    <div class="modal fade" id="modalCategoria" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header bg-primary text-white">
                            <h5 class="modal-title">
                                <asp:Literal ID="Literal2" runat="server" Text="Nueva Categoria"></asp:Literal>
                            </h5>
                            <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="HF_Categoria" runat="server" Value="0" />

                            <div class="form-group">
                                <label>Nombre </label>
                                <asp:TextBox ID="TB_Categoria" runat="server" CssClass="form-control" Placeholder="Ej: Celular,Tablet,Laptops"></asp:TextBox>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BTN_AltaCategoria" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="BTN_AltaCategoria_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="Catalogo.js"> </script>
    <script>
        function guardarTab(tabId) {
            $('#<%= hfTabActiva.ClientID %>').val(tabId);
        }

        // Restaura la pestaña activa guardada en el HiddenField
        function restaurarTabActiva() {
            var tabActiva = $('#<%= hfTabActiva.ClientID %>').val();
            if (tabActiva) {
                $('.nav-tabs a[href="' + tabActiva + '"]').tab('show');
            }
        }

        // Se ejecuta automáticamente en la carga inicial y tras cada PostBack de UpdatePanel
        $(document).ready(function () {
            restaurarTabActiva();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm) {
            prm.add_endRequest(function () {
                restaurarTabActiva();
            });
        }
    </script>
</asp:Content>
