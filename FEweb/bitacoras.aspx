<%@ Page Title="Bitacoras" Language="C#" MasterPageFile="~/container.Master" AutoEventWireup="true" CodeBehind="bitacoras.aspx.cs" Inherits="FE.bitacoras" EnableEventValidation="false"%>
<%@ Register src="UCCalendar.ascx" tagname="UCCalendar" tagprefix="uc1" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">

<div id="fh5co-main-sistema">
    <div id="titulo-modulo">
        <div class="container">
            <asp:Label ID="lblModuloBitacoras" runat="server"  CssClass="modulo-label" Text="BITACORAS" />
        </div>
    </div>
	<div class="container">
		<div class="row">
            <asp:Label ID="lblInfo" runat="server"  CssClass="st-label" />
            <div class="col-md-12 fh5co-lead-wrap">                
                <div class="col-md-6">
                    
                    <div class="row">
                        <div class="col-md-3 text-right">
                            <asp:Label ID="lblUsuario" runat="server"  CssClass="st-label" Text="Usuario:"/>
                        </div>
                        <div class="col-md-4 text-left">
                            <asp:TextBox ID="txtIdUsuario" runat="server" OnTextChanged="txtIdUsuario_TextChanged" AutoPostBack="True" CssClass="input-textbox" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 text-right">
                            <asp:Label ID="lblDescripcion" runat="server"  CssClass="st-label" Text="Descripción:"/>
                        </div>
                        <div class="col-md-4 text-left">
                            <asp:TextBox ID="txtDescripcion" runat="server" OnTextChanged="txtDescripcion_TextChanged" AutoPostBack="True" CssClass="input-textbox" />
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        
                        <div class="col-md-6 text-right">
                            <asp:Label ID="lblFecDesde" runat="server"  CssClass="st-label" Text="Fecha Desde (yyyy/mm/dd):"/>
                        </div>
                        <div class="col-md-3 text-left">
                            <uc1:UCCalendar ID="txtFecD" runat="server"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 text-right">
                            <asp:Label ID="lblFecHasta" runat="server"  CssClass="st-label" Text="Fecha Hasta (yyyy/mm/dd):"/>
                        </div>
                        <div class="col-md-3 text-left">
                            <uc1:UCCalendar ID="txtFecH" runat="server" />
                        </div>
                    </div>

				</div>        
                    <div class="row">
                        <div class="col-md-4 text-center">
                            <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-primary btn-mid" Text="Filtrar Bitácora" OnClick="btnFiltrar_Click"></asp:LinkButton>
                        </div>
                        <div class="col-md-4 text-center">
                            <asp:LinkButton ID="btnMigrar" runat="server" CssClass="btn btn-primary btn-mid" Text="Migrar Previo a Fecha Desde" OnClick="btnMigrar_Click"></asp:LinkButton>
                        </div>
                        <div class="col-md-4 text-center">
                            <asp:LinkButton ID="btnDesmigrar" runat="server" CssClass="btn btn-primary btn-mid" Text="Desmigrar Fecha Desde/Hasta" OnClick="btnDesmigrar_Click"></asp:LinkButton>
                        </div>
                    </div>            
                <asp:GridView ID="GridView1" runat="server" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  PageSize="50" 
                        OnPageIndexChanging="GridView1_PageIndexChanging">

                    <Columns>
                            <asp:BoundField ReadOnly="True" DataField="idBitacora" HeaderText="ID" />
                            <asp:BoundField ReadOnly="True" DataField="IdUsuario" HeaderText="Usuario" />
                            <asp:BoundField ReadOnly="True" DataField="Descripcion" HeaderText="Descripcion" />
                            <asp:BoundField ReadOnly="True" DataField="FechaHoraEvento" HeaderText="FechaHora Evento" />
                    </Columns>

                </asp:GridView>
            </div>
        </div>
	</div>
</div>
</asp:Content>
