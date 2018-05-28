<%@ Page Title="Main" Language="C#" MasterPageFile="~/container.Master" AutoEventWireup="true" CodeBehind="rproveedores.aspx.cs" Inherits="FE.rproveedores" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">

        <!-- Sistema -->
    <div id="fh5co-main-sistema">
        <div id="titulo-modulo">
            
            <div class="container">
                <asp:Label ID="lblModuloRProductos" runat="server"  CssClass="modulo-label" Text="REPORTE DE PRODUCTOS VIGENTES" />
            </div>
        </div>


		<div class="container">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"></rsweb:ReportViewer>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
		</div>
    </div>
</asp:Content>				
	