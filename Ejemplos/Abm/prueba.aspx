<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="prueba.aspx.cs" Inherits="WebApplication.prueba" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="card card-outline card-success" id="divNew">
        <div class="card-header with-border">
            <h3 class="card-title">Agregar</h3>

            <div class="card-tools pull-right">
                <button type="button" id="btnColapse" class="btn btn-tool" data-card-widget="collapse" data-toggle="tooltip" title="Collapse">
                    <i class="fa fa-plus"></i>
                </button>
            </div>
        </div>
        <div class="card-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <label>NombreConsecionaria:</label>
                    <asp:TextBox ID="TB_NombreConsecionaria" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Direccion:</label>
                    <asp:TextBox ID="TB_Direccion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

            </div>
        </div>
        <div class="card-footer">
            <div class=" pull-right">
                <asp:Button ID='BTN_Alta' runat='server' Text='Guardar' CssClass="btn btn-success" OnClientClick='return validar_alta();' OnClick="Btn_Alta_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:Button ID='BTN_Cancelar' runat='server' Text='Cancelar' CssClass="btn btn-default" OnClick="Btn_Cancelar_Click" />
            </div>
        </div>
    </div>

    <div class="card card-outline card-primary">
        <div class="card-header with-border">
            <h3 class="card-title">Filtro</h3>
        </div>
        <div class="card-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-2 control-label">NombreConsecionaria</label>
                    <div class="col-sm-12">
                        <asp:TextBox ID="TB_Busqueda" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="card-footer">
            <asp:Button ID='BTN_Filtrar' runat='server' Text='Filtrar' CssClass="btn btn-primary" OnClientClick="return validarFiltrar();" OnClick="Btn_Filtrar_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Button ID='BTN_Limpiar' runat='server' Text='Limpiar filtro' CssClass="btn btn-default" OnClick="Btn_Cancelar_Click" />
        </div>
    </div>

    <div class="card">
        <div class="card-header with-border">
            <h3 class="card-title">Listado</h3>
        </div>
        <div class="card-body">

            <asp:UpdatePanel ID="UPGrilla" runat="server">
                <ContentTemplate>
                    <span class="float-right description">
                        <asp:Literal ID="LTR_countGrilla" runat="server"></asp:Literal>
                        registros encontrados</span>

                    <WCL:CustomGridView ID='Grilla' runat='server' EmptyDataText='<center>Sin resultados</center>' AllowPaging='True' PageSize="100" AllowSorting='True' AutoGenerateColumns='False'>
                        <Columns>
                            <asp:TemplateField HeaderText="NombreConsecionaria">
                                <ItemTemplate>
                                    <%# Eval("NombreConsecionaria")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Direccion">
                                <ItemTemplate>
                                    <%# Eval("Direccion")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="30">
                                <ItemTemplate>
                                    <a href="javascript:info(<%# Eval("Id")%>);" title="Informaci&oacute;n completa">
                                        <img src="../../dist/img/grid_info2.png" alt="+ Info" />
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="30">
                                <ItemTemplate>
                                    <a href="javascript:editar(<%# Eval("Id")%>);" title="Editar">
                                        <img src="../../dist/img/grid_edit.png" alt="Editar" />
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="30">
                                <ItemTemplate>
                                    <a href="javascript:eliminar(<%# Eval("Id")%>);" title="Eliminar">
                                        <img src="../../dist/img/grid_delete2.png" alt="Eliminar" />
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </WCL:CustomGridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BTN_Filtrar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:HiddenField ID="HF_IdAfectado" runat="server" />

    <div class="modal fade" id="modalEditar">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Editar.</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>

                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label>NombreConsecionaria:</label>
                            <asp:TextBox ID="TB_Edit_NombreConsecionaria" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Direccion:</label>
                            <asp:TextBox ID="TB_Edit_Direccion" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID='BTN_Editar' runat='server' Text='Guardar' CssClass="btn btn-primary" OnClick="Btn_Editar_Click" OnClientClick='return validarEditar();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalInfo">
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
                            <label>NombreConsecionaria:</label>
                            <span id="LBL_Info_NombreConsecionaria"></span>
                        </div>
                        <div class="form-group">
                            <label>Direccion:</label>
                            <span id="LBL_Info_Direccion"></span>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade card-outline card-danger" id="modalBaja">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Eliminar.</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Est&aacute; seguro que desea ELIMINAR Consecionaria <span id="txtBorrar" style="color: Red"></span><span>&nbsp;?</span></p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID='BTN_Eliminar' runat='server' Text='Eliminar' CssClass="btn btn-primary" OnClick="Btn_Eliminar_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <script src="prueba.js"></script>

</asp:Content>

