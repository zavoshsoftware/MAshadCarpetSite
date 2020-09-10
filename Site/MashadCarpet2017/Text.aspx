<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Text.aspx.cs" Inherits="MashadCarpet.Text" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section id="content" role="main">
        <div class="breadcrumb-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">
                            <li><a href="Default.aspx" title="Home"><asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a></li>
                            <li class="active">
                                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></li>
                        </ul>
                    </div>
                    <!-- End .col-md-12 -->
                </div>
                <!-- End .row -->
            </div>
            <!-- End .container -->
        </div>
        <div class="container">
            <div class="row">
                <div class="col-md-12">       <div id="category-banner" class="valign-nav">
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
                </div></div>
            </div>
            </div><br />
            <div class="row">
                <div class="col-md-12 text-justify directionRTL" style="line-height:1.8em;">
                    <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                </div>

            </div>
        </div>
    </section>
</asp:Content>
