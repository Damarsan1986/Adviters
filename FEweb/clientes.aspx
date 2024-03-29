﻿<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/container.Master" AutoEventWireup="true" CodeBehind="clientes.aspx.cs" Inherits="FE.clientes" EnableEventValidation="false"%>
<%@ Register src="UCNumTextBox.ascx" tagname="UCNumTextBox" tagprefix="uc1" %>
<%@ Register src="UCMail.ascx" tagname="UCMail" tagprefix="uc1" %>
<%@ Register src="UCCuit.ascx" tagname="UCCuit" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
		
        <div id="fh5co-main-sistema">
            <div id="titulo-modulo">
                <div class="container">
                    <asp:Label ID="lblModuloClientes" runat="server"  CssClass="modulo-label" Text="ADMINISTRACION DE CLIENTES" />
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
                <asp:Label ID="lblInfo" runat="server" />
                <asp:PlaceHolder id="phAlta" runat="server">
                <div id="contenedor-bloque">
				    <div class="row">
					    <div class="col-md-12 fh5co-lead-wrap">
                            <div class="row">
                                <div class="col-md-3 text-right">
                                    <asp:Label ID="lblIdCliente" runat="server"  CssClass="st-label" Text="ID Cliente:"/>
                                </div>
                                <div class="col-md-4 text-left">
                                    <asp:TextBox ID="txtIdCliente" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-3 text-right">
                                    <asp:Label ID="lblRazonSocial" runat="server"  CssClass="st-label" Text="RazonSocial:"/>
                                </div>
                                <div class="col-md-4 text-left">
                                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-3 text-right">
                                    <asp:Label ID="lblEmail" runat="server"  CssClass="st-label" Text="Email:"/>
                                </div>
                                <div class="col-md-4 text-left">
                                    <uc1:UCMail ID="UCMail" runat="server" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-3 text-right">
                                    <asp:Label ID="lblCuit" runat="server"  CssClass="st-label" Text="Cuit:"/>
                                </div>
                                <div class="col-md-4 text-left">
                                    <uc1:UCCuit ID="UCCuit" runat="server" />
                                </div>
<%--                                <div class="col-md-2 text-right">
                                    <asp:LinkButton ID="btnLlamarAFIP" runat="server" CssClass="btn btn-primary btn-inLine" Text="Llamar Afip" OnClick="btnLlamarAFIP_Click"></asp:LinkButton>  
                                </div>  --%>  
                            </div>     
                            <div class="row">
                                <div class="col-md-3 text-right">
                                    <asp:Label ID="lblSFI" runat="server"  CssClass="st-label" Text="SFI:"/>
                                </div>
                                <div class="col-md-4 text-left">
                                    <uc1:UCNumTextBox ID="txtSFI" runat="server"/>
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-3 text-right">
                                    <asp:Label ID="lblDomicilio" runat="server"  CssClass="st-label" Text="Domicilio:"/>
                                </div>
                                <div class="col-md-4 text-left">
                                    <asp:TextBox ID="txtDomicilio" runat="server" CssClass="input-textbox" />
                                </div>      
                            </div>          
                            <div class="row">
                                <div class="col-md-3 text-right">
                                    <asp:RegularExpressionValidator ID="REV_CP" runat="server" ControlToValidate="txtCP" ForeColor="Red" Font-Bold="true" ErrorMessage="[a-z/0-9] --> "  ValidationExpression="^[a-zA-Z0-9\s]{1,10}$" ValidationGroup="formato"/>
                                    <asp:Label ID="lblCP" runat="server"  CssClass="st-label" Text="Código Postal:"/>
                                </div>
                                <div class="col-md-4 text-left">
                                    <asp:TextBox ID="txtCP" runat="server" CssClass="input-textbox" />
                                </div>     
                            </div>   
                            <div class="row">
                                <div class="col-md-3 text-right">
                                    <asp:RegularExpressionValidator ID="REV_Localidad" runat="server" ControlToValidate="txtLocalidad" ForeColor="Red" Font-Bold="true" ErrorMessage="[a-z/0-9] --> "  ValidationExpression="^[a-zA-Z0-9\s]{1,50}$" ValidationGroup="formato"/>
                                    <asp:Label ID="lblLocalidad" runat="server"  CssClass="st-label" Text="Localidad:"/>
                                </div>
                                <div class="col-md-4 text-left">
                                    <asp:TextBox ID="txtLocalidad" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>        
                            <div class="row">
                                <div class="col-md-3 text-right">
                                    <asp:RegularExpressionValidator ID="REV_Provincia" runat="server" ControlToValidate="txtProvincia" ForeColor="Red" Font-Bold="true" ErrorMessage="[a-z/0-9] --> "  ValidationExpression="^[a-zA-Z0-9\s]{1,50}$" ValidationGroup="formato"/>
                                    <asp:Label ID="lblProvincia" runat="server"  CssClass="st-label" Text="Provincia:"/>
                                </div>
                                <div class="col-md-4 text-left">
                                    <asp:TextBox ID="txtProvincia" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-3 text-right">
                                    <asp:RegularExpressionValidator ID="REV_Pais" runat="server" ControlToValidate="txtPais" ForeColor="Red" Font-Bold="true" ErrorMessage="[a-z/0-9] --> "  ValidationExpression="^[a-zA-Z0-9\s]{1,50}$" ValidationGroup="formato"/>
                                    <asp:Label ID="lblPais" runat="server"  CssClass="st-label" Text="Pais:"/>
                                </div>
                                <div class="col-md-4 text-left">
                                    <asp:TextBox ID="txtPais" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>                                                            
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:LinkButton ID="btnGuardar" runat="server" ValidationGroup="formato" CausesValidation="true" CssClass="btn btn-primary btn-mid" Text="Guardar" OnClick="btnGuardar_Click"><span class="glyphicon glyphicon-save"></span>&nbsp;Guardar</asp:LinkButton>
                    <asp:LinkButton ID="btnCerrar" runat="server" CssClass="btn btn-primary btn-mid" Text="Cerrar" OnClick="btnCerrar_Click"><span class="glyphicon glyphicon-remove"></span>&nbsp;Cerrar</asp:LinkButton>
                </div>
                </asp:placeholder>
                <asp:PlaceHolder id="phLista" runat="server">
                <div class="row contenedor-bloque">
					<div class="col-md-12 fh5co-lead-wrap">
                        <asp:GridView ID="GridView1" runat="server" GridLines="None" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  PageSize="7" 
                            OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="idCliente" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" 
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
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Quitar" CssClass="btn btn-danger" CommandName="Delete" OnClientClick="return confirm('¿Eliminar cliente?');"><span class="glyphicon glyphicon-minus"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnEdit" runat="server" Text="Editar" CssClass="btn btn-info" CommandName="Edit"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                    </ItemTemplate>

                                    <%--Botones de grabar y cancelar la edición de registro...--%>
                                    <Edititemtemplate>
                                        <asp:LinkButton ID="btnUpdate" runat="server" Text="Grabar" CssClass="btn btn-success" CommandName="Update" OnClientClick="return confirm('¿Seguro que quiere modificar los datos del cliente?');"><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-warning" CommandName="Cancel"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                    </Edititemtemplate>

                                </asp:TemplateField>
                                
                                <%-- Campos --%>
                                <asp:BoundField ReadOnly="True" DataField="idCliente" HeaderText="ID Cliente" />
                                <asp:BoundField DataField="razonSocial" HeaderText="Razon Social" />
                                <asp:BoundField DataField="CUIT" HeaderText="CUIT / CUIL" />
                                <asp:BoundField DataField="email" HeaderText="Email" />
                                <asp:BoundField DataField="Domicilio" HeaderText="Domicilio" />
                                <asp:BoundField DataField="Localidad" HeaderText="Localidad" />
                                <asp:BoundField DataField="Provincia" HeaderText="Provincia" />
                                <asp:BoundField DataField="Pais" HeaderText="Pais" />
                                <asp:BoundField DataField="CP" HeaderText="CP" />
                                <asp:BoundField DataField="SFI" HeaderText="SFI" />
                                <asp:BoundField ReadOnly="true" DataField="fechaAlta" HeaderText="Fecha de Alta" />

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                </asp:placeholder>

			</div>
		</div>
</asp:Content>				
	