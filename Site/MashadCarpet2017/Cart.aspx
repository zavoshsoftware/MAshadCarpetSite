<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="MashadCarpet.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content" role="main">
        <div class="breadcrumb-container">

            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">
                            <li><a href="/default.aspx" title="Home">
                                <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a> <i class="fa fa-arrow-right newfa"></i></li>
                            <li class="active">
                                <asp:Literal ID="ltContactTitle" runat="server" Text="<%$Resources:Resource,YourShoppingCart%>"></asp:Literal></li>
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
                <asp:Panel ID="pnlEmpry" runat="server" Visible="false">
                    <p class="alert-Error">
                        <asp:Literal ID="ltEmptyBasket" runat="server" Text="<%$Resources:Resource,EmptyBasket%>"></asp:Literal>
                    </p>
                </asp:Panel>

                <asp:Panel ID="pnlOrderDetails" runat="server">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlOrders" runat="server">
                            <table class="table cart-table directionRTL text-right">
                                <thead>
                                    <tr>

                                        <th class="table-title text-right">
                                            <asp:Literal ID="ltImg" runat="server" Text="<%$Resources:Resource,Image%>"></asp:Literal></th>
                                        <th class="table-title text-right">
                                            <asp:Literal ID="ltrpductname" runat="server" Text="<%$Resources:Resource,ProductName%>"></asp:Literal></th>
                                        <th class="table-title text-right">
                                            <asp:Literal ID="ltPrie" runat="server" Text="<%$Resources:Resource,Priceper%>"></asp:Literal></th>
                                        <th class="table-title text-right">
                                            <asp:Literal ID="ltPriceAfterDiscount" runat="server" Text="<%$Resources:Resource,PriceAfterDiscount%>"></asp:Literal></th>
                                        <th class="table-title text-right">
                                            <asp:Literal ID="ltNumber" runat="server" Text="<%$Resources:Resource,Number%>"></asp:Literal></th>
                                        <th class="table-title text-right">
                                            <asp:Literal ID="ltTotalfactor" runat="server" Text="<%$Resources:Resource,Totalfactor%>"></asp:Literal></th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <asp:Repeater ID="rptOrders" runat="server" OnItemDataBound="rptOrders_ItemDataBound" OnItemCommand="rptOrders_ItemCommand">
                                    <ItemTemplate>


                                        <tbody>
                                            <tr>
                                                <td class="text-right product-name-col">
                                                    <figure>
                                                        <a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>'>
                                                            <img src='<%# Eval("ProductImage","/Uploads/Products/{0}") %>' alt="فرش مشهد" style="width: 170px; height: 250px;"></a>
                                                    </figure>
                                                </td>
                                                <td class="text-right">

                                                    <h2 class="product-name"><a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>'><%# Eval("ProductTitle") %></a></h2>
                                                    <ul>
                                                        <li>
                                                            <asp:Literal ID="ltColor" runat="server" Text="<%$Resources:Resource,Color%>"></asp:Literal>: <%# Eval("ColorTitle") %></li>
                                                        <li>
                                                            <asp:Literal ID="ltsize" runat="server" Text="<%$Resources:Resource,Size%>"></asp:Literal>: <%# Eval("SizeTitle") %></li>
                                                    </ul>
                                                </td>
                                                <td class="product-price-col product-old-price"><span class="fontBRoya">
                                                    <%# string.Format("{0:N0}",Eval("ProductPrice")) %> </span></td>
                                                <td class="product-total-col"><span class="product-price-special">
                                                    <asp:HiddenField ID="hfOrderDetailID" runat="server" Value='<%# Eval("OrderDetailID") %>' />
                                                    <asp:Label ID="lblPriceWithDiscount" runat="server" Text="" CssClass="fontBRoya"></asp:Label></span>
                                                </td>
                                                <td>
                                                    <div class="custom-quantity-input">
                                                        <%--<input type="text" name="quantity" value="1">--%>
                                                        <asp:TextBox ID="txtCount" runat="server" Text='<%# Eval("Count") %>'></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td class="product-total-col"><span class="product-price-special">
                                                    <asp:Label ID="lblTotalPrice" runat="server" Text="" CssClass="fontBRoya"></asp:Label></span>
                                                </td>
                                                <td>
                                                    <%--<a href="#" class="close-button"></a>--%>
                                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="close-button" CommandArgument='<%# Eval("OrderDetailID") %>'></asp:LinkButton>
                                                </td>
                                            </tr>

                                        </tbody>



                                    </ItemTemplate>
                                </asp:Repeater>

                            </table>
                        </asp:Panel>
                        <div class="md-margin"></div>
                        <!-- space -->
                        <div class="bordered-box">
                            <div class="row directionRTL">


                                <div class="md-margin visible-sm visible-xs clearfix"></div>
                                <div class="col-md-2 text-center middle float-right">
                                    <asp:Literal ID="ltDesc" runat="server" Text="<%$Resources:Resource,Description%>"></asp:Literal>
                                </div>
                                <div class="col-md-10 float-right">
                                    <asp:TextBox ID="txtOrderDesc" runat="server" Rows="5" TextMode="MultiLine" Width="100%" CssClass="pull-right"></asp:TextBox>
                                </div>

                                <div class="md-margin visible-sm visible-xs clearfix"></div>
                            </div>
                            <br />
                            <div class="row directionRTL">
                                <div class="col-md-2 float-right text-center middle">
                                    <asp:Literal ID="ltTotal" runat="server" Text="<%$Resources:Resource,Totalfactor%>"></asp:Literal>:
                                </div>
                                <div class="col-md-10 float-right">
                                    <h4>
                                        <asp:Label ID="lblTotalAllPrice" runat="server" Text="" CssClass="fontBRoya"></asp:Label></h4>
                                </div>
                            </div>
                             <div class="md-margin visible-sm visible-xs clearfix"></div>
                            <div class="row directionRTL">

                                <div class="col-md-4">
                                    <asp:Button ID="btnUpdateBasket" runat="server" Style="padding: 16px 18px;" Text="<%$Resources:Resource,UpdateBasket%>" CssClass="btn btn-custom-6 btn-lger min-width-sm btn-cart" OnClick="btnUpdateBasket_Click" />
                                    <asp:Button ID="btnpay" runat="server" class="btn btn-custom-6 btn-lger min-width-sm btn-cart" Style="padding: 16px 18px;" Text="<%$Resources:Resource,OnlibePayment%>" OnClick="btnpay_Click" />

                                </div>
                            </div>
                            <%--  <div class="row">


                                <div class="md-margin visible-sm visible-xs clearfix"></div>
                                <!-- clear sm-xs -->

                                <div class="col-md-6">

                                    <div class="text-right marginLeft col-md-6">
                                        <asp:Button ID="btnUpdateBasket" runat="server" Style="padding: 16px 18px;" Text="<%$Resources:Resource,UpdateBasket%>" CssClass="btn btn-custom-6 btn-lger min-width-sm btn-cart" OnClick="btnUpdateBasket_Click" />
                                    </div>
                                    <div class="text-right col-md-4">
                                        <asp:Button ID="btnpay" runat="server" class="btn btn-custom-6 btn-lger min-width-sm btn-cart" Style="padding: 16px 18px;" Text="<%$Resources:Resource,OnlibePayment%>" OnClick="btnpay_Click" />
                                    </div>
                                </div>
                                <div class="md-margin visible-sm visible-xs clearfix"></div>
                                <div class="col-md-6">

                                    <table class="table total-table text-right directionRTL fontDroid">
                                        <tbody>
                                            <tr>
                                                <td class="total-table-title">
                                                    <asp:Literal ID="ltTotal" runat="server" Text="<%$Resources:Resource,Totalfactor%>"></asp:Literal>:</td>
                                                <td>
                                                    <asp:Label ID="lblTotalAllPrice" runat="server" Text="" CssClass="fontBRoya"></asp:Label></td>
                                            </tr>

                                        </tbody>

                                    </table>

                                    <div class="md-margin"></div>

                                </div>
                                <!-- End .col-md-4 -->


                            </div>--%>
                        </div>
                        <!-- End .row -->
                    </div>
                    <!-- End .col-md-12 -->
                </asp:Panel>
            </div>
            <!-- End .row -->
        </div>
        <!-- End .container -->

        <div class="lg-margin2x"></div>
        <!-- space -->
        <div class="container directionRTL">
            <asp:Label ID="lblCall" runat="server" Text="Label"></asp:Label>
        </div>
    </section>
    <!-- End #content -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>

    <script src="/js/persianumber.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            $('#ContentPlaceHolder1_lblTotalAllPrice').persiaNumber();

        });

    </script>
</asp:Content>
