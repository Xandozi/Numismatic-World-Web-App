<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="coin_details.aspx.cs" Inherits="Numismatica.coin_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-bottom: 20px;">
        <div class="row">
            <div class="col-md-3">
                <div class="list-group">
                    <a href="insert_coin.aspx" class="list-group-item list-group-item-action" id="insertCoinLink">Insert Coin</a>
                    <a href="manage_catalog.aspx" class="list-group-item list-group-item-action active" id="viewCoinsLink">Manage Catalog</a>
                    <a href="manage_users.aspx" class="list-group-item list-group-item-action" id="viewUsersLink">Manage Users</a>
                    <a href="manage_catalog.aspx" class="list-group-item list-group-item-action" id="go_back">Go back</a>
                </div>
            </div>
            <div class="d-flex justify-content-center">
                <div class="coin card text-center" style="width: 80%;">
                    <asp:Label ID="lbl_deleted" runat="server" Text=""></asp:Label>
                    <h2>
                        <asp:Label ID="lblNome" runat="server"></asp:Label></h2>
                    <div class="card-body">
                        <p>Description:
                            <asp:Label ID="lblDescricao" runat="server"></asp:Label></p>
                        <p>Type:
                            <asp:Label ID="lblType" runat="server"></asp:Label></p>
                        <p>Imprint:
                            <asp:Label ID="lblCunho" runat="server"></asp:Label></p>
                        <p>Grade:
                            <asp:Label ID="lblEstado" runat="server"></asp:Label></p>
                        <p>Current Value:
                            <asp:Label ID="lblValorAtual" runat="server"></asp:Label>€</p>
                        <asp:Repeater ID="rptFotos" runat="server">
                            <HeaderTemplate>
                                <div class="col">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <img class="img-fluid" src='<%# Container.DataItem %>' alt="Coin image" style="width: 50px; height: 50px;" />
                            </ItemTemplate>
                            <FooterTemplate>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <asp:Button ID="btn_editar" CssClass="btn-secondary" runat="server" Text="Edit" Style="height: 10%; margin-left: 10px;" OnClick="btn_editar_Click" />
                <a href="#" class="btn <%# lbl_deleted.Text == "Inactive" ? "btn-success" : "btn-danger" %>" data-toggle="modal" data-target="<%# lbl_deleted.Text == "Inactive" ? "#activateModal" : "#deleteModal" %>" style="height: 10%; margin-left: 10px;"><%# lbl_deleted.Text == "Inactive" ? "Activate" : "Deactivate" %></a>
            </div>
        </div>
    </div>
    <!-- Delete Confirmation Modal -->
    <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="deleteModalLabel">Confirm Deactivation</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Are you sure you want to deactivate this coin?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                    <asp:Button ID="btn_modal_delete" runat="server" CssClass="btn btn-danger" Text="Deactivate" OnClick="btn_modal_delete_Click" />
                </div>
            </div>
        </div>
    </div>
    <!-- Activation Confirmation Modal -->
    <div class="modal fade" id="activateModal" tabindex="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="activateModalLabel">Confirm Delete</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Are you sure you want to reactivate this coin?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                    <asp:Button ID="btn_modal_activate" runat="server" CssClass="btn btn-success" Text="Activate" OnClick="btn_modal_activate_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
