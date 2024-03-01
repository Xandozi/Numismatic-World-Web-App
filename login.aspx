<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Numismatica.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="container">
        <div class="container-fluid h-custom">
            <div class="row d-flex justify-content-center align-items-center h-100">
                <div class="col-md-9 col-lg-6 col-xl-5">
                    <img src="Images/logo.jpg" class="img-fluid" alt="Sample image" />
                </div>
                <div class="col-md-8 col-lg-6 col-xl-4 offset-xl-1">
                    <div runat="server">
                        <div class="d-flex flex-row align-items-center justify-content-center justify-content-lg-start">
                            <p class="lead fw-normal mb-0 me-3"></p>
                            <asp:LinkButton ID="btn_login_google" runat="server" CssClass="btn btn-primary btn-floating mx-1" OnClick="Login" CausesValidation="false">
                                <i class="fa fa-google"></i> Sign in with Google
                            </asp:LinkButton>
                        </div>

                        <div class="divider d-flex align-items-center my-4">
                            <p class="text-center fw-bold mx-3 mb-0">Or</p>
                        </div>

                        <!-- Email input -->
                        <div class="form-outline mb-4">
                            <asp:TextBox ID="tb_username" class="form-control form-control-lg" placeholder="Enter your username" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_username" runat="server" ErrorMessage="You did not insert an username." ControlToValidate="tb_username" Text="*"></asp:RequiredFieldValidator>
                            <label class="form-label" for="form3Example3">Username</label>
                        </div>

                        <!-- Password input -->
                        <div class="form-outline mb-3">
                            <asp:TextBox ID="tb_pw" class="form-control form-control-lg" placeholder="Enter password" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_pw" runat="server" ErrorMessage="You did not insert a password." ControlToValidate="tb_pw" Text="*"></asp:RequiredFieldValidator>
                            <label class="form-label" for="form3Example4">Password</label>
                        </div>

                        <div class="d-flex justify-content-between align-items-center">
                            <!-- Checkbox -->
                            <div class="form-check mb-0">
                                <input class="form-check-input me-2" type="checkbox" value="" id="form2Example3" />
                                <label class="form-check-label" for="form2Example3">
                                    Remember me
                                </label>
                            </div>
                            <a class="text-body" data-toggle="modal" data-target="#resetPasswordModal">Forgot password?</a>
                        </div>

                        <div class="text-center text-lg-start mt-4 pt-2" style="margin-bottom: 30px;">
                            <asp:Button ID="btn_login" class="btn btn-primary btn-lg"
                                Style="padding-left: 2.5rem; padding-right: 2.5rem;" runat="server" Text="Login" OnClick="btn_login_Click" />
                            <p class="small fw-bold mt-2 pt-1 mb-0">
                                Don't have an account? <a href="register.aspx"
                                    class="link-danger">Register</a>
                            </p>
                        </div>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" Style="margin-top: 30px;" />
                    </div>
                </div>
                <asp:Label ID="lbl_mensagem" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </section>
    <!-- Modal for resetting the password -->
    <div class="modal" id="resetPasswordModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Reset your password</h4>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <div class="modal-body">
                    <div class="container" style="font-size: 20px;">
                        Email:
                        <asp:TextBox ID="tb_email" runat="server" TextMode="Email"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btn_forgot_pw" runat="server" Text="Reset Password" OnClick="btn_forgot_pw_Click" CausesValidation="false" />
                        <br />
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
