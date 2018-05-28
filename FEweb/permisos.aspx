<%@ Page Title="Permisos" Language="C#" MasterPageFile="~/container.Master" AutoEventWireup="true" CodeBehind="permisos.aspx.cs" Inherits="FE.permisos" EnableEventValidation="false"%>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
		
        <div id="fh5co-main-sistema">
            <div id="titulo-modulo">
                <div class="container">
                    <asp:Label ID="lblModuloPermisos" runat="server"  CssClass="modulo-label" Text="ADMINISTRACION DE PERMISOS" />
                </div>
            </div>

            <asp:PlaceHolder id="phBotonera" runat="server">
            <div id="main-sistema-botonera">
                    <div class="container">
                        <asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-primary btn-mid" Text="Nuevo Usuario" OnClick="btnAgregar_Click"><span class="glyphicon glyphicon-plus"></span>&nbsp;Agregar</asp:LinkButton>
                        <asp:LinkButton ID="btnEliminarSeleccionados" runat="server" CssClass="btn btn-outline btn-mid" Text="Eliminar Seleccionados" OnClick="btnQuitarSeleccionados_Click"><span class="glyphicon glyphicon-minus"></span>&nbsp;Eliminar Seleccionados</asp:LinkButton>
                    </div>
            </div>
            </asp:PlaceHolder>
            <div class="container">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                <asp:PlaceHolder id="phAltaPermiso" runat="server">
                <div id="contenedor-bloque">
				    <div class="row">
					    <div class="col-md-8 fh5co-lead-wrap">
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:RegularExpressionValidator ID="REV_Nombre" runat="server" ControlToValidate="txtNombre" ForeColor="Red" Font-Bold="true" ErrorMessage="[a-z] --> "  ValidationExpression="^[a-zA-Z0-9\~\s\.\/]{1,50}$" ValidationGroup="formato"/>
                                    <asp:Label ID="lblNombre" runat="server"  CssClass="st-label" Text="Nombre:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:RegularExpressionValidator ID="REV_Descripcion" runat="server" ControlToValidate="txtDescripcion" ForeColor="Red" Font-Bold="true" ErrorMessage="[a-z] --> "  ValidationExpression="^[a-zA-Z0-9\s]{1,50}$" ValidationGroup="formato"/>
                                    <asp:Label ID="lblDescripcion" runat="server"  CssClass="st-label" Text="Descripción:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input-textbox"/>
                                </div>                
                            </div>

                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblesAccion" runat="server"  CssClass="st-label" Text="Es Acción?"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:CheckBox ID="chkesAccion" runat="server" cssclass="input-checkbox"/>
                                </div>       
                            </div>
                            
                            
                        </div>

                    </div>
                </div>
                </asp:placeholder>
                <asp:PlaceHolder id="phListaHijos" runat="server">
                
                        <asp:GridView ID="GridView2" runat="server" GridLines="None" AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  PageSize="10" 
                            OnPageIndexChanging="GridView2_PageIndexChanging" DataKeyNames="nombre">

                            <Columns>
                                <%--CheckBox para seleccionar varios registros...--%>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:CheckBox ReadOnly="true" ID="chkSeleccionar" runat="server" AutoPostBack="true" OnCheckedChanged="GridView2_OnCheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>            
                                
                                <%-- Campos --%>
                                <asp:BoundField ReadOnly="True" DataField="nombre" HeaderText="Nombre Permiso" />
                                <asp:BoundField ReadOnly="true" DataField="Descripcion" HeaderText="Descripción" />
                                <asp:CheckBoxField ReadOnly="true" DataField="esAccion" HeaderText="Es Acción?" />

                            </Columns>
                        </asp:GridView>
                
                </asp:placeholder>
                <asp:PlaceHolder id="phGuardaPermisos" runat="server">    
                <div class="row">
                    
                    <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-mid" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="formato" CausesValidation="true"><span class="glyphicon glyphicon-save"></span>&nbsp;Guardar</asp:LinkButton>
                    <asp:LinkButton ID="btnCerrar" runat="server" CssClass="btn btn-primary btn-mid" Text="Cerrar" OnClick="btnCerrar_Click"><span class="glyphicon glyphicon-remove"></span>&nbsp;Cerrar</asp:LinkButton>
                </div>
                </asp:placeholder>
                <asp:PlaceHolder id="phListaPermisos" runat="server">
                <div class="row contenedor-bloque">
					<div class="col-md-12 fh5co-lead-wrap">
                        <asp:Label ID="lblInfo" runat="server" />
                        <asp:GridView ID="GridView1" runat="server" GridLines="None" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  PageSize="15" 
                            OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="nombre" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" 
                            OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit">

                            <Columns>
                                <%--CheckBox para seleccionar varios registros...--%>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEliminar" runat="server" AutoPostBack="true" OnCheckedChanged="chk_OnCheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>            

                                <%--botones de acción sobre los registros...--%>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">

                                    <%--Botones de eliminar y editar cliente...--%>

                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Quitar" CssClass="btn btn-danger" CommandName="Delete" OnClientClick="return confirm('¿Eliminar Permiso?');"><span class="glyphicon glyphicon-minus"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnEdit" runat="server" Text="Editar" CssClass="btn btn-info" CommandName="Edit"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                    </ItemTemplate>

                                    <%--Botones de grabar y cancelar la edición de registro...--%>
                                    <Edititemtemplate>
                                        <asp:LinkButton ID="btnUpdate" runat="server" Text="Grabar" CssClass="btn btn-success" CommandName="Update" OnClientClick="return confirm('¿Seguro que quiere modificar los datos del permiso?');"><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-warning" CommandName="Cancel"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                    </Edititemtemplate>

                                </asp:TemplateField>
                                
                                <%-- Campos --%>
                                <asp:BoundField ReadOnly="True" DataField="nombre" HeaderText="Nombre Permiso" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:CheckBoxField DataField="esAccion" HeaderText="Es Acción?" />


                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                </asp:placeholder>

			</div>
		</div>
</asp:Content>				
	