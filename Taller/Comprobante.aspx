<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comprobante.aspx.cs" Inherits="SolucionBase.Taller.Comprobante" %>

<div id="ticketImpresion" style="max-width: 400px; margin: 0 auto; padding: 15px; font-family: Arial, sans-serif; font-size: 12px; color: #333; background: #fff; border: 1px solid #ccc;">
    
    <!-- Encabezado del Negocio -->
    <div style="text-align: center; border-bottom: 2px dashed #333; padding-bottom: 10px; margin-bottom: 10px;">
        <%--<h3 style="margin: 0; font-size: 16px; font-weight: bold;">RD SOLUTION'S</h3>
        <p style="margin: 2px 0; font-size: 11px;">Servicio Técnico y Reparaciones</p>
        <p style="margin: 2px 0; font-size: 11px;">Tel: 221-654-8564 | La Plata</p>
        --%>
        <img src="/dist/img/Logo_RdSolution.png" alt="RD SOLUTION'S" style="max-width: 250px; margin-top: 10px;" />
        <br />
        <br />
    </div>

    <!-- Datos de la Orden y Fecha -->
    <div style="margin-bottom: 10px; border-bottom: 1px solid #eee; padding-bottom: 8px;">
        <table style="width: 100%; font-size: 12px;">
            <tr>
                <td><strong>Orden N°:</strong> <span id="lblNroOrden" runat="server" >--</span></td>
                <td style="text-align: right;"><strong>Fecha:</strong> <span id="lblFecha" runat="server">--</span></td>
            </tr>
        </table>
    </div>

    <!-- Datos del Cliente -->
    <div style="margin-bottom: 10px; border-bottom: 1px solid #eee; padding-bottom: 8px;">
        <p style="margin: 2px 0;"><strong>Cliente:</strong> <span id="lblClienteNombre" runat="server">--</span></p>
        <p style="margin: 2px 0;"><strong>DNI / Doc:</strong> <span id="lblClienteDoc" runat="server">--</span></p>
        <p style="margin: 2px 0;"><strong>Teléfono:</strong> <span id="lblClienteTel" runat="server" >--</span></p>
    </div>

    <!-- Datos del Equipo y Falla -->
    <div style="margin-bottom: 10px; border-bottom: 1px solid #eee; padding-bottom: 8px;">
        <p style="margin: 2px 0;"><strong>Equipo / Modelo:</strong> <span id="lblEquipo" runat="server">--</span></p>
        <p style="margin: 4px 0 2px 0;"><strong>Falla / Observaciones declaradas:</strong></p>
        <div id="lblFalla" runat="server" style="background: #f9f9f9; padding: 6px; border-radius: 3px; font-size: 11px; min-height: 35px; border: 1px solid #ddd;">
            <!-- Acá se inyecta la descripción de la falla -->
        </div>
    </div>

    <!-- Condiciones y Firmas -->
    <div style="font-size: 10px; color: #555; margin-bottom: 15px; line-height: 1.3;">
        <p style="margin: 2px 0;">* Pasados los 30 días de notificada la reparación, el comercio no se responsabiliza por el equipo.</p>
        <p style="margin: 2px 0;">* Presentar este ticket o DNI para retirar.</p>
    </div>

    <!-- Sección de Firmas -->
    <table style="width: 100%; margin-top: 150px; font-size: 11px; text-align: center;">
        <tr>
            <td style="width: 50%; border-top: 1px solid #333; padding-top: 4px;">Firma Cliente</td>
            <td style="width: 50%; border-top: 1px solid #333; padding-top: 4px;">Recibió (Técnico)</td>
        </tr>
    </table>

</div>