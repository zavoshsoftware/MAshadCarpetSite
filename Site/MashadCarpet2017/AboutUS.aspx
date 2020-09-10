<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AboutUS.aspx.cs" Inherits="MashadCarpet.AboutUS" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
    <script src="js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#tabUL li:first").addClass("active");
        });

        $(document).ready(function () {
            $(".tab-content div:first").addClass("in active");
        });
        
    </script>
    <style type="text/css">
        .nav-tabs > li 
        {
            float:right !important;
        }
    </style>
  </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content" role="main">

        <div class="about-banner light parallax" data-stellar-background-ratio="0.4">
            <div class="breadcrumb-container absolute text-left">
                <div class="container">
                    <div class="row">
                        <div class="col-sm-12">
                            <ul class="breadcrumb">
                                <li><a href="/Default.aspx" title="Home">
                                    <asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a></li>
                                <li class="active"><a href="#" title="Blog">
                                    <asp:Literal ID="ltAboutTitle" runat="server" Text="<%$Resources:Resource,AboutTitle%>"></asp:Literal></a></li>
                                <%--<li class="active">Blog</li>--%>
                            </ul>
                        </div>
                    </div>
                    <!-- End .row -->
                </div>
                <!-- End .container -->
            </div>
            <!-- End .breadcrumb-container -->

            <div class="vcenter-container">
                <div class="vcenter">
                    <div class="owl-carousel about-banner-slider">
                        
                    </div>
                    <!-- End .about-banner-slider -->
                </div>
                <!-- End .vcenter -->
            </div>
            <!-- End .vcenter-container -->
        </div>
        <!-- end .about-banner -->

        <div class="md-margin2x hidden-xs"></div>
        <!-- space -->
        <div class="xlg-margin visible-xs"></div>
        <!-- space -->

        <div class="container">
            <div class="row">
                <div class="col-sm-12 promote-box text-center">
                    <span class="quote-icon"></span>
                    <h2>

                      
                           <asp:Image ID="imgSh" runat="server" CssClass="margincenter" />
                    </h2>
                </div>
                <!-- End .col-md-10 -->
            </div>
            <!-- End .row -->
        </div>
        <!-- End .container -->

        <div class="xlg-margin hidden-xs"></div>
        <!-- space -->
        <div class="md-margin visible-xs"></div>
        <!-- space -->

        <div class="container">
            <div class="row">
                <asp:Repeater ID="rptUnits" runat="server">
                    <ItemTemplate>


                        <div class="col-sm-4">
                            <div class="feature-box">
                                <span class="<%# Eval("TextImage","feature-icon {0}") %>"></span>
                                <h3><%# Eval("TextTitle") %> </h3>
                                <p class="text-justify">
                                    <%# Eval("TextDescription") %>
                                </p>
                            </div>
                            <!-- End .feature-box -->
                        </div>

                    </ItemTemplate>
                </asp:Repeater>

                <!-- End .col-sm-4 -->
 
                <!-- End .col-sm-4 -->
            </div>
            <!-- End .row -->
        </div>
        <!-- End .container -->

        <div class="lg-margin2x hidden-xs"></div>
        <!-- space -->
        <div class="lg-margin visible-xs"></div>
        <!-- space -->

        <div class="container about-company">
            <div class="row">
                <div class="col-sm-12">
 
                    <div class="row">
             
                           <ul id="tabUL" class="nav nav-tabs" role="tablist">
                             
                               <asp:Repeater ID="rptTextTabGroups" runat="server">
                                   <ItemTemplate>
                                        <li><a href='<%# Eval("TextName","#{0}") %>' role="tab" data-toggle="tab"><%# Eval("TextTitle") %></a></li>
                                   </ItemTemplate>
                               </asp:Repeater>
                           
                        </ul>

                        <!-- Tab panes -->
                        <div class="tab-content">
                            <asp:Repeater ID="rptTextBody" runat="server">
                                <ItemTemplate>
                                     <div class="tab-pane fade" id='<%# Eval("TextName") %>'>
                                         <%# Eval("TextDescription") %>
                                         </div>
                                </ItemTemplate>
                            </asp:Repeater>
                      
                        </div><!-- End .tab-content -->

                        <!-- End .col-sm-8 -->
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
        <div class="lg-margin2x visible-xs"></div>
        <!-- space -->
        <div class="testimonials-section home parallax" data-stellar-background-ratio="0.4">
            <div class="container">


                <img src="images/Logo_Fa.png" style="margin: 0 auto;" />



            </div>
            <!-- End .container -->

            <div class="vcenter-container">
                <div class="vcenter bottom-nav">
                    <div class="owl-carousel testimonials-slider">
                        <asp:Repeater ID="rptSlide" runat="server">
                            <ItemTemplate>
                                <div class="testimonial">
                                  
                                    <p style="margin:40px 0 0 20px;"><%# Eval("TextDescription") %></p>
                                    </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <!-- End .testimonial -->
                      
                        <!-- End .testimonial -->
                    </div>
                    <!-- End .about-banner-slider -->
                </div>
                <!-- End .vcenter -->
            </div>
            <!-- End .vcenter-container -->
        </div>
        <!-- end .testimonials-section -->
       

        <div class="lg-margin3x hidden-xs"></div>
        <!-- space -->
        <div class="lg-margin2x visible-xs"></div>
        <!-- space -->

        <div class="container services">
            <h2 class="text-right">
                <asp:Literal ID="ltIndustrial" runat="server" Text="<%$Resources:Resource,MashhadIndustrialGroupFactories %>"></asp:Literal></h2>
            <div class="row">
                <asp:Repeater ID="rptService1" runat="server">
                    <ItemTemplate>
                        <div class="col-md-6 service padding-right-md floatright">
                           
                            <img src='<%# Eval("textimage","/uploads/TextImages/{0}") %>' alt='<%# Eval("TextTitle") %>' class="service-icon" />
                            <div class="service-content">
                                <h3 class="text-right"><%# Eval("TextTitle") %></h3>
                                <p class="text-justify"><%# Eval("TextDescription") %></p>
                            </div>
                            <!-- End .service-content -->
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            
            </div>
            <!-- End .row -->
      

            <!-- End .row -->
        </div>
        <!-- End .container.services -->

        
        <div class="lg-margin2x visible-xs"></div>
        <!-- space -->

        <div class="container">
            <div class="carousel-container">
                <h2 class="carousel-title">
                    <asp:Literal ID="ltCertificationStandard" runat="server" Text="<%$Resources:Resource,CertificationStandard %>"></asp:Literal></h2>
                <div class="row">
                    <div class="owl-carousel latest-projects">
                        <asp:Repeater ID="rptPortfolio" runat="server">
                            <ItemTemplate>
                                <div class="portfolio-item">
                                    <figure>
                                        <img src='<%# Eval("TextImage","/images/standards/{0}") %>' alt='<%# Eval("TextTitle") %>' style="width: 273px; height: 204px;" />


                                    </figure>
                                    <!-- End .gallery-item-wrapper -->
                                    <div class="portfolio-meta" style="height:50px;">
                                        <h3><a href="single-portfolio.html" title="Lorem ipsum dolor"><%# Eval("TextTitle") %></a></h3>

                                    </div>
                                    <!-- End .portfolio-meta -->
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                     
                    </div>
                    <!-- End .owl-carousel -->
                </div>
                <!-- End .row -->
            </div>
            <!-- End .carousel-container -->
        </div>
        <!-- End .container -->

        <div class="lg-margin2x hidden-xs"></div>
        <!-- space -->
        <div class="xlg-margin visible-xs"></div>
        <!-- space -->

    </section>
    <!-- End #content -->
</asp:Content>
