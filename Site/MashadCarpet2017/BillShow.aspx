<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="BillShow.aspx.cs" Inherits="MashadCarpet.BillShow" %>

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
                      <asp:Panel ID="pnlSuccess" CssClass="alert alert-succeed" Visible="false" runat="server">
پرداخت شما با موفقیت انجام شد. کد رهگیری پرداخت: <asp:Label ID="lblSaleRefID" runat="server" Text=""></asp:Label>                    </asp:Panel>
                    <asp:Panel ID="pnlError" CssClass="alert alert-danger" Visible="false" runat="server">
                        کاربر گرامی خطایی در عملیات پرداخت رخ داده است، لطفا مجدداً عملیات پرداخت را انجام دهید.
                    </asp:Panel>
                    <asp:Panel ID="pnlOrders" runat="server">
                        <table class="table cart-table directionRTL text-right">
                            <thead>
                                <tr id="row">
                                    <th class="table-title text-right"><asp:Literal ID="ltorderdate" runat="server" Text="<%$Resources:Resource,orderdate%>"></asp:Literal></th>
                                    <th class="table-title text-right"><asp:Literal ID="ltIsPaid" runat="server" Text="<%$Resources:Resource,IsPaid%>"></asp:Literal></th>
                                    <th class="table-title text-right"><asp:Literal ID="ltamount" runat="server" Text="<%$Resources:Resource,amount%>"></asp:Literal></th>
                                    <th class="table-title text-right"></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <asp:Repeater ID="rptOrders" runat="server" OnItemDataBound="rptOrders_ItemDataBound" 
                                OnItemCommand="rptOrders_ItemCommand">
                                <ItemTemplate>


                                    <tbody>
                                        <tr id="row" runat="server">
                                            <td class="product-name-col text-right">
                                                <div class="product-price-special">
                                                    <%# string.Format("{0:dd MMMM yyyy}",Eval("SubmitDate")) %>
                                                </div>
                                            </td>



                                            <td class="product-price-col">
                                                <div class="custom-quantity-input">
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" Checked='<%# Eval("IsPaid") %>' />
                                                </div>
                                            </td>
                                            <td>
                                                <div class="product-price-special">
                                                    <asp:HiddenField ID="hfOrderID" runat="server" Value='<%# Eval("OrderID") %>' />
                                                    <asp:Label ID="lblPrice" runat="server" Text="" CssClass="fontBRoya"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbOrderDetails" runat="server" CommandName="factorDetail" CommandArgument='<%# Eval("OrderID") %>'><asp:Literal ID="ltShowDetails" runat="server" Text="<%$Resources:Resource,ShowDetails%>"></asp:Literal></asp:LinkButton>
                                                <br />
                                            
                                                <asp:HyperLink ID="hlPayment" runat="server" CommandArgument='<%# Eval("OrderID") %>'><asp:Literal ID="ltPayMent" runat="server" Text="<%$Resources:Resource,PayMent%>"></asp:Literal></asp:HyperLink>      <br />
                                                <asp:LinkButton ID="lbFactorDl" runat="server" CommandName="factorDL" CommandArgument='<%# Eval("OrderID") %>'><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Resource,FactorDownload%>"></asp:Literal></asp:LinkButton>
                                          
                                                
                                                  </td>
                                            <td></td>
                                        </tr>



                                        <tr>
                                            <td colspan="5" style="border: none! important;">
                                                <asp:Panel ID="pnlOrderDetails" runat="server" Visible="false">
                                                    <table class="table checkout-table directionRTL text-right">
                                                        <thead>
                                                            <tr>
                                                                <th class="table-title text-right" style="background-color: #1694D6; color: #fff;"><asp:Literal ID="ltProductName" runat="server" Text="<%$Resources:Resource,ProductName%>"></asp:Literal></th>
                                                                <th class="table-title text-right" style="background-color: #1694D6; color: #fff;"><asp:Literal ID="ltPriceper" runat="server" Text="<%$Resources:Resource,Priceper%>"></asp:Literal></th>
                                                                <th class="table-title text-right" style="background-color: #1694D6; color: #fff;"><asp:Literal ID="ltNumber" runat="server" Text="<%$Resources:Resource,Number%>"></asp:Literal></th>
                                                                <th class="table-title text-right" style="background-color: #1694D6; color: #fff;"><asp:Literal ID="ltTotalPrice" runat="server" Text="<%$Resources:Resource,TotalPrice%>"></asp:Literal></th>
                                                                <th style="background-color: #1694D6; color: #fff;"></th>
                                                            </tr>
                                                        </thead>

                                                        <asp:Repeater ID="rptOrderDetails" runat="server" OnItemDataBound="rptOrderDetails_ItemDataBound">
                                                            <ItemTemplate>

                                                                <tbody>
                                                                    <tr>
                                                                        <td class="product-name-col text-right">
                                                                            <figure>
                                                                                <a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>'>
                                                                                    <img src='<%# Eval("ProductImage","/Uploads/Products/{0}") %>' alt="White linen sheer dress" style="width: 170px; height: 250px;"></a>
                                                                            </figure>
                                                                            <h2 class="product-name"><a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>'><%# Eval("ProductTitle") %></a></h2>
                                                                            <ul>
                                                                                <li><asp:Literal ID="ltColor" runat="server" Text="<%$Resources:Resource,Color%>"></asp:Literal>: <%# Eval("ColorTitle") %></li>
                                                                                <li><asp:Literal ID="ltSize" runat="server" Text="<%$Resources:Resource,Size%>"></asp:Literal>: <%# Eval("SizeTitle") %></li>
                                                                            </ul>
                                                                        </td>



                                                                        <td class="product-price-col"><span class="product-price-special fontBRoya"><%# string.Format("{0:N0}",Eval("ProductPrice")) %> </span></td>
                                                                        <td>
                                                                            <div class="custom-quantity-input">
                                                                                <asp:TextBox ID="txtCount" runat="server" Text='<%# Eval("Count") %>' Enabled="false"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td class="product-total-col"><span class="product-price-special">
                                                                            <asp:HiddenField ID="hfOrderDetailID" runat="server" Value='<%# Eval("OrderDetailID") %>' />
                                                                            <asp:Label ID="lblTotalPrice" runat="server" Text="" CssClass="fontBRoya"></asp:Label></span>
                                                                        </td>

                                                                    </tr>

                                                                </tbody>



                                                            </ItemTemplate>
                                                        </asp:Repeater>

                                                    </table>
                                                </asp:Panel>
                                            </td>

                                        </tr>

                                    </tbody>



                                </ItemTemplate>
                            </asp:Repeater>

                        </table>

                    </asp:Panel>

                    <div class="md-margin"></div>
                    <!-- space -->
                 <%--   <div class="text-right pull-right">
                        <a href="/Bank.aspx" class="btn btn-custom-6 btn-lger min-width-sm" style="padding: 16px 18px;">پرداخت آنلاین</a>
                    </div>--%>

                    <!-- End .row -->
                </div>
                <!-- End .col-md-12 -->
            </div>
        </div>

    </section>
</asp:Content>
