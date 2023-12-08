<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Destinos.aspx.cs" Inherits="Destinos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row my-5">
        <div class="col-md-9">
            <h3>Destinos</h3>
        </div>
    </div>
    <div class="table-responsive mb-4 mt-4" id="div1" runat="server">
        <div id="zero-config_filter" class="dataTables_filter">
            <span class="noprint">
                <table>
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="exportExcel" OnClick="exportExcel_Click" CssClass="btn btn-primary">Excel</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </span>
        </div>
        <telerik:RadGrid runat="server" ID="RadDestinos" CssClass="table table-hover !important" EnableEmbeddedSkins="false"
            MasterTableView-NoMasterRecordsText="No hay destinos que mostrar." AutoGenerateColumns="false"
            DataKeyNames="ID" AllowAutomaticInserts="true">
            <ClientSettings Selecting-AllowRowSelect="true">
                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <telerik:GridBoundColumn DataField="ID" Visible="false" DataType="System.String" FilterControlAltText="Filter ID column"
                        HeaderText="ID" ReadOnly="True" SortExpression="ID" UniqueName="ID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Destino" Visible="true" DataType="System.String" FilterControlAltText="Filter Destino column"
                        HeaderText="Destino" ReadOnly="True" SortExpression="Destino" UniqueName="Destino">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>

