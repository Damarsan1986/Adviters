<%@ Page Title="Main" Language="C#" MasterPageFile="~/container.Master" AutoEventWireup="true" CodeBehind="forbidden.aspx.cs" Inherits="FE.forbidden" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">

        <!-- Sistema -->
    <div id="fh5co-main-sistema">
        <div id="titulo-modulo">
            <div class="container">
                <asp:Label ID="lblModuloForbidden" runat="server"  CssClass="modulo-label" Text="ERROR 403!" />
            </div>
        </div>
		<div class="container">
			<div class="text-center">

                <br><br><br><br><br>
                <h2><asp:Label ID="lblDenegado" runat="server"  CssClass="st-label" Text="ACCESO DENEGADO!" /><span></span></h2>
                <br><br>
                <asp:Label ID="lblAcceda" runat="server"  CssClass="st-label" Text="Tus credenciales no te permiten el acceso a la página que intentabas acceder" />
                <br><br><br><br><br><br>
			</div>
		</div>
    </div>
</asp:Content>				
	