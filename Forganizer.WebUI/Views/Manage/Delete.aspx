<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Forganizer.WebUI.Models.FileDeleteModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <% using (Html.BeginForm("Delete", "Manage")) {%>
            <fieldset>
                <% Html.RenderPartial("FormRoot", "delete files"); %>
            </fieldset>
            <fieldset>
                <legend><i class="arrow_blue"></i>tags</legend>
                <div> 
                    <label for="ExcludeTags">exclude tags</label>
                    <%= Html.TextBox("ExcludeTags") %>
                </div>
                <div>
                    <label for="IncludeTags">include only tags</label>
                    <%= Html.TextBox("IncludeTags") %>
                </div>
                <div class="align_center">
                    <input type="submit" value="run it" />
                </div>
            </fieldset>
        <% } %>
        <% if ((IEnumerable<string>)TempData["report"] != null){ %>
        <fieldset class="messages">
            <legend><i class="success"></i>report</legend>
            <% foreach(string message in (IEnumerable<string>)TempData["report"]) { %>
                <div><%= message %></div>
            <% } %>
        </fieldset>
        <% } %>
    </div>
    <div id="sidebar">
        <%= Html.ManageMenu(Url, ViewContext.RouteData.Values) %>
        <fieldset class="box">
            <legend>info</legend>
            <div>this is a very powerful tool, be careful with it</div>
            <div>leaving 'from folder' empty will recurse through all files in forganizer</div>
            <%= Html.ManageInfo() %>
            <div>if you delete a file, forganizer will retain it's tags; if you later restore it, the tags will still be there</div>
        </fieldset>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    delete files
</asp:Content>

