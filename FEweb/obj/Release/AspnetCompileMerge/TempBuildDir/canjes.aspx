<%@ Page Title="Canje de Puntos" Language="C#" MasterPageFile="~/container.Master" AutoEventWireup="true" CodeBehind="canjes.aspx.cs" Inherits="FE.canjes" EnableEventValidation="false"%>
<%@ Register src="UCNumTextBox.ascx" tagname="UCNumTextBox" tagprefix="uc1" %>
<%@ Register src="UCMail.ascx" tagname="UCMail" tagprefix="uc1" %>
<%@ Register src="UCCuit.ascx" tagname="UCCuit" tagprefix="uc1" %>
<%@ Register src="UCPrecioTextBox.ascx" tagname="UCPrecioTextBox" tagprefix="uc1" %>
<%@ Register src="UCCantGrid.ascx" tagname="UCCantGrid" tagprefix="uc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
		
        <div id="fh5co-main-sistema">
            <div id="titulo-modulo">
                <div class="container">
                    <asp:Label ID="lblModuloCanjes" runat="server"  CssClass="modulo-label" Text="ADMINISTRACION DE CANJES" />
                </div>
            </div>

            <asp:PlaceHolder id="phBotonera" runat="server">
            <div id="main-sistema-botonera">
                    <div class="container">
                        <div class="row">
					        <div class="col-md-6 text-left">
                                <asp:LinkButton ID="btnVerCarrito" runat="server" CssClass="btn btn-primary btn-mid" Text="Ver Carrito" OnClick="btnAgregar_Click"></asp:LinkButton>
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
                    <div class="col-md-12 fh5co-lead-wrap">
                        <asp:GridView ID="GridView2" runat="server" GridLines="None" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  PageSize="7" 
                            OnPageIndexChanging="GridView2_PageIndexChanging" DataKeyNames="idProducto" OnRowDeleting="GridView2_RowDeleting">

                            <Columns>
                                <%--botones de acción ver mas...--%>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">

                                    <%--Botones de grabar y cancelar la edición de registro...--%>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Quitar" CssClass="btn btn-danger" CommandName="Delete" OnClientClick="return confirm('¿Eliminar Producto?');"><span class="glyphicon glyphicon-minus"></span></asp:LinkButton>
                                        <%--<asp:LinkButton ID="btnEdit" runat="server" Text="Agregar" CssClass="btn btn-info" CommandName="Edit" CommandArgument='<%# Eval("idProducto")%>' ><span class="glyphicon glyphicon-plus"></span></asp:LinkButton>--%>
                                    </ItemTemplate>

                                    <%--Botones de grabar y cancelar la edición de registro...--%>
<%--                                    <Edititemtemplate>
                                        <asp:LinkButton ID="btnUpdate" runat="server" Text="Guardar" CssClass="btn btn-success" CommandName="Update" CommandArgument='<%# Eval("idProducto")%>'><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-warning" CommandName="Cancel"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                    </Edititemtemplate>--%>

                                </asp:TemplateField>                      
                                <asp:BoundField ReadOnly="True" DataField="idProducto" HeaderText="ID" />                                          
                                <asp:BoundField ReadOnly="True" DataField="tituloProducto" HeaderText="Título" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                <asp:BoundField ReadOnly="True" DataField="Descripcion" HeaderText="Descripcion" />
                                <asp:BoundField ReadOnly="True" DataField="Precio" HeaderText="Precio" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <asp:LinkButton ID="btnConfirmar" runat="server" CssClass="btn btn-primary btn-mid" Text="Confirmar" OnClick="btnGuardar_Click"><span class="glyphicon glyphicon-save"></span>&nbsp;Confirmar</asp:LinkButton>
                    <asp:LinkButton ID="btnCerrar" runat="server" CssClass="btn btn-primary btn-mid" Text="Cerrar" OnClick="btnCerrar_Click"><span class="glyphicon glyphicon-remove"></span>&nbsp;Cerrar</asp:LinkButton>
                </div>
                </asp:placeholder>
                <asp:PlaceHolder id="phLista" runat="server">
                <div class="row contenedor-bloque">
					<div class="col-md-12 fh5co-lead-wrap">
                        <asp:GridView ID="GridView1" runat="server" GridLines="None" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  PageSize="7" 
                            OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="idProducto" OnRowCommand="GridView1_RowCommand" OnRowUpdating="GridView1_RowUpdating" 
                            OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit">

                            <Columns>
                                <%--CheckBox para seleccionar varios registros...--%>
<%--                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEliminar" runat="server" AutoPostBack="true" OnCheckedChanged="chk_OnCheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>      --%>      

                                <%--botones de acción ver mas...--%>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">

                                    <%--Botones de grabar y cancelar la edición de registro...--%>
                                    <ItemTemplate>
                                        <%--<asp:LinkButton ID="btnVer" runat="server" Text="Ver" CssClass="btn btn-success" CommandName="Ver" CommandArgument='<%# Eval("idProducto")%>' ><span class="glyphicon glyphicon-eye-open"></span></asp:LinkButton>--%>
                                        <asp:LinkButton ID="btnEdit" runat="server" Text="Agregar" CssClass="btn btn-info" CommandName="Edit" CommandArgument='<%# Eval("idProducto")%>' ><span class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                                    </ItemTemplate>

                                    <%--Botones de grabar y cancelar la edición de registro...--%>
                                    <Edititemtemplate>
                                        <uc1:UCCantGrid ID="txtCantidad" runat="server" HeaderText="Cantidad"/>
                                        <asp:LinkButton ID="btnUpdate" runat="server" Text="Guardar" CssClass="btn btn-success" CommandName="Update" CommandArgument='<%# Eval("idProducto")%>'><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-warning" CommandName="Cancel"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                        
                                    </Edititemtemplate>

                                </asp:TemplateField>
                                
                                <%-- Campos --%>
                                <asp:BoundField ReadOnly="True" DataField="idProducto" HeaderText="ID Producto" />
                                <asp:BoundField ReadOnly="True" DataField="tituloProducto" HeaderText="Título" />
                                <asp:BoundField ReadOnly="True" DataField="Descripcion" HeaderText="Descripcion" />
                                <asp:BoundField ReadOnly="True" DataField="tipoProducto" HeaderText="Tipo de Producto" />
                                <asp:BoundField ReadOnly="True" DataField="Marca" HeaderText="Marca" />
                                <asp:BoundField ReadOnly="True" DataField="Categoria" HeaderText="Categoria" />
                                <asp:BoundField ReadOnly="True" DataField="Precio" HeaderText="Precio" />
                                
                                <asp:TemplateField HeaderText="Imagen">

                                    <ItemTemplate>        
                                                                    
                                        <asp:Image id="imgProducto" runat="server" Height="100px" Width="100px" ImageUrl='<%# "~/productos/" + Eval("picture")%>' />
                                 <%-- Continuar con el agegar al carrito de compras --%>                                                                
                                    </ItemTemplate>                                                                                                                                              
                                </asp:TemplateField>    
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                </asp:placeholder>
			</div>
		</div>
</asp:Content>				
	