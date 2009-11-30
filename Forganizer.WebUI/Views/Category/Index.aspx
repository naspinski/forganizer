<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Forganizer.DomainModel.Entities.Category>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="content">
        <fieldset class="clear">
            <legend><i class="arrow_blue"></i>categories</legend>
            <% using (Html.BeginForm("Submit", "Category")) { %>
                <ul class="list_vertical">
                    <li class="title">
                        <span class="name"></span>
                        <span class="actions"> </span>
                        <span class="tags title">extensions<input type="submit" value="+extensions" /></span>
                    </li>
                    <% int count = 1;  foreach (var category in Model) { %>
                    <li>
                        <a class="name" title="<%= Html.Encode(category.Name) %>">
                            <i class="category"></i>
                            <%= Html.Encode(category.Name) %>
                        </a>    
                        <span class="actions">
                            <%= Html.ActionLink(" ", "Delete", new { category.Id }, new { @class = "delete", title = "delete" })%>
                            <%= Html.ActionLink(" ", "Edit", new { category.Id }, new { @class = "edit", title = "edit" })%>
                        </span>
                        <span class="tags">
                            <span class="add_tag"><%= Html.TextBox("AddExtensionsTo" + category.Id, null, new { tabindex = count++ })%></span>
                            <% foreach (var extension in category.Extensions) { %>
                            <span class="tag">
                                <%= Html.Encode(extension) %><%= Html.ActionLink("[delete]", "DeleteExtension", 
                                    new { category.Id, extension }, 
                                    new { title = "delete" })%></span>
                            <% } %>
                        </span>
                    </li>
                    <% } %>
                </ul>
            <% } %>
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

