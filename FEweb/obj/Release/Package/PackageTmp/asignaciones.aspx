<%@ Page Title="Asignación de Puntos" Language="C#" MasterPageFile="~/container.Master" AutoEventWireup="true" CodeBehind="asignaciones.aspx.cs" Inherits="FE.asignaciones" EnableEventValidation="false"%>
<%@ Register src="UCNumTextBox.ascx" tagname="UCNumTextBox" tagprefix="uc1" %>
<%@ Register src="UCMail.ascx" tagname="UCMail" tagprefix="uc1" %>
<%@ Register src="UCCuit.ascx" tagname="UCCuit" tagprefix="uc1" %>
<%@ Register src="UCPrecioTextBox.ascx" tagname="UCPrecioTextBox" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
		
        <div id="fh5co-main-sistema">
            <div id="titulo-modulo">
                <div class="container">
                    <asp:Label ID="lblModuloAsignaciones" runat="server"  CssClass="modulo-label" Text="ADMINISTRACION DE ASIGNACIONES" />
                </div>
            </div>

            <asp:PlaceHolder id="phBotonera" runat="server">
            <div id="main-sistema-botonera">
                    <div class="container">
                        <div class="row">
					        <div class="col-md-6 text-left">
                                <asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-primary btn-mid" Text="Nueva Asignación" OnClick="btnAgregar_Click"><span class="glyphicon glyphicon-plus"></span>&nbsp;Agregar</asp:LinkButton>
                            </div>
                            <div class="col-md-6 text-right">
                                <asp:Label ID="lblCant" runat="server" CssClass="botonera-label" Text="" />
                            </div>  
                        </div>
                    </div>
            </div>
            </asp:PlaceHolder>
            <div class="container">
                <asp:Label ID="lblInfo" runat="server" />
                <asp:PlaceHolder id="phAlta" runat="server">
                <div id="contenedor-bloque">
				    <div class="row">
					    <div class="col-md-6 fh5co-lead-wrap">
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblCliente" runat="server"  CssClass="st-label" Text="Cliente:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:DropDownList ID="lstCliente" runat="server" cssclass="input-dropdownlist"/>
                                </div>                                                                                            
                            </div>

                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblConsumidor" runat="server"  CssClass="st-label" Text="Consumidor:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:DropDownList ID="lstConsumidor" runat="server" cssclass="input-dropdownlist"/>
                                </div>                                                                                            
                            </div>

                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblCantPuntos" runat="server"  CssClass="st-label" Text="Cantidad de Puntos:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <uc1:UCNumTextBox ID="txtCantPuntos" runat="server"/>
                                </div>                
                            </div>

                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblMotivo" runat="server"  CssClass="st-label" Text="Motivo:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtMotivo" runat="server" CssClass="input-textbox" />
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
                <asp:PlaceHolder id="phLista" runat="server">
                <div class="row contenedor-bloque">
					<div class="col-md-12 fh5co-lead-wrap">
                        <asp:GridView ID="GridView1" runat="server" GridLines="None" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  PageSize="20" 
                            OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="idCliente, idCustomer, idComprobante">

                            <Columns>
                                
                                <%-- Campos --%>
                                <asp:BoundField ReadOnly="True" DataField="idCliente" HeaderText="Cliente" />
                                <asp:BoundField ReadOnly="True" DataField="idCustomer" HeaderText="Consumidor" />
                                <asp:BoundField ReadOnly="True" DataField="idComprobante" HeaderText="ID Comprobante" />
                                <asp:BoundField ReadOnly="True" DataField="cantidad" HeaderText="Cantidad" />
                                <asp:BoundField ReadOnly="True" DataField="accion" HeaderText="Acción" />
                                <asp:BoundField ReadOnly="True" DataField="observaciones" HeaderText="Comentario" />
                                <asp:BoundField ReadOnly="True" DataField="fechaHora" HeaderText="Fecha" />

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                </asp:placeholder>

			</div>
		</div>
</asp:Content>				
	