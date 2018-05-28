<%@ Page Title="Administración de BD" Language="C#" MasterPageFile="~/container.Master" AutoEventWireup="true" CodeBehind="adminbd.aspx.cs" Inherits="FE.adminbd" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div id="fh5co-main-sistema">
        <div id="titulo-modulo">
            <div class="container">
                <asp:Label ID="lblModuloAdminBD" runat="server"  CssClass="modulo-label" Text="ADMINISTRACION DE BASE DE DATOS" />
            </div>
        </div>
		<div class="container">
			<div class="row">
				<div class="col-md-12 text-center fh5co-lead-wrap">
                    
                    <asp:LinkButton ID="btnRecalcularDVH" runat="server" CssClass="btn btn-outline btn-lg" Text="RecalcularDVH" OnClick="btnRecalcularDVH_Click"><span class="glyphicon glyphicon-sound-7-1"></span>&nbsp;Recalcular DVH</asp:LinkButton>
                    <asp:LinkButton ID="btnRecalcularDVV" runat="server" CssClass="btn btn-outline btn-lg" Text="RecalcularDVV" OnClick="btnRecalcularDVV_Click"><span class="glyphicon glyphicon-sound-7-1"></span>&nbsp;Recalcular DVV</asp:LinkButton>
                    <br />
                    <hr/>
                    <br />
                    <asp:LinkButton ID="btnBackUp" runat="server" CssClass="btn btn-outline btn-lg" Text="BackUp" OnClick="btnBackUp_Click"><span class="glyphicon glyphicon-cloud-upload"></span>&nbsp;Realizar BackUp</asp:LinkButton>
                    <br />
                    <hr/>
                    <br />
                    <label class="btn btn-outline btn-lg file-upload" >
                        <span class="glyphicon glyphicon-folder-open"> ... BKUP</span>
                        <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                    </label>
                    
                    <asp:LinkButton ID="btnRestore" runat="server" CssClass="btn btn-outline btn-lg" Text="Restore" OnClick="btnRestore_Click"><span class="glyphicon glyphicon-cloud-download"></span>&nbsp;Realizar Restore</asp:LinkButton>
                    <br />
                    <asp:Label ID="lblInfo" runat="server" />
                    
                </div>
                        
			</div>
		</div>
    </div>

</asp:Content>				
	