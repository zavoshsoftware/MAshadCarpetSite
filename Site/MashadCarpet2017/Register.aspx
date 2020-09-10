<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MashadCarpet.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content" role="main">
        <div class="breadcrumb-container">
       

             <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">
                            <li><a href="/default.aspx" title="Home">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a> <i class="fa fa-arrow-right newfa"></i></li>
                            <li class="active">
                                <asp:Literal ID="ltContactTitle" runat="server" Text="<%$Resources:Resource,Register%>"></asp:Literal></li>
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
                     
                        <asp:Panel ID="pnlSuccess" CssClass="alert alert-succeed" runat="server" Visible="false">
                          <asp:Literal ID="ltThanks" runat="server" Text="<%$Resources:Resource,Thanks%>"></asp:Literal>
                               <asp:Literal ID="ltSuccessfulRegister" runat="server" Text="<%$Resources:Resource,SuccessfulRegister%>"></asp:Literal>

                        </asp:Panel>
                  
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger text-right" ValidationGroup="re"/>

                         <div class="col-sm-6 padding-right-md">
                        <h2 class="registerHeading"> 
                            <asp:Literal ID="ltAddress" runat="server" Text="<%$Resources:Resource,StoreAddress%>"></asp:Literal> </h2>
                        <div class="form-group text-right">
                            <label for="company" class="form-label"><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Resource,StoreAddress%>"></asp:Literal>1</label>
                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <!-- End .form-group -->
                        <div class="form-group text-right">
                            <label for="address1" class="form-label"><asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Resource,StoreAddress%>"></asp:Literal>2</label>
                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <!-- End .form-group -->

                        <div class="form-group text-right">
                            <label for="postcode" class="form-label"><asp:Literal ID="ltPostalcode" runat="server" Text="<%$Resources:Resource,Postalcode%>"></asp:Literal></label>
                            <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>

                             <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                 <ContentTemplate>
                        <!-- End .form-group -->
                        <div class="form-group text-right">
                            <label for="country" class="form-label"><asp:Literal ID="ltProvince" runat="server" Text="<%$Resources:Resource,Province%>"></asp:Literal></label>
                            <div class="large-selectbox clearfix">
                                <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control directionRTL" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>
                            <!-- End .large-selectbox-->
                        </div>
                        <!-- End .form-group -->
                        <div class="form-group text-right">
                            <label for="region" class="form-label"><asp:Literal ID="ltStoreCity" runat="server" Text="<%$Resources:Resource,StoreCity%>"></asp:Literal></label>
                            <div class="large-selectbox clearfix">
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control directionRTL">
                                </asp:DropDownList>
                                  <asp:CustomValidator ID="cvCity" Display="Dynamic" runat="server" ErrorMessage="<%$Resources:Resource,EnterCity%>" ForeColor="Red" OnServerValidate="cvCity_ServerValidate"  ValidationGroup="re">*</asp:CustomValidator>
                           
                            </div>
                            <!-- End .large-selectbox-->
                        </div>
                        <!-- End .form-group -->
</ContentTemplate>

                             </asp:UpdatePanel>



                    </div>
                   
                    <!-- End .col-sm-6 -->

                
                    <!-- space -->

                <div class="col-sm-6 padding-left-md">
                        <h2 class="registerHeading"><asp:Literal ID="ltPersonalInformation" runat="server" Text="<%$Resources:Resource,PersonalInformation%>"></asp:Literal></h2>
                        <div class="form-group text-right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                 ErrorMessage="<%$Resources:Resource,EnterName%>" ControlToValidate="txtUserName" ForeColor="Red" ValidationGroup="re">*</asp:RequiredFieldValidator>
                          
                             <label for="firstname" class="form-label"><asp:Literal ID="ltName" runat="server" Text="<%$Resources:Resource,Name%>"></asp:Literal></label>
                            <%--<input type="text" name="firstname" id="firstname" class="form-control input-lg" required>--%>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <!-- End .form-group -->
                                <div class="form-group text-right">
                            <label for="telephone" class="form-label"><asp:Literal ID="ltPhone" runat="server" Text="<%$Resources:Resource,StorePhone%>"></asp:Literal></label>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <div class="form-group text-right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="re" ErrorMessage="شماره موبایل خود را وارد نمایید" ControlToValidate="txtMobile" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <label for="telephone" class="form-label"><asp:Literal ID="ltMobile" runat="server" Text="<%$Resources:Resource,Mobile%>"></asp:Literal></label>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <!-- End .form-group -->
                        <div class="form-group text-right">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="re" ErrorMessage="<%$Resources:Resource,EnterEmail%>" ControlToValidate="txtEmail" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="re" runat="server" ErrorMessage="<%$Resources:Resource,EnterCorrectEmail%>" ForeColor="Red" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            <asp:CustomValidator ID="cvEmail" runat="server" ErrorMessage="<%$Resources:Resource,InvalidEmail%>" ForeColor="Red" OnServerValidate="cvEmail_ServerValidate" ValidationGroup="re">*</asp:CustomValidator>
                            <label for="email" class="form-label">   <asp:Literal ID="ltEmail" runat="server" Text="<%$Resources:Resource,Email%>"></asp:Literal></label>

                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control input-lg text-right"></asp:TextBox>
                        </div>
                        <!-- End .form-group -->
               
                        <!-- End .form-group -->
                        <div class="form-group text-right">

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="re" runat="server" ErrorMessage="<%$Resources:Resource,EnterYourPassword%>" ControlToValidate="txtPass" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <label for="fax" class="form-label"><asp:Literal ID="ltPassword" runat="server" Text="<%$Resources:Resource,Password%>"></asp:Literal></label>
                            <asp:TextBox ID="txtPass" runat="server" CssClass="form-control input-lg text-right" TextMode="Password"></asp:TextBox>
                        </div>
                        <!-- End .form-group -->
                    </div>
                    <!-- End .col-sm-6 -->
                </div>
                <!-- End .row -->
 


                <%--<input type="submit" class="btn btn-custom btn-lg pull-right" value="Create Account">--%>
                <asp:Button ID="btnRegister" runat="server" Text="<%$Resources:Resource,Register%>"  OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true; this.value = 'در حال ثبت اطلاعات';" UseSubmitBehavior="false" CssClass="btn btn-custom btn-lg pull-right" OnClick="btnRegister_Click" ValidationGroup="re" />
            </div>
        </div>
        <!-- End .container -->

        <div class="xlg-margin"></div>
        <!-- space -->

    </section>
    <!-- End #content -->
</asp:Content>
