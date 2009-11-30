<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Forganizer.DomainModel.Entities.Folder>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <div id="content">
        <% using (Html.BeginForm("Folder", "Manage")) {%>
        <fieldset>
            <% Html.RenderPartial("FormRoot", "add files"); %>
            <%= Html.Hidden("Id") %>
            <div class="align_center">
                <input type="submit" value="run it" />
            </div>
        </fieldset>
        <% } %>
        <% if ((IEnumerable<string>)TempData["report"] != null){ %>
        <fieldset class="messages">
            <legend><i class="success"></i>report</legend>
            <% foreach(string message in (IEnumerable<string>)TempData["report"]) { %>
                <div><%= Html.Encode(message) %></div>
            <% } %>
        </fieldset>
        <% } %>
    </div>
    <div id="sidebar">
        <%= Html.ManageMenu(Url, ViewContext.RouteData.Values) %>
        <fieldset class="box">
            <legend>info</legend>
            <%= Html.ManageInfo() %>
        </fieldset>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    add files
</asp:Content>