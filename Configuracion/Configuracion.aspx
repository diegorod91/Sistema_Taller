<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="SolucionBase.Configuracion.Configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <!-- Encabezado de la Página -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">
                <i class="fas fa-store mr-2"></i>Configuración del Taller
            </h1>
        </div>

        <!-- Tarjeta Principal de Configuración -->
        <div class="card shadow mb-4">
            <div class="card-header py-3 bg-white">
                <h6 class="m-0 font-weight-bold text-primary">Datos Generales y Mensajería</h6>
            </div>
            <div class="card-body">
                <div class="row">
                    <!-- Columna Izquierda: Identidad del Negocio -->
                    <div class="col-md-6">
                        <div class="form-group mb-3">
                            <label class="font-weight-bold">Nombre del Taller / Marca</label>
                            <asp:TextBox ID="TB_NombreTaller" runat="server" CssClass="form-control" Placeholder="Ej: Jose Lopez Service"></asp:TextBox>
                        </div>
                        <div class="form-group mb-3">
                            <label class="font-weight-bold">Teléfono / WhatsApp de Contacto</label>
                            <asp:TextBox ID="TB_TelefonoTaller" runat="server" CssClass="form-control" Placeholder="Ej: 54922111111"></asp:TextBox>
                            <small class="form-text text-muted">Número que aparecerá en los reportes o enlaces de contacto.</small>
                        </div>
                        <div class="form-group mb-3">
                            <label class="font-weight-bold">Dirección del Local</label>
                            <asp:TextBox ID="TB_DireccionTaller" runat="server" CssClass="form-control" Placeholder="Ej: Calle 7 nro 123, La Plata"></asp:TextBox>
                        </div>
                        <!-- En la Columna Izquierda, debajo de los datos del negocio, agregamos esto: -->
                        <div class="form-group mb-3">
                            <label class="font-weight-bold">Logotipo del Taller</label>
                            <div class="mb-2">
                                <!-- Vista previa del logo actual (si ya tiene uno cargado) -->
                                <asp:Image ID="ImgLogoActual" runat="server" CssClass="img-thumbnail" Style="max-height: 80px;" Visible="false" />
                            </div>
                            <asp:FileUpload ID="FuLogoTaller" runat="server" CssClass="form-control-file" />
                            <small class="form-text text-muted">Formatos permitidos: PNG, JPG (Recomendado fondo transparente).</small>
                        </div>
                    </div>

                    <!-- Columna Derecha: Plantilla de Mensajes de WhatsApp -->
                    <div class="col-md-6">
                        <div class="form-group mb-3">
                            <label class="font-weight-bold">Plantilla de Mensaje de WhatsApp</label>
                            <asp:TextBox ID="TB_PlantillaWsp" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6"
                                Placeholder="Hola! Te escribimos de *{Taller}* para avisarte que tu equipo ha ingresado..."></asp:TextBox>
                            <small class="form-text text-muted">Podés usar comodines como <code>{Taller}</code> para que el sistema inserte automáticamente el nombre de tu negocio.
                            </small>
                        </div>
                    </div>
                </div>

                <hr class="my-4" />

                <!-- Botón de Guardado -->
                <div class="row">
                    <div class="col-md-12 text-right">
                        <asp:Button ID="BtnGuardarConfig" runat="server" CssClass="btn btn-success px-5 font-weight-bold" Text="Guardar Cambios" OnClick="BtnGuardarConfig_Click" />
                    </div>
                </div>

                <!-- Mensaje de éxito/alerta -->
                <asp:Label ID="LblMensaje" runat="server" CssClass="d-block mt-3 font-weight-bold text-center"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
