<%@ Page Title="Main" Language="C#" MasterPageFile="~/container.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="FE.main" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">

        <!-- Sistema -->
    <div id="fh5co-main-sistema">
        <div id="titulo-modulo">
            <div class="container">
                <asp:Label ID="lblModuloMain" runat="server"  CssClass="modulo-label" Text="VISION GENERAL" />
            </div>
        </div>
		<div class="container">
			<div class="text-center">

                <br><br><br><br><br>
                <h2><asp:Label ID="lblBienvenido" runat="server"  CssClass="st-label" Text="Bienvenido!" /><span></span></h2>
                <br><br>
                <asp:Label ID="lblAcceda" runat="server"  CssClass="st-label" Text="Acceda desde el menú a las distintas opciones" />
                <br><br><br><br><br><br>
			</div>
		</div>
    </div>
</asp:Content>				
	