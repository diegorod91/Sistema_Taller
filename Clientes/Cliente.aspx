<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="SolucionBase.Clientes.Cliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">

        <!-- CABECERA: Título y Botón de Nueva Orden -->
        <div class="row mb-3 align-items-center">
            <div class="col-sm-8">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb bg-transparent p-0 mb-1">
                        <li class="breadcrumb-item"><a href="ListadoClientes.aspx">Clientes</a></li>
                        <li class="breadcrumb-item active" aria-current="page"><asp:Literal ID="ltrNombreBreadcrumb" runat="server" Text="ALALA"></asp:Literal></li>
                    </ol>
                </nav>
                <h2 class="m-0 font-weight-bold text-dark">
                    <asp:Literal ID="ltrNombreClienteTitulo" runat="server" Text="ALALA"></asp:Literal>
                </h2>
                <span class="text-muted small">NO POSEE EMPRESA</span> <!-- O el dato que corresponda -->
            </div>
            <div class="col-sm-4 text-right">
                <asp:Button ID="btnNuevaOrdenCliente" runat="server" CssClass="btn btn-info font-weight-bold" Text="+ Nueva orden" OnClick="btnNuevaOrdenCliente_Click" />
            </div>
        </div>

        <!-- FILA PRINCIPAL: DOS COLUMNAS -->
        <div class="row">
            
            <!-- COLUMNA IZQUIERDA: DATOS DEL CLIENTE -->
            <div class="col-md-8">
                <div class="card card-outline card-success shadow-sm mb-4">
                    <div class="card-header bg-success text-white">
                        <h3 class="card-title font-weight-bold mb-0">
                            <i class="fas fa-id-card mr-2"></i>Datos del cliente
                        </h3>
                    </div>
                    <div class="card-body">
                        
                        <!-- Nombre -->
                        <div class="form-group mb-3">
                            <label class="text-muted font-weight-bold small">NOMBRE</label>
                            <asp:TextBox ID="TB_DetalleNombre" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>

                        <!-- DNI / CUIT y Empresa -->
                        <div class="row">
                            <div class="col-md-6 form-group mb-3">
                                <label class="text-muted font-weight-bold small">DNI / CUIT</label>
                                <asp:TextBox ID="TB_DetalleDocumento" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group mb-3">
                                <label class="text-muted font-weight-bold small">EMPRESA</label>
                                <asp:TextBox ID="TB_DetalleEmpresa" runat="server" CssClass="form-control" ReadOnly="true" Text="NO POSEE"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Teléfono y Celular -->
                        <div class="row">
                            <div class="col-md-6 form-group mb-3">
                                <label class="text-muted font-weight-bold small">TELÉFONO</label>
                                <asp:TextBox ID="TB_DetalleTelefono" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group mb-3">
                                <label class="text-muted font-weight-bold small">CELULAR</label>
                                <asp:TextBox ID="TB_DetalleCelular" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Email -->
                        <div class="form-group mb-3">
                            <label class="text-muted font-weight-bold small">EMAIL</label>
                            <asp:TextBox ID="TB_DetalleEmail" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>

                        <!-- Dirección -->
                        <div class="form-group mb-3">
                            <label class="text-muted font-weight-bold small">DIRECCIÓN</label>
                            <asp:TextBox ID="TB_DetalleDireccion" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>

                        <!-- Localidad, Provincia, CP -->
                        <div class="row">
                            <div class="col-md-5 form-group mb-3">
                                <label class="text-muted font-weight-bold small">LOCALIDAD</label>
                                <asp:TextBox ID="TB_DetalleLocalidad" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-md-4 form-group mb-3">
                                <label class="text-muted font-weight-bold small">PROVINCIA</label>
                                <asp:TextBox ID="TB_DetalleProvincia" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-md-3 form-group mb-3">
                                <label class="text-muted font-weight-bold small">CP</label>
                                <asp:TextBox ID="TB_DetalleCP" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <!-- Notas -->
                        <div class="form-group mb-3">
                            <label class="text-muted font-weight-bold small">NOTAS</label>
                            <asp:TextBox ID="TB_DetalleNotas" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ReadOnly="true"></asp:TextBox>
                        </div>

                        <!-- Botón inferior (Guardar o Habilitar edición) -->
                        <div class="form-group mb-0 text-center">
                            <asp:Button ID="btnGuardarCambiosCliente" runat="server" CssClass="btn btn-info btn-block font-weight-bold py-2" Text="Guardar" OnClick="btnGuardarCambiosCliente_Click" />
                        </div>

                    </div>
                </div>
            </div>

            <!-- COLUMNA DERECHA: HISTORIAL DE ÓRDENES Y EQUIPOS -->
            <div class="col-md-4">
                
                <!-- Tarjeta 1: Historial de Órdenes -->
                <div class="card card-outline card-success shadow-sm mb-4">
                    <div class="card-header bg-success text-white">
                        <h3 class="card-title font-weight-bold mb-0">
                            Historial de órdenes (<asp:Literal ID="ltrCantOrdenes" runat="server" Text="2"></asp:Literal>)
                        </h3>
                    </div>
                    <div class="card-body p-0">
                        <div class="list-group list-group-flush">
                            <!-- Repetidor o items estáticos / dinámicos con un GridView o Repeater -->
                            <asp:Repeater ID="rptHistorialOrdenes" runat="server">
                                <ItemTemplate>
                                    <div class="list-group-item d-flex justify-content-between align-items-center py-2 px-3">
                                        <div>
                                            <span class="font-weight-bold text-dark">#OP-<%# Eval("Id") %></span><br />
                                            <small class="text-uppercase text-muted"><%# Eval("NombreModelo") %></small>
                                        </div>
                                        <div class="text-right">
                                            <span class="badge badge-secondary"><%# Eval("EstadoNombre") %></span><br />
                                            <small class="text-muted"><%# Convert.ToDateTime(Eval("FechaIngreso")).ToString("dd/MM/yy") %></small>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

                <!-- Tarjeta 2: Equipos del Cliente -->
                <div class="card card-outline card-success shadow-sm mb-4">
                    <div class="card-header bg-success text-white">
                        <h3 class="card-title font-weight-bold mb-0">
                            Equipos (<asp:Literal ID="ltrCantEquipos" runat="server" Text="2"></asp:Literal>)
                        </h3>
                    </div>
                    <div class="card-body p-0">
                        <div class="list-group list-group-flush">
                            <asp:Repeater ID="rptEquiposCliente" runat="server">
                                <ItemTemplate>
                                    <div class="list-group-item py-2 px-3 d-flex align-items-center">
                                        <i class="fas fa-laptop mr-2 text-secondary"></i>
                                        <span class="font-weight-bold"><%# Eval("NombreModelo") %></span>
                                        <span class="ml-auto badge badge-light border text-muted"><i class="fas fa-clock mr-1"></i>en taller</span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>
</asp:Content>
