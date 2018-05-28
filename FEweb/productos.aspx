<%@ Page Title="Productos" Language="C#" MasterPageFile="~/container.Master" AutoEventWireup="true" CodeBehind="productos.aspx.cs" Inherits="FE.Productos" EnableEventValidation="false"%>
<%@ Register src="UCNumTextBox.ascx" tagname="UCNumTextBox" tagprefix="uc1" %>
<%@ Register src="UCMail.ascx" tagname="UCMail" tagprefix="uc1" %>
<%@ Register src="UCCuit.ascx" tagname="UCCuit" tagprefix="uc1" %>
<%@ Register src="UCPrecioTextBox.ascx" tagname="UCPrecioTextBox" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
		
        <div id="fh5co-main-sistema">
            <div id="titulo-modulo">
                <div class="container">
                    <asp:Label ID="lblModuloProductos" runat="server"  CssClass="modulo-label" Text="ADMINISTRACION DE PRODUCTOS" />
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
                    <div class="col-md-6 fh5co-lead-wrap">
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblIdProducto" runat="server"  CssClass="st-label" Text="ID Producto:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtIdProducto" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblTitulo" runat="server"  CssClass="st-label" Text="Título:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblDescripcion" runat="server"  CssClass="st-label" Text="Descripción:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblTipo" runat="server"  CssClass="st-label" Text="Tipo:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtTipo" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblMarca" runat="server"  CssClass="st-label" Text="Marca:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtMarca" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblCategoria" runat="server"  CssClass="st-label" Text="Categoría:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtCategoria" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblPrecio" runat="server"  CssClass="st-label" Text="Precio:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <uc1:UCPrecioTextBox ID="txtPrecio" runat="server"/>
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblStockMinimo" runat="server"  CssClass="st-label" Text="Stock Mínimo:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <uc1:UCNumTextBox ID="txtStockMinimo" runat="server"/>
                                </div>                
                            </div>                                                 
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblStockMaximo" runat="server"  CssClass="st-label" Text="Stock Máximo:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <uc1:UCNumTextBox ID="txtStockMaximo" runat="server"/>
                                </div>                
                            </div>
                        </div>

                    <div class="col-md-6 fh5co-lead-wrap">
                        <div class="row">
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <asp:Image ID="imgProducto" runat="server" CssClass="input-image"/>
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <label class="btn btn-discreto file-upload-slim">
                                        <span class="glyphicon glyphicon-folder-open">...</span>
                                        <asp:FileUpload ID="FileUpload1" runat="server" onchange="javascript:submit();"></asp:FileUpload></label>
                                        <asp:Label ID="lblimg" runat="server"  CssClass="st-label" Visible="false" Text="IMAGEN"/>
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
                        <asp:GridView ID="GridView1" runat="server" GridLines="None" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  PageSize="7" 
                            OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="idProducto" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" 
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

                                    <%--Botones de eliminar y editar Producto...--%>

                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Quitar" CssClass="btn btn-danger" CommandName="Delete" OnClientClick="return confirm('¿Eliminar Producto?');"><span class="glyphicon glyphicon-minus"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnEdit" runat="server" Text="Editar" CssClass="btn btn-info" CommandName="Edit"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                    </ItemTemplate>

                                    <%--Botones de grabar y cancelar la edición de registro...--%>
                                    <Edititemtemplate>
                                        <asp:LinkButton ID="btnUpdate" runat="server" Text="Grabar" CssClass="btn btn-success" CommandName="Update" OnClientClick="return confirm('¿Seguro que quiere modificar los datos del Producto?');"><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-warning" CommandName="Cancel"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                    </Edititemtemplate>

                                </asp:TemplateField>
                                
                                <%-- Campos --%>
                                <asp:BoundField ReadOnly="True" DataField="idProducto" HeaderText="ID Producto" />
                                <asp:BoundField DataField="tituloProducto" HeaderText="Título" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                <asp:BoundField DataField="tipoProducto" HeaderText="Tipo de Producto" />
                                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" />
                                <asp:BoundField DataField="StockMaximo" HeaderText="Stock Max" />
                                <asp:BoundField DataField="StockMinimo" HeaderText="Stock Min" />
                                
                                <asp:TemplateField HeaderText="Imagen">
                                    <ItemTemplate>                                    
                                        <asp:Image id="imgProducto" runat="server" Height="50px" Width="50px" ImageUrl='<%# "~/productos/" + Eval("picture")%>' />
                                                                                                 
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
	