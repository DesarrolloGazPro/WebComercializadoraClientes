<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset='utf-8'>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
    <title>GAZPRO</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link rel="stylesheet" href="assets/css/bootstrap.min.css">    
    <link rel="stylesheet" href="assets/css/styles.css">  
    <style>
.mb-5, .my-5 {
    margin-bottom: 1rem!important;
}
.mt-5, .my-5 {
    margin-top: 1rem!important;
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
    </style>             
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <header id="banner">    
        <div class="container-fluid">
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
          <div class="flex-logo">              
            <a class="navbar-brand" href="default.aspx"></a>
            <!---->
          </div>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
              <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
              <ul class="navbar-nav">
                <li class="nav-item active">
                  <a class="nav-link" href="#"><span class="icon home"></span>Inicio <span class="sr-only">(current)</span></a>
                </li>
                <li class="nav-item" id="menuReporte" runat="server">
                  <a class="nav-link" href="#"><span class="icon gas"></span>Restructuración de litros</a>
                </li>                               
                <li class="nav-item" id="menuConfiguracion" runat="server">
                    <a class="nav-link" href="#"><span class="icon config"></span>Configuración</a>
                </li>                                                                        
              </ul>
            </div>
            <div class="user-logged">
              <asp:Label ID="lblUsuario" runat="server"></asp:Label>
            </div>
          </nav>
        </div>   
    </header> 

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section class="content">
        <div class="container-fluid">
          <div class="row my-5">
            <div class="col-md-9">
              <h3>Información de embarques programados  </h3>
            </div>
            <div class="col-md-3 text-right">
                                  <asp:LinkButton runat="server"  id="exportExcel"  OnClick="BtnExportarXls_Click" CssClass="btn btn-success">Excel</asp:LinkButton>
            </div>
          </div>


            <div class="row">
              <div class="form-group filters col-md-3">
                <label for="exampleFormControlSelect1">Centro de origen</label>
                    <asp:DropDownList runat="server" ID ="dropCentro" class="form-control" 
                        AutoPostBack="True" OnSelectedIndexChanged="dropCentro_SelectedIndexChanged">                                                                                                                                                 </asp:DropDownList>
                    <asp:SqlDataSource ID="dsCentros" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ComercializadoraConnectionString %>" 
                         SelectCommand="select 0 as Id , 'Todos' as Centro 
                         union SELECT [Id], [Centro] 
                          FROM [Centros]">
                   </asp:SqlDataSource>
              </div>
              <div class="form-group filters col-md-2">
                <label for="exampleFormControlSelect1">Destino</label>
                <asp:DropDownList runat="server" ID ="ddDestino" class="form-control"                                                         
                    AutoPostBack="True" 
                    OnSelectedIndexChanged="ddDestino_SelectedIndexChanged">                                                                                                                                                 
                <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                </asp:DropDownList>
              </div>
              <div class="form-group filters col-md-2">
                <label for="exampleFormControlSelect1">Fecha</label>
                  <%--<asp:TextBox runat="server" class="form-control" id="exampleFormControlInput1" placeholder="dd/MM/yyyy" 
                      OnTextChanged="exampleFormControlInput1_TextChanged" AutoPostBack="true">

                  </asp:TextBox>--%>
                  <telerik:RadDatePicker RenderMode="Lightweight" ID="txtFecha" runat="server"
                    EnableEmbeddedSkins="false" OnSelectedDateChanged="RadDateTimePicker1_SelectedDateChanged" AutoPostBack="true">
                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"  ></DateInput>

                  </telerik:RadDatePicker>
              </div>
             <div id="divActualizacion" runat="server">
                 <asp:Label ID="lblupdate" runat="server" Text="Ultima actualización: "></asp:Label>
                <asp:Label ID="lblActualizacion" runat="server" Text=""></asp:Label>
              </div>
            </div>            
<asp:UpdatePanel runat="server">
                            <ContentTemplate>
<telerik:RadGrid runat="server" ID="RadEmbarques" CssClass="table" EnableEmbeddedSkins="false"  
                                        MasterTableView-NoMasterRecordsText="No hay embarques que mostrar." AutoGenerateColumns="false"   OnItemCommand="RadEmbarques_ItemCommand"
                                        DataKeyNames="Remision,Folio,Estado,Centro,Destino" AllowAutomaticInserts="true" > 
                                        <ClientSettings Selecting-AllowRowSelect="true"  >
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                                        </ClientSettings>
                                        <MasterTableView DataKeyNames="Remision,Folio,Estado,Centro,Destino" >
                                             <Columns>
                                                <telerik:GridBoundColumn DataField="Remision"  DataType="System.String" FilterControlAltText="Filter Remision column"  Visible="false"
                                                    HeaderText="Remision" ReadOnly="True" SortExpression="Remision" UniqueName="Remision">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Folio"  DataType="System.String" FilterControlAltText="Filter Folio column"  Visible="false"
                                                    HeaderText="Folio" ReadOnly="True" SortExpression="Folio" UniqueName="Folio">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Centro" Visible="true"  DataType="System.String" FilterControlAltText="Filter Centro column" 
                                                    HeaderText="Centro" ReadOnly="True" SortExpression="Centro" UniqueName="Centro">
                                                    
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Destino" Visible="true"  DataType="System.String" FilterControlAltText="Filter Destino column" 
                                                    HeaderText="Destino" ReadOnly="True" SortExpression="Destino" UniqueName="Destino">
                                                    <HeaderStyle Wrap="true" Width="200px" />
													<ItemStyle Wrap="true" Width="200px"  />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Producto" Visible="true"  DataType="System.String" FilterControlAltText="Filter Producto column" 
                                                    HeaderText="Producto" ReadOnly="True" SortExpression="Producto" UniqueName="Producto">
                                                     <HeaderStyle Wrap="true" Width="120px" />
													<ItemStyle Wrap="true" Width="120px"  />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Presentacion" Visible="false"  DataType="System.String" FilterControlAltText="Filter Presentacion column" 
                                                    HeaderText="Presentacion" ReadOnly="True" SortExpression="Presentacion" UniqueName="Presentacion">
                                                    
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Turno" Visible="true"  DataType="System.String" FilterControlAltText="Filter Turno column" 
                                                    HeaderText="Turno" ReadOnly="True" SortExpression="Turno" UniqueName="Turno">
                                                    <HeaderStyle Wrap="true" Width="50px" />
													<ItemStyle Wrap="true" Width="50px"  />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Medio" Visible="false"  DataType="System.String" FilterControlAltText="Filter Medio column" 
                                                    HeaderText="Medio" ReadOnly="True" SortExpression="Medio" UniqueName="Medio">
                                                    
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Clave" Visible="true"  DataType="System.String" FilterControlAltText="Filter Clave column" 
                                                    HeaderText="Clave" ReadOnly="True" SortExpression="Clave" UniqueName="Clave">
                                                    <HeaderStyle Wrap="true" Width="100px" />
													<ItemStyle Wrap="true" Width="100px"  />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Transportista" Visible="true"  DataType="System.String" FilterControlAltText="Filter Transportista column" 
                                                    HeaderText="Transportista" ReadOnly="True" SortExpression="Transportista" UniqueName="Transportista">
                                                    <HeaderStyle Wrap="true" Width="200px" />
													<ItemStyle Wrap="true" Width="200px"  />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Tonel" Visible="true"  DataType="System.String" FilterControlAltText="Filter Tonel column" 
                                                    HeaderText="Tonel" ReadOnly="True" SortExpression="Tonel" UniqueName="Tonel">
                                                   <HeaderStyle Wrap="true" Width="50px" />
													<ItemStyle Wrap="true" Width="50px"  />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Litros" Visible="true"  DataType="System.String" FilterControlAltText="Filter Litros column" 
                                                    HeaderText="Litros" ReadOnly="True" SortExpression="Litros" UniqueName="Litros">
                                                    <HeaderStyle Wrap="true" Width="80px" />
													<ItemStyle Wrap="true" Width="80px"  />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaHoraEstimada" Visible="true"  DataType="System.String" FilterControlAltText="Filter FechaHoraEstimada column" 
                                                    HeaderText="FechaEstimada" ReadOnly="True" SortExpression="FechaHoraEstimada" UniqueName="FechaHoraEstimada">
                                                    
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaFactura" Visible="true"  DataType="System.String" FilterControlAltText="Filter FechaFactura column" 
                                                    HeaderText="FechaFactura" ReadOnly="True" SortExpression="FechaFactura" UniqueName="FechaFactura">
                                                    
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Estado" Visible="true"  DataType="System.String" FilterControlAltText="Filter Estado column" 
                                                    HeaderText="Estado" ReadOnly="True" SortExpression="Estado" UniqueName="Estado">
                                                    
                                                </telerik:GridBoundColumn>
                                                                    <telerik:GridButtonColumn ButtonType="FontIconButton" CommandName="Ver"  Visible="true"
                        ConfirmTitle="Ver" HeaderTooltip="Ver" HeaderText="Ver" ImageUrl="assets/img/icon-pdf.svg">
                    </telerik:GridButtonColumn>

                                                                    <telerik:GridButtonColumn ButtonType="FontIconButton" CommandName="litros"  Visible="false"
                        ConfirmTitle="litros" HeaderTooltip="Devolucion litros" HeaderText="Devolucion litros" ImageUrl="images/undo24.png">
                                                                       
                    </telerik:GridButtonColumn>
					                            <telerik:GridBoundColumn DataField="FechaActualizacion" Visible="false"  DataType="System.String" FilterControlAltText="Filter FechaActualizacion column" 
                                                    HeaderText="FechaActualizacion" ReadOnly="True" SortExpression="FechaActualizacion" UniqueName="FechaActualizacion">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                            </ContentTemplate>
                        </asp:UpdatePanel>                                    
        </div>
             <table style="width:100%; margin-left:10px">
                <tr>
                   <td style="width:50%">
                       <asp:Label ID="lblTotal" runat="server" Text="Total registros:"></asp:Label><asp:Label ID="lblRegistros" runat="server" Text=""></asp:Label>
                   </td>
               </tr>
            </table>
        </section>                
            <footer>  
      <div class="container">
        <div class="row">
          <div class="col-md-4 offset-md-4 text-center">
            <img class="logo-footer" src="assets/img/icon-pemex.svg"/>
            <p>Todos los derechos reservados  |  2023</p>              
          </div>
        </div>
      </div>
    </footer>   
    </div>
    </form>
</body>
</html>
