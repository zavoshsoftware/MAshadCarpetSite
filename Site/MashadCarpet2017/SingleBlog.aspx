<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SingleBlog.aspx.cs" Inherits="MashadCarpet.SingleBlog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content" role="main">
        <div class="breadcrumb-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">
                             <li><a href="/Default.aspx" title="Home">
                                <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a>  <i class="fa fa-arrow-right newfa"></i></li>
                            <li><asp:HyperLink ID="hlBlogGroupTitle" runat="server"></asp:HyperLink> <i class="fa fa-arrow-right newfa"></i></li>
                            <li class="active"><a href="#" title="Blog">
                                <h1>
                              <asp:Label ID="lblBlogTitle" runat="server" Text=""></asp:Label>
                                    </h1></a></li>

                        
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
                <aside class="col-md-3 sidebar margin-top-up" role="complementary">
                    <div class="widget">
                        <h3><asp:Literal ID="ltProductCategories" runat="server" Text="<%$Resources:Resource,ProductCategories%>"></asp:Literal></h3>

                        <ul id="category-widget">
                            <li><a href="/Blog/all?PageID=1"><asp:Literal ID="ltAll" runat="server" Text="<%$Resources:Resource,All%>"></asp:Literal></a></li>
                            <asp:Repeater ID="rptBlogGroup" runat="server" OnItemDataBound="rptBlogGroup_ItemDataBound">
                                <ItemTemplate>
                                    <li><a href='<%# Eval("BlogGroupName","/Blog/{0}?PageID=1") %>'><%# Eval("BlogGroupTitle") %> <span class="category-widget-btn"></span></a>
                                        <ul>
                                            <asp:HiddenField ID="hfBlogGroupID" runat="server" Value='<%# Eval("BlogGroupID") %>' />
                                            <asp:Repeater ID="rptBlogs" runat="server">
                                                <ItemTemplate>

                                                    <li><a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>'><%# Eval("BlogTitle") %></a></li>


                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>

                        </ul>
                    </div>
                    <!-- End .widget -->

                    <div class="widget">
                        <h3><asp:Literal ID="ltLatestNews" runat="server" Text="<%$Resources:Resource,LatestNews%>"></asp:Literal></h3>

                        <div class="owl-carousel latest-posts-slider">
                            <div class="article-list">
                                <asp:Repeater ID="rptLatestBlogs" runat="server">
                                    <ItemTemplate>

                                        <article class="article">
                                            <div class="article-media-container">
                                                <a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>'>
                                                    <img src='<%# Eval("BlogImage","/Uploads/Blogs/{0}") %>' alt='<%# Eval("BlogTitle") %>' style="height: 90px; width: 270px;"></a>
                                            </div>
                                            <!-- End .article-media-container -->
                                            <div class="article-meta-box">
                                                <span class="article-icon article-date-icon"></span>
                                                <span class="meta-box-text"><%# string.Format("{0:d}",Eval("SubmitDate")) %></span>
                                            </div>
                                            <!-- End .article-meta-box -->
                                            <h4 class="text-right">
                                                <a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>'><%# Eval("BlogTitle") %></a>
                                            </h4>
                                            <p><%# Eval("BlogText") %></p>

                                            <a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>' class="readmore pull-right" role="button"><asp:Literal ID="ltContinuation" runat="server" Text="<%$Resources:Resource,Continuation%>"></asp:Literal></a>

                                        </article>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>

                            <!-- End .article-list -->
                        </div>
                        <!-- End .owl-carousel -->

                    </div>
                    <!-- End .widget -->


                    <div class="xs-margin"></div>
                    <!-- space -->

                    <div class="widget">
                        <h3><asp:Literal ID="ltPopularNews" runat="server" Text="<%$Resources:Resource,PopularNews%>"></asp:Literal></h3>


                        <div class="owl-carousel popular-posts-slider">
                            <div class="article-list">
                                <asp:Repeater ID="rptPopularBlogs" runat="server">
                                    <ItemTemplate>
                                        <article class="article">
                                            <div class="article-meta-box">
                                                <span class="article-icon article-date-icon"></span>
                                                <span class="meta-box-text"><%# string.Format("{0:d}",Eval("SubmitDate")) %></span>
                                            </div>
                                            <!-- End .article-meta-box -->
                                            <h4 class="text-right">
                                                <a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>'>
                                                    <%# Eval("BlogTitle") %></a></a>
                                            </h4>
                                        </article>
                                    </ItemTemplate>
                                </asp:Repeater>


                            </div>
                            <!-- End .article-list -->


                            <!-- End .article-list -->

                        </div>
                        <!-- End .owl-carousel -->

                    </div>

                </aside>
                <!-- End .sidebar -->


                <div class="lg-margin visible-sm visible-xs clearfix"></div>
                <!-- space -->
                <div class="col-md-9 padding-right-larger">

                    <article class="article">
                        <div id="post-id-20" class="article-media-container carousel slide" data-ride="carousel" data-interval="6000">
                            <!-- Wrapper for slides -->
                            <div class="carousel-inner">
                                <div class="item active">
                                    <%--<img src="/images/blog/post4.jpg" class="img-responsive" alt="Slider 1">--%>
                                    <asp:Image ID="BlogImg" runat="server" class="img-responsive"  Width="770px" Height="450px" />
                                </div>
                                <!-- End .item -->

                                <%--  <div class="item">
                                        <img src="/images/blog/post2.jpg" class="img-responsive" alt="Slider 2">
                                    </div><!-- End .item -->

                                    <div class="item">
                                        <img src="/images/blog/post3.jpg" class="img-responsive" alt="Slider 3">
                                    </div><!-- End .item -->--%>
                            </div>
                            <!-- End .carousel-inner -->

                            <!-- Controls -->
                            <%--   <a class="left carousel-control" href="#post-id-20" role="button" data-slide="prev">Prev</a>
                                <a class="right carousel-control" href="#post-id-20" role="button" data-slide="next">Next</a>--%>
                        </div>
                        <!-- End .article-media-container -->

                        <div class="article-meta-box">
                            <span class="article-icon article-date-icon"></span>
                            <span class="meta-box-text"> 
                                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></span>
                        </div>
                        <!-- End .article-meta-box -->
                        <div class="article-meta-box article-meta-comments">
                            <span class="article-icon article-comment-icon"></span>
                            <a href="#" class="meta-box-text">
                                <asp:Label ID="lblVisitCount" runat="server" Text=""></asp:Label></a>
                        </div>
                        <!-- End .article-meta-box -->

                        <h2>
                            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h2>
                        <p style="text-align:justify;">
                            <asp:Literal ID="lblBlogText" runat="server"></asp:Literal>
                         
                        </p>

                        <div class="sm-margin"></div>
                        <!-- space -->

                      
                        <!-- End .artcile-meta-container -->
                    </article>
                    <!-- End .article -->

                    <div class="sm-margin"></div>
                    <!-- space -->
                     

                    <div class="carousel-container">
                        <h3 class="carousel-title"><asp:Literal ID="ltRelatedBlogs" runat="server" Text="<%$Resources:Resource,RelatedBlogs%>"></asp:Literal></h3>

                        <div class="row">
                            <div class="owl-carousel related-posts-carousel">

                                <asp:Repeater ID="rptRelatedBlogs" runat="server">
                                    <ItemTemplate>
                                        <article class="article">
                                            <div class="article-media-container">
                                                <a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>'>
                                                    <img src='<%# Eval("BlogImage","/Uploads/Blogs/{0}") %>' alt='<%# Eval("BlogTitle") %>'></a>
                                            </div>
                                            <!-- End .article-media-container -->
                                            <div class="article-meta-box">
                                                <span class="article-icon article-date-icon"></span>
                                                <span class="meta-box-text"><%# string.Format("{0:d}",Eval("SubmitDate")) %></span>
                                            </div>
                                            <!-- End .article-meta-box -->
                                            <h3 class="text-right">
                                                <a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>'><%# Eval("BlogTitle") %></a>
                                            </h3>
                                            <p><%# Eval("BlogText") %></p>

                                            <a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>' class="readmore" role="button"><asp:Literal ID="ltContinuation" runat="server" Text="<%$Resources:Resource,Continuation%>"></asp:Literal></a>

                                        </article>
                                    </ItemTemplate>
                                </asp:Repeater>
 
                            </div>
                            <!-- End .owl-carousel -->
                        </div>
                        <!-- End .row -->
                    </div>
                    <!-- End .carousel-container -->

                </div>
                <!-- End .col-md-9 -->



            </div>
            <!-- End .row -->
        </div>
        <!-- End .container -->

        <div class="md-margin2x"></div>
        <!-- space -->

    </section>
    <!-- End #content -->
</asp:Content>
