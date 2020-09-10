<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="MashadCarpet.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section id="content" role="main">
        <div class="breadcrumb-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">
                            <li><a href="Default.aspx" title="Home"><asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a></li>
                            <li class="active"><asp:Literal ID="ltMyAccount" runat="server" Text="<%$Resources:Resource,MyAccount%>"></asp:Literal></li>
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
            <div id="register-form">
                <div class="row">
                           <div class="col-sm-6 padding-left-md">
                        <h2 class="color2 text-right"><asp:Literal ID="ltAddress" runat="server" Text="<%$Resources:Resource,StoreAddress%>"></asp:Literal> </h2>
                        <div class="form-group text-right">
                            <label for="company" class="form-label"><asp:Literal ID="ltAddress1" runat="server" Text="<%$Resources:Resource,StoreAddress%>"></asp:Literal>1</label>
                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <!-- End .form-group -->
                        <div class="form-group text-right">
                            <label for="address1" class="form-label"><asp:Literal ID="ltAddress2" runat="server" Text="<%$Resources:Resource,StoreAddress%>"></asp:Literal>2</label>
                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <!-- End .form-group -->

                        <div class="form-group text-right">
                            <label for="postcode" class="form-label"><asp:Literal ID="ltPostalCode" runat="server" Text="<%$Resources:Resource,Postalcode%>"></asp:Literal></label>
                            <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <!-- End .form-group -->
                        <div class="form-group text-right">
                            <label for="country" class="form-label"><asp:Literal ID="ltProvince" runat="server" Text="<%$Resources:Resource,Province%>"></asp:Literal></label>
                            <div class="large-selectbox clearfix">
                                <asp:DropDownList ID="ddlProvince" runat="server" CssClass="selectbox" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>
                            <!-- End .large-selectbox-->
                        </div>
                        <!-- End .form-group -->
                        <div class="form-group text-right">
                            <label for="region" class="form-label"><asp:Literal ID="ltCity" runat="server" Text="<%$Resources:Resource,StoreCity%>"></asp:Literal></label>
                            <div class="large-selectbox clearfix">
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="selectbox">
                                </asp:DropDownList>
                            </div>
                            <!-- End .large-selectbox-->
                        </div>
                        <!-- End .form-group -->


                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger text-right" />



                    </div>
                    <!-- End .col-sm-6 -->
                       <div class="md-margin visible-xs clearfix"></div>
                    <!-- space -->




                    <div class="col-sm-6 padding-right-md">
                        <h2 class="color2 text-right"><asp:Literal ID="ltPersonalInfo" runat="server" Text="<%$Resources:Resource,PersonalInformation%>"></asp:Literal></h2>
                        <div class="form-group text-right">
                            <label for="firstname" class="form-label"><asp:Literal ID="ltName" runat="server" Text="<%$Resources:Resource,Name%>"></asp:Literal></label>
                            <%--<input type="text" name="firstname" id="firstname" class="form-control input-lg" required>--%>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <!-- End .form-group -->
                        <div class="form-group text-right">

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$Resources:Resource,EnterFamily%>" ControlToValidate="txtFamily" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <label for="lastname" class="form-label">نام خانوادگی</label>
                            <%--<input type="text" name="lastname" id="lastname" class="form-control input-lg" required>--%>
                            <asp:TextBox ID="txtFamily" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <!-- End .form-group -->
                        <div class="form-group text-right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$Resources:Resource,EnterEmail%>" ControlToValidate="txtEmail" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$Resources:Resource,EnterCorrectEmail%>" ForeColor="Red" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            <asp:CustomValidator ID="cvEmail" runat="server" ErrorMessage="<%$Resources:Resource,InvalidEmail%>" ForeColor="Red" OnServerValidate="cvEmail_ServerValidate">*</asp:CustomValidator>
                            <label for="email" class="form-label"><asp:Literal ID="ltEmail" runat="server" Text="<%$Resources:Resource,Email%>"></asp:Literal></label>

                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <!-- End .form-group -->
                        <div class="form-group text-right">
                            <label for="telephone" class="form-label"><asp:Literal ID="ltphone" runat="server" Text="<%$Resources:Resource,StorePhone%>"></asp:Literal></label>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <div class="form-group text-right">
                            <label for="telephone" class="form-label"><asp:Literal ID="ltmobile" runat="server" Text="<%$Resources:Resource,Mobile%>"></asp:Literal></label>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <!-- End .form-group -->
                     <%--   <div class="form-group text-right">

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="کلمه عبور را وارد نمایید" ControlToValidate="txtPass" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <label for="fax" class="form-label">کلمه عبور</label>
                            <asp:TextBox ID="txtPass" runat="server" CssClass="form-control input-lg text-right" TextMode="Password"></asp:TextBox>
                        </div>--%>
                        <!-- End .form-group -->
                    </div>
                    <!-- End .col-sm-6 -->

                 
             
                </div>
                <!-- End .row -->

                <div class="lg-margin2x"></div>
                <!-- space -->


                <%--<input type="submit" class="btn btn-custom btn-lg pull-right" value="Create Account">--%>
                <asp:Button ID="btnRegister" runat="server" Text="<%$Resources:Resource,SaveButton%>" CssClass="btn btn-custom btn-lg pull-right" OnClick="btnRegister_Click" />
            </div>
        </div>
        <!-- End .container -->

        <div class="xlg-margin"></div>
        <!-- space -->

    </section>
    <!-- End #content -->
</asp:Content>
