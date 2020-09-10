<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="MashadCarpet.Blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/css/animate.css" rel="stylesheet" />
    <script src="/js/wow.js"></script>
    <section id="content" role="main">
        <div class="breadcrumb-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">
                            <li><a href="/Default.aspx" title="Home">
                                <asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a>  <i class="fa fa-arrow-right newfa"></i></li>
                            <li><a href="/blog/all?PageID=1" title="Home">
                                <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Resource,Blog%>"></asp:Literal></a>  <i class="fa fa-arrow-right newfa"></i></li>
                            <li class="active"><a href="#" title="Blog">
                                <h1>
                                <asp:Literal ID="ltBlog" runat="server" Text=""></asp:Literal>
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
                <aside class="col-md-3 col-sm-4 sidebar margin-top-up" role="complementary">
                    <div class="widget">
                        <h3>
                            <asp:Literal ID="ltProductCategories" runat="server" Text="<%$Resources:Resource,ProductCategories%>"></asp:Literal></h3>

                        <ul id="category-widget">
                            <li><a href="/Blog/all?PageID=1">
                                <asp:Literal ID="ltAll" runat="server" Text="<%$Resources:Resource,All%>"></asp:Literal></a></li>
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
                        <h3>
                            <asp:Literal ID="ltLatestNews" runat="server" Text="<%$Resources:Resource,LatestNews%>"></asp:Literal></h3>

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

                                            <a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>' class="readmore pull-right" role="button">
                                                <asp:Literal ID="ltContinuation" runat="server" Text="<%$Resources:Resource,Continuation%>"></asp:Literal></a>

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
                        <h3>
                            <asp:Literal ID="ltPopularNews" runat="server" Text="<%$Resources:Resource,PopularNews%>"></asp:Literal></h3>


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
                                                <a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>'><%# Eval("BlogTitle") %></a></a>
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
                    <!-- End .widget -->


                </aside>
                <!-- End .sidebar -->
                <div class="col-md-9 col-sm-8 padding-right-lg">
                    <asp:Repeater ID="rptBlogs" runat="server">
                        <ItemTemplate>
                            <article class="article">


                                <div id="post-id-20" class="article-media-container carousel slide" data-ride="carousel" data-interval="6000">
                                    <!-- Wrapper for slides -->
                                    <div class="carousel-inner">
                                        <div class="item active">
                                            <a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>'>
                                                <img src='<%# Eval("BlogImage","/Uploads/Blogs/{0}") %>' class="img-responsive wow rotateInUpRight animated" alt='<%# Eval("BlogTitle") %>' style="height: 450px; width: 750px;"></a>
                                            <%--<img src="/images/boy1.png" alt="boy" class="wow bounce pull-left" />--%>
                                        </div>
                                        <!-- End .item -->


                                    </div>
                                    <!-- End .carousel-inner -->


                                </div>
                                <!-- End .article-media-container -->

                                <div class="article-meta-box">
                                    <span class="article-icon article-date-icon"></span>
                                    <span class="meta-box-text"><%# string.Format("{0:d}",Eval("SubmitDate")) %>
                                </div>
                                <!-- End .article-meta-box -->
                                <div class="article-meta-box article-meta-comments">
                                    <span class="article-icon article-comment-icon"></span>
                                    <a href="#" class="meta-box-text"><%# Eval("VisitCounts") %><asp:Literal ID="ltVisit2" runat="server" Text="<%$Resources:Resource,Visit%>"></asp:Literal></a>
                                </div>
                                <!-- End .article-meta-box -->

                                <h2><a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>'><%# Eval("BlogTitle") %></a></h2>
                                <p><%# Eval("BlogText") %></p>

                                <div class="article-meta-container clearfix">
                                    <a href='<%# String.Format("/blog/{0}/{1}", Eval("BlogGroupName"), Eval("BlogName")) %>' class="readmore pull-right" role="button">
                                        <asp:Literal ID="ltContinuation" runat="server" Text="<%$Resources:Resource,Continuation%>"></asp:Literal></a>
                                    <div class="article-meta-wrapper pull-left">

                                        <span class="article-meta">
                                            <asp:Literal ID="ltBlogGroup" runat="server" Text="<%$Resources:Resource,BlogGroup%>"></asp:Literal>: <a href='<%# Eval("BlogGroupName","/Blog/{0}?PageID=1") %>' title="Fashion"><%# Eval("BlogGroupTitle") %></a></span>
                                        <span class="article-meta">
                                            <asp:Literal ID="ltVisit" runat="server" Text="<%$Resources:Resource,Visit%>"></asp:Literal>: <%# Eval("VisitCounts") %></span>

                                    </div>
                                    <!-- End .article-meta-wrapper -->
                                </div>
                                <!-- End .artcile-meta-container -->
                            </article>
                            <!-- End .article -->
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:Panel ID="pnlPagination" runat="server">
                        <div class="pagination-container clear-margin clearfix">

                            <!-- End.pagination-info -->
                            <ul class="pagination pull-right">
                                <asp:Repeater ID="rptPageNum" runat="server" OnItemDataBound="rptPageNum_ItemDataBound" OnItemCommand="rptPageNum_ItemCommand">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfPageID" runat="server" Value='<%# Eval("pageid") %>' />
                                        <li id='<%# Eval("pageid") %>'>

                                            <asp:LinkButton ID="c" runat="server" CommandArgument='<%# Eval("pageid") %>'>
                                                <asp:Label ID="lblPageNum" runat="server" Text='<%# Eval("pageid") %>'></asp:Label>
                                            </asp:LinkButton>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </ul>
                        </div>
                    </asp:Panel>

                    <!-- End pagination-container -->
                    <div class="md-margin2x visible-sm visible-xs"></div>
                    <!-- space -->
                </div>
                <!-- End .col-md-9 -->


            </div>
            <!-- End .row -->
        </div>
        <!-- End .container -->

        <div class="lg-margin2x"></div>
        <!-- space -->

    </section>
    <!-- End #content -->
</asp:Content>
