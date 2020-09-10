<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Social.aspx.cs" Inherits="MashadCarpet.Social" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section id="content" role="main">
        <div class="container">
            <div class="row">
                <div class="col-md-12">

                    <ul class="breadcrumb">
                        <li><a href="Default.aspx" title="Home">
                            <asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a>    <i class="fa fa-arrow-right newfa"></i></li>
                        <li class="active">
                            <h1>
                            <asp:Literal ID="lblProductsTitle" runat="server" Text="<%$Resources:Resource,SocialResponsibility%>"></asp:Literal></h1></li>
                    </ul>

                   
                    <!-- space -->


                    <hr class="nomrt" />

                    <!-- Tab nav -->
                    <ul class="nav nav-pills" role="tablist">
                        <li class="active" style="margin-left: 19px;"><a href="#tab1" role="tab" data-toggle="tab">
                            <asp:Literal ID="lblSocialResponsibility" runat="server" Text="<%$Resources:Resource,SocialResponsibility%>"></asp:Literal></a></li>
                        <asp:Repeater ID="rptTabs" runat="server">
                            <ItemTemplate>
                                <li>
                                    <h2>
                                    <a href='#<%# Eval("TextID") %>' role="tab" data-toggle="tab"><%# Eval("TextTitle") %></a>
                                        </h2>
                                        </li>
                            </ItemTemplate>
                        </asp:Repeater>

                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div class="tab-pane fade in active" id="tab1">

                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Image ID="TextImage" runat="server" CssClass="textImage" />
                                </div>
                                <div class="col-md-6 directionRTL text-justify">
                                    <asp:Label ID="lblDesc" runat="server" Text="" CssClass="directionRTL text-right"></asp:Label>
                                </div>

                            </div>

                        </div>
                        <!-- End .tab-pane -->
                        <asp:Repeater ID="rptTabPane" runat="server">
                            <ItemTemplate>
                                <div class="tab-pane fade" id='<%# Eval("TextID") %>'>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <img src='<%# Eval("TextImage","/Uploads/TextImages/{0}") %>' class="textImage" />
                                        </div>
                                        <div class="col-md-6">
                                            <p class="directionRTL text-justify">
                                                <%# Eval("TextDescription") %>
                                            </p>
                                        </div>

                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <!-- End .tab-pane -->
                    </div>
                    <!-- End .tab-content -->

                    <div class="lg-margin2x"></div>
                    <!-- space -->
                </div>
            </div>
        </div>
    </section>

</asp:Content>
