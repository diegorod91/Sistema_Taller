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
                    <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
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
                                  <i class="fas fa-pencil-alt"></i>
                            </asp:LinkButton>

                            <a href='<%# "https://wa.me/" + Eval("TelefonoLimpio") + "?text=Hola%20" + Eval("NombreCliente") + ",%20te%20escribimos%20de%20RD%20solution's%20por%20tu%20orden%20N°%20" + Eval("IdOrden") %>'
                                target="_blank" class="btn btn-success btn-sm" title="Enviar WhatsApp">
                                <i class="fab fa-whatsapp"></i>
                            </a>
                            <!-- Botón con imagen para Cambiar Estado -->
                            <asp:ImageButton ID="btnCambiarEstado" runat="server" ImageUrl="~/dist/img/Estado_Conexion1.png"
                                CommandName="CambiarEstado" CommandArgument='<%# Eval("IdOrden") %>'
                                OnCommand="btnCambiarEstado_Command" ToolTip="Cambiar Estado"
                                Width="24px" Height="24px" Style="vertical-align: middle; display: inline-block; margin-bottom: 2px;" />

                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- Modal Cambiar Estado -->
    <div class="modal fade" id="modalCambiarEstado" tabindex="-1" role="dialog" aria-labelledby="modalCambiarEstadoLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header bg-info text-white">
                    <h5 class="modal-title" id="modalCambiarEstadoLabel">
                        <i class="fas fa-sync-alt"></i>&nbsp;Modificar Estado de la Orden
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <!-- HiddenField para retener el ID de la orden que se está modificando -->
                    <asp:HiddenField ID="hfIdOrdenModal" runat="server" />

                    <div class="form-group">
                        <label for="ddlEstadosModal">Seleccione el nuevo estado:</label>
                        <asp:DropDownList ID="ddlEstadosModal" runat="server" CssClass="form-control select2-modal">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <!-- Botón Cancelar -->
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">
                        Cancelar
                    </button>
                    <!-- Botón Modificar / Guardar -->
                    <asp:Button ID="btnModificarEstado" runat="server" CssClass="btn btn-primary"
                        Text="Modificar" OnClick="btnModificarEstado_Click" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Editar Orden -->
    <div class="modal fade" id="modalEditarOrden" tabindex="-1" role="dialog" aria-labelledby="modalEditarOrdenLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <!-- modal-lg por si necesitas más espacio para los campos -->
            <div class="modal-content">
                <div class="modal-header bg-warning text-dark">
                    <h5 class="modal-title" id="modalEditarOrdenLabel">
                        <i class="fas fa-pencil-alt"></i>&nbsp;Editar Orden de Trabajo
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfIdOrdenEditar" runat="server" />
                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label>Cliente:</label>
                            <asp:TextBox ID="txtClienteEditar" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Equipo / Modelo:</label>
                            <br />
                            <asp:Label ID="LBL_EquipoEditar" Text="" runat="server" />
                            <%--<asp:TextBox ID="txtEquipoEditar" runat="server" CssClass="form-control"></asp:TextBox>--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 form-group">
                            <label>Cambiar Equipo:</label>
                            <asp:DropDownList ID="ddlModeloEditar" runat="server" CssClass="form-control select2-modal"
                                DataTextField="Nombre" DataValueField="Id">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label>DNI / Documento</label>
                            <asp:TextBox ID="TB_DNI" runat="server" CssClass="form-control" Placeholder="Ej: 12345678"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Dirección</label>
                            <asp:TextBox ID="TB_Direccion" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label>Telefono Principal</label>
                            <asp:TextBox ID="TB_Principal" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Telefono Secundario</label>
                            <asp:TextBox ID="TB_Secundario" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 form-group mb-0">
                            <label>Correo Electrónico (Opcional)</label>
                            <asp:TextBox ID="TB_Correo" runat="server" CssClass="form-control" Placeholder="Ej: juan.perez@example.com"></asp:TextBox>
                        </div>

                        <div class="col-md-12 form-group">
                            <label>Falla / Observaciones:</label>
                            <asp:TextBox ID="txtFallaEditar" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <!-- Botón Cancelar -->
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">
                        Cancelar
                    </button>
                    <!-- Botón Guardar Cambios -->
                    <asp:Button ID="btnGuardarEdicion" runat="server" CssClass="btn btn-primary"
                        Text="Guardar Cambios" OnClick="btnGuardarEdicion_Click" />
                </div>
            </div>
        </div>
    </div>
    <script src="ListadoOrdenes.js"></script>

</asp:Content>
