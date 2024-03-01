<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Numismatica.home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-top: 20px;">
        <div class="card-header" style="background-color: #333; color: #fff; margin-bottom: -5px;">
            <h2 class="display-4" style="font-size: 40px;">Latest Additions to the Catalog</h2>
        </div>
        <div class="card" style="border-color: #333; padding: 10px;">
            <div class="row">
                <asp:Repeater ID="rptCoins" runat="server">
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
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div class="container" style="margin-top: 20px;">
        <div class="card-header" style="background-color: #333; color: #fff; margin-bottom: -5px;">
            <h2 class="display-4" style="font-size: 40px;">Most Favorited</h2>
        </div>
        <div class="card" style="border-color: #333; padding: 10px;">
            <div class="row">
                <asp:Repeater ID="rpt2" runat="server">
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
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
