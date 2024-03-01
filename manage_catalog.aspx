<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="manage_catalog.aspx.cs" Inherits="Numismatica.manage_catalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-bottom: 20px;">
        <div id="filterForm" runat="server" class="form-inline" style="justify-content: space-evenly; margin-bottom: 10px;">
            <div class="form-group">
                <label>Search:</label>
                <asp:TextBox ID="tb_search" runat="server" Style="margin-left: 5px;"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Type:</label>
                <asp:DropDownList ID="ddl_type" runat="server" AppendDataBoundItems="true" Style="margin-left: 5px;" DataSourceID="type" DataTextField="tipo" DataValueField="cod_tipo"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="type" ConnectionString='<%$ ConnectionStrings:numismaticaConnectionString %>' SelectCommand="SELECT * FROM [tipo]"></asp:SqlDataSource>
            </div>
            <div class="form-group">
                <label>Grade</label>
                <asp:DropDownList ID="ddl_grade" AppendDataBoundItems="true" runat="server" DataSourceID="grade" DataTextField="estado" DataValueField="cod_estado"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="grade" ConnectionString='<%$ ConnectionStrings:numismaticaConnectionString %>' SelectCommand="SELECT * FROM [estado]"></asp:SqlDataSource>
            </div>
            <div class="form-group">
                <label>Sort by price:</label>
                <asp:DropDownList ID="ddl_price" runat="server" Style="margin-left: 5px;">
                    <asp:ListItem>None</asp:ListItem>
                    <asp:ListItem Value="asc">Ascending</asp:ListItem>
                    <asp:ListItem Value="desc">Descending</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>Status:</label>
                <asp:DropDownList ID="ddl_deleted" runat="server" Style="margin-left: 5px;">
                    <asp:ListItem Value="2">All</asp:ListItem>
                    <asp:ListItem Value="0">Active</asp:ListItem>
                    <asp:ListItem Value="1">Inactive</asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Button ID="btn_filter" runat="server" Text="Apply Filters" />
        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="list-group">
                    <a href="insert_coin.aspx" class="list-group-item list-group-item-action" id="insertCoinLink">Insert Coin</a>
                    <a href="manage_catalog.aspx" class="list-group-item list-group-item-action active" id="viewCoinsLink">Manage Catalog</a>
                    <a href="manage_users.aspx" class="list-group-item list-group-item-action" id="viewUsersLink">Manage Users</a>
                    <a href="personal_zone.aspx" class="list-group-item list-group-item-action" id="go_back">Go back</a>
                </div>
            </div>
            <div class="col-md-9">
                <div id="viewCoinsDiv">
                    <div class="card-header" style="background-color: #333; color: #fff; margin-bottom: -5px;">
                        <h2 class="display-4" style="font-size: 40px;">Coin Catalog Management</h2>
                    </div>
                    <div class="card" style="border-color: #333; padding: 10px;">
                        <div class="row">
                            <asp:Repeater ID="rptCoins" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-4">
                                        <div class="card" style="width: 90%; height: 75%; padding: 10px; margin: 5px;">
                                            <a href='coin_details.aspx?id=<%# Eval("cod_moeda_estado") %>' style="width: 100%; height: 80%; display: flex; justify-content: center; text-decoration: none; color: black;">
                                                <div style="position: absolute; top: 0; background-color: red; width: 35%; height: 20%; display: <%# Convert.ToBoolean(Eval("deleted")) ? "block" : "none" %>;">
                                                    <p style="color: white; text-align: center;">Inactive</p>
                                                </div>
                                                <img class="card-img-top" src='<%# Eval("foto") %>' alt="Coin image" style="height: 50px; width: 50px;">
                                                <div class="card-body">
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
                    <div class="d-flex justify-content-center" causesvalidation="true">
                        <asp:Button ID="btn_previous" runat="server" Text="Previous" CssClass="btn btn-primary m-2" OnClick="btn_previous_Click" />
                        <asp:Button ID="btn_next" runat="server" Text="Next" CssClass="btn btn-primary m-2" OnClick="btn_next_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
