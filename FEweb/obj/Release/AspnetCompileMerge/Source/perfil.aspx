<%@ Page Title="Perfil del Usuario" Language="C#" MasterPageFile="~/container.Master" AutoEventWireup="true" CodeBehind="perfil.aspx.cs" Inherits="FE.perfil" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div id="fh5co-main-sistema">
        <div id="titulo-modulo">
            <div class="container">
                <asp:Label ID="lblModuloPerfil" runat="server"  CssClass="modulo-label" Text="ADMINISTRACION DEL PERFIL" />
            </div>
        </div>

        <div class="container">
            <div id="contenedor-bloque">
			    <div class="row">
				    <div class="col-md-6 fh5co-lead-wrap">
                        <div class="row">
                            <div class="col-md-4 text-right">
                                <asp:Label ID="lblUsuario" runat="server" CssClass="st-label" Text="Usuario:"/>
                            </div>
                            <div class="col-md-8 text-left">
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="input-textbox" />
                            </div>                
                        </div>
                        <div class="row">
                            <div class="col-md-4 text-right">
                                <asp:Label ID="lblNombre" runat="server" CssClass="st-label" Text="Nombre:"/>
                            </div>
                            <div class="col-md-8 text-left">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="input-textbox" />
                            </div>                
                        </div>
                        <div class="row">
                            <div class="col-md-4 text-right">
                                <asp:Label ID="lblApellido" runat="server" CssClass="st-label" Text="Apellido:"/>
                            </div>
                            <div class="col-md-8 text-left">
                                <asp:TextBox ID="txtApellido" runat="server" CssClass="input-textbox" />
                            </div>                
                        </div>                                            
                        <div class="row">
                            <div class="col-md-4 text-right">
                                <asp:Label ID="lblEmail" runat="server" CssClass="st-label" Text="Email:"/>
                            </div>
                            <div class="col-md-8 text-left">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="input-textbox" />
                            </div>                
                        </div>
                        <div class="row">
                            <div class="col-md-4 text-right">
                                <asp:Label ID="lblClave" runat="server" CssClass="st-label" Text="Contraseña:"/>
                            </div>
                            <div class="col-md-8 text-left">
                                <asp:TextBox ID="txtClave" runat="server" TextMode="Password" CssClass="input-textbox" />
                            </div>                
                        </div>
                        <div class="row">
                            <div class="col-md-4 text-right">
                                <asp:Label ID="lblClave2" runat="server" CssClass="st-label" Text="Repetir Contraseña:"/>
                            </div>
                            <div class="col-md-8 text-left">
                                <asp:TextBox ID="txtClave2" runat="server" TextMode="Password" CssClass="input-textbox" />
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
        </div>
        <asp:Label ID="lblinfo" runat="server" Text=""></asp:Label>

                            
    </div>

</asp:Content>
