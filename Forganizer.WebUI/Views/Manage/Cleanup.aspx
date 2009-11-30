<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<string>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <% using (Html.BeginForm("Cleanup", "Manage")) {%>
            <fieldset>
                <legend><i class="arrow_blue"></i>file cleanup</legend>
                <div class="align_center">
                    <br />
                    <input type="submit" value="clean up missing files" />
                </div>
            </fieldset>
        <% } %>
        <% if (Model.Count() > 0){ %>
        <fieldset class="messages">
            <legend><i class="success"></i>report</legend>
            <% foreach(string message in Model) { %>
                <div><%= Html.Encode(message) %></div>
            <% } %>
        </fieldset>
        <% } %>
    </div>
    <div id="sidebar">
        <%= Html.ManageMenu(Url, ViewContext.RouteData.Values) %>
        <fieldset class="box">
            <legend>info</legend>
            <div>cleans up files that are in the system, but no longer present on the network</div>
            <div>
                if a share/folder is unavailable, this will delete all the files but all tags will be saved; just restore the share/folder when it is 
                back up by going to the <%= Html.ActionLink("add files", "AddFolder") %> page
            </div>
        </fieldset>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    file cleanup
</asp:Content>
