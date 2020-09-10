<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SendTicket.aspx.cs" Inherits="MashadCarpet.SendTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content" role="main">
        <div class="breadcrumb-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">
                            <li><a href="/Default.aspx" title="Home"><asp:Literal ID="lblMainPageTitle" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a></li>
                            <li class="active"><asp:Literal ID="ltSubmitTicket" runat="server" Text="<%$Resources:Resource,SubmitTicket%>"></asp:Literal></li>
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
                <div class="col-sm-6 col-md-push-3 padding-left-md">
                    <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
                        <p class="alert alert-success directionRTL">
                           <asp:Literal ID="ltDearuser" runat="server" Text="<%$Resources:Resource,Dearuser%>"></asp:Literal>
                            <br />
                         <asp:Literal ID="ltSuccessfulMessage" runat="server" Text="<%$Resources:Resource,SuccessfulMessage%>"></asp:Literal>
                            <br />
                         <asp:Literal ID="ltThanks" runat="server" Text="<%$Resources:Resource,Thanks%>"></asp:Literal>
                        </p>
                    </asp:Panel>
                    <asp:ValidationSummary ID="ValidationSummary1" CssClass="alert alert-danger directionRTL " runat="server" ValidationGroup="st"/>
                   
                    <asp:TextBox ID="txtTicketSubject" runat="server" class="form-control input-lg text-right fontDroid" placeholder="<%$Resources:Resource,TicketSubject%>"></asp:TextBox>
                    
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="directionRTL pull-right col-sm-6 " runat="server" ValidationGroup="st" ControlToValidate="txtTicketMessage" ForeColor="Red" ErrorMessage="<%$Resources:Resource,EnterTicketText%>">*</asp:RequiredFieldValidator>
                    <div class="input-group textarea-container">
                        <span class="input-group-addon" style="text-align:right!important;"><asp:Literal ID="ltTicketText" runat="server" Text="<%$Resources:Resource,TicketText%>"></asp:Literal></span>
                        <asp:TextBox ID="txtTicketMessage" runat="server" class="form-control text-right" Rows="6" Columns="30" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    
                   
                    <br /><br />
                    <asp:Button ID="btnSend" runat="server" Text="<%$Resources:Resource,Send%>" CssClass="btn btn-custom pull-right" OnClick="btnSend_Click" ValidationGroup="st"/>
                 
                </div>
            </div>
        </div>


    </section>
</asp:Content>
