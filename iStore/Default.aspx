﻿<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="iStore._Default" EnableEventValidation="false" Title="Home | Marvel Worldwide" %>

<%@Register TagPrefix="iS" TagName="AddToCart" Src="~/Modules/Controls/AddToCard.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <h1><a href="<%= iStore.Site.SiteUrl %>NewDeals/">New Deals</a></h1>    
    </div>
    <div class="last_goods">
        <%counter=0; %>
        <asp:Repeater runat="server" id="rpt">
        <ItemTemplate>
            <div class="goods_item">
                <div class="img_field02">
                    <img  src="/Content/Products/Preview/<%=GetPreviewUrl() %>" />
                </div>
                <div class="price_name">
                    <span class="name_product"><%#Eval("Name")%></span>
                    <span class="price_product">$<%#Eval("Price")%></span> <br />
                    <a class="more_details" href="<%= iStore.Site.SiteUrl %>Products/?pid=<%#Eval("ProductID")%>">
                        more details
                    </a>
                </div>
                <div class="cart_button">
                    <iS:AddToCart ID="AddToCart1" ProductId='<%#Eval("ProductID") %>' runat="server" IsCounterVisible="false"  />
                </div>
            </div>
            <% counter++; %>
        </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>