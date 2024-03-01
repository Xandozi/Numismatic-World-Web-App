<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="edit_coin_details.aspx.cs" Inherits="Numismatica.edit_coin_details" %>

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
                <div class="coin card text-center" style="width: 100%; padding: 15px; padding-right: 25px;">
                    <h2 style="font-size: 10px;">Coin Code: 
                        <asp:Label ID="lbl_cod_moeda" runat="server" Text=""></asp:Label>
                    </h2>
                    <h2 style="font-size: 10px;">Coin State Code:
                        <asp:Label ID="lbl_cod_moeda_estado" runat="server" Text="" Style="font-size: 10px;"></asp:Label>
                    </h2>
                    <h2 style="font-size: 15px;">Name: 
                        <asp:TextBox ID="tb_nome" runat="server" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_nome" runat="server" ErrorMessage="Name is required." ControlToValidate="tb_nome" Text="*"></asp:RequiredFieldValidator>
                    </h2>
                    <div class="card-body">
                        <p style="font-size: 12px;">
                            Description: 
                            <asp:TextBox ID="tb_descricao" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_descricao" runat="server" ErrorMessage="Description is required." Text="*" ControlToValidate="tb_descricao"></asp:RequiredFieldValidator>
                        </p>
                        <p style="font-size: 12px;">
                            Type: 
                            <asp:DropDownList ID="ddl_tipo" runat="server" AppendDataBoundItems="true" DataSourceID="tipo" DataTextField="tipo" DataValueField="cod_tipo"></asp:DropDownList>
                            <asp:SqlDataSource runat="server" ID="tipo" ConnectionString='<%$ ConnectionStrings:numismaticaConnectionString %>' SelectCommand="SELECT * FROM [tipo]"></asp:SqlDataSource>
                        </p>
                        <p style="font-size: 12px;">
                            Imprint: 
                            <asp:TextBox ID="tb_cunho" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_cunho" runat="server" ErrorMessage="Imprint is required." ControlToValidate="tb_cunho" Text="*"></asp:RequiredFieldValidator>
                        </p>
                        <p style="font-size: 12px;">
                            Grade: 
                            <asp:DropDownList ID="ddl_estado" runat="server" AppendDataBoundItems="true" DataSourceID="estado" DataTextField="estado" DataValueField="cod_estado"></asp:DropDownList>
                            <asp:SqlDataSource runat="server" ID="estado" ConnectionString='<%$ ConnectionStrings:numismaticaConnectionString %>' SelectCommand="SELECT * FROM [estado]"></asp:SqlDataSource>
                        </p>
                        <p style="font-size: 12px;">
                            Current Value: 
                            <asp:TextBox ID="tb_valor_atual" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_valor_atual" runat="server" ErrorMessage="Current value is required." Text="*" ControlToValidate="tb_valor_atual"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rev_valor_atual" runat="server" ErrorMessage="Only numbers please" ControlToValidate="tb_valor_atual" Text="*" ValidationExpression="^\d{1,16}(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                        </p>
                        <p style="font-size: 12px;">
                            All the inserted photos will overwrite the previous ones 
                            <br />
                            <br />
                            <asp:FileUpload ID="fu_photos" runat="server" AllowMultiple="True" />
                        </p>
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
                <asp:Button ID="btn_editar" CssClass="btn btn-success" runat="server" Text="Save Changes" Style="height: 10%; margin-left: 10px;" OnClick="btn_editar_Click" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                <asp:Label ID="lbl_mensagem" runat="server" Text="" Style="width: 20%; height: 15%; margin-left: 10px; font-size: 10px;"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
