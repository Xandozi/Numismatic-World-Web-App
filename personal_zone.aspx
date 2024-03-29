﻿<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="personal_zone.aspx.cs" Inherits="Numismatica.personal_zone" %>

<%@ Import Namespace="Numismatica.Classes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <!-- Sidebar -->
            <div class="col-md-3">
                <div class="list-group">
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#changeUsernameModal">Change Username</a>
                    <% if (Session["googlefb_log"] != "yes")
                        { %>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#changePasswordModal">Change Password</a>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#changeEmailModal">Change Email</a>
                    <% } %>
                    <% if (Session["perfil"].ToString() == "Administrator")
                        { %>
                    <a href="dashboard.aspx" class="list-group-item list-group-item-action">Dashboard
                    </a>
                    <a href="management.aspx" class="list-group-item list-group-item-action">Management
                    </a>
                    <% } %>
                    <a href="myCollection.aspx?id=<%= Session["user_code"] %>" class="list-group-item list-group-item-action">Manage My Collection
                    </a>
                    <a href="logout.aspx" class="list-group-item list-group-item-action">LOGOUT
                    </a>
                </div>
            </div>
            <!-- Main Content -->
            <div class="col-md-9">
                <div class="card" style="border-color: #333;">
                    <div class="card-header" style="background-color: #333; color: #fff;">
                        <h2 class="display-4" style="font-size: 40px;">Welcome to your area, <%: Session["username"] %></h2>
                    </div>
                    <div class="card-body" style="color: #333;">
                        <h5 class="card-title">User Details</h5>
                        <p class="lead">Your User Code: <%: Extract.Code(Session["username"].ToString()) %></p>
                        <p class="lead">Your profile: <%: Session["perfil"] %></p>
                        <p class="lead">Your Email: <%: Extract.Email(Session["username"].ToString()) %></p>
                        <div class="d-flex justify-content-lg-between">
                            <asp:Button ID="btn_export_pdf" class="btn btn-outline-dark btn-lg" runat="server" Text="Export My Collection to PDF" CausesValidation="false" OnClick="btn_export_pdf_Click" />
                            <a class="btn btn-outline-dark btn-lg" text="Delete my Account" data-toggle="modal" data-target="#deleteAccountModal">Delete My Account</a>
                        </div>
                    </div>
                </div>
            </div>
            <a>
                <asp:Label ID="lblMessage" runat="server" CssClass="mt-3" Style="margin-top: 40px;" />
            </a>
            <!-- Modal for deleting the account -->
            <div class="modal" id="deleteAccountModal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Delete Account</h4>
                            <button type="button" class="close" data-dismiss="modal">×</button>
                        </div>
                        <div class="modal-body">
                            <p>Are you sure you want to delete your account? This action cannot be undone.</p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btn_delete_account_confirm" class="btn btn-danger" runat="server" Text="Delete Account" CausesValidation="false" onclick="btn_delete_account_confirm_Click" />
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Modal for changing the password -->
            <div class="modal" id="changePasswordModal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Change Password</h4>
                            <button type="button" class="close" data-dismiss="modal">×</button>
                        </div>
                        <div class="modal-body">
                            <div id="changePasswordForm" runat="server">
                                <div class="form-group">
                                    <label for="tb_pw">Current Password</label>
                                    <asp:TextBox ID="tb_pw" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="tb_new_pw">New Password</label>
                                    <asp:TextBox ID="tb_new_pw" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="rev_pw_nova" runat="server" ErrorMessage="Invalid Password" Text="*" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{9,20}$" ControlToValidate="tb_new_pw"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <label for="tb_new_pw_repeat">Confirm New Password</label>
                                    <asp:TextBox ID="tb_new_pw_repeat" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                                <a>
                                    <asp:Label ID="lbl_message2" runat="server" CssClass="mt-3" /></a>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btn_change_pw" class="btn btn-primary" runat="server" Text="Change Password" OnClick="btn_change_pw_Click" />
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal for changing the username -->
            <div class="modal" id="changeUsernameModal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Change Username</h4>
                            <button type="button" class="close" data-dismiss="modal">×</button>
                        </div>
                        <div class="modal-body">
                            <div id="Div1" runat="server">
                                <div class="form-group">
                                    <label for="tb_pw">New Username</label>
                                    <asp:TextBox ID="tb_new_username" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="tb_new_pw">Password</label>
                                    <asp:TextBox ID="tb_pw_username" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btn_change_username" class="btn btn-primary" runat="server" Text="Change Password" OnClick="btn_change_username_Click" />
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal for changing the email -->
            <div class="modal" id="changeEmailModal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Change Email</h4>
                            <button type="button" class="close" data-dismiss="modal">×</button>
                        </div>
                        <div class="modal-body">
                            <div id="Div2" runat="server">
                                <div class="form-group">
                                    <label for="tb_email">New Email</label>
                                    <asp:TextBox ID="tb_new_email" class="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="rfv_new_email" runat="server" ErrorMessage="New email not inserted correctly" Text="*" ControlToValidate="tb_new_email" ValidationExpression="^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <label for="tb_pw_email">Password</label>
                                    <asp:TextBox ID="tb_pw_email" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btn_change_email" class="btn btn-primary" runat="server" Text="Change Email" OnClick="btn_change_email_Click" />
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

