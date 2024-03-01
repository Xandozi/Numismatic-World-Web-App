<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="management.aspx.cs" Inherits="Numismatica.coin_management" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-bottom: 20px;">
        <div class="row">
            <div class="col-md-3">
                <div class="list-group">
                    <a href="insert_coin.aspx" class="list-group-item list-group-item-action" id="insertCoinLink">Insert Coin</a>
                    <a href="manage_catalog.aspx" class="list-group-item list-group-item-action" id="viewCoinsLink">Manage Catalog</a>
                    <a href="manage_users.aspx" class="list-group-item list-group-item-action" id="viewUsersLink">Manage Users</a>
                    <a href="personal_zone.aspx" class="list-group-item list-group-item-action" id="go_back">Go back</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
