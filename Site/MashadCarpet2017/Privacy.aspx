<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Privacy.aspx.cs" Inherits="MashadCarpet.Privacy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content" role="main">
        <div class="breadcrumb-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12"> 
                          <ul class="breadcrumb">
                            <li><a href="/default.aspx" title="Home">
                                <asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a> <i class="fa fa-arrow-right newfa"></i></li>
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
                <div class="col-md-12">
                    <asp:Image ID="PrivacyImage" runat="server" Width="100%" />
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12 text-justify directionRTL" style="line-height: 1.8em;">
                    <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                </div>

            </div>
        </div>
    </section>
</asp:Content>
