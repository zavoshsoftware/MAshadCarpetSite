<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MashadCarpet.Default" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="Controls/UCLanguage.ascx" TagName="UCLanguage" TagPrefix="uc1" %>
<%@ Register Src="Controls/UCUsefullLinks.ascx" TagName="UCUsefullLinks" TagPrefix="uc2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title></title>


    <!--[if IE]> <meta http-equiv="X-UA-Compatible" content="IE=edge"> <![endif]-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="author" href="/humans.txt" />
    <link rel="stylesheet" href="DefaultCss/bootstrap.min.css" />
    <link rel="stylesheet" href="DefaultCss/revslider2.min.css" />
    <link rel="stylesheet" href="DefaultCss/style.min.css" />
    <link rel="stylesheet" href="DefaultCss/responsive.min.css" />
    <link href="css/DefaultCss.min.css" rel="stylesheet" />
    <!-- Favicon and Apple Icons -->
    <link rel="icon" type="image/png" href="images/icons/icon.png" />
    <link rel="apple-touch-icon" sizes="57x57" href="images/icons/apple-icon-57x57.png" />
    <link rel="apple-touch-icon" sizes="72x72" href="images/icons/apple-icon-72x72.png" />
    <!--- jQuery -->
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script>window.jQuery || document.write('<script src="js/jquery-2.1.1.min.js"><\/script>')</script>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/ZavoshStyle.min.css" rel="stylesheet" />
    <link href="css/SliderText.css" rel="stylesheet" />
    <link rel="icon" href="/images/favicon.png">
    <asp:Literal ID="StyleSheet" runat="Server" />
    <link href="css/MashadCarpet.css" rel="stylesheet" />

    <script type="text/javascript">
        $(window).scroll(function () {
            var windscroll = $(window).scrollTop();
            if (windscroll >= 100) {
                $("#namadli").css("display", "none");
            }
        });
    </script>

    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-73802235-1', 'auto');
        ga('send', 'pageview');

    </script>
    <meta name="google-site-verification" content="61nKd1IOYG93ZLzhzfZC8V5LuLQi4X_JjimWAeujCDE" />
</head>
<body>
    <form runat="server" id="form1">
        <div id="wrapper">
            <div id="sticky-header" class="fullwidth-menu header2" data-fixed="fixed"></div>
            <!-- End #sticky-header -->
            <header id="header" class="fullwidth-menu header2">
                <div id="header-top">
                    <div class="container clearfix">
                        <div class="left-side">
                            <ul class="header-links">
                                <li><a href="http://club.mashadcarpet.com" target="_blank"><i class="fa fa-user" style="color: #fff; margin-right: 10px;"></i><span>
                                    <asp:Literal ID="lblCustomerClub" runat="server" Text="<%$Resources:Resource,CustomerClub%>"></asp:Literal></span></a></li>
                                <li><a href="/corporate-social-responsibility"><i class="fa fa-users" style="color: #fff; margin-right: 10px;"></i><span>
                                    <asp:Literal ID="lblSocialResponsibility" runat="server" Text="<%$Resources:Resource,SocialResponsibility%>"></asp:Literal>
                                </span></a></li>
                                <li>
                                    <a href="/Sales-Representatives"><i class="fa fa-shopping-cart" style="color: #fff; margin-right: 10px;"></i><span>
                                        <asp:Literal ID="lblMarkets" runat="server" Text="<%$Resources:Resource,Markets%>"></asp:Literal>
                                    </span></a></li>
                                <asp:LoginView ID="LoginView1" runat="server">
                                    <AnonymousTemplate>
                                        <li><a href="/Register.aspx"><i class="fa fa-plus" style="color: #fff; margin-right: 10px;"></i><span>
                                            <asp:Literal ID="lblRegister" runat="server" Text="<%$Resources:Resource,Register%>"></asp:Literal></span></a></li>
                                        <li><a href="/login"><i class="fa fa-sign-in" style="color: #fff; margin-right: 10px;"></i><span>
                                            <asp:Literal ID="lblLogin" runat="server" Text="<%$Resources:Resource,Login%>"></asp:Literal></span></a></li>
                                    </AnonymousTemplate>
                                    <RoleGroups>
                                        <asp:RoleGroup Roles="Customer">
                                            <ContentTemplate>
                                                <li>
                                                    <asp:LinkButton CausesValidation="False" ID="lbExit" OnClick="lbExit_Click" runat="server" meta:resourcekey="lbExitResource1">
                                                        <i class="fa fa-sign-out" style="color: #fff; margin-right: 10px;"></i>
                                                        <asp:Literal ID="lblLogout" runat="server" Text="<%$Resources:Resource,Logout%>"></asp:Literal>
                                                    </asp:LinkButton></li>
                                            </ContentTemplate>
                                        </asp:RoleGroup>
                                        <asp:RoleGroup Roles="Admin">
                                            <ContentTemplate>
                                                <li>
                                                    <asp:LinkButton CausesValidation="False" ID="lbExit2" OnClick="lbExit_Click" runat="server" meta:resourcekey="lbExit2Resource1">
                                                        <i class="fa fa-sign-out" style="color: #fff; margin-right: 10px;"></i>
                                                        <asp:Literal ID="lblLogout" runat="server" Text="<%$Resources:Resource,Logout%>"></asp:Literal>
                                                    </asp:LinkButton>
                                                </li>
                                                <li><a href="/Admin"><i class="fa fa-dashboard" style="color: #fff; margin-right: 10px;"></i><span>
                                                    <asp:Literal ID="lblAdminlogin" runat="server" Text="<%$Resources:Resource,Adminlogin%>"></asp:Literal></span></a></li>
                                            </ContentTemplate>
                                        </asp:RoleGroup>
                                        <asp:RoleGroup Roles="Admin">
                                            <ContentTemplate>
                                                <li>
                                                    <asp:LinkButton CausesValidation="False" ID="lbExit2" OnClick="lbExit_Click" runat="server" meta:resourcekey="lbExit2Resource2">
                                                        <i class="fa fa-sign-out" style="color: #fff; margin-right: 10px;"></i>
                                                        <asp:Literal ID="lblLogout3" runat="server" Text="<%$Resources:Resource,Logout%>"></asp:Literal>
                                                    </asp:LinkButton>
                                                </li>
                                                <li><a href="/Admin"><i class="fa fa-dashboard" style="color: #fff; margin-right: 10px;"></i><span>
                                                    <asp:Literal ID="lblAdminLogin2" runat="server" Text="<%$Resources:Resource,Adminlogin%>"></asp:Literal></span></a></li>
                                            </ContentTemplate>
                                        </asp:RoleGroup>
                                    </RoleGroups>
                                    <RoleGroups>
                                        <asp:RoleGroup Roles="Admin">
                                            <ContentTemplate>
                                                <li>
                                                    <asp:LinkButton CausesValidation="false" ID="lbExit2" OnClick="lbExit_Click" runat="server">
                                                        <i class="fa fa-sign-out" style="color: #fff; margin-right: 10px;"></i>
                                                        <asp:Literal ID="lblLogout4" runat="server" Text="<%$Resources:Resource,Logout%>"></asp:Literal>
                                                    </asp:LinkButton>
                                                </li>
                                                <li><a href="/Admin"><i class="fa fa-dashboard" style="color: #fff; margin-right: 10px;"></i><span>
                                                    <asp:Literal ID="lblAdminLogin3" runat="server" Text="<%$Resources:Resource,Adminlogin%>"></asp:Literal></span></a></li>
                                            </ContentTemplate>
                                        </asp:RoleGroup>
                                    </RoleGroups>
                                </asp:LoginView>
                            </ul>
                            <div class="user-dropdown dropdown visible-sm visible-xs">
                                <a title="My Account" class="dropdown-toggle" data-toggle="dropdown"><span class="header-links-icon icon-account"></span><span class="user-text">
                                    <asp:Literal ID="lblMyaccount" runat="server" Text="<%$Resources:Resource,MyAccount%>"></asp:Literal></span><span class="dropdown-arrow"></span></a>
                                <ul class="dropdown-menu" role="menu">
                                    <asp:LoginView ID="LoginView2" runat="server">
                                        <AnonymousTemplate>
                                            <li><a href="#"><span class="header-links-icon icon-account"></span><span>
                                                <asp:Literal ID="lblMyaccount2" runat="server" Text="<%$Resources:Resource,MyAccount%>"></asp:Literal></span></a></li>
                                            <li><a href="/Track.aspx"><span class="header-links-icon icon-checkout"></span><span>
                                                <asp:Literal ID="lblTrackOrder" runat="server" Text="<%$Resources:Resource,TrackOrder%>"></asp:Literal></span></a></li>
                                            <li><a href="/cart.aspx"><span class="header-links-icon icon-wishlist"></span><span>
                                                <asp:Literal ID="lblShoppingbag" runat="server" Text="<%$Resources:Resource,shoppingbag%>"></asp:Literal></span></a></li>
                                            <li><a href="/login.aspx"><span class="header-links-icon icon-login"></span><span>
                                                <asp:Literal ID="lblLogin2" runat="server" Text="<%$Resources:Resource,Login%>"></asp:Literal></span></a></li>
                                        </AnonymousTemplate>
                                        <RoleGroups>
                                            <asp:RoleGroup Roles="Admin">
                                                <ContentTemplate>
                                                    <li><a href="#"><span class="header-links-icon icon-account"></span><span>
                                                        <asp:Literal ID="lblMyaccount2" runat="server" Text="<%$Resources:Resource,MyAccount%>"></asp:Literal></span></a></li>
                                                    <li><a href="/Track.aspx"><span class="header-links-icon icon-checkout"></span><span>
                                                        <asp:Literal ID="lblTrackOrder" runat="server" Text="<%$Resources:Resource,TrackOrder%>"></asp:Literal></span></a></li>
                                                    <li><a href="/cart.aspx"><span class="header-links-icon icon-wishlist"></span><span>
                                                        <asp:Literal ID="lblShoppingbag" runat="server" Text="<%$Resources:Resource,shoppingbag%>"></asp:Literal></span></a></li>
                                                    <li><a href="/Admin"><span class="fa fa-dashboard"></span><span>
                                                        <asp:Literal ID="lblLogin2" runat="server" Text="<%$Resources:Resource,Adminlogin%>"></asp:Literal></span></a></li>
                                                    <li>
                                                        <asp:LinkButton CausesValidation="false" ID="lbExit2" OnClick="lbExit_Click" runat="server">
                                                            <i class="fa fa-sign-out" style="color: #fff; margin-right: 10px;"></i>
                                                            <asp:Literal ID="lblLogout4" runat="server" Text="<%$Resources:Resource,Logout%>"></asp:Literal>
                                                        </asp:LinkButton></li>
                                                </ContentTemplate>
                                            </asp:RoleGroup>
                                        </RoleGroups>
                                    </asp:LoginView>

                                </ul>
                            </div>
                            <!-- End .user-dropdown -->
                        </div>
                        <!-- End .left-side -->
                        <div class="right-side hidden-xs">
                            <div class="search-container">
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch" meta:resourcekey="pnlSearchResource1">
                                    <div class="search-form">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="s directionRTL" placeholder="<%$Resources:Resource,SearchText%>" meta:resourcekey="txtSearchResource1"></asp:TextBox>
                                        <a href="#" title="Close Search" class="search-close-btn"></a>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="search-submit-btn" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1" />
                                    </div>
                                </asp:Panel>
                            </div>
                            <!-- End .search-container -->
                            <a href="#" class="header-search-btn" title="Search">
                                <i class="fa fa-search" style="color: #fff; margin-right: 10px;"></i>
                                <asp:Literal ID="lblSearchBtn" runat="server" Text="<%$Resources:Resource,SearchBtn%>"></asp:Literal></a>
                            <div class="cart-dropdown dropdown pull-right floatleftinen">
                                <a title="Shopping Cart" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-shopping-cart" style="color: #fff; margin-right: 10px;"></i><span class="badge">2</span><span class="hidden-sm hidden-xs"><asp:Literal ID="lblYourCart" runat="server" Text="<%$Resources:Resource,YourShoppingCart%>"></asp:Literal></span></a>
                                <div class="dropdown-menu">
                                    <div class="cart-dropdown-header" style="direction: rtl; text-align: right;">
                                        <span class="dropdown-icon pull-right" style="direction: rtl;"></span>&nbsp;&nbsp;
               <asp:Label ID="lblOrderCount" runat="server" meta:resourcekey="lblOrderCountResource1"></asp:Label>
                                        <asp:Literal ID="lblSubmitOrder" runat="server" Text="<%$Resources:Resource,YourOrder%>"></asp:Literal>
                                    </div>
                                    <p class="cart-desc" style="direction: rtl; text-align: right;">
                                        <asp:Label ID="lblOrderCount2" runat="server" meta:resourcekey="lblOrderCount2Resource1"></asp:Label>
                                        <asp:Literal ID="lblSubmitOrder2" runat="server" Text="<%$Resources:Resource,YourOrder%>"></asp:Literal>- 
           <asp:Label ID="lblTotal" runat="server" CssClass="fontBRoya" meta:resourcekey="lblTotalResource1"></asp:Label>
                                    </p>
                                    <asp:Repeater ID="rptBasketItems" runat="server" OnItemDataBound="rptBasketItems_ItemDataBound" OnItemCommand="rptBasketItems_ItemCommand">
                                        <ItemTemplate>
                                            <div class="product clearfix">
                                                <asp:LinkButton ID="lbDeleteOrder" runat="server" CssClass="delete-btn pull-left" CommandArgument='<%# Eval("OrderDetailID") %>' meta:resourcekey="lbDeleteOrderResource1"></asp:LinkButton>
                                                <figure class="product-image-container pull-right">
                                                    <a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>' title="Mustard yellow ruffle dress">
                                                        <img src='<%# Eval("ProductImage","/Uploads/Products/{0}") %>' alt="Product image" class="product-image">
                                                    </a>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </figure>
                                                <div class="product-content">
                                                    <h3 class="product-name" style="text-align: right; padding-right: 10px;"><a href='<%# String.Format("/carpet-online-shopping/{0}/{1}/?ColorID={2}", Eval("ProductGroupName"), Eval("ProductName"), Eval("ProductColorID")) %>' title="Mustard yellow ruffle dress"><%# Eval("ProductTitle") %></a></h3>
                                                    <div class="product-price-container" style="direction: rtl; text-align: right; padding-right: 10px;">
                                                        <span class="product-price">
                                                            <asp:Literal ID="lblNumber" runat="server" Text="<%$Resources:Resource,Number%>"></asp:Literal>: <%# Eval("Count") %></span><br />
                                                        <span class="product-price">
                                                            <asp:HiddenField ID="hfOrderDetailID" runat="server" Value='<%# Eval("OrderDetailID") %>' />
                                                            <asp:Label ID="lblPrice" runat="server" CssClass="fontBRoya" meta:resourcekey="lblPriceResource1"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <!-- End .product-price-container -->
                                                </div>
                                                <!-- End .product-content -->
                                            </div>
                                            <!-- End .product -->
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <div class="clearfix">
                                        <asp:Panel ID="pnlIsActiveFactor" runat="server">
                                            <ul class="pull-right action-info-container">
                                                <li>
                                                    <asp:Literal ID="lblTotalFactor" runat="server" Text="<%$Resources:Resource,Totalfactor%>"></asp:Literal>: <span class="first-color">
                                                        <asp:Label ID="lblOrderTotal" runat="server" CssClass="fontBRoya" meta:resourcekey="lblOrderTotalResource1"></asp:Label></span></li>
                                                <li>
                                                    <asp:Literal ID="lblTaxTitle" runat="server" Text="<%$Resources:Resource,TaxTitle%>"></asp:Literal>: <span class="text-lowercase directionRTL">%0</span></li>
                                                <li>
                                                    <asp:Literal ID="lblTotalfactor2" runat="server" Text="<%$Resources:Resource,Totalfactor%>"></asp:Literal>: <span class="first-color">
                                                        <asp:Label ID="lblTotalPrice" runat="server" CssClass="fontBRoya" meta:resourcekey="lblTotalPriceResource1"></asp:Label></span></li>
                                            </ul>
                                            <ul class="pull-left action-btn-container">
                                                <li>
                                                    <asp:HyperLink ID="hlPayment" runat="server" CssClass="btn btn-custom-5 paymentBtn">

                                                        <asp:Literal ID="lblPayment" runat="server" Text="<%$Resources:Resource,PayMent%>"></asp:Literal>
                                                    </asp:HyperLink></li>

                                                <li><a href="/Cart.aspx" class="btn btn-custom-5" style="width: 115px; height: 30px;">
                                                    <asp:Literal ID="lblViewCart" runat="server" Text="<%$Resources:Resource,ViewCart%>"></asp:Literal>
                                                </a></li>
                                            </ul>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="language-dropdown dropdown floatleftinen">
                                <uc1:UCLanguage ID="UCLanguage1" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </header>
            <section id="content" role="main">
                <div style="position: relative;">
                    <div id="revslider-container">
                        <div style="width: 100%; opacity: 0.75; position: absolute; z-index: 100; background-color: #fff; border-bottom: 3px solid #2f4497;" class="fullwidth-menu header2 banner-top">
                            <div class="container" data-clone="sticky">
                                <div class="row">
                                    <div class="col-md-4 pull-right hidden-xs">
                                        <ul class="menu right-menu clearfix">
                                            <li>
                                                <asp:HyperLink ID="hlHome" runat="server" meta:resourcekey="hlHomeResource1">
                                                    <asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal>
                                                </asp:HyperLink></li>
                                            <li><a href="/carpet-online-shopping">
                                                <asp:Literal ID="lblOnlineStore" runat="server" Text="<%$Resources:Resource,OnlineStore%>"></asp:Literal></a></li>
                                            <li
                                                class="megamenu-container"><a href="/carpet-online-shopping">
                                                    <asp:Literal ID="lbl" runat="server" Text="<%$Resources:Resource,ProductsTitle%>"></asp:Literal></a>
                                                <div class="megamenu">
                                                    <div class="container">
                                                        <div class="row">
                                                            <div class="col-md-6 menu-banner">
                                                                <a href="#" class=" text-left">
                                                                    <img src="images/Carpet-1.png" alt="فرش مشهد" />
                                                                    <!-- End .banner-container -->
                                                                </a>
                                                                <!-- End .banner -->
                                                            </div>
                                                            <div class="col-md-6" style="padding-top: 55px;">
                                                                <asp:Repeater ID="rptProductGroup" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="col-md-6">
                                                                            <a href='<%# Eval("ProductGroupName","/carpet-online-shopping/{0}") %>' class="megamenu-title"><%# Eval("ProductGroupTitle") %></a>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                                <asp:Repeater ID="rptProductGroup2" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="col-md-6"><a href='<%# Eval("ProductGroupName","/carpet-online-shopping/{0}") %>' class="megamenu-title"><%# Eval("ProductGroupTitle") %></a> </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                    <!-- End .col-md-4-->
                                    <div class="col-md-3 logo-container pull-right col-xs-2">
                                        <div class="logo clearfix">
                                            <a href="/default.aspx">

                                                <asp:Image ID="imglogo" runat="server" AlternateText="فرش مشهد" />


                                            </a>
                                        </div>
                                    </div>
                                    <!-- End .md-md-4-->
                                    <div class="col-md-5 clearfix pull-right col-xs-9 menu-collapse">
                                        <nav>
                                            <div id="responsive-nav" class="floatright">
                                                <a id="responsive-btn" href="#">
                                                    <span class="responsive-btn-icon">
                                                        <span class="responsive-btn-block"></span>
                                                        <span class="responsive-btn-block"></span>
                                                        <span class="responsive-btn-block last"></span>
                                                    </span>
                                                    <%--<span class="responsive-btn-text visible-sm-inline-block visible-xs-inline-block">
                                                        <asp:Literal ID="lblMenu" runat="server" Text="<%$Resources:Resource,MenuTitle%>"></asp:Literal></span>--%>
                                                </a>
                                                <div id="responsive-menu-container">
                                                    <ul style="background-color: RGB(52, 56, 143);">
                                                        <li class="sx-menu"><a href="/Default.aspx" style="color: #fff;">
                                                            <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a>

                                                        </li>
                                                        <li class="sx-menu"><a href="/carpet-online-shopping" style="color: #fff;">
                                                            <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Resource,ProductsTitle%>"></asp:Literal></a>
                                                        </li>
                                                        <li class="sx-menu"><a href="/Blog/all?PageID=1" style="color: #fff;">
                                                            <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Resource,NewsTitle%>"></asp:Literal></a>

                                                        </li>
                                                        <li class="sx-menu"><a href="/About-us" style="color: #fff;">
                                                            <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Resource,AboutTitle%>"></asp:Literal></a>

                                                        </li>
                                                        <li class="sx-menu"><a href="/Contact-us" style="color: #fff;">
                                                            <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Resource,ContactTitle%>"></asp:Literal></a>

                                                        </li>
                                                        <li class="sx-menu"><a href="/cart.aspx" style="color: #fff;">
                                                            <asp:Literal ID="Literal5" runat="server" Text="سبد خرید"></asp:Literal></a>

                                                        </li>

                                                    </ul>
                                                </div>
                                                <!-- End #responsive-menu-container -->
                                            </div>
                                            <!-- End .responsive-nav -->
                                            <ul class="menu right-menu clearfix pull-left">
                                                <li><a href="/Blog/all?PageID=1">
                                                    <asp:Literal ID="lblNewTitle" runat="server" Text="<%$Resources:Resource,NewsTitle%>"></asp:Literal></a>
                                                </li>
                                                <li><a href="/About-us">
                                                    <asp:Literal ID="lblAbout" runat="server" Text="<%$Resources:Resource,AboutTitle%>"></asp:Literal></a>
                                                </li>
                                                <li class="reverse"><a href="/Contact-us">
                                                    <asp:Literal ID="lblContact" runat="server" Text="<%$Resources:Resource,ContactTitle%>"></asp:Literal></a>
                                                </li>
                                                <li class="nopaddingtop" id="namadli">
                                                    <img id='drftxlapgwmdgwmddrft' style='cursor: pointer' onclick='window.open("http://trustseal.enamad.ir/Verify.aspx?id=13991&p=nbpdfuixjzpgjzpgnbpd", "Popup","toolbar=no, location=no, statusbar=no, menubar=no, scrollbars=1, resizable=0, width=580, height=600, top=30")' alt='' src='http://trustseal.enamad.ir/logo.aspx?id=13991&p=lznbvjymzpfvzpfvlznb' />

                                                </li>
                                            </ul>
                                        </nav>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="revslider" style="opacity: 1;">
                            <ul>
                                <asp:Repeater ID="rptSlider" runat="server" OnItemDataBound="rptSlider_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfID" Value='<%# Eval("SliderID") %>' runat="server" />
                                        <li data-transition="cube" data-slotamount="8" data-masterspeed="800" data-title="Trends">
                                            <img src="images/revslider/dummy.png" alt="فرش مشهد" data-lazyload='<%# Eval("SliderImage","/Uploads/Sliders/{0}") %>' data-bgposition="center center" data-bgfit="cover" data-bgrepeat="no-repeat">
                                            <asp:Repeater ID="rptSliderText" runat="server">
                                                <ItemTemplate>
                                                    <%--<div class='<%# String.Format("fontDroid tp-caption rev-subtitle class{1} {0}", Eval("InAndOutClass"), Eval("SliderTextID")) %>' data-x='<%# Eval("datax") %>' data-y='<%# Eval("datay") %>' data-speed='<%# Eval("speed") %>' data-start='<%# Eval("start")%>' data-easing="Back.easeInOut" data-endspeed="600" style='<%# Eval("fontsize","z-index: 10; direction:rtl; font-size:{0}px;")%>'>--%>
                                                    <div class='<%# String.Format("fontDroid tp-caption rev-subtitle class{1} {0}", Eval("InAndOutClass"), Eval("SliderTextID")) %>' data-x='<%# Eval("datax") %>' data-y='<%# Eval("datay") %>' data-speed='<%# Eval("speed") %>' data-start='<%# Eval("start")%>' data-easing="Back.easeInOut" data-endspeed="600" 
                                                        style='<%# String.Format("z-index: 10; direction:rtl; font-size:{0}px; color:#{1} !important;", Eval("fontsize"), Eval("textColor")) %>'>
                                                        <%# Eval("Text") %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <!-- End #revslider -->
                    </div>
                </div>
                
            <div class="xlg-margin"></div>
            <div class="testimonials-section home parallax" data-stellar-background-ratio="0.4" style="padding: 76px 0;">
                <div class="container">
               <p style="font-size: 20px; color: #dc143c; line-height: 45px;">
                   هرگونه سایت و فروشگاه اینترنتی که تحت عنوان فرش مشهد، محصولات این شرکت را عرضه می نماید مطلقا مورد تایید این شرکت نمی باشد
                   <br />
                   و بدیهی است که محصولات معرفی شده در اینگونه سایت ها شامل هیچگونه ضمانت و یا خدمات پس از فروش شرکت فرش مشهد نمی باشد.
               </p>

                </div>
              
            </div>

                <div class="container newestinHome">
                    <div class="carousel-container">
                        <h2 class="carousel-title">
                            <a href="/carpet-online-shopping">
                                <asp:Literal ID="ltNewestPro" runat="server" Text="<%$Resources:Resource,NewestProductTitle%>"></asp:Literal>
                            </a></h2>
                        <div class="row">
                            <div class="owl-carousel new-arrivals-carousel">
                                <asp:Repeater ID="rptRecentProducts" runat="server">
                                    <ItemTemplate>
                                        <div class="product product2">
                                            <div class="product-top">
                                                <figure class="product-image-container">
                                                    <a href='<%# String.Format("/carpet-online-shopping/{0}", Eval("ProductGroupName") ) %>' title="White linen sheer dress">
                                                        <img src='<%# Eval("ProductGroupImage","PhotoHandler.ashx?h=300&w=220&file=/uploads/ProductGroup/{0}") %>' alt='<%# Eval("ProductGroupTitle") %>' class="product-image">
                                                        <img src='<%# Eval("ProductGroupImage","PhotoHandler.ashx?h=300&w=220&file=/uploads/ProductGroup/{0}") %>' alt='<%# Eval("ProductGroupTitle") %>' class="product-image-hover">
                                                    </a>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </figure>
                                            </div>
                                            <!-- End .product-top -->
                                            <!-- End .product-price-container -->
                                            <div class="product-name text-right">

                                                <%# Eval("ProductGroupTitle") %>
                                            </div>
                                            <div class="product-action-container clearfix">
                                                <a href='<%# String.Format("/carpet-online-shopping/{0}", Eval("ProductGroupName")) %>' title="مشاهده محصولات" class="product-add-btn" style="padding: 9px 14px;">
                                                    <span class="add-btn-text">
                                                        <asp:Literal ID="ltShowDetails" runat="server" Text="<%$Resources:Resource,ShowDetails%>"></asp:Literal></span>
                                                </a>
                                                <!-- End .product-action-right -->
                                            </div>
                                            <!-- End .product-action-container -->
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sm-margin visible-md"></div>
                <div class="xlg-margin"></div>
                <div class="container">
                    <div class="row">
                        <ul>
                            <asp:Repeater ID="rptLinks" runat="server">
                                <ItemTemplate>
                                    <li class="col-md-4" style="text-align: center;"><a href='<%# Eval("LinkAddres") %>'>
                                        <img src='<%# Eval("imgFile","/Uploads/Blogs/{0}") %>' alt='<%# Eval("linktitle") %>' style="margin-bottom: 4px;" /><span style="font-size: 16px;"> <%# Eval("linktitle") %> </span></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
                <!-- End .container -->
                <div class="xlg-margin"></div>
                <div class="testimonials-section home parallax" data-stellar-background-ratio="0.4">
                    <div class="container">
                        <asp:Image ID="imgSh" runat="server" CssClass="margincenter" />


                    </div>
                    <!-- End .container -->
                    <div class="vcenter-container">
                        <div class="vcenter bottom-nav">
                            <div class="owl-carousel testimonials-slider">
                                <asp:Repeater ID="rptMiddelText" runat="server">
                                    <ItemTemplate>
                                        <div class="testimonial">
                                            <p style="margin-top: 20px;"><%# Eval("TextDescription") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="lg-margin3x xs-margin2x"></div>
                <div class="sm-margin visible-xs"></div>
                <div class="container">
                    <div class="carousel-container">
                        <h2 class="carousel-title">
                            <asp:Literal ID="lblNews2" runat="server" Text="<%$Resources:Resource,NewsTitle%>"></asp:Literal></h2>
                        <div class="row">
                            <div class="owl-carousel blog-posts-carousel">
                                <asp:Repeater ID="rptBlogs" runat="server">
                                    <ItemTemplate>
                                        <article class="article">
                                            <div class="article-media-container">
                                                <a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>'>
                                                    <img src='<%# Eval("BlogImage","/Uploads/Blogs/{0}") %>' class="img-responsive" alt="<%# Eval("BlogTitle") %>" style="height: 285px;"></a>
                                            </div>
                                            <!-- End .article-media-container -->
                                            <div class="article-meta-box">
                                                <span class="article-icon article-date-icon"></span>
                                                <span class="meta-box-text directionRTL"><%# string.Format("{0:dd MMMM}",Eval("SubmitDate")) %></span>
                                            </div>
                                            <!-- End .article-meta-box -->
                                            <div class="article-meta-box article-meta-comments">
                                                <i class="fa fa-user"></i>
                                                <a href="#" class="meta-box-text"><%# Eval("VisitCounts") %></a>
                                                <asp:Literal ID="lblVisit" runat="server" Text="<%$Resources:Resource,VisitTitle%>"></asp:Literal>
                                            </div>
                                            <h4 style="text-align: right;"><a style="color: #868279;" href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>'><%# Eval("BlogTitle") %></a></h4>
                                        </article>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="lg-margin2x hidden-xs"></div>
                <div class="xlg-margin visible-xs"></div>
            </section>
            <footer id="footer" class="footer3">
                <div id="footer-inner">
                    <div class="container">
                        <div class="row">
                            <div class="row">
                                <div class="col-sm-5 widget">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3237.1000955784853!2d51.41528790000001!3d35.77291750000001!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3f8e0689f7d18999%3A0x21ed013cd321e531!2sTehran%2C+W+Atefi+St!5e0!3m2!1sen!2sir!4v1440220941433" width="100%" height="300" frameborder="0" style="border: 0" allowfullscreen></iframe>
                                </div>
                                <!-- End .col-sm-4 -->
                                <div class="col-sm-3 widget">

                                    <uc2:UCUsefullLinks ID="UCUsefullLinks2" runat="server" />


                                </div>
                                <div class="col-sm-4 widget">
                                    <h4 style="text-align: right;">
                                        <asp:Literal ID="ltPurchaseguide" runat="server" Text="<%$Resources:Resource,Purchaseguide%>"></asp:Literal></h4>


                                    <div class="accordion links" id="accordion" style="text-align: right;">


                                        <asp:Repeater ID="rptBuyingManual" runat="server">
                                            <ItemTemplate>
                                                <div class="accordion-group panel">
                                                    <div class="accordion-title">
                                                        <a class="accordion-btn" data-toggle="collapse" data-parent="#accordion" href='<%# Eval("TextName","#{0}") %>'></a>
                                                        <span><%# Eval("TextTitle") %></span>

                                                    </div>
                                                    <!-- End .accourdion-title -->

                                                    <div class="accordion-body collapse" id='<%# Eval("TextName") %>'>
                                                        <div class="accordion-body-wrapper">
                                                            <p><%# Eval("TextDescription") %> </p>

                                                        </div>
                                                        <!-- End .accordion-body-wrapper -->
                                                    </div>
                                                    <!-- End .accordion-body -->
                                                </div>
                                                <!-- End .accordion-group -->

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="footer-bottom">
                    <div class="container">
                        <div class="row">
                            <div class="col-xs-12 clearfix">
                                <p class="copyright-text">
                                    &copy; 2015 All Rights Reserved. Powered by
                                    <a class="copy-mg" href="http://www.mygroups.co" target="_blank">
                                        <img style="vertical-align: middle;" src="/images/MyMedia-Logo.png" alt="MyMedia Logo" width="59" height="19">
                                    </a>
                                </p>
                                <ul class="clearfix">
                                    <li><a href="https://t.me/MashadCarpetCo" class="" title="Facebook"><i class="fa fa-2x fa-telegram"></i></a></li>
                                    <li><a href="https://www.instagram.com/mashadcarpet/" class="" title="Facebook"><i class="fa fa-2x fa-instagram"></i></a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </footer>
        </div>
        <a href="#header" id="scroll-top" title="Go to top">Top</a>

        <script src="//maps.googleapis.com/maps/api/js?sensor=false"></script>
        <script src="js/bootstrap.min.js"></script>
        <script src="js/smoothscroll.min.js"></script>
        <script src="js/waypoints.min.js"></script>
        <script src="js/waypoints-sticky.min.js"></script>
        <script src="js/jquery.debouncedresize.min.js"></script>
        <script src="js/retina.min.js"></script>
        <script src="js/jquery.placeholder.min.js"></script>
        <script src="js/jquery.hoverIntent.min.js"></script>
        <script src="js/owl.carousel.min.js"></script>
        <script src="js/twitter/jquery.tweet.min.js"></script>
        <script src="js/jquery.themepunch.tools.min.js"></script>
        <script src="js/jquery.themepunch.revolution.min.js"></script>
        <script src="js/jquery.stellar.min.js"></script>
        <script src="js/maplabel.min.js"></script>
        <script src="js/main.min.js"></script>
        <script>
            $(function () {
                "use strict";
                // Slider Revolution
                jQuery('#revslider').revolution({
                    delay: 8000,
                    startwidth: 1170,
                    startheight: 600,
                    fullWidth: "on",
                    fullScreen: "off",
                    hideTimerBar: "on",
                    spinner: "spinner3",
                });
            });
        </script>
        <script>
            function get_cookie(cookie_name) {
                var results = document.cookie.match('(^|;) ?' + cookie_name + '=([^;]*)(;|$)');

                if (results)
                    return (unescape(results[2]));
                else
                    return null;
            }
            function CheckLanguage() {
                if (get_cookie('CurrentLanguage') == "en-US")
                    alert('Language : US-English');
                else
                    alert('Language : IR-Farsi');
            }
        </script>
    </form>
</body>
</html>
