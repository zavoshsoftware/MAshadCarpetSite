<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="RecycledProducts.aspx.cs" Inherits="MashadCarpet.Admin.RecycledProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvSetting" runat="server">
        <asp:View ID="vwList" runat="server">
            <%--<asp:Button ID="btnAdd" runat="server" Text="افزودن" CssClass="btn btn-success" OnClick="btnAdd_Click" />--%>
            <asp:Button ID="btnReturn" runat="server" Text="بازگشت" CssClass="btn btn-warning" OnClick="btnReturn_Click" CausesValidation="false" />
            <br />
            <br />
            <asp:Panel ID="pnlEmptyForm" runat="server" Visible="false">
                <p class="alert alert-danger pnlEmptyForm">اطلاعاتی جهت نمایش موجود نمی باشد.</p>
            </asp:Panel>
            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grdTable_RowCommand" Width="100%" AllowPaging="True" PageSize="15" OnPageIndexChanging="grdTable_PageIndexChanging">

                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="ProductTitle" HeaderText="عنوان" />

                    <asp:BoundField DataField="EN_ProductTitle" HeaderText="عنوان انگلیسی" />

                    <asp:BoundField DataField="ProductName" HeaderText="عنوان سیستمی" />
              <%--      <asp:TemplateField HeaderText="تصویر">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ProductImage","~/Uploads/Products/{0}") %>' Width="100px" Height="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="Frame" HeaderText="تعداد رنگ" />
                    <asp:BoundField DataField="DesignNo" HeaderText="شماره نقشه" />
                    <%--<asp:CheckBoxField DataField="IsAvailable" HeaderText="موجود؟" />--%>
                    <asp:CheckBoxField DataField="IsEspecial" HeaderText="پیشنهاد ویژه؟" />
                           <asp:CheckBoxField DataField="IsMostSell" HeaderText="پرفروش ترین؟" />
                    <asp:BoundField DataField="Collection" HeaderText="خانواده" />
                    <%--<asp:BoundField DataField="EN_Collection" HeaderText="خانواده انگلیسی" />--%>
                    <asp:BoundField DataField="PileType" HeaderText="کوالیته" />
                    <%--<asp:BoundField DataField="EN_PileType" HeaderText="کوالیته انگلیسی" />--%>
                    <asp:BoundField DataField="Reeds" HeaderText="شانه" />
                    <asp:BoundField DataField="Shots" HeaderText="تراکم" />
                    <asp:BoundField DataField="Points" HeaderText="سرنخ" />
                    <asp:BoundField DataField="Knots" HeaderText="گره" />
                    <asp:TemplateField HeaderText="توضیحات">

                        <ItemTemplate>
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbColors" runat="server" CommandArgument='<%# Eval("ProductID") %>' CommandName="Details" CssClass="lb">مشاهده جزئیات</asp:LinkButton>
                            <br />
                    
                            <i class="fa fa-times"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("ProductID") %>' CommandName="Restore" CssClass="lb">بازیابی</asp:LinkButton>
                          
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
                        <td>عنوان:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>

                            
                        </td>
                    </tr>

                    <tr>
                        <td>عنوان انگلیسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtENTitle" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>

                            
                        </td>
                    </tr>

                      <tr>
                        <td>عنوان روسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRusTitle" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>

                           
                        </td>
                    </tr>

                      <tr>
                        <td>عنوان چینی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtChinaTitle" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>

                           
                        </td>
                    </tr>


                    <tr>
                        <td>عنوان سیستمی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>

             
                     <tr>
                        <td>پیشنهاد ویژه؟:
                        </td>
                        <td>
                            <asp:CheckBox ID="cbIsEspecial" runat="server" Enabled="false" />
                        </td>
                    </tr>
                     <tr>
                        <td>پرفروش ترین؟:
                        </td>
                        <td>
                            <asp:CheckBox ID="chkMostSell" runat="server" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>گروه محصول:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProductGroup" runat="server" CssClass="form-control" Width="300px" Enabled="false"> </asp:DropDownList>

                           
                        </td>
                    </tr>
                    <tr>
                        <td>شماره نقشه:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDesignNO" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                           
                            
                             </td>
                    </tr>

                    <tr>
                        <td>تعداد رنگ:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFrame" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                           
                              </td>
                    </tr>
                    <tr>
                        <td>خانواده:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCollection" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>خانواده انگلیسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEN_Collection" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>

                     <tr>
                        <td>خانواده روسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRus_Collection" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>

                     <tr>
                        <td>خانواده چینی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtChina_Collection" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>


                    <tr>
                        <td>کوالیته:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPileType" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>کوالیته انگلیسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEN_PileType" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>

                        <tr>
                        <td>کوالیته روسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRus_PileType" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>

                        <tr>
                        <td>کوالیته چینی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtChina_PileType" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>شانه:
                        </td>
                        <td>
                            <asp:TextBox ID="txtReeds" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>تراکم:
                        </td>
                        <td>
                            <asp:TextBox ID="txtShots" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>

                    <tr>
                        <td>سرنخ:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPoints" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>گره:
                        </td>
                        <td>
                            <asp:TextBox ID="txtKnots" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="بازیابی" CssClass="btn btn-success" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="انصراف" CssClass="btn btn-danger" OnClick="btnCancel_Click1" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
      

    </asp:MultiView>
</asp:Content>
