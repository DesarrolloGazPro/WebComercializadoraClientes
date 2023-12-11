<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Usuarios.aspx.cs" Inherits="Usuarios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function showCompose() {
            var overlay = document.getElementById('composeOverlay');
            overlay.style.display = 'block';
        }
    </script>
    <style>
        .mb-5, .my-5 {
            margin-bottom: 1rem !important;
        }

        .mt-5, .my-5 {
            margin-top: 1rem !important;
        }

        .RadGrid {
            border-width: 1px;
            border-style: none !important;
        }

            .RadGrid div.rgHeaderWrapper {
                border-left: 0 none;
                border-right: 0 none;
                padding: 0;
                overflow: hidden;
                background-color: #0d1733 !important;
            }

        .overlay {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5);
            z-index: 9999;
        }

        .popup {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background-color: white;
            width: 900px;
            max-width: 90%;
            padding: 20px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }

        .popup-header {
            background-color: #f0f0f0;
            padding: 10px;
        }

        .popup {
            padding: 0;
            border-radius: 8px !important;
        }

            .popup > *:not(.popup-header) {
                margin: 15px 15px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row my-5">
        <div class="col-md-9">
            <h3>Usuarios</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-2">
            <asp:TextBox runat="server" ID="txtBusqueda" type="search" class="form-control" placeholder="Buscar usuarios..." aria-controls="zero-config" AutoPostBack="true" OnTextChanged="txtBusqueda_TextChanged"></asp:TextBox>
        </div>
        <div class="col-sm-1">
            <asp:LinkButton runat="server" ID="exportExcel" OnClick="BtnExportarXls_Click" CssClass="btn btn-primary">Excel</asp:LinkButton>
        </div>
        <div class="col-sm-8"></div>
        <div class="col-sm-1">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnAgregar" runat="server" type="button" class="btn btn-primary mb-2 mr-2" data-toggle="modal" data-target="#registerModal" Text="Agregar" OnClick="btnAgregar_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
        <telerik:RadGrid runat="server" ID="RadUsuarios" CssClass="table !Important" EnableEmbeddedSkins="false"
            MasterTableView-NoMasterRecordsText="No hay usuarios que mostrar." AutoGenerateColumns="false"
            datakeynames="ID" AllowAutomaticInserts="true" OnItemCommand="RadUsuarios_ItemCommand">
            <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true"><Scrolling AllowScroll="true" UseStaticHeaders="true" /></ClientSettings>
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <telerik:GridBoundColumn DataField="ID" Visible="false" DataType="System.String" FilterControlAltText="Filter ID column"
                        HeaderText="ID" ReadOnly="True" SortExpression="ID" UniqueName="ID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Nombre" Visible="true" DataType="System.String" FilterControlAltText="Filter Nombre column"
                        HeaderText="Nombre" ReadOnly="True" SortExpression="Nombre" UniqueName="Nombre">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Usuario" Visible="true" DataType="System.String" FilterControlAltText="Filter Usuario column"
                        HeaderText="Usuario" ReadOnly="True" SortExpression="Usuario" UniqueName="Usuario">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Correo" Visible="true" DataType="System.String" FilterControlAltText="Filter Correo column"
                        HeaderText="Correo" ReadOnly="True" SortExpression="Correo" UniqueName="Correo">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="esAdmin" Visible="true" DataType="System.String" FilterControlAltText="Filter esAdmin column"
                        HeaderText="Administrador" ReadOnly="True" SortExpression="esAdmin" UniqueName="esAdmin">
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Editar" FilterControlAltText="Filter Editar column"
                        HeaderText="Editar" ImageUrl="assets/img/edit.svg" Text="Editar" UniqueName="Editar" Exportable="false">
                        <ItemStyle Width="35px" CssClass="noprint" />
                        <HeaderStyle Width="35px" CssClass="noprint" />
                    </telerik:GridButtonColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" ImageUrl="assets/img/trash.svg" CommandName="Delete" HeaderText="Borrar"
                        ConfirmDialogType="RadWindow" ConfirmText="Desea Borrar el Registro?" FilterControlAltText="Filter column column"
                        UniqueName="Borrar" Exportable="false">
                        <ItemStyle Width="35px" CssClass="noprint" />
                        <HeaderStyle Width="35px" CssClass="noprint" />
                    </telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    <br />
    <%-- Ventana popup --%>
    <div id="composeOverlay" class="overlay">
        <div id="composePopup" class="popup">
            <div class="popup-header">
                <h5>Detalle usuarios</h5>
            </div>
            <asp:TextBox runat="server" ID="txtIDUsuarioModal" ReadOnly="true"></asp:TextBox>
            <br />
            <center>
                <asp:Button runat="server" ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" Width="13%"/>
                <asp:Button runat="server" ID="btnCerrar" Text="Cerrar" OnClick="btnCerrar_Click" CssClass="btn btn-secondary"/>
            </center>
        </div>
    </div>
    <%-- Ventana popup --%>
</asp:Content>

