<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Track.aspx.cs" Inherits="MashadCarpet.Track" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content" role="main">
        <div class="breadcrumb-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">
                            <li><a href="/Default.aspx" title="Home">صفحه اصلی</a></li>
                            <li class="active">پیگیری خرید</li>
                        </ul>
                    </div>
                    <!-- End .col-md-12 -->
                </div>
                <!-- End .row -->
            </div>
            <!-- End .container -->
            <asp:Panel ID="pnlCode" runat="server">
               
                <div class="col-sm-6 col-md-push-3 padding-left-md">
                    <div id="login-form">

                        <div class="form-group text-right">
                            <label for="email" class="form-label text-right">شماره پیگیری</label>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="شماره پیگیری معتبر نمی باشد." ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate">*</asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="شماره پیگیری خود را وارد نمایید." ControlToValidate="txtTrackCode" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtTrackCode" runat="server" class="form-control input-lg text-right"></asp:TextBox>

                        </div>
                        <!-- End .form-group -->

                        <div class="xs-margin"></div>
                        <!-- space -->
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger text-right directionRTL" />
                        <br />
                        <br />
                        <asp:Button ID="btnTracking" runat="server" Text="پیگیری" CssClass="btn btn-custom btn-lg min-width pull-right" OnClick="btnTracking_Click" />





                    </div>
                </div>
          
            </asp:Panel>
            <asp:Panel ID="pnlTrack" runat="server" Visible="false">

                <div class="accordion" id="collapse">
                    <div class="accordion-group panel">
                        <div class="container">
                            <h2 class="accordion-title text-right">
                                <span>اطلاعات کاربر</span>
                                <a class="accordion-btn pull-right" data-toggle="collapse" href="#collapse-three"></a>
                            </h2>
                            <!-- End .accourdion-title -->

                            <div class="accordion-body collapse" id="collapse-three">
                                <div class="row accordion-body-wrapper">
                                    <div class="col-sm-6 col-md-push-3 padding-left-md">
                                        <table class="directionRTL table table-responsive">
                                            <tr>
                                                <td>نام کاربر
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>ایمیل
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>تلفن
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>ادرس1
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAddress1" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>آدرس2
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAddress2" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>کد پستی
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPostalCode" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <!-- End .col-md-12 -->
                                </div>
                                <!-- End .accordion-body-wrapper -->
                                <div class="lg-margin2x"></div>
                                <!-- space -->
                            </div>
                            <!-- End .accordion-body -->
                        </div>
                        <!-- End .container -->
                    </div>
                    <!-- End .accordion-group -->


                    <div class="accordion-group panel">
                        <div class="container">
                            <h2 class="accordion-title text-right">
                                <span>اطلاعات گیرنده سفارش</span>
                                <a class="accordion-btn" data-toggle="collapse" href="#collapse-four"></a>
                            </h2>
                            <!-- End .accourdion-title -->

                            <div class="accordion-body collapse" id="collapse-four">
                                <div class="row accordion-body-wrapper">
                                    <div class="col-sm-6 col-md-push-3 padding-left-md">
                                        <table class="directionRTL table table-responsive">
                                            <tr>
                                                <td>نام گیرنده سفارش
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRecipientName" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>تلفن
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRecipientPhone" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>آدرس
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRecipientAddress" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                    <!-- End .col-md-12 -->
                                </div>
                                <!-- End .accordion-body-wrapper -->
                                <div class="lg-margin2x"></div>
                                <!-- space -->
                            </div>
                            <!-- End .accordion-body -->
                        </div>
                        <!-- End .container -->
                    </div>
                    <!-- End .accordion-group -->



                    <div class="accordion-group panel">
                        <div class="container">
                            <h2 class="accordion-title text-right">
                                <span>اطلاعات سفارش</span>
                                <a class="accordion-btn open" data-toggle="collapse" href="#collapse-five"></a>
                            </h2>
                            <!-- End .accourdion-title -->

                            <div class="accordion-body collapse in" id="collapse-five">
                                <div class="row accordion-body-wrapper">
                                    <div class="col-md-12">
                                        <h3 class="text-right">وضعیت سفارش:
                                            <asp:Label ID="lblOrderStatus" runat="server" Text=""></asp:Label>
                                        </h3>
                                        <table class="table checkout-table directionRTL text-right">
                                            <thead>
                                                <tr>
                                                    <th class="table-title text-right">نام محصول</th>
                                                    <th class="table-title text-right">قیمت فی</th>
                                                    <th class="table-title text-right">تعداد</th>
                                                    <th class="table-title text-right">قیمت کل</th>
                                                    <th></th>
                                                </tr>
                                            </thead>

                                            <asp:Repeater ID="rptOrderDetails" runat="server" OnItemDataBound="rptOrderDetails_ItemDataBound">
                                                <ItemTemplate>

                                                    <tbody>
                                                        <tr>
                                                            <td class="product-name-col text-right">
                                                                <figure>
                                                                    <a href='<%# Eval("ProductName","/Product/{0}") %>'>
                                                                        <img src='<%# Eval("ProductImage","/Uploads/Products/{0}") %>' alt="White linen sheer dress" style="width: 170px; height: 250px;"></a>
                                                                </figure>
                                                                <h2 class="product-name"><a href='<%# Eval("ProductName","/Product/{0}") %>'><%# Eval("ProductTitle") %></a></h2>
                                                                <ul>
                                                                    <li>رنگ: <%# Eval("ColorTitle") %></li>
                                                                    <li>سایز: <%# Eval("SizeTitle") %></li>
                                                                </ul>
                                                            </td>



                                                            <td class="product-price-col fontBRoya"><span class="product-price-special fontBRoya"><%# string.Format("{0:N0}",Eval("ProductPrice")) %> تومان</span></td>
                                                            <td>
                                                                <div class="custom-quantity-input">
                                                                    <asp:TextBox ID="txtCount" runat="server" Text='<%# Eval("Count") %>' Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                            <td class="product-total-col"><span class="product-price-special">
                                                                <asp:HiddenField ID="hfOrderDetailID" runat="server" Value='<%# Eval("OrderDetailID") %>' />
                                                                <asp:Label ID="lblTotalPrice" runat="server" Text="" CssClass="fontBRoya"></asp:Label></span>
                                                            </td>

                                                        </tr>

                                                    </tbody>



                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </table>
                                        <div class="md-margin half"></div>
                                        <!-- space -->



                                    </div>
                                    <!-- End .col-md-12 -->
                                </div>
                                <!-- End .accordion-body-wrapper -->
                            </div>
                            <!-- End .accordion-body -->
                        </div>
                        <!-- End .container -->
                    </div>
                    <!-- End .accordion-group -->
                </div>
            </asp:Panel>
        </div>
    </section>
</asp:Content>
