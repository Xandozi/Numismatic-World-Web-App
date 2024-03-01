<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="manage_users.aspx.cs" Inherits="Numismatica.manage_users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-bottom: 20px;">
        <div id="filterForm" runat="server" class="form-inline" style="justify-content: space-evenly; margin-bottom: 10px;">
            <div class="form-group">
                <label>Search:</label>
                <asp:TextBox ID="tb_search" runat="server" Style="margin-left: 5px;"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Email:</label>
                <asp:TextBox ID="tb_email" runat="server" Style="margin-left: 5px;"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Sort by Username:</label>
                <asp:DropDownList ID="ddl_username" runat="server" Style="margin-left: 5px;">
                    <asp:ListItem Value="">None</asp:ListItem>
                    <asp:ListItem Value="asc">Ascending</asp:ListItem>
                    <asp:ListItem Value="desc">Descending</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label>Status:</label>
                <asp:DropDownList ID="ddl_ativo" runat="server" Style="margin-left: 5px;">
                    <asp:ListItem Value="2">All</asp:ListItem>
                    <asp:ListItem Value="1">Active</asp:ListItem>
                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Button ID="btn_filter" runat="server" Text="Apply Filters" />
        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="list-group">
                    <a href="insert_coin.aspx" class="list-group-item list-group-item-action" id="insertCoinLink">Insert Coin</a>
                    <a href="manage_catalog.aspx" class="list-group-item list-group-item-action" id="viewCoinsLink">Manage Catalog</a>
                    <a href="manage_users.aspx" class="list-group-item list-group-item-action active" id="viewUsersLink">Manage Users</a>
                    <a href="personal_zone.aspx" class="list-group-item list-group-item-action" id="go_back">Go back</a>
                </div>
            </div>
            <div class="col-md-9">
                <div id="viewUsersDiv">
                    <div class="card-header" style="background-color: #333; color: #fff; margin-bottom: -5px;">
                        <h2 class="display-4" style="font-size: 40px;">User Management</h2>
                    </div>
                    <div class="card" style="border-color: #333; padding: 10px;">
                        <div class="row">
                            <asp:Repeater ID="rptUsers" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-4">
                                        <div class="card" style="width: 90%; height: 75%; padding: 10px; margin: 5px;">
                                            <a href='user_details.aspx?id=<%# Eval("cod_user") %>' style="width: 100%; height: 80%; display: flex; justify-content: center; text-decoration: none; color: black;">
                                                <div style="position: absolute; top: 0; background-color: red; width: 35%; height: 20%; display: <%# Convert.ToBoolean(Eval("ativo")) ? "none" : "block" %>;">
                                                    <p style="color: white; text-align: center;">Inactive</p>
                                                </div>
                                                <div class="card-body">
                                                    <h5 class="card-title" style="font-size: 15px;"><b><%# Eval("username") %></b></h5>
                                                    <h5 class="card-title" style="font-size: 10px;"><b>Email</b> - <%# Eval("email") %></h5>
                                                    <h5 class="card-title" style="font-size: 10px;"><b>Perfil</b> - <%# Eval("perfil") %></h5>
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
