<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Numismatica.register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-12 col-xl-11">
                <div class="card text-black" style="border-radius: 25px;">
                    <div class="card-body p-md-5">
                        <div class="row justify-content-center">
                            <div class="col-md-10 col-lg-6 col-xl-5 order-2 order-lg-1">
                                <div class="mx-1 mx-md-4" runat="server">
                                    <p class="text-center h1 fw-bold mb-5 mx-1 mx-md-4 mt-4">Sign up</p>
                                    <div class="d-flex flex-row align-items-center mb-4">
                                        <i class="fa fa-user fa-lg me-3 fa-fw"></i>
                                        <div class="form-outline flex-fill mb-0">
                                            <asp:TextBox ID="tb_username" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_username" runat="server" ErrorMessage="Username Required" Text="*" ControlToValidate="tb_username"></asp:RequiredFieldValidator>
                                            <label class="form-label" for="form3Example1c">Your Username</label>
                                        </div>
                                    </div>
                                    <div class="d-flex flex-row align-items-center mb-4">
                                        <i class="fa fa-envelope fa-lg me-3 fa-fw"></i>
                                        <div class="form-outline flex-fill mb-0">
                                            <asp:TextBox ID="tb_email" class="form-control" runat="server" TextMode="Email"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_email" runat="server" ErrorMessage="Email Required" Text="*" ControlToValidate="tb_email"></asp:RequiredFieldValidator>
                                            <label class="form-label" for="form3Example3c">Your Email</label>
                                        </div>
                                    </div>
                                    <div class="d-flex flex-row align-items-center mb-4">
                                        <i class="fa fa-lock fa-lg me-3 fa-fw"></i>
                                        <div class="form-outline flex-fill mb-0">
                                            <asp:TextBox ID="tb_pw" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_pw" runat="server" ErrorMessage="Password Required" Text="*" ControlToValidate="tb_pw"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="rev_pw" runat="server" ErrorMessage="Invalid Password. Must have 9 characters, letters, numbers and special characters." Text="**" ControlToValidate="tb_pw" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{9,20}$"></asp:RegularExpressionValidator>
                                            <label class="form-label" for="form3Example4c">Password</label>
                                        </div>
                                    </div>
                                    <div class="d-flex flex-row align-items-center mb-4">
                                        <i class="fa fa-key fa-lg me-3 fa-fw"></i>
                                        <div class="form-outline flex-fill mb-0">
                                            <asp:TextBox ID="tb_repeat_pw" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                            <label class="form-label" for="form3Example4cd">Repeat your password</label>
                                        </div>
                                    </div>
                                    <div class="d-flex justify-content-center mx-4 mb-3 mb-lg-4">
                                        <asp:Button ID="btn_register" class="btn btn-primary btn-lg" runat="server" Text="Register" OnClick="btn_register_Click" />
                                        <asp:LinkButton ID="btn_login_google" runat="server" CssClass="btn btn-primary btn-floating mx-1" OnClick="Login" CausesValidation="false">
                                            <i class="fa fa-google"></i> Sign in with Google
                                        </asp:LinkButton>
                                    </div>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                                    <asp:Label ID="lbl_mensagem" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-10 col-lg-6 col-xl-7 d-flex align-items-center order-1 order-lg-2">
                                <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-registration/draw1.webp" class="img-fluid" alt="Sample image">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
