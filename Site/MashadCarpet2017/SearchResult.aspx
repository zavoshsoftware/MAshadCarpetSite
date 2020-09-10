<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SearchResult.aspx.cs" Inherits="MashadCarpet.SearchResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/jquery.js"></script>

    <script type="text/javascript">
        function AddCurrent(currentMenu) {
            $('li').removeClass('active');
            $(currentMenu).addClass('active');
        }
    </script>
    <section id="content" role="main">
        <div class="breadcrumb-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">
                          <li><a href="Default.aspx" title="Home"><asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a>    <i class="fa fa-arrow-right newfa"></i></li>
                            <li class="active"><asp:Literal ID="lblProductsTitle" runat="server" Text="<%$Resources:Resource,SearchBtn%>"></asp:Literal></li>
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
                        <div class="owl-carousel category-banner-slider">




                            <asp:Repeater ID="rptbannerImages" runat="server">
                                <ItemTemplate>
                                    <div class="banner-item">
                                        <img src='<%# Eval("imgSliderImage","/Uploads/ProductGroup/{0}") %>' alt='<%# Eval("ProductGroupTitle") %>' class="img-responsive" style="width: 1170px; height: 370px;" />
                                        <div class="banner-container nopadding text-center">
                                            <div class="vcenter-container">
                                                <div class="vcenter">
                                                    <div class="banner-content">
                                                        <h1 style="color: #fff;" class="directionRTL fontYekan">
                                                            <%# Eval("ProductGroupTitle") %>

                                                        </h1>
                                                        <a href='<%# Eval("ProductGroupName","/carpet-online-shopping/{0}") %>'
                                                            class="btn btn-custom-7 min-width-md fontYekan">
                                                            <asp:Literal ID="ltonlineshopping" runat="server" Text="<%$Resources:Resource,onlineshopping%>"></asp:Literal></a>
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

                    <div class="row">
                        <aside class="col-md-3 sidebar margin-top-up" role="complementary">
                            <div class="widget">
                                <h3>
                                    <asp:LinkButton ID="lbAllProductGroup" runat="server" ForeColor="#908876">   گروه محصولات</asp:LinkButton>
                                     
                                </h3>

                                <ul id="category-widget">
                                    <asp:Repeater ID="rptCategoryGroup" runat="server">
                                        <ItemTemplate>
                                            <li><a class="directionRTL" href='<%# Eval("ProductGroupName","/carpet-online-shopping/{0}") %>'><%# Eval("ProductGroupTitle") %>  </a>
                                            
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </ul>
                            </div>
                            <!-- End .widget -->


                            <!-- End .widget -->

                            
                            <!-- End .widget -->

                            <div class="widget">
                                <div class="sidebar-banner">
                                    <img src="/images/banners/1.jpg" alt="banner" class="img-responsive">
                                    <div class="sidebar-banner-content">
                                        <div class="vcenter-container">
                                            <div class="vcenter">

                                                <a href="/carpet-online-shopping" class="btn btn-custom-7 min-width-md">فروشگاه</a>
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

                            <div id="category-filter-bar" class="clearfix">
                                <div class="normal-selectbox clearfix">
                                    <asp:TextBox ID="txtSearch" runat="server" Height="32" CssClass="pull-right directionRTL" placeholder="کلمه مورد نظر خود را وارد نمایید." Width="90%"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="جستجو" CssClass="btn btn-custom-8 btn-compare pull-left" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                            <div class="category-grid">
                                <asp:Panel ID="pnlEmpty" runat="server" Visible="false">
                                    <p style="text-align: center; direction: rtl; color: red; font-size: 20px;">
                                        محصولی جهت نمایش موجود نمی باشد.
                                    </p>
                                </asp:Panel>
                                <div class="row">
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
                                                    <img src='<%# Eval("ProductGroupImage","/Uploads/ProductGroup/{0}") %>' class="" style="height: auto; width: auto" alt='<%# Eval("ProductGroupTitle") %>' />
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






                                    <asp:Repeater ID="rptProducts" runat="server" >
                                        <ItemTemplate>


                                            <asp:HiddenField ID="hfProductID" runat="server" Value='<%# Eval("ProductID") %>' />

                                            <div class="col-sm-3 md-margin2x">
                                                <div class="product">
                                                    <div class="product-top">
                                                        <asp:Panel ID="pnlIsEspecial" runat="server" Visible="false">
                                                            <span class="new-box top-left" style="background-color: #fff;">جدید</span>
                                                        </asp:Panel>

                                                        <figure class="product-image-container">
                                                            <a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>' title="White linen sheer dress">
                                                                                             <img src='<%# Eval("ProductImage","/PhotoHandler.ashx?h=300&w=220&file=/uploads/Products/{0}") %>' alt="Product image" class="product-image">
                                                            <img src='<%# Eval("ProductImage","/PhotoHandler.ashx?h=300&w=220&file=/uploads/Products/{0}") %>' alt="Product image" class="product-image-hover">
                                                      </a>
                                                        </figure>
                                                        <div class="product-action-container">
                                                            <div class="product-action-wrapper action-responsive">
                                                                <a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>' style="width: 100%;" title="Add to Cart" class="product-add-btn">
                                                                    <span>مشاهده جزئیات محصول</span>

                                                                </a>
                                                                 </div>
                                                            <!-- End .product-action-wrapper -->
                                                        </div>
                                                        <!-- End .product-action-container -->
                                                    </div>
                                                    <!-- End .product-top -->
                                                    <h3 class="product-name"><a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>' title="White linen sheer dress"><%# Eval("ProductTitle") %></a></h3>
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
                                    </asp:Repeater>

                                    <!-- Emd .col-sm-4 -->

                                    <!-- Emd .col-sm-4 -->
                                </div>
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
