<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ProductGroupList.aspx.cs" Inherits="MashadCarpet.ProductGroupList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content" role="main">
        <div class="breadcrumb-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">
                            <li><a href="Default.aspx" title="Home">
                                <asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a>    <i class="fa fa-arrow-right newfa"></i></li>
                            <li class="active">
                                <asp:Literal ID="lblProductsTitle" runat="server" Text="<%$Resources:Resource,ProductsTitle%>"></asp:Literal></li>
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
                    <div class="category-grid">
                        <asp:Panel ID="pnlEmpty" runat="server" Visible="false">
                            <p style="text-align: center; direction: rtl; color: red; font-size: 20px;">
                                <asp:Literal ID="ltNotAvailable" runat="server" Text="<%$Resources:Resource,NotAvailableProduct%>"></asp:Literal>.
                            </p>
                        </asp:Panel>
                        <div class="alert pageTitleAndDesc">
                            <div class="testimonials-section light parallax" data-stellar-background-ratio="0.4">
                                <div class="container">
                                    <h1 class="h1">
                                        <asp:Literal ID="ltPageTitle" runat="server"></asp:Literal>
                                    </h1>
                                </div>
                                <!-- End .container -->
                                <div class="vcenter-container">
                                    <div class="vcenter bottom-nav light-nav">
                                        <div class=" testimonials-slider">
                                            <div class="testimonial">
                                                <p>
                                                    <asp:Literal ID="ltPageDesc" runat="server"></asp:Literal>
                                                  </p>
                                            </div>
                                            <!-- End .testimonial -->
                                        </div>
                                        <!-- End .about-banner-slider -->
                                    </div>
                                    <!-- End .vcenter -->
                                </div>
                                <!-- End .vcenter-container -->
                            </div>
                            <!-- end .testimonials-section -->
                        </div>
                        <asp:Repeater ID="rptProductgroups" runat="server">
                            <HeaderTemplate>
                                <div class="row">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="col-md-3 md-margin2x floatright">
                                    <div class="product">
                                        <div class="product-top">
                                            <figure class="product-image-container">
                                                <a href='<%# Eval("ProductGroupName","/carpet-online-shopping/{0}") %>' title="">
                                                    <img src='<%# Eval("ProductGroupImage","/Uploads/ProductGroup/{0}") %>' class="sizeImg" style="height: auto; width: auto" alt='<%# Eval("ProductGroupTitle") %>' />
                                                </a>
                                            </figure>

                                            <div class="product-action-container">
                                                <div class="product-action-wrapper action-responsive">
                                                    <a href='<%# Eval("ProductGroupName","/carpet-online-shopping/{0}") %>' style="width: 100%;" title="Add to Cart" class="product-add-btn">
                                                        <span class="">
                                                            <asp:Literal ID="ltShowProducts" runat="server" Text="<%$Resources:Resource,ShowProducts%>"></asp:Literal></span>
                                                    </a>
                                                </div>
                                                <!-- End .product-action-wrapper -->
                                            </div>
                                            <!-- End .product-action-container -->
                                        </div>
                                        <!-- End .product-top -->
                                        <h2 class="product-name"><a href='<%# Eval("ProductGroupName","/carpet-online-shopping/{0}") %>' title="White linen sheer dress fontBYekan"><%# Eval("ProductGroupTitle") %></a></h2>
                                        <div class="product-price-container">
                                            <span class="product-price">
                                                <asp:Label ID="lblPrice" runat="server" Text="" CssClass="fontBRoya"></asp:Label>
                                            </span>
                                        </div>
                                        <!-- End .product-price-container -->
                                    </div>
                                    <!-- End .product -->
                                </div>
                            </ItemTemplate>
                            <FooterTemplate></div></FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="container directionRTL">
                        <asp:Label ID="lblCall" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="md-margin2x visible-sm visible-xs"></div>
                    <!-- space -->
                </div>
            </div>
        </div>
    </section>
</asp:Content>
