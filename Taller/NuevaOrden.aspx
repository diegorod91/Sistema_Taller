<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaOrden.aspx.cs" Inherits="SolucionBase.Taller.NuevaOrden" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .pattern-dot {
            position: absolute;
            width: 20px;
            height: 20px;
            background-color: #adb5bd;
            border-radius: 50%;
            cursor: pointer;
            z-index: 2; /* Siempre sobre el trazo del canvas */
            transition: background-color 0.2s, transform 0.2s, box-shadow 0.2s;
            pointer-events: none;
        }

            /* Estado prendido / encendido */
            .pattern-dot.active {
                background-color: #007bff;
                transform: scale(1.3);
                box-shadow: 0 0 10px rgba(0, 123, 255, 0.7); /* Destello de encendido */
            }
    </style>
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
                            <div runat="server" id="DivCardHeader" class="card-header bg-primary  text-white">
                                <h3 class="card-title mb-0 font-weight-bold">
                                    <i id="iconTitulo" runat="server" class="fas fa-user-plus mr-2"></i>
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
                            <div class="col-md-4 form-group mb-0">
                                <label>Cliente <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 form-group mb-0 d-flex align-items-end">
                                <asp:LinkButton ID="btnNuevoCliente" runat="server" CssClass="btn btn-success btn-block mt-2" OnClick="btnNuevoCliente_Click" CausesValidation="false">
                                    <i class="fas fa-user-plus mr-1"></i>Nuevo Cliente
                                </asp:LinkButton>
                            </div>
                            <div class="col-md-4 form-group mb-0 d-flex align-items-end">
                                <asp:LinkButton ID="LNK_EditarCliente" runat="server" CssClass="btn btn-warning btn-block mt-2" OnClick="LNK_EditarCliente_Click" CausesValidation="false">
                                    <i class="fas fa-pencil-alt"></i> Editar Cliente
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
                        <!-- Primera fila: Categoría, Marca y Modelo -->
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
                        </div>

                        <!-- Segunda fila: Botón de Nuevo Modelo e IMEI / Serie -->
                        <div class="row align-items-end">
                            <div class="col-md-4 form-group mb-0">
                                <asp:LinkButton ID="LNK_NuevoModelo" runat="server" CssClass="btn btn-outline-success btn-block" OnClick="LNK_NuevoModelo_Click" CausesValidation="false">
                                <i class="fas fa-plus mr-1"></i>Nuevo Modelo
                                </asp:LinkButton>
                            </div>
                            <div class="col-md-8 form-group mb-0">
                                <label>IMEI / Número de Serie</label>
                                <asp:TextBox ID="TB_Imei" runat="server" CssClass="form-control" Placeholder="Ej: 35298110... o Número de Serie"></asp:TextBox>
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
                                                <asp:Label ID="LBL_CategMarca" runat="server" Text=""></asp:Label></strong>
                                        </div>

                                        <div class="form-group mb-3">
                                            <label>Nombre del Modelo <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="TB_NombreModelo" runat="server" CssClass="form-control" Placeholder="Ej: Galaxy S21, iPhone 13"></asp:TextBox>
                                        </div>
                                        <div class="form-group mb-0">
                                            <label>Número Técnico / Código OEM (Opcional)</label>
                                            <asp:TextBox ID="TB_ModeloTecnico" runat="server" CssClass="form-control" Placeholder="Ej: SM-G991B, A2633"></asp:TextBox>
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
                                <asp:DropDownList ID="DDL_TipoOperacion" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6 form-group mb-3">
                                <label>Estado Inicial</label>
                                <asp:DropDownList ID="DDL_Estado" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-12 form-group mb-0">
                                <label>Falla Reportada / Observaciones del Ingreso</label>
                                <asp:TextBox ID="TB_Observaciones" runat="server" TextMode="MultiLine" Rows="3"
                                    CssClass="form-control" Placeholder="Ej: Pantalla rota, no enciende, mojado..."></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- SECCIÓN 4: PRESUPUESTO, ANTICIPO Y ESTADO FÍSICO -->
                <div class="card card-default card-outline border-left-primary shadow-sm mb-4">
                    <div class="card-header">
                        <h3 class="card-title text-primary font-weight-bold">
                            <i class="fas fa-file-invoice-dollar mr-2"></i>4. Presupuesto, Anticipo y Estado del Equipo
                        </h3>
                        <div class="card-tools">
                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                <i class="fas fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <!-- Columna Izquierda: Contraseña y Checklist de Estado Físico -->
                            <div class="col-md-6">
                                <div class="form-group mb-3">
                                    <label>Contraseña / PIN de Desbloqueo</label>
                                    <asp:TextBox ID="TB_ClaveEquipo" runat="server" CssClass="form-control" Placeholder="Ej: 1234, Patrón L"></asp:TextBox>
                                    <br />
                                    <button type="button" class="btn btn-outline-success btn-block py-3 font-weight-bold" onclick="abrirModalPatron()">
                                        <i class="fas fa-fingerprint mr-2"></i>Solicitar Patrón de Desbloqueo al Cliente
                                    </button>
                                </div>

                                <div class="form-group mb-3">
                                    <label class="text-secondary font-weight-bold mb-2">Estado al Recibir (Checklist)</label>
                                    <div class="row ml-1">
                                        <div class="col-4 form-check mb-2">
                                            <asp:CheckBox ID="CHK_Encendido" runat="server" CssClass="form-check-input" />
                                            <asp:Label ID="LBL_Encendido" runat="server" AssociatedControlID="CHK_Encendido" CssClass="form-check-label ml-1">Encendido</asp:Label>
                                        </div>
                                        <div class="col-4 form-check mb-2">
                                            <asp:CheckBox ID="CHK_Apagado" runat="server" CssClass="form-check-input" />
                                            <asp:Label ID="LBL_Apagado" runat="server" AssociatedControlID="CHK_Apagado" CssClass="form-check-label ml-1">Apagado</asp:Label>
                                        </div>
                                        <div class="col-4 form-check mb-2">
                                            <asp:CheckBox ID="CHK_Golpes" runat="server" CssClass="form-check-input" />
                                            <asp:Label ID="LBL_Golpes" runat="server" AssociatedControlID="CHK_Golpes" CssClass="form-check-label ml-1">Golpes</asp:Label>
                                        </div>
                                        <div class="col-4 form-check">
                                            <asp:CheckBox ID="CHK_Humedad" runat="server" CssClass="form-check-input" />
                                            <asp:Label ID="LBL_Humedad" runat="server" AssociatedControlID="CHK_Humedad" CssClass="form-check-label ml-1">Humedad</asp:Label>
                                        </div>
                                        <div class="col-4 form-check">
                                            <asp:CheckBox ID="CHK_SinSim" runat="server" CssClass="form-check-input" />
                                            <asp:Label ID="LBL_SinSim" runat="server" AssociatedControlID="CHK_SinSim" CssClass="form-check-label ml-1">Sin SIM</asp:Label>
                                        </div>
                                        <div class="col-4 form-check">
                                            <asp:CheckBox ID="CHK_Accesorios" runat="server" CssClass="form-check-input" />
                                            <asp:Label ID="LBL_Accesorios" runat="server" AssociatedControlID="CHK_Accesorios" CssClass="form-check-label ml-1">Con Funda</asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Columna Derecha: Costos, Anticipo y Plazos -->
                            <div class="col-md-6">
                                <div class="form-group mb-3">
                                    <label>Presupuesto Estimado ($)</label>
                                    <asp:TextBox ID="TB_Presupuesto" runat="server" CssClass="form-control" TextMode="Number" min="0" step="0.01" Placeholder="0.00"></asp:TextBox>
                                </div>
                                <div class="form-group mb-3">
                                    <label>Seña / Anticipo ($)</label>
                                    <asp:TextBox ID="TB_Anticipo" runat="server" CssClass="form-control" TextMode="Number" min="0" step="0.01" Placeholder="0.00"></asp:TextBox>
                                </div>
                                <div class="form-group mb-3">
                                    <label>Fecha Estimada de Entrega</label>
                                    <asp:TextBox ID="TB_FechaEntrega" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="form-group mb-0">
                                    <div class="form-check">
                                        <asp:CheckBox ID="CHK_AvisoWhatsapp" runat="server" CssClass="form-check-input" Checked="true" />
                                        <asp:Label ID="LBL_AvisoWhatsapp" runat="server" AssociatedControlID="CHK_AvisoWhatsapp" CssClass="form-check-label ml-1">Avisar recepción y presupuesto por WhatsApp</asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- BOTONES GLOBALES DE LA ORDEN -->
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row mb-4">
            <div class="col-12 text-right">
                <a href="Default.aspx" class="btn btn-default mr-2">Cancelar</a>
                <asp:Button ID="btnGuardarOrden" runat="server" Text="Guardar Orden"
                    CssClass="btn btn-primary" OnClick="btnGuardarOrden_Click" />
            </div>
        </div>
    </div>

    <!-- Modal Limpio para el Patrón del Cliente (Sin recuadro) -->
    <div class="modal fade" id="modalPatronCliente" tabindex="-1" role="dialog" aria-labelledby="lblPatronTitulo" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content text-center">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title w-100 font-weight-bold" id="lblPatronTitulo">
                        <i class="fas fa-lock mr-2"></i>Dibuje el Patrón de Desbloqueo
                    </h5>
                </div>
                <div class="modal-body py-4">
                    <p class="text-muted mb-3">Deslice el dedo o el mouse para trazar el patrón de seguridad del equipo:</p>

                    <!-- 🎨 GRILLA Y CANVAS LIMPIOS (Sin contenedor con bordes) -->
                    <div class="d-flex flex-column align-items-center justify-content-center">
                        <div id="patternLockGrid" style="position: relative; width: 140px; height: 140px; touch-action: none; background: #ffffff; border-radius: 6px; user-select: none;">
                            <!-- Lienzo transparente para el trazo -->
                            <canvas id="patternCanvas" width="140" height="140" style="position: absolute; top: 0; left: 0; z-index: 1; pointer-events: none;"></canvas>
                            <!-- Puntos interactivos -->
                            <div class="pattern-dot" data-id="1" style="top: 15px; left: 15px;"></div>
                            <div class="pattern-dot" data-id="2" style="top: 15px; left: 60px;"></div>
                            <div class="pattern-dot" data-id="3" style="top: 15px; left: 105px;"></div>
                            <div class="pattern-dot" data-id="4" style="top: 60px; left: 15px;"></div>
                            <div class="pattern-dot" data-id="5" style="top: 60px; left: 60px;"></div>
                            <div class="pattern-dot" data-id="6" style="top: 60px; left: 105px;"></div>
                            <div class="pattern-dot" data-id="7" style="top: 105px; left: 15px;"></div>
                            <div class="pattern-dot" data-id="8" style="top: 105px; left: 60px;"></div>
                            <div class="pattern-dot" data-id="9" style="top: 105px; left: 105px;"></div>
                        </div>
                        <button type="button" id="btnLimpiarPatron" class="btn btn-xs btn-outline-secondary mt-3" style="font-size: 11px; padding: 4px 12px;">
                            <i class="fas fa-eraser mr-1"></i>Borrar Patrón
                        </button>
                    </div>

                    <!-- Input oculto para guardar la secuencia resultante -->
                    <asp:HiddenField ID="hfPatronResultado" runat="server" />
                </div>
                <div class="modal-footer justify-content-between">
                    <!-- Botón de cancelar con clase o evento que dispara la limpieza -->
                    <button type="button" class="btn btn-secondary px-4" data-dismiss="modal" onclick="limpiarResultadoPatron()">
                        <i class="fas fa-times mr-1"></i>Cancelar
                    </button>
                    <button type="button" class="btn btn-success px-4" id="btnConfirmarPatron">
                        <i class="fas fa-check mr-1"></i>Confirmar Patrón
                    </button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal de Éxito / WhatsApp -->
    <div id="modalExito" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-success">
                    <h4 class="modal-title">¡Orden Registrada!</h4>
                </div>
                <div class="modal-body text-center">
                    <p>La orden se guardó correctamente en el taller.</p>
                    <p>¿Deseas enviarle el comprobante al cliente por WhatsApp?</p>

                    <!-- Enlace directo: al ser un clic real del usuario, el navegador JAMÁS lo bloquea -->
                    <a id="lnkWhatsAppModal" runat="server" href="#" target="_blank" class="btn btn-success btn-lg" style="margin-right: 10px;">
                        <i class="fa fa-whatsapp"></i>Enviar WhatsApp
                    </a>

                    <asp:Button ID="btnCerrarModal" runat="server" CssClass="btn btn-secondary btn-lg" Text="Continuar sin WhatsApp" OnClick="btnCerrarModal_Click" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
    var idTextBoxClave = '<%= TB_ClaveEquipo.ClientID %>';
    </script>

    <script src="NuevaOrden.js?v=1"></script>
</asp:Content>
