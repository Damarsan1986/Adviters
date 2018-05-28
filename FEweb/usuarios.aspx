<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/container.Master" AutoEventWireup="true" CodeBehind="usuarios.aspx.cs" Inherits="FE.usuarios" EnableEventValidation="false"%>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
		
        <div id="fh5co-main-sistema">
            <div id="titulo-modulo">
                <div class="container">
                    <asp:Label ID="lblModuloUsuarios" runat="server"  CssClass="modulo-label" Text="ADMINISTRACION DE USUARIOS" />
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
                <asp:PlaceHolder id="phAltaUsuario" runat="server">
                <div id="contenedor-bloque">
				    <div class="row text-left">
					    <div class="col-md-8 fh5co-lead-wrap">
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:RegularExpressionValidator ID="REV_Nombre" runat="server" ControlToValidate="txtNombre" ForeColor="Red" Font-Bold="true" ErrorMessage="[a-z] --> "  ValidationExpression="^[a-zA-Z'\s]{1,50}$" ValidationGroup="formato" />
                                    <asp:Label ID="lblNombre" runat="server"  CssClass="st-label" Text="Nombre:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:RegularExpressionValidator ID="REV_Apellido" runat="server" ControlToValidate="txtApellido" ForeColor="Red" Font-Bold="true" ErrorMessage="[a-z] --> "  ValidationExpression="^[a-zA-Z'\s]{1,50}$" ValidationGroup="formato" />
                                    <asp:Label ID="lblApellido" runat="server"  CssClass="st-label" Text="Apellido:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="input-textbox"/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:RegularExpressionValidator ID="REV_idUsuario" runat="server" ControlToValidate="txtIdUsuario" ForeColor="Red" Font-Bold="true" ErrorMessage="[a-z/0-9] --> "  ValidationExpression="^[a-zA-Z0-9]{1,20}$" ValidationGroup="formato"/>
                                    <asp:Label ID="lblIdUsuario" runat="server"  CssClass="st-label" Text="IdUsuario:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="input-textbox"/>
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblClave" runat="server"  CssClass="st-label" Text="Clave:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtClave" runat="server" TextMode="Password" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblClaveRepetir" runat="server"  CssClass="st-label" Text="Repetir Clave:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtClaveRepetir" TextMode="Password" runat="server" CssClass="input-textbox" />
                                </div>                
                            </div>
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:RegularExpressionValidator ID="REV_Email" runat="server" ControlToValidate="txtEmail" ForeColor="Red" Font-Bold="true" ErrorMessage="[error] --> "  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="formato" />
                                    <asp:Label ID="lblEmail" runat="server" CssClass="st-label" Text="Email:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="input-textbox"/>
                                </div>     
       
                            </div>  

                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblPerfil" runat="server"  CssClass="st-label" Text="Perfil:"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:DropDownList ID="lstPerfil" runat="server" OnTextChanged="lstPerfil_TextChanged" AutoPostBack="true" cssclass="input-dropdownlist"/>
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
                            <div class="row">
                                <div class="col-md-4 text-right">
                                    <asp:Label ID="lblBloqueado" runat="server"  CssClass="st-label" Text="Bloqueado?"/>
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:CheckBox ID="chkBloqueado" runat="server" cssclass="input-checkbox"/>
                                </div>       
                            </div>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-mid" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="formato" CausesValidation="true"><span class="glyphicon glyphicon-save"></span>&nbsp;Guardar</asp:LinkButton>
                    <asp:LinkButton ID="btnCerrar" runat="server" CssClass="btn btn-primary btn-mid" Text="Cerrar" OnClick="btnCerrar_Click"><span class="glyphicon glyphicon-remove"></span>&nbsp;Cerrar</asp:LinkButton>
                </div>
                </asp:placeholder>

                <asp:PlaceHolder id="phListaUsuarios" runat="server">
                <div class="row contenedor-bloque">
					<div class="col-md-12 fh5co-lead-wrap">
                        <asp:Label ID="lblInfo" runat="server" />
                        <asp:GridView ID="GridView1" runat="server" GridLines="None" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  PageSize="7" 
                            OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="idUsuario" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" 
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
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Quitar" CssClass="btn btn-danger" CommandName="Delete" OnClientClick="return confirm('¿Eliminar Usuario?');"><span class="glyphicon glyphicon-minus"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnEdit" runat="server" Text="Editar" CssClass="btn btn-info" CommandName="Edit"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                    </ItemTemplate>

                                    <%--Botones de grabar y cancelar la edición de registro...--%>
                                    <Edititemtemplate>
                                        <asp:LinkButton ID="btnUpdate" runat="server" Text="Grabar" CssClass="btn btn-success" CommandName="Update" OnClientClick="return confirm('¿Seguro que quiere modificar los datos del Usuario?');"><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-warning" CommandName="Cancel"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                    </Edititemtemplate>

                                </asp:TemplateField>
                                
                                <%-- Campos --%>
                                <asp:BoundField ReadOnly="True" DataField="idUsuario" HeaderText="Usuario" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="IntentosFallidos" HeaderText="Intentos Fallidos" />

                                <asp:TemplateField HeaderText="Perfil">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="lstPerfilTabla" runat="server" DataTextField="Descripcion" DataValueField="Nombre" SelectedItem='<%# Bind("Perfil.Nombre") %>'></asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPerfilTabla" runat="server" Text='<%# Eval("Perfil.Nombre") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cultura">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="lstCultura" runat="server" DataTextField="descripcion" DataValueField="idCUltura" SelectedItem='<%# Bind("Cultura") %>'></asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCultura" runat="server" Text='<%# Eval("Cultura") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:CheckBoxField DataField="Bloqueado" HeaderText="Bloqueado?" />


                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                </asp:placeholder>
                                <asp:PlaceHolder ID="phListaPermisos" runat="server">
                                                <div class="row">
                                <asp:GridView ID="GridView2" runat="server" GridLines="None" AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  PageSize="7" 
                                DataKeyNames="nombre" OnPageIndexChanging="GridView2_PageIndexChanging">

                                    <Columns>
                                         <%--CheckBox para seleccionar varios registros...--%>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSeleccionar" runat="server" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <%-- Campos --%>
                                        <asp:BoundField ReadOnly="True" DataField="nombre" HeaderText="Nombre Permiso" />
                                        <asp:BoundField ReadOnly="True" DataField="Descripcion" HeaderText="Descripción" />
                                        <asp:CheckBoxField ReadOnly="True" DataField="esAccion" HeaderText="Acción?" />

                                    </Columns>
                                </asp:GridView>
                            </div>

                </asp:PlaceHolder>
			</div>
		</div>
</asp:Content>				
	