<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="MashadCarpet.ErrorPages._404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content" role="main">

        <div class="container">
            <div class="no-content-box wow fadeInRightBig">
                <div class="vcenter-container">
                    <div class="vcenter notfoundpageContent">
                        <h2>خطا 404</h2>
                        <h3>متاسفانه صفحه مورد نظر شما پیدا نشد.</h3>
                        <p>
                            متاسفانه صفحه مورد نظر شما در دسترس نمی باشد. 
                            می توانید از طریق لینک های زیر، صفحه مورد نظر خود را پیدا کنید
                            
                        </p>
                        <div class="notfoundpageLinks">
                            <a href="/default.aspx" class="btn btn-success">صفحه اصلی سایت</a>
                            <a href="/carpet-online-shopping" class="btn btn-success">محصولات</a>
                            <a href="/Blog/all?PageID=1" class="btn btn-success">اخبار و تازه ها</a>
                            <a href="/About-us" class="btn btn-success">درباره ما</a>
                            <a href="/Contact-us" class="btn btn-success">تماس با ما</a>
                            <a href="/corporate-social-responsibility" class="btn btn-success">مسئولیت اجتماعی</a>
                            <a href="/Sales-Representatives" class="btn btn-success">فروشگاه ها</a>

                        </div>
                    </div>
                    <!-- End .vcenter -->
                </div>
                <!-- End .vcenter-container -->
            </div>
            <!-- End .no-content-box -->
        </div>
        <!-- End .container -->
    </section>
</asp:Content>
