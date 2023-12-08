<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DetalleFacturacion.aspx.cs" Inherits="DetalleFacturacion2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .justify-content-between {
            -ms-flex-pack: justify !important;
            justify-content: space-between !important;
            margin: 0px !important;
        }

        .RadGrid .rgRow > td, .RadGrid .rgAltRow > td, .RadGrid .rgEditRow > td, .RadGrid .rgFooter > td {
            /* padding-top: 4px; */
            /* padding-bottom: 3px; */
        }

        .RadGrid .rgRow > td, .RadGrid .rgAltRow > td, .RadGrid .rgEditRow > td, .RadGrid .rgFooter > td, .RadGrid .rgFilterRow > td, .RadGrid .rgHeader, .RadGrid .rgResizeCol, .RadGrid .rgGroupHeader td {
            /* padding-left: 7px; */
            /* padding-right: 7px; */
        }

        .RadGrid .rgHeader, .RadGrid th.rgResizeCol {
            text-align: left;
            font-weight: 700 !important;
        }

        .RadGrid .rgMasterTable .rgSelectedCell, .RadGrid .rgSelectedRow > td, .RadGrid td.rgEditRow .rgSelectedRow, .RadGrid .rgSelectedRow td.rgSorted {
            color: #fff;
            background: #2196f3 !important;
            border-color: #fff;
        }

        .RadGrid {
            border-width: 1px;
            border-style: none !important;
        }

        .table thead th {
            color: #000;
        }

        .table > thead > tr > th {
            color: #000;
            font-weight: 700 !important;
            font-size: 13px !important;
            border: none;
            letter-spacing: 1px;
            text-transform: uppercase;
        }

        .table td, .table th {
            padding: .75rem !important;
            vertical-align: top;
            border-top: 1px solid #dee2e6;
            color: #888ea8;
        }

        @media print {
            .noprint {
                background-color: black;
                display: none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-header">
        <nav class="breadcrumb-one" aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="default.aspx">Regresar a lista</a></li>
            </ol>
        </nav>
    </div>
    <div class="row invoice layout-spacing">
        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
            <div class="doc-container">
                <div class="row">
                    <div class="col-xl-12">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div style="align-content: center" class="widget-content widget-content-area br-6" runat="server" id="divLista">
                                </div>
                                <%--<table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%"></td>
                                        <td style="width: 50%; text-align: right">
                                            <img alt="logo" src="assets/img/pemex.svg" style="width: 150px; height: 50px">
                                        </td>
                                    </tr>
                                </table>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

