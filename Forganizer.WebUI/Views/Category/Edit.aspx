<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Forganizer.DomainModel.Entities.Category>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="content"><% using (Html.BeginForm("Edit", "Category", FormMethod.Post)) {%>
        <fieldset>
            <legend>
                <i class="edit"></i><%= Html.Encode(((string)ViewContext.RouteData.Values["action"] ==  "Edit" ? "edit" : "create")) %> category
            </legend>
            <%= Html.Hidden("Id") %>
            <% if (Model.IsValid) { %><div>
                <label for="Name">
                    <% if (TempData["duplicate"] != null) { %><span class="field-validation-error"><%= Html.Encode(TempData["duplicate"]) %></span><% } %>
                    <%= Html.ValidationMessage("Name") %>
                    name
                </label>
                <%= Html.TextBox("Name", null, new { @class = (TempData["duplicate"] == null ? "" : "input-validation-error") }) %>
            </div>
            <div>
                <label for="ExtensionString"><%= Html.ValidationMessage("ExtensionString") %>extensions</label>
                <%= Html.TextBox("ExtensionString") %>
            </div><div class="align_center">
                <input type="submit" value="save" />
            </div><% } %>
        </fieldset>
        <% } %>
    </div>
    <div id="sidebar">
        <fieldset class="box">
            <legend>actions</legend>
            <ul class="list_vertical">
                <% if((string)ViewContext.RouteData.Values["action"] ==  "Edit") { %>
                <li><%= Html.ActionLink("create category", "Create") %></li>
                <% } %>
                <li><%=Html.ActionLink("category list", "Index")%></li>
            </ul>
        </fieldset>
        <fieldset class="box">
            <legend>info</legend>
            <%= Html.ManageInfo()%>
        </fieldset>
    </div>
    <div class="clear"> </div>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    <%= Html.Encode(((string)ViewContext.RouteData.Values["action"] ==  "Edit" ? "edit" : "create")) %> category <%= Html.Encode(Model.Name) %>
</asp:Content>

