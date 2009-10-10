<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="third left">
        <fieldset class="box">
            <legend>tags</legend>
            <% Html.RenderAction("TagCloud", "Files"); %>
        </fieldset>
    </div>
    
    <div class="third left">
        <fieldset class="box">
            <legend>extensions</legend>
            <% Html.RenderAction("ExtensionCloud", "Files"); %>
        </fieldset>
    </div>
    
    <div class="third left">
        <fieldset class="box">
            <legend>categories</legend>
        </fieldset>
    </div>
    
    <fieldset class="clear">
        <legend>recently modified</legend>
        <% Html.RenderAction("Recent", "Files", new { count = 20 }); %>
    </fieldset>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    forganizer
</asp:Content>
