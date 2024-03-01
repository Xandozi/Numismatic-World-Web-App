<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="user_details.aspx.cs" Inherits="Numismatica.user_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-bottom: 20px;">
        <div class="row">
            <div class="col-md-3">
                <div class="list-group">
                    <a href="insert_coin.aspx" class="list-group-item list-group-item-action" id="insertCoinLink">Insert Coin</a>
                    <a href="manage_catalog.aspx" class="list-group-item list-group-item-action" id="viewCoinsLink">Manage Catalog</a>
                    <a href="manage_users.aspx" class="list-group-item list-group-item-action active" id="viewUsersLink">Manage Users</a>
                    <a href="manage_users.aspx" class="list-group-item list-group-item-action" id="go_back">Go back</a>
                </div>
            </div>
            <div class="d-flex justify-content-center">
                <div class="coin card text-center" style="width: 80%;">
                    <asp:Label ID="lbl_ativo" runat="server" Text=""></asp:Label>
                    <h2>
                        <asp:Label ID="lbl_nome" runat="server"></asp:Label></h2>
                    <div class="card-body">
                        <p>Email:
                            <asp:Label ID="lbl_email" runat="server"></asp:Label></p>
                        <p>Perfil:
                            <asp:Label ID="lbl_perfil" runat="server"></asp:Label></p>
                    </div>
                </div>
                <a href="#" class="btn <%# lbl_ativo.Text == "Inactive" ? "btn-success" : "btn-danger" %>" data-toggle="modal" data-target="<%# lbl_ativo.Text == "Inactive" ? "#activateModal" : "#deactivateModal" %>" style="height: 20%; margin-left: 10px;"><%# lbl_ativo.Text == "Inactive" ? "Activate" : "Deactivate" %></a>
                <a href="#" class="btn btn-danger" data-toggle="modal" data-target="#deleteModal" style="height: 20%; margin-left: 10px;">Delete</a>
            </div>
        </div>
    </div>
    <!-- Delete Confirmation Modal -->
    <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="deleteModalLabel">Confirm Deletion</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Are you sure you want to delete this user?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                    <asp:Button ID="btn_modal_delete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btn_modal_delete_Click" />
                </div>
            </div>
        </div>
    </div>
    <!-- Activation Confirmation Modal -->
    <div class="modal fade" id="activateModal" tabindex="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="activateModalLabel">Confirm Activation</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Are you sure you want to reactivate this user?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                    <asp:Button ID="btn_modal_activate" runat="server" CssClass="btn btn-success" Text="Activate" OnClick="btn_modal_activate_Click" />
                </div>
            </div>
        </div>
    </div>
    <!-- Deactivation Confirmation Modal -->
    <div class="modal fade" id="deactivateModal" tabindex="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="deactivateModalLabel">Confirm Deactivation</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Are you sure you want to deactivate this user?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                    <asp:Button ID="btn_deactivation" runat="server" CssClass="btn btn-danger" Text="Deactivate" OnClick="btn_deactivation_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
