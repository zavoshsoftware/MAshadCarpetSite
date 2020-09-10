<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="TicketResponseSetting.aspx.cs" Inherits="MashadCarpet.Admin.TicketResponseSetting" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="pnlTicket" runat="server" CssClass="pnl-default">
        <div class="div-tr HeaderTitle">
            مشخصات تیکت
        </div>
        <div class="div-tr">
            <div class="div-td-right lbl-default">نام کاربر</div>
            <div class="div-td">
                <asp:Label ID="lblUserName" runat="server" Text="" CssClass="lbl-default"></asp:Label>
            </div>
        </div>

        <%-- <div class="div-tr">
            <div class="div-td-right lbl-default">گروه تیکت</div>
            <div class="div-td">
                <asp:Label ID="lblTicketGroup" runat="server" Text="" CssClass="lbl-default"></asp:Label>

            </div>
        </div>--%>

        <div class="div-tr">
            <div class="div-td-right lbl-default">عنوان تیکت</div>
            <div class="div-td">
                <asp:Label ID="lblTicketSubject" runat="server" Text="" CssClass="lbl-default"></asp:Label>

            </div>
        </div>

        <div class="div-tr">
            <div class="div-td-right lbl-default">تاریخ</div>
            <div class="div-td">
                <asp:Label ID="lblTicketDate" runat="server" Text="" CssClass="lbl-default"></asp:Label>

            </div>
        </div>

        <div class="div-tr">
            <div class="div-td-right lbl-default">پیغام</div>
            <div class="div-td">
                <asp:Label ID="lblTicketMessage" runat="server" Text="" CssClass="lbl-default"></asp:Label>

            </div>
        </div>

        <div class="div-tr">
            <div class="div-td-right lbl-default">حالت</div>
            <div class="div-td">
                <asp:Label ID="lblStatus" runat="server" Text="" CssClass="lbl-default"></asp:Label>

            </div>
        </div>

        <p style="clear: both;"></p>
    </asp:Panel>
    <asp:MultiView ID="mvSetting" runat="server">
        <asp:View ID="vwList" runat="server">
            <asp:Panel ID="pnlAnswers" runat="server" CssClass="pnl-default">
                <div class="div-tr HeaderTitle">
                    پاسخ ها
                </div>
                <asp:Panel ID="pnlEmptyForm" runat="server" Visible="false">
                    <p class="alert alert-danger pnlEmptyForm">اطلاعاتی جهت نمایش موجود نمی باشد.</p>
                </asp:Panel>
                <asp:Repeater ID="rptAnswers" runat="server" OnItemCommand="rptAnswers_ItemCommand">
                    <ItemTemplate>

                        <div class="div-tr Header2">
                            نام کاربر:
              <%# Eval("Name") %>
                    &nbsp &nbsp &nbsp &nbsp
                    تاریخ پاسخ:
                <%# Eval("ResponseDate") %>

                    &nbsp &nbsp &nbsp &nbsp
                    <div class="pull-left">

                        <i class="fa fa-eye"></i>
                        <asp:LinkButton ID="lbShow" runat="server" CommandArgument='<%# Eval("TicketResponseID") %>' CommandName="Show" CssClass="lb">ادامه</asp:LinkButton>
                        &nbsp &nbsp &nbsp &nbsp

                        <i class="fa fa-edit"></i>
                        <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("TicketResponseID") %>' CommandName="DoEdit" CssClass="lb">ویرایش</asp:LinkButton>

                        &nbsp &nbsp &nbsp &nbsp

                     <i class="fa fa-times"></i>
                        <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("TicketResponseID") %>' CommandName="DoDelete" CssClass="lb">حذف</asp:LinkButton>

                    </div>
                        </div>


                        <div class="Text-Msg">
                            <%# Eval("ResponseText") %>
                        </div>
                        <br />
                        <br />
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Button ID="btnAnswer" runat="server" Text="پاسخ" CssClass="btn btn-success" OnClick="btnAnswer_Click" />
                <asp:Button ID="btnReturn" runat="server" Text="بازگشت" CssClass="btn btn-warning" OnClick="btnReturn_Click" CausesValidation="false" />
            </asp:Panel>
        </asp:View>

        <br />
        <asp:View ID="vwEdit" runat="server">
            <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
            </telerik:RadStyleSheetManager>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            </telerik:RadAjaxManager>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:Panel ID="pnlSubmitAnswer" runat="server" CssClass="pnl-default" Visible="false">
                <div class="div-tr">
                    <div class="div-tr HeaderTitle">
                        پاسخ 
                    </div>
                    <table>
                        <tr>
                            <td>پاسخ</td>
                            <td>
                                <telerik:RadEditor ID="reResponseText" runat="server"></telerik:RadEditor>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="ثبت" CssClass="btn btn-success" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>

                </div>

            </asp:Panel>
        </asp:View>


        <asp:View ID="vwShow" runat="server">

            <asp:Panel ID="Panel1" runat="server" CssClass="pnl-default">
                <div class="div-tr">
                    <div class="div-tr HeaderTitle">
                        پاسخ 
                    </div>


                    <div class="div-tr Header2">
                        نام کاربر:
                            <asp:Label ID="lblUserAnswerName" runat="server" Text=""></asp:Label>
                        &nbsp &nbsp &nbsp &nbsp
                        تاریخ پاسخ:
                              <asp:Label ID="lblAnswerDate" runat="server" Text=""></asp:Label>
                        &nbsp &nbsp &nbsp &nbsp
                        <div class="pull-left">
                            <i class="fa fa-reply"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CssClass="lb" OnClick="lbEdit_Click" CausesValidation="false">بازگشت</asp:LinkButton>
                        </div>

                    </div>


                    <div class="Text-Msg">
                        <asp:Label ID="lblAnswer" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </asp:Panel>

        </asp:View>
    </asp:MultiView>
</asp:Content>
