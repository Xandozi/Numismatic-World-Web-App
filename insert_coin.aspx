<%@ Page Title="" Language="C#" MasterPageFile="~/numismatica.Master" AutoEventWireup="true" CodeBehind="insert_coin.aspx.cs" Inherits="Numismatica.insert_coin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-bottom: 20px;">
        <div class="row">
            <div class="col-md-3">
                <div class="list-group">
                    <a href="insert_coin.aspx" class="list-group-item list-group-item-action active" id="insertCoinLink">Insert Coin</a>
                    <a href="manage_catalog.aspx" class="list-group-item list-group-item-action" id="viewCoinsLink">Manage Catalog</a>
                    <a href="manage_users.aspx" class="list-group-item list-group-item-action" id="viewUsersLink">Manage Users</a>
                    <a href="personal_zone.aspx" class="list-group-item list-group-item-action" id="go_back">Go back</a>
                </div>
            </div>

            <div id="insertCoinDiv">
                <div class="col-md-12">
                    <div class="card" style="border-color: #333;">
                        <div class="card-header" style="background-color: #333; color: #fff;">
                            <h2 class="display-4" style="font-size: 20px; height: 20px;">Insert Coin</h2>
                        </div>
                        <div class="card-body" style="color: #333;">
                            <p class="lead" style="font-size: 15px;">
                                <b>Name</b>
                                <asp:TextBox ID="tb_name" runat="server" Style="border-radius: 7px;" required="true" MaxLength="50"></asp:TextBox>
                            </p>
                            <p class="lead" style="font-size: 15px;">
                                <b>Description</b>
                                <br />
                                <asp:TextBox ID="tb_description" runat="server" TextMode="MultiLine" Style="border-radius: 7px; width: 95%;" required="true"></asp:TextBox>
                            </p>
                            <p class="lead" style="font-size: 15px;">
                                <b>Cunho</b>
                                <asp:TextBox ID="tb_cunho" runat="server" Style="border-radius: 7px;" required="true" MaxLength="50"></asp:TextBox>
                            </p>
                            <p class="lead" style="font-size: 15px;">
                                <b>Type</b>
                                <asp:DropDownList ID="ddl_type" runat="server" AppendDataBoundItems="True" DataSourceID="tipo" DataTextField="tipo" DataValueField="cod_tipo"></asp:DropDownList>
                                <asp:SqlDataSource ID="tipo" runat="server" ConnectionString="<%$ ConnectionStrings:numismaticaConnectionString %>" SelectCommand="SELECT * FROM [tipo]"></asp:SqlDataSource>
                            </p>
                            <p class="lead" style="font-size: 15px;">
                                <b>Grade</b>
                                <asp:DropDownList ID="ddl_state" runat="server" AppendDataBoundItems="True" Style="border-radius: 7px;" DataSourceID="state" DataTextField="estado" DataValueField="cod_estado"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="state" ConnectionString='<%$ ConnectionStrings:numismaticaConnectionString %>' SelectCommand="SELECT * FROM [estado]"></asp:SqlDataSource>
                            </p>
                            <p class="lead" style="font-size: 15px;">
                                <b>Photos</b>
                                <asp:FileUpload ID="fu_photos" runat="server" AllowMultiple="true" />
                            </p>
                            <p class="lead" style="font-size: 15px;">
                                <b>Value</b>
                                <asp:TextBox ID="tb_value" runat="server" Style="border-radius: 7px;" required="true"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="rev_valor_atual" runat="server" ErrorMessage="Only numbers please" ControlToValidate="tb_value" Text="*" ValidationExpression="^\d{1,16}(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                            </p>
                            <p class="lead" style="font-size: 15px;">
                                <asp:Button ID="btn_submit" runat="server" Text="Insert Coin" OnClick="btn_submit_Click" />
                            </p>
                            <a>
                                <asp:Label ID="lbl_mensagem" runat="server" CssClass="mt-3" /></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
