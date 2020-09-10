<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="MashadCarpet.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content" role="main">
        <div class="breadcrumb-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">Login
                            <li><a href="/Default.aspx" title="Home"><asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a></li>
                            <li class="active"><asp:Literal ID="ltLogin" runat="server" Text="<%$Resources:Resource,Login%>"></asp:Literal></li>
                        </ul>
                    </div>
                    <!-- End .col-md-12 -->
                </div>
                <!-- End .row -->
            </div>
            <!-- End .container -->
        </div>
        <!-- End .breadcrumb-container -->

        <div class="xs-margin half"></div>
        <!-- space -->

        <div class="container">
            <div class="row">


                <div class="xlg-margin visible-xs clearfix"></div>
                <!-- space -->
                <asp:Panel ID="pnlLogin" runat="server" DefaultButton="btnLogin">
                <div class="col-sm-6 col-md-push-3 padding-left-md">
                    <div id="login-form">
                        <asp:Panel ID="pnlRetURL" runat="server" CssClass="alert alert-success" Visible="false">
                            <p class="directionRTL"><asp:Literal ID="ltDearuser" runat="server" Text="<%$Resources:Resource,Dearuser%>"></asp:Literal>
                                <br />
                          <asp:Literal ID="ltLoginForAccess" runat="server" Text="<%$Resources:Resource,LoginForAccess%>"></asp:Literal>
                                <br />
                       <asp:Literal ID="ltThanks" runat="server" Text="<%$Resources:Resource,Thanks%>"></asp:Literal>
                            </p>
                        </asp:Panel>
                        <h2 class="color2 text-right"><asp:Literal ID="ltLoginToSite" runat="server" Text="<%$Resources:Resource,LoginToSite%>"></asp:Literal></h2>
                        <div class="form-group text-right">
                            <label for="email" class="form-label text-right">    <asp:Literal ID="ltEmail" runat="server" Text="<%$Resources:Resource,Email%>"></asp:Literal></label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$Resources:Resource,EnterCorrectEmail%>" ValidationGroup="l" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red">*</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$Resources:Resource,EnterEmail%>" ValidationGroup="l" ControlToValidate="txtEmail" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtEmail" runat="server" class="form-control input-lg text-right"></asp:TextBox>

                        </div>
                        <!-- End .form-group -->
                        <div class="form-group text-right">
                            <label for="password" class="form-label text-right"><asp:Literal ID="ltPassword" runat="server" Text="<%$Resources:Resource,Password%>"></asp:Literal></label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$Resources:Resource,EnterYourPassword%>" ValidationGroup="l" ControlToValidate="txtPass" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtPass" runat="server" class="form-control input-lg text-right" TextMode="Password"></asp:TextBox>

                        </div>
                        <!-- End .form-group -->
                        <span class="help-block text-right"><asp:Literal ID="ltFor" runat="server" Text="<%$Resources:Resource,For%>"></asp:Literal> 
                               <%--<a href="/Register.aspx">ثبت نام کلیک</a>--%>
                            <asp:LinkButton ID="lbRegister" runat="server" OnClick="lbRegister_Click" CausesValidation="false"><asp:Literal ID="ltClickForRegister" runat="server" Text="<%$Resources:Resource,ClickForRegister%>"></asp:Literal> </asp:LinkButton>
                            <asp:Literal ID="ltDo" runat="server" Text="<%$Resources:Resource,Do%>"></asp:Literal> </span><br />
                        <span class="help-block text-right"><a href="#"><asp:Literal ID="ltForgetPassword" runat="server" Text="<%$Resources:Resource,ForgetPassword%>"></asp:Literal></a></span>


                        <div class="xs-margin"></div>
                        <!-- space -->

                        <%--<input type="submit" class="btn btn-custom btn-lg min-width" value="ورود">--%>
                        <asp:Button ID="btnLogin" runat="server" Text="<%$Resources:Resource,Login%>" CssClass="btn btn-custom btn-lg min-width" OnClick="btnLogin_Click" ValidationGroup="l" />
                        <br />
                        <br />
                        <asp:CustomValidator ID="cvLogin" runat="server" ErrorMessage="<%$Resources:Resource,IncorrectUsernameOrPass%>" ForeColor="Red" OnServerValidate="cvLogin_ServerValidate" ValidationGroup="l">*</asp:CustomValidator>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger text-right directionRTL" ValidationGroup="l"/>



                    </div>
                </div>
                    </asp:Panel>
                <!-- End .col-md-6 -->
            </div>
            <!-- End .row -->
        </div>
        <!-- End .container -->

        <div class="lg-margin2x"></div>
        <!-- space -->

    </section>
    <!-- End #content -->
</asp:Content>
