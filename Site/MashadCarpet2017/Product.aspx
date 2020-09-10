<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="MashadCarpet.Product" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .col-sm-6, .col-md-6, .col-sm-12 {
            float: left !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">
        <section id="content" role="main">
            <div id="product-single-container" class="light">

                <!-- End .breadcrumb-container -->


                <!-- End .sidebg.right -->

                <div class="carousel-container">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-7  product-single-meta">
                                <div id="successDiv" class="alert alert-success">
                                    محصول مورد نظر با موفقیت به
                                       <a href="/Cart.aspx">سبد خرید
                                       </a>
                                    شما اضافه گردید.
                                </div>
                                <h1 class="product-name">
                                    <asp:Label ID="lblProductTitle2" runat="server" Text=""></asp:Label>
                                </h1>
                                <div class="clearfix">
                                    <div class="ratings-container pull-left">
                                    </div>
                                    <!-- End .rating-container -->
                                </div>

                                <div class="xs-margin"></div>
                                <!-- space -->
                                <ul class="liStyle">
                                    <li><span class="colorlabel">
                                        <asp:Literal ID="ltProductName" runat="server" Text="<%$Resources:Resource,ProductName%>"></asp:Literal>
                                        :</span>
                                        <asp:Label ID="lblProductTitle" runat="server" Text=""></asp:Label></li>
                                    <li><span class="colorlabel">
                                        <asp:Literal ID="ltDesignNo" runat="server" Text="<%$Resources:Resource,DesignNo%>"></asp:Literal>:</span>
                                        <asp:Label ID="lblDesignNo" runat="server" Text=""></asp:Label></li>
                                    <li><span class="colorlabel">
                                        <asp:Literal ID="ltCollection" runat="server" Text="<%$Resources:Resource,Collection%>"></asp:Literal>:</span>
                                        <asp:Label ID="lblCollection" runat="server" Text=""></asp:Label></li>
                                    <li><span class="colorlabel">
                                        <asp:Literal ID="ltReeds" runat="server" Text="<%$Resources:Resource,Reeds%>"></asp:Literal>:</span>
                                        <asp:Label ID="lblShots" runat="server" Text=""></asp:Label></li>
                                    <li><span class="colorlabel">
                                        <asp:Literal ID="ltColor" runat="server" Text="<%$Resources:Resource,Color%>"></asp:Literal>:</span>
                                        <asp:Label ID="lblColorTitle" runat="server" Text=""></asp:Label></li>
                                    <li><span class="colorlabel">
                                        <asp:Literal ID="ltshots" runat="server" Text="<%$Resources:Resource,shots%>"></asp:Literal>:</span>
                                        <asp:Label ID="lblReeds" runat="server" Text=""></asp:Label></li>


                                </ul>

                                <div class="filter-box floatright margintop20 newfilterbox marginleft120">
                                    <span class="filter-label colorlabel" style="font-size: 20px;">
                                        <asp:Literal ID="ltAvailableColors" runat="server" Text="<%$Resources:Resource,AvailableColors%>"></asp:Literal></span>
                                    <div class="row">
                                        <asp:Repeater ID="rptColors" runat="server" OnItemCommand="rptColors_ItemCommand"
                                            OnItemDataBound="rptColors_ItemDataBound">
                                            <ItemTemplate>
                                                <%--<img src='<%# Eval("ProductImage","/Uploads/Products/{0}") %>' style="width:50px;height:50px;" />
                                            <br />
                                            <asp:LinkButton ID="Color" runat="server" CommandArgument='<%# Eval("ColorName") %>' class="filter-color-box"><%# Eval("ColorTitle") %></asp:LinkButton>--%>
                                                <asp:HiddenField ID="hfcolorID" runat="server" Value='<%# Eval("fk_colorID") %>' />
                                                <asp:LinkButton ID="lbColor" runat="server"
                                                    class="filter-color-box withoutSize" CommandArgument='<%# Eval("fk_colorID") %>'>
                                                    <asp:Image ID="imgColors" ImageUrl='<%# Eval("ProductImage","/PhotoHandler.ashx?h=65&w=50&file=~/Uploads/Products/{0}") %>' runat="server" />
                                                </asp:LinkButton>


                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </div>
                                    <!-- End .row -->
                                </div>


                                <asp:Panel ID="pnlAvailableSize" runat="server" Visible="false">
                                    <div class="filter-box floatright margintop20 newfilterbox newmargin">
                                        <span class="filter-label colorlabel" style="font-size: 20px;">
                                            <asp:Literal ID="ltAvailableSize" runat="server" Text="<%$Resources:Resource,AvailableSize%>"></asp:Literal></span>
                                        <div class="row">
                                            <asp:Repeater ID="rptSize" runat="server" OnItemCommand="rptSize_ItemCommand" OnItemDataBound="rptSize_ItemDataBound">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfProductColorSizeID" runat="server" Value='<%# Eval("ProductColorSizeID") %>' />
                                                    <asp:LinkButton ID="lbColor" runat="server" class="filter-size-box" CommandArgument='<%# Eval("SizeID") %>'><%# Eval("SizeTitle") %></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </div>
                                        <!-- End .row -->
                                    </div>
                                </asp:Panel>
                                <!-- end .filter-box -->
                                <div style="clear: both;"></div>
                                <!-- end .filter-box -->

                                <div class="product-action-container clearfix">

                                    <div class="product-action-content clearfix">

                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="false">
                                            <ContentTemplate>

                                                <asp:Panel ID="pnlNewPrice" runat="server" Visible="false" CssClass="floatLeft directionRTL">
                                                    <span class="fontBRoya marginright20 pricestyle">
                                                        <asp:Literal ID="ltNewPrice" runat="server"></asp:Literal>
                                                    </span>

                                                </asp:Panel>


                                                <asp:Label ID="lblPrice" runat="server" Text="" CssClass="fontBRoya floatLeft marginright20 directionRTL pricestyle"></asp:Label>


                                                <asp:Button ValidationGroup="countValidate" ID="btnAddToCart" runat="server" Text="<%$Resources:Resource,AddToBasket%>" CssClass="btn btn-custom-6 min-width-md" OnClick="btnAddToCart_Click" />

                                                <asp:TextBox ID="txtCount" runat="server" CssClass="product-amount-input " Text="1"></asp:TextBox>
                                                <p class="clearfix"></p>
                                                <asp:RegularExpressionValidator ValidationGroup="countValidate" ID="reCountInteger" runat="server" Display="None"
                                                    ErrorMessage="برای فیلد تعداد مصول مقدار عددی وارد نمایید" ControlToValidate="txtCount" ValidationExpression="^[0-9]*$">*</asp:RegularExpressionValidator>
                                                <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger" Visible="false">
                                                    لطفا تعداد محصول را کاهش دهید، این تعداد محصول هم اکنون موجود نمی باشد
                                                </asp:Panel>

                                            </ContentTemplate>

                                        </asp:UpdatePanel>


                                    </div>

                                    <!-- End .product-action-right -->
                                </div>
                                <asp:Panel ID="pnlNoStock" runat="server" CssClass="alert alert-danger directionRTL" Visible="false">
                                    این محصول در حال حاضر موجود نمی باشد.
                                </asp:Panel>
                                <asp:ValidationSummary ID="ValidationSummary2" ValidationGroup="countValidate" runat="server" CssClass="alert alert-danger directionRTL" />
                                <!-- End .product-action-container -->
                            </div>
                            <!-- End .col-sm-6 -->

                            <div class="col-sm-5 paddingtop37">
                                <asp:Repeater ID="rptProductImg" runat="server">
                                    <ItemTemplate>
                                        <asp:Image ID="ImgFirstProduct" runat="server" alt="product 1" Height="450px" CssClass="marginleft60"
                                            ImageUrl='<%# Eval("ProductImage","~/Uploads/Products/{0}") %>'
                                            data-magnify-src='<%# Eval("ProductImage","/Uploads/Products/{0}") %>' />
                                    </ItemTemplate>
                                </asp:Repeater>

                                <!-- End. product-single-carousel -->
                            </div>


                        </div>
                        <div class="lg-margin"></div>

                        <div class="row">
                            <asp:Panel ID="pnlDesc" runat="server" CssClass="descstyle propageDesc">
                                <asp:Literal ID="ltProDesc" runat="server"></asp:Literal>
                            </asp:Panel>
                        </div>
                        <!-- End .row -->
                    </div>
                    <!-- End .container -->
                </div>
                <!-- End .carousel-contaienr -->


                <!-- space -->




            </div>
            <!-- End #product-single-container -->



            <%-- <div class="lg-margin2x"></div>--%>
            <!-- space -->

            <div class="container">
                <div class="carousel-container" style="padding: 0 100px;">
                    <h2 class="carousel-title">
                        <asp:Literal ID="ltRelatedProducts" runat="server" Text="<%$Resources:Resource,RelatedProducts%>"></asp:Literal></h2>
                    <div class="row relatedpro">
                        <div class="owl-carousel bestsellers-carousel">
                            <asp:Repeater ID="rptRelatedProducts" runat="server">
                                <ItemTemplate>
                                    <div class="product product2">
                                        <div class="product-top">
                                            <figure class="product-image-container">
                                                <a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>' title="White linen sheer dress">

                                                    <img src='<%# Eval("ProductImage","/PhotoHandler.ashx?h=220&w=170&file=/uploads/Products/{0}") %>' alt='<%# Eval("ProductTitle") %>' class="product-image">
                                                    <img src='<%# Eval("ProductImage","/PhotoHandler.ashx?h=220&w=170&file=/uploads/Products/{0}") %>' alt='<%# Eval("ProductTitle") %>' class="product-image-hover">
                                                </a>
                                            </figure>
                                        </div>
                                        <!-- End .product-top -->
                                        <h3 class="product-name text-center"><a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>' title="White linen sheer dress" class="colorlabel"><%# Eval("ProductTitle") %></a></h3>
                                        <%-- <div class="product-price-container text-right">
                                            <span class="product-price fontBRoya">

                                                <%# string.Format("{0:N0}",Eval("ProductPrice")) %> تومان </span>
                                        </div>--%>
                                        <!-- End .product-price-container -->


                                        <div class="product-action-container clearfix fontdetail text-center">
                                            <a style="padding: 8px 14px;" href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>' title="Add to Cart" class="product-add-btn">
                                                <asp:Literal ID="ltShowDetails" runat="server" Text="<%$Resources:Resource,ShowDetails%>"></asp:Literal>

                                            </a>
                                            <%--<div class="product-action-inner">
                                                <a href="#" title="لایک" class="product-btn product-favorite">لایک</a>
                                                <a href="#" title="لایک" class="product-btn product-compare">لایک</a>
                                            </div>--%>
                                            <!-- End .product-action-right -->
                                        </div>
                                        <!-- End .product-action-container -->
                                    </div>
                                    <!-- End .product -->
                                </ItemTemplate>
                            </asp:Repeater>

                            <!-- End .product -->
                        </div>
                        <!-- End .owl-carousel -->
                    </div>
                    <!-- End .row -->
                </div>
                <!-- End .carousel-container -->
            </div>
            <!-- End .container -->

            <div class="md-margin3x"></div>
            <!-- space -->
        </section>
        <!-- End #content -->
    </div>
    <link href="/js/zoommagnify/magnify.css" rel="stylesheet" />
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="/js/zoommagnify/jquery.magnify.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_rptProductImg_ImgFirstProduct_0').magnify();
        });
    </script>
</asp:Content>
