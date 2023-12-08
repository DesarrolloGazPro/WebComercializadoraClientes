<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RestructuracionLitros.aspx.cs" Inherits="RestructuracionLitros" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .mb-5, .my-5 {
            margin-bottom: 1rem !important;
        }

        .mt-5, .my-5 {
            margin-top: 1rem !important;
        }

        .RadCalendar .rcMain {
            background-color: white !important;
            padding: 0;
            width: auto;
            border-width: 0 1px 1px;
            border-style: solid;
            border-color: inherit;
            *zoom: 1;
        }

        .RadCalendar .rcRow th {
            text-align: center;
            border: 0;
            padding: 0;
            font-weight: normal;
            vertical-align: middle;
            cursor: default;
            background-color: #d3dde7 !important;
        }

        .RadCalendar .rcTitlebar {
            background-color: white !important;
            border-style: solid;
            border-width: 1px;
            text-align: center;
            padding: 4px;
            *zoom: 1;
        }

        .RadCalendar .rcRow a, .RadCalendar .rcRow span {
            display: inline-block;
            border: 1px solid #000 !important;
            border-radius: 3px;
            padding: 4px;
            width: 1.42857143em;
            text-decoration: none;
            outline: 0;
            background-color: lightgray !important;
        }

        .RadCalendarMonthView {
            line-height: 1.42857143;
            table-layout: auto;
            border-collapse: separate;
            border-style: solid;
            border-width: 1px;
            border-spacing: 0;
            padding: 4px;
            background-color: whitesmoke !important;
        }

            .RadCalendarMonthView .rcButtons a {
                color: #f8f9fa !important;
                display: inline-block;
                vertical-align: top;
                min-width: 64px;
                box-sizing: border-box;
                background-color: #0d1733 !important;
            }

            .RadCalendarMonthView a {
                display: block;
                padding: 4px 10px;
                text-align: center;
                text-decoration: none;
                border: 1px solid #e1dcdc !important;
                border-radius: 3px;
                color: inherit;
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

        @media print {
            .noprint {
                background-color: black;
                display: none;
            }
        }
        .row {
            margin-top: -20px;
            margin-bottom: -20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row my-5">
        <div class="col-md-9">
            <h3>Restitución de litros</h3>
        </div>
        <div class="col-md-3 text-right">
            <asp:LinkButton runat="server" ID="exportExcel" OnClick="BtnExportarXls_Click" CssClass="btn btn-primary" Width="25%">Excel</asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="form-group filters col-md-2">
            <label for="exampleFormControlSelect1" style="margin-top:18px">Fecha Inicial</label>
            <telerik:RadDatePicker RenderMode="Lightweight" ID="txtFecha" runat="server" CssClass="table table-hover !important" EnableEmbeddedSkins="false">
                <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"></DateInput>
            </telerik:RadDatePicker>
        </div>
        <div class="form-group filters col-md-2">
            <label for="exampleFormControlSelect1" style="margin-top:18px">Fecha Final</label>
            <telerik:RadDatePicker RenderMode="Lightweight" ID="txtFechaFin" runat="server" CssClass="table table-hover !important" EnableEmbeddedSkins="false">
                <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"></DateInput>
            </telerik:RadDatePicker>
        </div>
        <div class="form-group filters col-md-1">
            <asp:LinkButton runat="server" ID="btnGenerar" CssClass="btn btn-primary" OnClick="btnGenerar_Click"  style="margin-top:15px">Consultar</asp:LinkButton>
        </div>
        <div class="form-group filters col-md-2">
            <asp:Label ID="lblTotal" runat="server" Text="Total registros:" style="margin-top:8px"></asp:Label><asp:Label ID="lblRegistros" runat="server" Text="" style="margin-top:8px"></asp:Label>
        </div>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <telerik:RadGrid runat="server" ID="RadDevolucion" CssClass="table table-hover !important" EnableEmbeddedSkins="false"
                MasterTableView-NoMasterRecordsText="No hay devoluciones que mostrar." AutoGenerateColumns="false"
                AllowAutomaticInserts="true">
                <ClientSettings Selecting-AllowRowSelect="true">
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <MasterTableView>
                    <Columns>
                        <telerik:GridBoundColumn DataField="Fecha" Visible="true" DataType="System.String" FilterControlAltText="Filter Fecha column"
                            HeaderText="Fecha" ReadOnly="True" SortExpression="Fecha" UniqueName="Fecha">
                            <HeaderStyle Wrap="true" Width="100px" />
                            <ItemStyle Wrap="true" Width="100px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PermisoCre" Visible="true" DataType="System.String" FilterControlAltText="Filter PermisoCre column"
                            HeaderText="PermisoCre" ReadOnly="True" SortExpression="PermisoCre" UniqueName="PermisoCre">
                            <HeaderStyle Wrap="true" Width="200px" />
                            <ItemStyle Wrap="true" Width="200px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Comprobante" Visible="true" DataType="System.String" FilterControlAltText="Filter Comprobante column"
                            HeaderText="Comprobante" ReadOnly="True" SortExpression="Comprobante" UniqueName="Comprobante">
                            <HeaderStyle Wrap="true" Width="200px" />
                            <ItemStyle Wrap="true" Width="200px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Remision" Visible="true" DataType="System.String" FilterControlAltText="Filter Remision column"
                            HeaderText="Remision" ReadOnly="True" SortExpression="Remision" UniqueName="Remision">
                            <HeaderStyle Wrap="true" Width="200px" />
                            <ItemStyle Wrap="true" Width="200px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Producto" Visible="true" DataType="System.String" FilterControlAltText="Filter Producto column"
                            HeaderText="Producto" ReadOnly="True" SortExpression="Producto" UniqueName="Producto">
                            <HeaderStyle Wrap="true" Width="200px" />
                            <ItemStyle Wrap="true" Width="200px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="VolumenComprobante" Visible="true" DataType="System.String" FilterControlAltText="Filter VolumenComprobante column"
                            HeaderText="Volumen(Carta porte)" ReadOnly="True" SortExpression="VolumenComprobante" UniqueName="VolumenComprobante">
                            <HeaderStyle Wrap="true" Width="200px" />
                            <ItemStyle Wrap="true" Width="200px" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="VolumenRestituido" Visible="true" DataType="System.String" FilterControlAltText="Filter VolumenRestituido column"
                            HeaderText="Volumen(Restituido)" ReadOnly="True" SortExpression="VolumenRestituido" UniqueName="VolumenRestituido">
                            <HeaderStyle Wrap="true" Width="200px" />
                            <ItemStyle Wrap="true" Width="200px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Destino" Visible="true" DataType="System.String" FilterControlAltText="Filter Destino column"
                            HeaderText="Destino" ReadOnly="True" SortExpression="Destino" UniqueName="Destino">
                            <HeaderStyle Wrap="true" Width="200px" />
                            <ItemStyle Wrap="true" Width="200px" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

