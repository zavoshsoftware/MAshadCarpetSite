<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UploadColorSizeProduct.aspx.cs" Inherits="MashadCarpet.Admin.UploadColorSizeProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="false">
    تغییرات با موفقیت انجام شد.
 </asp:Panel>
    <asp:Panel ID="pnlNoProductGroupError" runat="server" CssClass="alert alert-danger" Visible="false">
        گروه محصول 
        <asp:Label ID="lblProGroup" runat="server" Text=""></asp:Label>
         در وب سایت به ثبت نرسیده است.
    </asp:Panel>
    <asp:Panel ID="pnlColorandsizeError" runat="server" CssClass="alert alert-danger" Visible="false">
        <asp:Label ID="lblColor" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblsize" runat="server" Text="" Visible="false"></asp:Label>
    </asp:Panel>
   برای دانلود فایل نمونه 
    <a href="/BulkFolder/mashadcarpetProduct2.xls">
    اینجا</a>را کلیک کنید
     <asp:FileUpload ID="FileUpload1" runat="server" />       
    <asp:Button ID="Button1" runat="server" Text="Import" OnClick="btnUpload_Click" CssClass="btn btn-success" />
</asp:Content>
