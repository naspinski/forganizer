<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Forganizer.WebUI.Models.TagEditModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <% using (Html.BeginForm("Tags", "Manage", new { tagEditType = Model.EditType.ToString() })) {%>
            <fieldset>
                <% Html.RenderPartial("FormRoot", Model.EditType.ToString().ToLower() + " tags"); %>
            </fieldset>
            <fieldset>
                <legend><i class="arrow_blue"></i>tag info</legend>
                <% if (Model.EditType != Forganizer.WebUI.Models.TagEditType.Add)
                   { %><div>
                    <label for="Replace"><%= Html.ValidationMessage("Replace")%><%= 
                        (Model.EditType == Forganizer.WebUI.Models.TagEditType.Delete ? "delete" : "replace")%> tag(s)</label>
                    <%= Html.TextBox("Replace")%>
                </div><% } %>
                <% if (Model.EditType != Forganizer.WebUI.Models.TagEditType.Delete)
                   { %><div>
                    <label for="Replace"><%= Html.ValidationMessage("With")%><%= 
                        (Model.EditType == Forganizer.WebUI.Models.TagEditType.Add ? "add" : "with")%> tag(s)</label>
                    <%= Html.TextBox("With")%>
                </div><% } %>
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
            <div>this is a very powerful tool, be careful with it</div>
            <div>leaving 'from folder' empty will recurse through all files in forganizer</div>
            <%= Html.ManageInfo() %>
        </fieldset>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    mass tag editing
</asp:Content>
