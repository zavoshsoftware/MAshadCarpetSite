<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="MyTickets.aspx.cs" Inherits="MashadCarpet.MyTickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content" role="main">
        <div class="breadcrumb-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">
                            <li><a href="/Default.aspx" title="Home"><asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a></li>
                            <li class="active"><asp:Literal ID="ltInvoicesList" runat="server" Text="<%$Resources:Resource,InvoicesList%>"></asp:Literal></li>
                        </ul>
                    </div>
                    <!-- End .col-md-12 -->
                </div>
                <!-- End .row -->
            </div>
            <!-- End .container -->
        </div>
        <!-- End .breadcrumb-container -->
        <div class="container">
            <div class="row">
                <div class="col-md-12">





                    <asp:Panel ID="pnlTickets" runat="server">
                        <table class="table cart-table directionRTL text-right">
                            <thead>
                                <tr id="row">
                                    <th class="table-title text-right"><asp:Literal ID="ltTicketDate" runat="server" Text="<%$Resources:Resource,TicketDate%>"></asp:Literal></th>
                                    <th class="table-title text-right"><asp:Literal ID="ltStatus" runat="server" Text="<%$Resources:Resource,Status%>"></asp:Literal></th>
                                    <th class="table-title text-right"></th>

                                    <th></th>
                                </tr>
                            </thead>
                            <asp:Repeater ID="rptTickets" runat="server" OnItemDataBound="rptTickets_ItemDataBound" OnItemCommand="rptTickets_ItemCommand">
                                <ItemTemplate>


                                    <tbody>
                                        <tr id="row" runat="server">
                                            <td class="product-name-col text-right">
                                                <div class="product-price-special">
                                                    <%# string.Format("{0:dd MMMM yyyy}",Eval("TicketDate")) %>
                                                </div>
                                            </td>



                                            <td class="product-price-col">
                                                <div class="product-price-special">
                                                    <asp:HiddenField ID="hfTicketID" runat="server" Value='<%# Eval("TicketID") %>' />
                                                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="product-price-special">
                                                    <asp:LinkButton ID="lbShow" runat="server" CommandName="Show" CommandArgument='<%# Eval("TicketID") %>'><asp:Literal ID="ltShow" runat="server" Text="<%$Resources:Resource,Show%>"></asp:Literal></asp:LinkButton>
                                                </div>
                                            </td>

                                        </tr>


                                    </tbody>



                                </ItemTemplate>
                            </asp:Repeater>

                        </table>

                    </asp:Panel>



                    <asp:Panel ID="pnlAswers" runat="server" Visible="false">
                        <div class="col-sm-8 col-md-push-2 padding-left-md">
                            <div style="background-color: #E7E2D1; color: #A29C8E; padding: 10px; text-align: center;">
                               <span class="text-center"> <asp:Literal ID="ltTicket" runat="server" Text="<%$Resources:Resource,Ticket%>"></asp:Literal></span>
                                <asp:LinkButton ID="lbBack" runat="server" OnClick="lbBack_Click" CssClass="pull-left"><asp:Literal ID="ltReturn" runat="server" Text="<%$Resources:Resource,Return%>"></asp:Literal></asp:LinkButton>
                            </div>
                            <div style="padding: 10px; margin-bottom: 10px;">
                                <asp:Label ID="lblDate" runat="server" Text="" CssClass="pull-right" Style="margin-left: 25px;"></asp:Label>
                                <asp:Label ID="lblUserName" runat="server" Text="" CssClass="pull-right"></asp:Label>
                                <br />
                                <hr />
                                <div class="text-right">
                                    <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-8 col-md-push-2 padding-left-md">
                            <div style="background-color: #E7E2D1; color: #A29C8E; padding: 10px; text-align: center;">
                                <asp:Literal ID="ltAnswers" runat="server" Text="<%$Resources:Resource,Answers%>"></asp:Literal>
                            </div>
                        </div>
                        <asp:Repeater ID="rptAnswers" runat="server">
                            <ItemTemplate>
                                <div class="col-sm-8 col-md-push-2 padding-left-md">

                                    <div style="padding: 10px; border-bottom: 1px solid #A9A399;">
                                        <span class="pull-right" style="margin-left: 25px;">  <asp:Literal ID="ltDate" runat="server" Text="<%$Resources:Resource,Date%>"></asp:Literal>:  <%# string.Format("{0:d}",Eval("ResponseDate")) %>
                                        </span>
                                        <span class="pull-right"> <asp:Literal ID="ltUserName" runat="server" Text="<%$Resources:Resource,UserName%>"></asp:Literal>:   <%# Eval("UserName") %>
                                        </span>
                                        <br />
                                        <hr />
                                        <div class="text-right directionRTL">
                                            <%# Eval("ResponseText") %>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                      
                        <div class="col-sm-8 col-md-push-2 padding-left-md" style="margin-top:25px;">
                            <div class="input-group textarea-container">
                                <div style="background-color: #E7E2D1; color: #A29C8E; padding: 10px; text-align: center;">
                                    <asp:Literal ID="ltAns" runat="server" Text="<%$Resources:Resource,Answers%>"> </asp:Literal>
                                </div>
                                <asp:TextBox ID="txtAnswerText" runat="server" class="form-control text-right" Rows="6" Columns="30" TextMode="MultiLine"></asp:TextBox>
                                <br /><br />
                                <asp:Button ID="btnSend" runat="server" Text="<%$Resources:Resource,Send%>" CssClass="btn btn-custom pull-right" OnClick="btnSend_Click" />
                            </div>
                        </div>

                    </asp:Panel>

                </div>
                <!-- End .col-md-12 -->
            </div>
        </div>

    </section>
</asp:Content>
