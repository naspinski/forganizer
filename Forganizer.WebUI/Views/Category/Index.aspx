<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Forganizer.DomainModel.Entities.Category>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="content">
        <fieldset class="clear">
            <legend><i class="arrow_blue"></i>categories</legend>
            <ul class="list_vertical">
                <li class="title">
                    <span class="name"></span>
                    <span class="tags title">extensions</span>
                </li>
                <% foreach(var category in Model) { %>
                <li>
                    <a class="name" title="<%= category.Name %>">
                        <i class="category"></i>
                        <%= category.Name%>
                    </a>    
                    <span class="tags"><% foreach (var extension in category.Extensions) { %><%= extension %> <% } %></span>
                    <span class="actions">
                        <%= Html.ActionLink(" ", "Delete", new { category.Id }, new { @class = "delete", title = "delete" })%>
                        <%= Html.ActionLink(" ", "Edit", new { category.Id }, new { @class = "edit", title = "edit" })%>
                    </span>
                </li>
                <% } %>
            </ul>
        </fieldset>
    </div>
    <div id="sidebar">
        <fieldset class="box">
            <legend>actions</legend>
            <ul class="list_vertical">
                <li><%= Html.ActionLink("create category", "Create") %></li>
            </ul>
        </fieldset>
    </div>
    <div class="clear"> </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    categories
</asp:Content>

