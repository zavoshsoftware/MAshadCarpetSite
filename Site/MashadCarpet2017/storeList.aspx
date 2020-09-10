<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="storeList.aspx.cs" Inherits="MashadCarpet.storeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="content" role="main">


        <%--  <asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>--%>
        <div class="breadcrumb-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                    <%--    <ul class="breadcrumb">
                            <li><a href="/Default.aspx" title="Home">
                                <asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a></li>
                            <li class="active">
                                <asp:Literal ID="lblMarkets" runat="server" Text="<%$Resources:Resource,Markets%>"></asp:Literal></li>
                        </ul>--%>


                           <ul class="breadcrumb">
                            <li><a href="default.aspx" title="Home">
                                <asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a> <i class="fa fa-arrow-right newfa"></i></li>
                            <li class="active">
                                <asp:Literal ID="ltContactTitle" runat="server" Text="<%$Resources:Resource,Markets%>"></asp:Literal></li>
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
                    <div id="category-banner" class="valign-nav">
                        <div >





                            <div>
                                <img src="images/Namayandegi-Site.jpg" class="img-responsive" style="width: 1170px; margin-bottom: 20px;" />


                            </div>

                            <!-- End .banner-item -->

                            <!-- End .banner-item -->

                            <!-- End .banner-item -->
                        </div>
                        <!-- End .owl-carousel -->
                    </div>


                    <table class="table cart-table directionRTL text-right">
                        <thead>
                            <tr id="row">


                                <th class="table-title text-right">
                                    <asp:Literal ID="ltStorecity" runat="server" Text="<%$Resources:Resource,StoreCity%>"></asp:Literal></th>
                                <th class="table-title text-right">
                                    <asp:Literal ID="ltAddress" runat="server" Text="<%$Resources:Resource,StoreAddress%>"></asp:Literal></th>
                                <th class="table-title text-right">
                                    <asp:Literal ID="ltPhone" runat="server" Text="<%$Resources:Resource,StorePhone%>"></asp:Literal></th>


                            </tr>
                        </thead>
                        <asp:Repeater ID="rptStores" runat="server">
                            <ItemTemplate>


                                <tbody>
                                    <tr id="row" runat="server">





                                        <td>
                                            <div class="product-price-special">
                                                <%# Eval("StoreCity") %>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="product-price-special">
                                                <%# Eval("StoreAddress") %>
                                            </div>
                                        </td>
                                        <td class="product-price-col">
                                            <div class="custom-quantity-input fontBYekan font21">
                                                <%# Eval("StorePhone") %>
                                            </div>
                                        </td>
                                    </tr>



                                    <tr>
                                    </tr>

                                </tbody>



                            </ItemTemplate>
                        </asp:Repeater>

                    </table>

                </div>
            </div>
        </div>
    </section>
</asp:Content>
