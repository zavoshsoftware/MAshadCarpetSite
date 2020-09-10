<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ContactUS.aspx.cs" Inherits="MashadCarpet.ContactUS" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">

    </asp:Content>
    
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
                                <asp:Literal ID="ltContactTitle" runat="server" Text="<%$Resources:Resource,ContactTitle%>"></asp:Literal></li>
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
                <div class="col-md-12">
                    <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3237.1000955784853!2d51.41528790000001!3d35.77291750000001!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3f8e0689f7d18999%3A0x21ed013cd321e531!2sTehran%2C+W+Atefi+St!5e0!3m2!1sen!2sir!4v1440220941433" width="100%" height="450" frameborder="0" style="border: 0" allowfullscreen></iframe>
                </div>
                <!-- End #map -->
            </div>
        </div>
        <div class="lg-margin"></div>
        <!-- space -->

        <div class="container">
            <div class="row">
                <div class="col-sm-9 padding-right-larger contact-container">
                    <h1 class="text-right">
                        <asp:Literal ID="ltContactForm" runat="server" Text="<%$Resources:Resource,ContactForm%>"></asp:Literal></h1>
                    <div id="contact-form">
                        <div class="row">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger text-right" ValidationGroup="cu" />
                            <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
                                <p class="alert alert-success text-right">
                                    <asp:Literal ID="ltSuccessfulMessage" runat="server" Text="<%$Resources:Resource,SuccessfulMessage%>"></asp:Literal>
                                    <br />
                                    <asp:Literal ID="ltThanks" runat="server" Text="<%$Resources:Resource,Thanks%>"></asp:Literal>
                                </p>
                            </asp:Panel>



                            <div class="col-sm-6">
                                <div class="form-group lg-margin">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$Resources:Resource,EnterText%>" ForeColor="Red" ControlToValidate="txtMsg" ValidationGroup="cu">*</asp:RequiredFieldValidator>
                                    <label for="contactmessage" class="form-label">
                                        <asp:Literal ID="ltText" runat="server" Text="<%$Resources:Resource,Text%>"></asp:Literal></label>

                                    <asp:TextBox ID="txtMsg" runat="server" CssClass="form-control input-lg min-height-lg" Columns="30" Height="156px" TextMode="MultiLine" ValidationGroup="cu"></asp:TextBox>
                                </div>
                                <div class="xs-margin"></div>
                                <div class="form-group">
                                    <asp:Button ID="btnSend" runat="server" Text="<%$Resources:Resource,Send%>" CssClass="btn btn-lg btn-custom-5" OnClick="btnSend_Click" ValidationGroup="cu" />
                                </div>
                            </div>



                            <!-- End .col-sm-6 -->

                            <div class="col-sm-6">
                                <div class="form-group lg-margin">
                                    <label for="contactname" class="form-label">
                                        <asp:Literal ID="ltSend" runat="server" Text="<%$Resources:Resource,Name%>"></asp:Literal></label>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-lg" ValidationGroup="cu"></asp:TextBox>
                                </div>
                                <div class="form-group lg-margin">
                                    <label for="contactemail" class="form-label">
                                        <asp:Literal ID="ltEmail" runat="server" Text="<%$Resources:Resource,Email%>"></asp:Literal></label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$Resources:Resource,EnterEmail%>" ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="cu">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$Resources:Resource,EnterCorrectEmail%>" ForeColor="Red" ValidationGroup="cu" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                    
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control input-lg"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <!-- End .row -->
                    </div>
                </div>
                <!-- End .col-md-9 -->




                <div class="lg-margin visible-xs clearfix"></div>
                <!-- End .space -->


                <div class="col-sm-3 contact-box-lg">
                    <h1>
                        <asp:Literal ID="ltContacts" runat="server" Text="<%$Resources:Resource,Contacts%>"></asp:Literal></h1>
                    
                    <ul class="contact-list">
                        <li>
                            <h2>
                                <asp:Literal ID="ltMashhadcarpetheadquarters" runat="server" Text="<%$Resources:Resource,Mashhadcarpetheadquarters%>"></asp:Literal>
                            </h2>
                        </li>
                        <asp:Repeater ID="rptheadquarters" runat="server">
                            <ItemTemplate>
                                <li><span><%# Eval("TextTitle") %>   :</span><%# Eval("TextDescription") %>  </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%-- <li><span>آدرس   :</span>تهران، خیابان ولیعصر، خیابان عاطفی، پلاک ۱۰۰، طبقه چهارم</li>
                        <li><span>تلفن:</span>۲۲۰۱۲۶۱۲</li>
                        <li><span>فکس:</span>۲۲۰۱۲۶۲۱</li>
                        <li><span>کد پستی:</span>۱۹۶۷۹۳۲۷۶۵</li>
                        <li><span>پست الکترونیک :</span>info@mashadcarpet.com</li>--%>
                    </ul>
                     <br />
                    <ul class="contact-list">
                        <li>
                            <h2>
                                <asp:Literal ID="ltFactory" runat="server" Text="<%$Resources:Resource,Factory%>"></asp:Literal></h2>
                        </li>
                        <asp:Repeater ID="rptFactoryInfo" runat="server">
                            <ItemTemplate>
                                <li><span><%# Eval("TextTitle") %>   :</span><%# Eval("TextDescription") %>  </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%--<li><span>آدرس   :</span>مشهد کیلومتر ۱۸ جاده قوچان</li>
                        <li><span>تلفن:</span> ۵۴۲۲۴۸۳-۰۵۱۱</li>
                        <li><span>فکس:</span>۵۴۲۲۴۹۵-۰۵۱۱</li>--%>
                    </ul>
                   
                    <ul class="clearfix">
                        <li class="floatright"><a href="https://t.me/MashadCarpetCo" class="" title="تلگرام فرش مشهد"><i class="fa fa-2x fa-telegram"   ></i></a></li>
                        <li class="floatright" style="margin-right: 5px;"><a href="https://www.instagram.com/mashadcarpet/" class="" title="اینستاگرام فرش مشهد"><i class="fa fa-2x fa-instagram"  ></i></a></li>
                    </ul>
                </div>
                <!-- End .col-sm-3 -->
            </div>
            <!-- End .row -->
        </div>
        <!-- End .container -->

        <div class="xlg-margin"></div>
        <!-- space -->

    </section>
    <!-- End #content -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>

    <script src="/js/persianumber.min.js"></script>

        <script type="text/javascript">

            $(document).ready(function () {

                $('*').persiaNumber();

            });

</script>
</asp:Content>
