<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="content">
        <fieldset>
            <legend><i class="error"></i>page not found (404)</legend>
            <div>
                what the hell are you doing here? <br />
                if you clicked on a link to get here, please open a new <a href="http://forganizer.codeplex.com/WorkItem/List.aspx">work item</a> telling us which link 
            </div>
            <div>
                <label>invalid url</label>
                <%= Html.Encode(ViewData["url"]) %>
            </div>
        </fieldset>    
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    page not found
</asp:Content>
