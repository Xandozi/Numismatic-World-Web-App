<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="myCollection.aspx.cs" Inherits="Numismatica.myCollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-bottom: 20px;">
        <div class="col-md-9" style="max-width: 100%;">
            <div id="filterForm" runat="server" class="form-inline" style="justify-content: space-evenly; margin-bottom: 20px;">
                <div class="form-group">
                    <label>Search:</label>
                    <asp:TextBox ID="tb_search" runat="server" Style="margin-left: 5px;"></asp:TextBox>
                </div>
                <div class="form-group">
                <label>Grade</label>
                    <asp:DropDownList ID="ddl_grade" AppendDataBoundItems="true" runat="server" DataSourceID="grade" DataTextField="estado" DataValueField="cod_estado"></asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="grade" ConnectionString='<%$ ConnectionStrings:numismaticaConnectionString %>' SelectCommand="SELECT * FROM [estado]"></asp:SqlDataSource>
                </div>
                <div class="form-group">
                    <label>Type: </label>
                    <asp:DropDownList ID="ddl_type" runat="server" AppendDataBoundItems="true" DataSourceID="type" DataTextField="tipo" DataValueField="cod_tipo"></asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="type" ConnectionString='<%$ ConnectionStrings:numismaticaConnectionString %>' SelectCommand="SELECT * FROM [tipo]"></asp:SqlDataSource>
                </div>
                <div class="form-group">
                    <label>Sort by price:</label>
                    <asp:DropDownList ID="ddl_price" runat="server" Style="margin-left: 5px;">
                        <asp:ListItem>None</asp:ListItem>
                        <asp:ListItem Value="asc">Ascending</asp:ListItem>
                        <asp:ListItem Value="desc">Descending</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:Button ID="btn_filter" runat="server" Text="Apply Filters" />
            </div>
            <asp:Label ID="lbl_mensagem" runat="server" Text="" Style="margin: 20px;"></asp:Label>
            <div id="viewCoinsDiv" style="margin-top: 20px;">
                <div class="card-header" style="background-color: #333; color: #fff; margin-bottom: -5px;">
                    <h2 class="display-4" style="font-size: 40px;">Welcome to your collection, <%: Session["username"] %></h2>
                </div>
                <div class="card" style="border-color: #333; padding: 10px;">
                    <div class="row">
                        <asp:Repeater ID="rptCoins" runat="server" OnItemDataBound="rptCoins_ItemDataBound">
                            <ItemTemplate>
                                <div class="col-lg-2 col-sm-4">
                                    <div class="card" style="width: 100%; height: 85%; padding: 10px; margin: 5px;">
                                        <a href='coin_details_client.aspx?id=<%# Eval("cod_moeda_estado") %>' style="width: 100%; height: 80%; display: flex; justify-content: center; text-decoration: none; color: black;">
                                            <img class="card-img-top" src='<%# Eval("foto") %>' alt="Coin image" style="height: 50px; width: 50px;">
                                            <div class="card-body" style="padding: 5px;">
                                                <h5 class="card-title" style="font-size: 15px;"><%# Eval("nome") %></h5>
                                                <h5 class="card-title" style="font-size: 10px;"><b>Grade</b> - <%# Eval("estado") %></h5>
                                                <h5 class="card-title" style="font-size: 10px;"><b>Value</b> - <%# Eval("valor_atual") %>€</h5>
                                            </div>
                                        </a>
                                        <asp:LinkButton ID="lkb_add_favorites" runat="server" CssClass="fa fa-heart" OnClick="lkb_add_favorites_Click"></asp:LinkButton>
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
</asp:Content>
