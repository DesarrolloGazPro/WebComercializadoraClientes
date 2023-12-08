<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
    <title>GAZPRO</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link rel="stylesheet" href="assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/css/styles.css">
</head>
<body>
    <form id="form1" runat="server">
        <!--Slider-->
        <section class="login">
            <div class="container">
                <div class="row">
                    <div class="col-md-6 offset-md-3 white text-center">
                        <img src="assets/img/logo.svg">
                        <div class="form-login">
                            <h2>Iniciar sesión</h2>
                            <br />
                            <br />
                            <div>
                                <div class="form-group">
                                    <%--<input type="text" class="form-control user" id="exampleInputuser" aria-describedby="emailHelp" placeholder="Usuario">--%>
                                    <asp:TextBox ID="txtUsuario" type="text" CssClass="form-control user" aria-describedby="emailHelp" placeholder="Usuario" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <%--<input type="password" class="form-control pass" id="exampleInputPassword1" placeholder="Contraseña">--%>
                                    <asp:TextBox type="password" ID="password" CssClass="form-control pass" runat="server" placeholder="Contraseña"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <div>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="btnIngresar_Click" />
                                        <%--<a href="dash.html" class="btn btn-primary">Ingresar</a>--%>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="row politics">
                    <div class="col-md-12 text-center">
                        <p>
                            Todos los derechos reservados  |  2023<br>
                            <a href="#">Términos y condiciones</a>
                        </p>
                    </div>
                </div>
            </div>
        </section>
    </form>
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="assets/js/slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="assets/js/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <script src="assets/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
</body>
</html>
