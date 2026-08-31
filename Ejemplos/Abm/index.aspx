<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="SolucionBase.Ejemplos.Abm.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="card card-outline card-success" id="divNew">
        <div class="card-header with-border">
            <h3 class="card-title">Alta</h3>
            <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-card-widget="collapse">
                    <i class="fas fa-minus"></i>
                  </button>
                </div>
        </div>
        <div class="card-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <label for="TBNombreBusqueda" class="col-sm-1 control-label">Nombre</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="TBNombre" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="TBNombreBusqueda" class="col-sm-1 control-label">Domicilio</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="TBDomicilio" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.box-body -->
        <div class="card-footer">
            <div class=" pull-right">
                <asp:Button ID="btnAlta" CssClass="btn btn-success" runat="server" Text="Aceptar" OnClientClick="return validarAlta();" OnClick="btnAlta_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                        
                <input type="button" id="btnColapse" class="btn btn-default" onclick="javascript: cancelarAlta();" value="Cancelar" />
            </div>
        </div>
        <!-- /.box-footer-->
    </div>

    <div class="card card-outline card-primary">
        <div class="card-header with-border">
            <h3 class="card-title">Filtro</h3>
        </div>
        <div class="card-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <label for="ctl00_CPH1_TBNombreBusqueda" class="col-sm-1 control-label">Nombre</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="TBNombreBusqueda" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    
                </div>
            </div>
        </div>
        <div class="card-footer">
                <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltrarxNombre" Text="Buscar" OnClientClick="return validarFiltrar();" OnClick="btnFiltrarxNombre_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
                <asp:Button runat="server" CssClass="btn btn-default" ID="btnLimpiar" Text="Limpiar" OnClick="btnLimpiar_Click" />
        </div>
    </div>

    <div class="card">
        <div class="card-header with-border">
            <h3 class="card-title">Listado</h3>
        </div>
        <div class="card-body">
            <span class="float-right description"><asp:Literal ID="LTR_countGrilla" runat="server"></asp:Literal> registros encontrados</span>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <WCL:CustomGridView ID="Grilla" runat="server"
                        EmptyDataText="<center>Sin resultados</center>" AllowPaging="True" CssClass="grilla"
                        PageSize="15" AllowSorting="True" AutoGenerateColumns="False">
                        <FooterStyle BackColor="Tan" HorizontalAlign="Left" />
                        <HeaderStyle CssClass="filaCabecera" />
                        <Columns>
                            <asp:TemplateField HeaderText="Nombre" SortExpression="NombreEmpresa">
                                <ItemTemplate>
                                    <%# Eval("NombreEmpresa")%>
                                    <input type="hidden" id='HF_Empresa_<%# Eval("IdEmpresa")%>' value='<%# WebExtensions.JsonSinCiclo(Container.DataItem) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dirección" SortExpression="DomicilioEmpresa">
                                <ItemTemplate><%# Eval("DomicilioEmpresa")%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <a href="javascript:info('<%# Eval("IdEmpresa")%>');" title="Ver Info">
                                        <img src="../../dist/img/grid_info.png" id='imgInt<%# Eval("IdEmpresa") %>' alt="Ver Info" border="0" /></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <a href="javascript:editar('<%# Eval("IdEmpresa")%>');" title="Editar">
                                        <img src="../../dist/img/grid_edit.png" id='imgInt<%# Eval("IdEmpresa") %>' alt="Editar" border="0" /></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <a href="javascript:eliminar('<%# Eval("IdEmpresa")%>');" title="Eliminar">
                                        <img src="../../dist/img/grid_delete.png" id='imgInt<%# Eval("IdEmpresa") %>' alt="Eliminar" border="0" /></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </WCL:CustomGridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFiltrarxNombre" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
       
    </div>

    <input id="idAfectado" name="idAfectado" runat="server" type="hidden" /><asp:HiddenField ID="HiddenField1" runat="server" />

    <div class="modal fade card-outline card-danger" id="TBaja">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Eliminar.</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Esta seguro que desea eliminar a <span id='txtBorrar' style="color: Red"></span>??
                </div>
                <div class="modal-footer">
                    <asp:Button ID='btnBorrar' CssClass="btn btn-danger" runat='server' Text='Aceptar' OnClick='btnBorrar_Click' />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="TEdicion">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Edici&oacute;n.</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>                    
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-12">Nombre</label>
                            <div class="col-sm-12">
                                <asp:TextBox ID="TBNombreE" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-12">Domicilio</label>
                            <div class="col-sm-12">
                                <asp:TextBox ID="TBDomicilioE" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnEdicion" CssClass="btn btn-primary" runat="server" Text="Aceptar" OnClientClick="return validarEdicion();" OnClick="btnEdicion_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="TInfoGrilla">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Informaci&oacute;n completa.</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-12">Nombre</label>
                            <div class="col-sm-12">
                                <span id="LBLNombre"></span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-12">Domicilio</label>
                            <div class="col-sm-12">
                                <span id="LBLDomicilio"></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript" src="index.js"></script>

</asp:Content>
