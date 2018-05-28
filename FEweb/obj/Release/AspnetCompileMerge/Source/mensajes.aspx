<%@ Page Title="Mensajes" Language="C#" MasterPageFile="~/container.Master" AutoEventWireup="true" CodeBehind="mensajes.aspx.cs" Inherits="FE.mensajes" EnableEventValidation="true"%>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
		
        <div id="fh5co-main-sistema">
            <div id="titulo-modulo">
                <div class="container">
                    <asp:Label ID="lblModuloMensajes" runat="server"  CssClass="modulo-label" Text="ADMINISTRACION DE MENSAJES" />
                </div>
            </div>

            <asp:PlaceHolder id="phBotonera" runat="server">
            <div id="main-sistema-botonera">
                    <div class="container">
                        <asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-primary btn-mid" Text="Nueva Cultura" OnClick="btnAgregar_Click"><span class="glyphicon glyphicon-plus"></span>&nbsp;Agregar</asp:LinkButton>
                        <asp:LinkButton ID="btnEliminarSeleccionados" runat="server" CssClass="btn btn-outline btn-mid" Text="Eliminar Seleccionados" OnClick="btnQuitarSeleccionados_Click"><span class="glyphicon glyphicon-minus"></span>&nbsp;Eliminar Seleccionados</asp:LinkButton>
                    </div>
            </div>
            </asp:PlaceHolder>
            <div class="container">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                <asp:PlaceHolder id="phAltaMensaje" runat="server">
                <div id="contenedor-bloque">
				    <div class="row">
					    <div class="col-md-6 fh5co-lead-wrap">
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblIdMensaje" runat="server"  CssClass="st-label" Text="ID Mensaje:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtIdMensaje" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblDescripcion" runat="server"  CssClass="st-label" Text="Descripcion:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblCultura" runat="server"  CssClass="st-label" Text="Cultura:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:DropDownList ID="lstCultura" runat="server" cssclass="input-dropdownlist"/>
                                </div>                                                                                            
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-mid" Text="Guardar" OnClick="btnGuardar_Click"><span class="glyphicon glyphicon-save"></span>&nbsp;Guardar</asp:LinkButton>
                    <asp:LinkButton ID="btnCerrar" runat="server" CssClass="btn btn-primary btn-mid" Text="Cerrar" OnClick="btnCerrar_Click"><span class="glyphicon glyphicon-remove"></span>&nbsp;Cerrar</asp:LinkButton>
                </div>
                </asp:placeholder>
                <asp:PlaceHolder id="phListaMensaje" runat="server">

                <div class="row contenedor-bloque">
                    <asp:Label ID="lblInfo" runat="server" />
                    <div class="row">
					    <div class="col-md-6 fh5co-lead-wrap">
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblculturaModelo" runat="server"  CssClass="st-label" Text="Cultura Modelo:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:DropDownList ID="lstCulturaModelo" runat="server" cssclass="input-dropdownlist" AutoPostBack = true OnSelectedIndexChanged="lstCultura_Changed"/>
                                </div>                                                                                            
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblCulturaEdicion" runat="server"  CssClass="st-label" Text="Cultura Edición:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:DropDownList ID="lstCulturaEdicion" runat="server" cssclass="input-dropdownlist" AutoPostBack = true OnSelectedIndexChanged="lstCultura_Changed"/>
                                </div>                                                                                            
                            </div>
                        </div>
                    </div>
					<div class="col-md-12 fh5co-lead-wrap">
                        <asp:GridView ID="GridView1" runat="server" GridLines="None" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  PageSize="25" 
                            OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="idMensaje, cultura" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" 
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
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Quitar" CssClass="btn btn-danger" CommandName="Delete" OnClientClick="return confirm('¿Eliminar mensaje?');"><span class="glyphicon glyphicon-minus"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnEdit" runat="server" Text="Editar" CssClass="btn btn-info" CommandName="Edit"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                    </ItemTemplate>

                                    <%--Botones de grabar y cancelar la edición de registro...--%>
                                    <Edititemtemplate>
                                        <asp:LinkButton ID="btnUpdate" runat="server" Text="Grabar" CssClass="btn btn-success" CommandName="Update" OnClientClick="return confirm('¿Seguro que quiere modificar los datos del mensaje?');"><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-warning" CommandName="Cancel"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                    </Edititemtemplate>

                                </asp:TemplateField>
                                
                                <%-- Campos --%>
                                <asp:BoundField ReadOnly="True" DataField="idMensaje" HeaderText="ID Mensaje" />
                                <asp:BoundField ReadOnly="True" DataField="Cultura" HeaderText="Cultura" />
                                <asp:BoundField ReadOnly="True" DataField="descripcion" HeaderText="Mensaje Modelo" />
                                <asp:BoundField DataField="descripcion2" HeaderText="Mensaje Edición" />
                                
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                </asp:placeholder>

			</div>
		</div>
</asp:Content>				
	