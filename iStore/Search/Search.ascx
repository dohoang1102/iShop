﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Search.ascx.cs" Inherits="iStore.Search.Search" %>

<script type="text/javascript">
    $(document).ready(function () {
        $("#txtSearch").focus(function () {
            if ($(this).val() == "Search request here") {
                $(this).val("");
            }
        });
        $("#txtSearch").blur(function () {
            if ($(this).val() == "") {
                $(this).val("Search request here");
            }
        });
    });
</script>

<span class="paraq_search">
    <asp:DropDownList style="display:none;" runat="server" ID="ddlSearchType">
        <asp:ListItem Value="0">By product</asp:ListItem>
        <asp:ListItem Value="1">By category</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox runat="server" ID="txtSearch" ClientIDMode="Static">Search request here</asp:TextBox>
    <asp:Button runat="server" CssClass="go_btn" ID="btnSearch" Text="" OnClick="SearchText" />
</span>
