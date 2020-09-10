<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminEditProfile.aspx.cs" Inherits="MashadCarpet.Admin.AdminEditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
            <div class="ad-div">

                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />
                <table>

                    <tr>
                        <td>نام&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator Display="Dynamic" ID="rfvFname" runat="server" ErrorMessage="نام را وارد نمایید." ForeColor="Red" ControlToValidate="txtFirstName">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>نام خانوادگی</td>
                        <td>
                            <asp:TextBox ID="txtFamily" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ErrorMessage="نام خانوادگی را وارد نمایید." ForeColor="Red" ControlToValidate="txtFamily">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td>نقش کاربر</td>
                        <td>
                            <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control" Height="40"></asp:DropDownList>
                            <asp:CustomValidator Display="Dynamic" ID="cvRole" runat="server" ErrorMessage="نقش کاربر را وارد نمایید" OnServerValidate="cvRole_ServerValidate" ForeColor="Red">*</asp:CustomValidator>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>ایمیل</td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="rfvEmail" runat="server" ErrorMessage="ایمیل را وارد نمایید." ForeColor="Red" ControlToValidate="txtEmail">*</asp:RequiredFieldValidator>
                            <%--<asp:CustomValidator Display="Dynamic" ID="cvEmail" runat="server" ErrorMessage="این ایمیل قبلا وارد شده است" OnServerValidate="cvEmail_ServerValidate" ForeColor="Red">*</asp:CustomValidator>--%>

                            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator1" ControlToValidate="txtEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" ErrorMessage="ایمیل را صحیح وارد نمایید.">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>کلمه عبور</td>
                        <td>

                            <asp:TextBox ID="txtpass" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="rfvPass" runat="server" ErrorMessage="کلمه عبور را وارد نمایید." ForeColor="Red" ControlToValidate="txtpass">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>تلفن</td>
                        <td>

                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="rfvPhone" runat="server" ErrorMessage="تلفن را وارد نمایید." ForeColor="Red" ControlToValidate="txtPhone">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>موبایل</td>
                        <td>

                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ErrorMessage="موبایل را وارد نمایید." ForeColor="Red" ControlToValidate="txtMobile">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>1آدرس</td>
                        <td>

                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="1آدرس را وارد نمایید." ForeColor="Red" ControlToValidate="txtAddress1">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>2آدرس</td>
                        <td>

                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="2آدرس را وارد نمایید." ForeColor="Red" ControlToValidate="txtAddress2">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>استان</td>
                        <td>
                              <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control" Width="300" Height="40" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                        <%--    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="شهر را وارد نمایید." ForeColor="Red" ControlToValidate="txtCity">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>شهر</td>
                        <td>
                              <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" Width="300" Height="40">
                                </asp:DropDownList>
                          <%--  <asp:TextBox ID="txtProvince" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="استان را وارد نمایید." ForeColor="Red" ControlToValidate="txtProvince">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>کدپستی</td>
                        <td>

                            <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="کدپستی را وارد نمایید." ForeColor="Red" ControlToValidate="txtPostalCode">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="ثبت" OnClick="btnSave_Click" CssClass="btn btn-success" />
                            <asp:Button ID="btnCancel" runat="server" Text="بازگشت" OnClick="btnCancel_Click" CssClass="btn btn-warning" CausesValidation="false" />
                        </td>
                    </tr>

                </table>
            </div>
</asp:Content>
