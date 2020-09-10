<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SliderTextSetting.aspx.cs" Inherits="MashadCarpet.Admin.SliderTextSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jscolor/jscolor.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:MultiView ID="mvSetting" runat="server">
        <asp:View ID="vwList" runat="server">
            <asp:Button ID="btnAdd" runat="server" Text="افزودن" CssClass="btn btn-success" OnClick="btnAdd_Click" />
            <asp:Button ID="btnReturn" runat="server" Text="بازگشت" CssClass="btn btn-warning" OnClick="btnReturn_Click" CausesValidation="false" />
            <br />
            <br />
            <asp:Panel ID="pnlEmptyForm" runat="server" Visible="false">
                <p class="alert alert-danger pnlEmptyForm">اطلاعاتی جهت نمایش موجود نمی باشد.</p>
            </asp:Panel>
            <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success pnlEmptyForm">
                اطلاعات با موفقیت تغییر یافت.
            </asp:Panel>

            <asp:GridView ID="grdTable" runat="server" OnDataBound="grdTable_DataBound" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grdTable_RowCommand" Width="100%">

                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="مکان نمایش">

                        <ItemTemplate>
                            <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("SliderTextID") %>' />
                            <asp:Label ID="lblx" runat="server" Text=""></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ترتیب نمایش">

                        <ItemTemplate>

                            <asp:Label ID="lbly" runat="server" Text=""></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:BoundField DataField="Text" HeaderText="متن" />
                    <asp:BoundField DataField="EN_Text" HeaderText="متن انگلیسی" />
                    <asp:BoundField DataField="Rus_Text" HeaderText="متن روسی" />
                    <asp:BoundField DataField="China_Text" HeaderText="متن چینی" />

                    <%--  <asp:CheckBoxField DataField="IsLink" HeaderText="لینک می باشد؟" />--%>

                    <asp:TemplateField HeaderText="توضیحات">

                        <ItemTemplate>
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("SliderTextID") %>' CommandName="DoEdit" CssClass="lb">ویرایش</asp:LinkButton>
                            <br />
                            <i class="fa fa-times"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("SliderTextID") %>' CommandName="DoDelete" CssClass="lb">حذف</asp:LinkButton>
                            <br />
                        </ItemTemplate>

                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

            </asp:GridView>

        </asp:View>
        <asp:View ID="vwEdit" runat="server">
            <div class="ad-div">

                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />
                <table>
                    <tr>
                        <td>مختصات X:
                        </td>
                        <td>
                      
                            <asp:TextBox ID="txtX" runat="server"></asp:TextBox>


                        </td>
                    </tr>



                    <tr>
                        <td>مختصات Y:
                        </td>
                        <td>
                            <asp:TextBox ID="txtY" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>ورود از جهت:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEnterSide" runat="server">
                                <asp:ListItem Value="0">جهت</asp:ListItem>
                                <asp:ListItem Value="1">از بالا</asp:ListItem>
                                <asp:ListItem Value="2">از راست</asp:ListItem>
                                <asp:ListItem Value="3">از چپ</asp:ListItem>
                                <asp:ListItem Value="4">از پایین</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CustomValidator ID="cvEnterSide" runat="server" ErrorMessage="جهت ورود متن را وارد نمایید" OnServerValidate="cvEnterSide_ServerValidate" Display="None"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>سرعت ورود:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEnterSpeed" runat="server">
                                <asp:ListItem Value="0">سرعت</asp:ListItem>
                                <asp:ListItem Value="1">کم</asp:ListItem>
                                <asp:ListItem Value="2">زیاد</asp:ListItem>
                            </asp:DropDownList>

                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="سرعت ورود متن را وارد نمایید" OnServerValidate="CustomValidator1_ServerValidate" Display="None"></asp:CustomValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>خروج از جهت:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlExitSide" runat="server">
                                <asp:ListItem Value="0">جهت</asp:ListItem>
                                <asp:ListItem Value="1">از بالا</asp:ListItem>
                                <asp:ListItem Value="2">از راست</asp:ListItem>
                                <asp:ListItem Value="3">از چپ</asp:ListItem>
                                <asp:ListItem Value="4">از پایین</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="جهت خروج متن را وارد نمایید" OnServerValidate="CustomValidator2_ServerValidate" Display="None"></asp:CustomValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>سرعت خروج:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlExitSpeed" runat="server">
                                <asp:ListItem Value="0">سرعت</asp:ListItem>
                                <asp:ListItem Value="1">کم</asp:ListItem>
                                <asp:ListItem Value="2">زیاد</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="سرعت خروج متن را وارد نمایید" OnServerValidate="CustomValidator3_ServerValidate" Display="None"></asp:CustomValidator>

                        </td>
                    </tr>

                    <tr>
                        <td>سرعت:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSpeed" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>

                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server"
                                ErrorMessage="سرعت را وارد نمایید." ControlToValidate="txtSpeed">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                      <tr>
                        <td>اولویت:
                        </td>
                        <td>

                            <asp:TextBox ID="txtStart" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>

                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server"
                                ErrorMessage="زمان شروع را وارد نمایید." ControlToValidate="txtStart">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>اندازه فونت:
                        </td>
                        <td>                      
                            <asp:TextBox ID="txtFont" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>کد رنگ:
                        </td>
                        <td>
                            #<asp:TextBox ID="txtColor" runat="server" CssClass="form-control color" Width="300px"></asp:TextBox>
                             
                        </td>
                    </tr>

                  

                    <%-- <tr>
                        <td>لینک می باشد :
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsLink" Text="لینک؟" runat="server" />
                        </td>
                    </tr>
                

                    <tr>
                        <td>آدرس لینک</td>
                        <td>
                             <asp:TextBox ID="txtLinkAddress" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>متن</td>
                        <td>
                            <asp:TextBox ID="txtText" runat="server" CssClass="form-control" Width="300px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td> متن انگلیسی</td>
                        <td>
                            <asp:TextBox ID="txtEN_Text" runat="server" CssClass="form-control" Width="300px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td>متن روسی</td>
                        <td>
                            <asp:TextBox ID="txtRus_Text" runat="server" CssClass="form-control" Width="300px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td>متن چینی</td>
                        <td>
                            <asp:TextBox ID="txtChina_Text" runat="server" CssClass="form-control" Width="300px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="ثبت" CssClass="btn btn-success" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="انصراف" CssClass="btn btn-danger" OnClick="btnCancel_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View ID="vwDelete" runat="server">
            آیا مایل به حذف 
            <asp:Label ID="lblDelete" runat="server" Text="Label"></asp:Label>
            هستید؟
            <asp:Button ID="btnYes" runat="server" Text="بلی" OnClick="btnYes_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnNo" runat="server" Text="خیر" OnClick="btnNo_Click" CssClass="btn btn-warning" />

        </asp:View>
    </asp:MultiView>
</asp:Content>
