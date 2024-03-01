<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="coin_details_client.aspx.cs" Inherits="Numismatica.coin_details_client" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-bottom: 20px;">
        <asp:Label ID="lbl_mensagem" runat="server" Text=""></asp:Label>
        <div class="row" style="display: flex; justify-content: center;">
            <div class="d-flex justify-content-center">
                <div class="coin card text-center" style="width: 80%;">
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
                        <asp:LinkButton ID="lkb_add_favorites" runat="server" CssClass="fa fa-heart" OnClick="lkb_add_favorites_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
