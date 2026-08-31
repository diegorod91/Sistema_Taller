<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMCatalogo.aspx.cs" Inherits="SolucionBase.Catalogo.ABMCatalogo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        
        <!-- TARJETA PRINCIPAL CON PESTAÑAS -->
        <div class="card card-primary card-outline card-tabs">
            <div class="card-header p-0 pt-1 border-bottom-0">
                <ul class="nav nav-tabs" id="custom-tabs-two-tab" role="tablist">
                    <li class="nav-item">
                        <a class="nav-link active" id="tab-modelos-link" data-toggle="pill" href="#tab-modelos" role="tab">
                            <i class="fas fa-mobile-alt mr-1"></i> Modelos
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="tab-marcas-link" data-toggle="pill" href="#tab-marcas" role="tab">
                            <i class="fas fa-tags mr-1"></i> Marcas
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="tab-categorias-link" data-toggle="pill" href="#tab-categorias" role="tab">
                            <i class="fas fa-list mr-1"></i> Categorías
                        </a>
                    </li>
                </ul>
            </div>

            <div class="card-body">
                <asp:UpdatePanel ID="upCatalogo" runat="server">
                    <ContentTemplate>
                        <div class="tab-content" id="custom-tabs-two-tabContent">
                            
                            <!-- PESTAÑA 1: MODELOS -->
                            <div class="tab-pane fade show active" id="tab-modelos" role="tabpanel">
                                <div class="row mb-3">
                                    <div class="col-md-3">
                                        <label>Filtrar Categoría:</label>
                                        <asp:DropDownList ID="ddlFiltroCategoria" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroCategoria_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Filtrar Marca:</label>
                                        <asp:DropDownList ID="ddlFiltroMarca" runat="server" CssClass="form-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroMarca_SelectedIndexChanged">
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
                                            <asp:BoundField DataField="NumeroModeloTecnico" HeaderText="N° Técnico / OEM" />
                                            <asp:BoundField DataField="Marca.Nombre" HeaderText="Marca" />
                                            <asp:BoundField DataField="Categoria.Nombre" HeaderText="Categoría" />
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
                                <asp:DropDownList ID="ddlModalCategoria" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label>Marca</label>
                                <asp:DropDownList ID="ddlModalMarca" runat="server" CssClass="form-control" />
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

    <script type="text/javascript">
        // Abrir y cerrar Modales de Bootstrap desde C# con ScriptManager
        function abrirModalModelo() {
            $('#modalModelo').modal('show');
        }
        function cerrarModalModelo() {
            $('#modalModelo').modal('hide');
        }
    </script>

</asp:Content>
