<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SolucionBase._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Tablero del Taller</h1>
                </div>
                <div class="col-sm-6 text-right">
                    <asp:HyperLink runat="server" NavigateUrl="~/Taller/ListadoOrdenes.aspx" CssClass="btn btn-default">
                    <i class="fas fa-list"></i> Ver como lista
                    </asp:HyperLink>
                </div>
            </div>
        </div>
    </section>

    <section class="content">
        <div class="container-fluid">
            <div class="row" style="display: flex; flex-wrap: nowrap; overflow-x: auto; padding-bottom: 15px;">

                <!-- COLUMNA 1: Ingreso al taller -->
                <div class="col-md-3" style="min-width: 280px;">
                    <div class="card card-outline card-info">
                        <div class="card-header">
                            <h3 class="card-title">Ingreso al taller</h3>
                            <span class="badge bg-info float-right" runat="server" id="lblCountIngreso">0</span>
                        </div>
                        <div class="card-body p-2 kanban-column" data-estado="1" style="background-color: #f4f6f9; min-height: 500px; max-height: 700px; overflow-y: auto;">
                            <asp:Repeater ID="rptIngreso" runat="server" OnItemCommand="rptOrdenes_ItemCommand">
                                <ItemTemplate>
                                    <div class="card card-body shadow-sm mb-2 p-3 kanban-item" data-id='<%# Eval("IdOrden") %>' style="background: #fff; border-radius: 5px;">
                                        <div class="d-flex justify-content-between">
                                            <strong>N° <%# Eval("IdOrden", "{0:D6}") %></strong>
                                            <small class="text-muted"><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></small>
                                        </div>
                                        <p class="mb-1 mt-1 font-weight-bold"><%# Eval("ClienteNombre") %></p>
                                        <p class="mb-2 text-muted" style="font-size: 12px;"><%# Eval("EquipoModelo") %></p>
                                        <div class="text-right">
                                            <%--<asp:LinkButton runat="server" CommandName="AvanzarEstado" CommandArgument='<%# Eval("IdOrden") %>' CssClass="btn btn-xs btn-primary" title="Avanzar al siguiente estado">
                                                Avanzar <i class="fas fa-arrow-right"></i>
                                            </asp:LinkButton>--%>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

                <!-- COLUMNA 2: Diagnóstico -->
                <div class="col-md-3" style="min-width: 280px;">
                    <div class="card card-outline card-warning">
                        <div class="card-header">
                            <h3 class="card-title">Diagnóstico</h3>
                            <span class="badge bg-warning float-right" runat="server" id="lblCountDiagnostico">0</span>
                        </div>
                        <div class="card-body p-2 kanban-column" data-estado="2" style="background-color: #f4f6f9; min-height: 500px; max-height: 700px; overflow-y: auto;">
                            <asp:Repeater ID="rptDiagnostico" runat="server" OnItemCommand="rptOrdenes_ItemCommand">
                                <ItemTemplate>
                                    <div class="card card-body shadow-sm mb-2 p-3 kanban-item" data-id='<%# Eval("IdOrden") %>' style="background: #fff; border-radius: 5px;">
                                        <div class="d-flex justify-content-between">
                                            <strong>N° <%# Eval("IdOrden", "{0:D6}") %></strong>
                                            <small class="text-muted"><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></small>
                                        </div>
                                        <p class="mb-1 mt-1 font-weight-bold"><%# Eval("ClienteNombre") %></p>
                                        <p class="mb-2 text-muted" style="font-size: 12px;"><%# Eval("EquipoModelo") %></p>
                                        <div class="p-2 mb-2 rounded" style="background-color: #f8f9fa; font-size: 11px; border-left: 3px solid #17a2b8;">
                                            <i class="fas fa-comment-dots text-info mr-1"></i>
                                            <span class="text-dark"><%# Eval("UltimaObservacion") %></span>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

                <!-- COLUMNA 3: Pendiente de respuesta -->
                <div class="col-md-3" style="min-width: 280px;">
                    <div class="card card-outline card-danger">
                        <div class="card-header">
                            <h3 class="card-title">A la espera de respuesto</h3>
                            <span class="badge bg-danger float-right" runat="server" id="lblCountPendiente">0</span>
                        </div>
                        <div class="card-body p-2 kanban-column" data-estado="3" style="background-color: #f4f6f9; min-height: 500px; max-height: 700px; overflow-y: auto;">
                            <asp:Repeater ID="rptPendiente" runat="server" OnItemCommand="rptOrdenes_ItemCommand">
                                <ItemTemplate>
                                    <div class="card card-body shadow-sm mb-2 p-3 kanban-item" data-id='<%# Eval("IdOrden") %>' style="background: #fff; border-radius: 5px;">
                                        <div class="d-flex justify-content-between">
                                            <strong>N° <%# Eval("IdOrden", "{0:D6}") %></strong>
                                            <small class="text-muted"><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></small>
                                        </div>
                                        <p class="mb-1 mt-1 font-weight-bold"><%# Eval("ClienteNombre") %></p>
                                        <p class="mb-2 text-muted" style="font-size: 12px;"><%# Eval("EquipoModelo") %></p>
                                        <div class="p-2 mb-2 rounded" style="background-color: #f8f9fa; font-size: 11px; border-left: 3px solid #17a2b8;">
                                            <i class="fas fa-comment-dots text-info mr-1"></i>
                                            <span class="text-dark"><%# Eval("UltimaObservacion") %></span>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

                <!-- COLUMNA 4: Entregado -->
                <div class="col-md-3" style="min-width: 280px;">
                    <div class="card card-outline card-success">
                        <div class="card-header">
                            <h3 class="card-title">Entregado</h3>
                            <span class="badge bg-success float-right" runat="server" id="lblCountEntregado">0</span>
                        </div>
                        <div class="card-body p-2 kanban-column" data-estado="4" style="background-color: #f4f6f9; min-height: 500px; max-height: 700px; overflow-y: auto;">
                            <asp:Repeater ID="rptEntregado" runat="server" OnItemCommand="rptOrdenes_ItemCommand">
                                <ItemTemplate>
                                    <div class="card card-body shadow-sm mb-2 p-3 kanban-item" data-id='<%# Eval("IdOrden") %>' style="background: #fff; border-radius: 5px;">
                                        <div class="d-flex justify-content-between">
                                            <strong>N° <%# Eval("IdOrden", "{0:D6}") %></strong>
                                            <small class="text-muted"><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></small>
                                        </div>
                                        <p class="mb-1 mt-1 font-weight-bold"><%# Eval("ClienteNombre") %></p>
                                        <p class="mb-2 text-muted" style="font-size: 12px;"><%# Eval("EquipoModelo") %></p>
                                        <div class="p-2 mb-2 rounded" style="background-color: #f8f9fa; font-size: 11px; border-left: 3px solid #17a2b8;">
                                            <i class="fas fa-comment-dots text-info mr-1"></i>
                                            <span class="text-dark"><%# Eval("UltimaObservacion") %></span>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>

    <div class="modal fade" id="modalTareaDiagnostico" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Registrar Diagnóstico / Tarea</h5>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Indique la observación o acción inicial:</label>
                        <textarea id="txtTareaRealizadaModal" class="form-control" rows="3" placeholder="Ej: Equipo ingresa con pantalla dañada..."></textarea>
                    </div>
                    <asp:Label ID="LBL_Error" Text="" runat="server" ForeColor="Red" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="location.reload();">Cancelar</button>
                    <button type="button" class="btn btn-primary"  onclick="confirmarCambioConTarea()">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Incluir SortableJS desde CDN -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Sortable/1.15.0/Sortable.min.js"></script>

    <script src="dist/js/Kanban.js?v=1"></script>

</asp:Content>
