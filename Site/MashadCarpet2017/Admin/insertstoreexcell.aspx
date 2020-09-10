<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="insertstoreexcell.aspx.cs" Inherits="MashadCarpet.Admin.insertstoreexcell" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <asp:FileUpload ID="FileUpload1" runat="server" />       
    <asp:Button ID="Button1" runat="server" Text="Import" OnClick="Button1_Click" CssClass="btn btn-success" />

</asp:Content>
