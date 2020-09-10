<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="MashadCarpet.ProductList" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
    <script src="/js/jquery-2.1.1.min.js"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content" role="main">
        <div class="breadcrumb-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">
                            <li><a href="Default.aspx" title="Home">
                                <asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a>
                                <i class="fa fa-arrow-right newfa"></i>
                            </li>
                           <li>
                                <a href="/carpet-online-shopping">
                                    <asp:Literal ID="lblProductsTitle" runat="server" Text="<%$Resources:Resource,ProductsTitle%>"></asp:Literal></a>
                                 <i class="fa fa-arrow-right newfa"></i>
                           </li>
                            <li class="active">
                                <h1>
                                <asp:Label ID="progroupList" runat="server" Text=""></asp:Label></h1>
                            </li>
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
                <div class="col-sm-12">
                 
                    <div id="category-banner" class="valign-nav">
                        <div class=" category-banner-slider">
                            <asp:Repeater ID="rptbannerImages" runat="server">
                                <ItemTemplate>
                                    <div class="banner-item">
                                        <img src='<%# Eval("imgSliderImage","/Uploads/ProductGroup/{0}") %>' alt='<%# Eval("ProductGroupTitle") %>' class="img-responsive" style="width: 1170px; height: 370px;" />
                                        <div class="banner-container nopadding text-center">
                                            <div class="vcenter-container">
                                                <div class="vcenter">
                                                    <div class="banner-content">
                                                        <h3 style="color: #fff;" class="directionRTL">
                                                            <%# Eval("ProductGroupTitle") %>

                                                        </h3>
                                                        <%--<a href='<%# Eval("ProductGroupName","/carpet-online-shopping/{0}") %>'
                                                            class="btn btn-custom-7 min-width-md fontYekan">
                                                            <asp:Literal ID="ltonlineshopping" runat="server" Text="<%$Resources:Resource,onlineshopping%>"></asp:Literal></a>--%>
                                                    </div>
                                                </div>
                                                <!-- End .vcenter -->
                                            </div>
                                            <!-- End .vcenter-container -->
                                        </div>
                                        <!-- End .banner-content -->
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <!-- End .banner-item -->

                            <!-- End .banner-item -->

                            <!-- End .banner-item -->
                        </div>
                        <!-- End .owl-carousel -->
                    </div>
                    <!-- End #category-banner -->

                    <div class="lg-margin"></div>
                    <!-- space -->
                    <asp:Panel ID="pnlDesc" runat="server" CssClass="descstyle propageDesc">
                      
                        <asp:Literal ID="ltPageDesc" runat="server"></asp:Literal> 
                     </asp:Panel>
                    <div class="lg-margin"></div>

                    <div class="row">
                        <aside class="col-md-3 sidebar margin-top-up" role="complementary">
                            <div class="widget">
                                <h3>
                                    <asp:LinkButton ID="lbAllProductGroup" runat="server" OnClick="lbAllProductGroup_Click" ForeColor="#908876">
                                        <asp:Literal ID="ltproductGroup" runat="server" Text="<%$Resources:Resource,ProductsGroup %>"></asp:Literal>
                                    </asp:LinkButton>
                                </h3>

                                <ul id="category-widget">
                                    <asp:Repeater ID="rptCategoryGroup" runat="server" OnItemDataBound="rptCategoryGroup_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfCatID" Value='<%# Eval("ProductGroupName") %>' runat="server" />
                                            <li>
                                                <asp:HyperLink CssClass="directionRTL" ID="hlprogroupLink" runat="server" NavigateUrl='<%# Eval("ProductGroupName","~/carpet-online-shopping/{0}") %>'>
                                             <%# Eval("ProductGroupTitle") %></asp:HyperLink>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <!-- End .widget -->
                            <div class="widget">
                                <div class="accordion" id="sidebar-collapse-filter">
                                    <div class="accordion-group panel">
                                        <div class="accordion-title">
                                            <div style="color: #908876;">
                                                <asp:Literal ID="ltColorfilter" runat="server" Text="<%$Resources:Resource,Colorfilter %>"></asp:Literal>
                                            </div>
                                            <a class="accordion-btn open leftinFa" data-toggle="collapse" href="#color-filter"></a>
                                        </div>
                                        <!-- End .accourdion-title -->
                                        <div class="accordion-body collapse in" id="color-filter">
                                            <div class="accordion-body-wrapper">
                                                <div class="filter-color-container">
                                                    <div class="row">
                                                        <asp:Repeater ID="rptColors" runat="server" OnItemCommand="rptColors_ItemCommand" OnItemDataBound="rptColors_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hfColorID" Value='<%# Eval("ColorID") %>' runat="server" />
                                                                <asp:LinkButton ID="lbColors" runat="server" class='<%# Eval("ColorID","filter-color-box class{0}") %>' CommandArgument='<%# Eval("ColorID") %>' data-bgcolor='<%# Eval("ColorNo") %>' ToolTip='<%# Eval("ColorTitle") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                    <!-- End .row -->
                                                </div>
                                                <!-- End .filter-color-container -->
                                            </div>
                                            <!-- End .accordion-body-wrapper -->
                                        </div>
                                        <!-- End .accordion-body -->
                                    </div>
                                    <!-- End .accordion-group -->
                                    <div class="accordion-group panel">
                                        <div class="accordion-title">
                                            <div style="color: #908876;">
                                                <asp:Literal ID="ltSizeFilter" runat="server" Text="<%$Resources:Resource,SizeFilter %>"></asp:Literal>
                                            </div>
                                            <a class="accordion-btn open leftinFa" data-toggle="collapse" href="#size-filter"></a>
                                        </div>
                                        <!-- End .accourdion-title -->

                                        <div class="accordion-body collapse in" id="size-filter">
                                            <div class="accordion-body-wrapper">
                                                <div class="filter-color-container">
                                                    <div class="row">
                                                        <asp:Repeater ID="rptSize" runat="server" OnItemCommand="rptSize_ItemCommand" OnItemDataBound="rptSize_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hfSizeID" Value='<%# Eval("SizeID") %>' runat="server" />
                                                                <asp:LinkButton ID="lbSizeFilter" runat="server" class='<%# Eval("SizeID","filter-size-box active fontBRoya class{0}") %>' CommandArgument='<%# Eval("SizeID") %>'><%# Eval("SizeTitle") %></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:Repeater>

                                                    </div>
                                                    <!-- End .row -->
                                                </div>
                                                <!-- End .filter-color-container -->
                                            </div>
                                            <!-- End .accordion-body-wrapper -->
                                        </div>
                                        <!-- End .accordion-body -->
                                    </div>
                                    <!-- End .accordion-group -->

                                </div>
                                <!-- End .accordion -->
                            </div>
                            <!-- End .widget -->



                            <!-- End .widget -->

                            <div class="widget">
                                <div class="sidebar-banner">
                                    <img src="/images/banners/1.jpg" alt="banner" class="img-responsive">
                                    <div class="sidebar-banner-content">
                                        <div class="vcenter-container">
                                            <div class="vcenter">
                                                <a href="/carpet-online-shopping" class="btn btn-custom-7 min-width-md">
                                                    <asp:Literal ID="ltProductsTitle" runat="server" Text="<%$Resources:Resource,ProductsTitle %>"></asp:Literal></a>
                                            </div>
                                            <!-- End .vcenter -->
                                        </div>
                                        <!-- End .vcenter-container -->
                                    </div>
                                    <!-- End .sidebar-banner-content -->
                                </div>
                                <!-- End .sidebar-banner -->
                            </div>

                            <!-- End .widget -->

                        </aside>
                        <!-- End .sidebar -->

                        <div class="col-md-9 padding-right-lg">


                            <div class="category-grid">
                                <asp:Panel ID="pnlEmpty" runat="server" Visible="false">
                                    <p style="text-align: center; direction: rtl; color: red; font-size: 20px;">
                                        <asp:Literal ID="ltNotAvailableProduct" runat="server" Text="<%$Resources:Resource,NotAvailableProduct %>"></asp:Literal>
                                    </p>
                                </asp:Panel>

                                <asp:Repeater ID="rptProducts" runat="server">
                                    <HeaderTemplate>
                                        <div class="row">
                                    </HeaderTemplate>

                                    <ItemTemplate>

                                        <div class="col-md-3 md-margin2x floatright">
                                            <div class="product">
                                                <div class="product-top">

                                                    <figure class="product-image-container">
                                                        <a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>' title="White linen sheer dress">                                                            
                                                            <img src='<%# Eval("ProductImage","/PhotoHandler.ashx?h=300&w=220&file=/uploads/Products/{0}") %>' alt='<%# Eval("ProductTitle") %>' class="product-image">
                                                            <img src='<%# Eval("ProductImage","/PhotoHandler.ashx?h=300&w=220&file=/uploads/Products/{0}") %>' alt='<%# Eval("ProductTitle") %>' class="product-image-hover">
                                                        </a>
                                                    </figure>

                                                    <div class="product-action-container">
                                                        <div class="product-action-wrapper action-responsive">
                                                            <a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>' style="width: 100%;" title="Add to Cart" class="product-add-btn">
                                                                <span class="">
                                                                    <asp:Literal ID="ltShowProductDetail" runat="server" Text="<%$Resources:Resource,ShowProductDetail %>"></asp:Literal></span>
                                                            </a>
                                                        </div>
                                                        <!-- End .product-action-wrapper -->
                                                    </div>
                                                    <!-- End .product-action-container -->
                                                </div>
                                                <!-- End .product-top -->
                                                <h2 class="product-name"><a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>' title="White linen sheer dress"><%# Eval("ProductTitle") %></a></h2>
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


                                <!-- Emd .col-sm-4 -->

                                <!-- Emd .col-sm-4 -->

                                <!-- End .row -->

                                <!-- End .row -->

                                <!-- End pagination-container -->

                            </div>
                            <!-- End .category-grid -->

                            <div class="md-margin2x visible-sm visible-xs"></div>
                            <!-- space -->
                        </div>
                        <!-- End .col-md-9 -->


                    </div>
                    <!-- End .row -->
                </div>
                <!-- End .col-sm-12 -->
            </div>
            <!-- End .row -->
        </div>
        <!-- End .container -->

        <div class="lg-margin3x hidden-xs"></div>
        <!-- space -->
        <div class="md-margin2x visible-xs"></div>
        <!-- space -->

    </section>
    <!-- End #content -->
</asp:Content>
