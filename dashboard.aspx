<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="Numismatica.dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="form1" class="flex-column justify-content-center" runat="server">
        <div class="container">
            <h1 class="text-center">Dashboard Statistics</h1>
            <hr />
            <div class="row">
                <div class="col-md-4">
                    <div class="card text-white bg-primary mb-3">
                        <div class="card-header">Total Users</div>
                        <div class="card-body">
                            <h5 class="card-title"><b><asp:Label ID="lblTotalUsers" runat="server" CssClass="lead"></asp:Label></b></h5>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card text-white bg-success mb-3">
                        <div class="card-header">Users With Collection Started</div>
                        <div class="card-body">
                            <h5 class="card-title"><b><asp:Label ID="lblTotalUsersWithFavorites" runat="server" CssClass="lead"></asp:Label></b></h5>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card text-white bg-info mb-3">
                        <div class="card-header">Average Coins per User</div>
                        <div class="card-body">
                            <h5 class="card-title"><b><asp:Label ID="lblAvgCoinsPerUser" runat="server" CssClass="lead"></asp:Label></b></h5>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="card text-white bg-warning mb-3">
                        <div class="card-header">User with Most Coins</div>
                        <div class="card-body">
                            <h5 class="card-title"><b><asp:Label ID="lblUserWithMostCoins" runat="server" CssClass="lead"></asp:Label></b></h5>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card text-white bg-danger mb-3">
                        <div class="card-header">The User with most coins has a total of </div>
                        <div class="card-body">
                            <h5 class="card-title"><b><asp:Label ID="lblMaxCoinsQuantity" runat="server" CssClass="lead"></asp:Label></b></h5>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div id="viewCoinsDiv" class="flex justify-content-center" style="width: 100%; margin-top: 20px;">
                    <div class="card-header" style="background-color: #333; color: #fff; margin-bottom: -5px;">
                        <h2 class="display-4" style="text-align: center; font-size: 40px;">Top 10 Most Valuable Coins</h2>
                    </div>
                    <div class="card" style="border-color: #333; padding: 10px;">
                        <div class="row">
                            <asp:Repeater ID="rptMostValuableCoins" runat="server">
                                <ItemTemplate>
                                    <div class="col-lg-2 col-sm-4">
                                        <div class="card" style="width: 100%; height: 85%; padding: 10px; margin: 5px;">
                                            <a href='coin_details_client.aspx?id=<%# Eval("cod_moeda_estado") %>' style="width: 100%; height: 80%; display: flex; justify-content: center; text-decoration: none; color: black;">
                                                <div class="card-body" style="padding: 5px;">
                                                    <h5 class="card-title" style="font-size: 15px;"><%# Eval("nome") %></h5>
                                                    <h5 class="card-title" style="font-size: 10px;"><b>Grade</b> - <%# Eval("estado") %></h5>
                                                    <h5 class="card-title" style="font-size: 10px;"><b>Value</b> - <%# Eval("valor_atual") %>€</h5>
                                                </div>
                                            </a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
