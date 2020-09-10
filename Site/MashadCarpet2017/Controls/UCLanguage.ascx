<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCLanguage.ascx.cs" Inherits="MashadCarpet.Controls.UCLanguage" %>
  <a title="Language" class="dropdown-toggle" data-toggle="dropdown">
      <span class="long-name">     <asp:Image ID="imgCurrentLang" runat="server" /></span><span class="short-name">
       
          
                                          </span><span class="dropdown-arrow"></span></a>

                                <ul class="dropdown-menu">
                                    <li> 

                                        <asp:LinkButton ID="lbEng" runat="server" OnClick="lbEnglish_Click">
                                             <img src="/images/flags/eng.jpg" alt="English" style="float:right;margin-left:3px;">
                                        <span class="long-name" style="float:right;">English</span><span class="short-name">English</span>
                                       
                                       </asp:LinkButton></li>
                                    <li>   <asp:LinkButton ID="lbFa" runat="server" OnClick="lbFa_Click">
                                          <img src="/images/flags/ir.jpg" alt="Iran" style="float:right;margin-left:3px;"> 
                                        <span class="long-name" style="float:right;">فارسی</span><span class="short-name">فارسی</span>
                                      </asp:LinkButton></li>
                                                          
                                     


                                     <li><asp:LinkButton ID="lbChine" runat="server" OnClick="lbChine_Click">
                                         <img src="/images/flags/chnFlag.jpg" style="float:right;margin-left:3px;" alt="French">
                                        <span class="long-name" style="float:right;">中文</span><span class="short-name">中文</span>
                                        </asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbRus" runat="server" OnClick="lbRus_Click">
                                        <img src="/images/flags/RussiaFlag.jpg" alt="German" style="float:right;margin-left:3px;">
                                        <span class="long-name" style="float:right;">русский</span><span class="short-name">русский</span>
                                        </asp:LinkButton></li>
                                </ul>