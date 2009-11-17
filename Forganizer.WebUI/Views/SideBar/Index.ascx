<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Forganizer.WebUI.Models.FileAndTagCollection>" %>
    <div id="sidebar" >
       <fieldset class="box">
            <legend>
                <span id="tagFilter">
                    <%= Html.ActionLink("or", "Index", "Search", 
                            new { extensions = Model.RouteData.Values["extensions"], tags = Model.RouteData.Values["tags"], page = Model.RouteData.Values["page"], tagAndOr = "Or" },
                            new { @class = "andOr" + (ViewData["tagAndOr"].ToString().Equals("Or") ? " selected" : "") })%>
                    <%= Html.ActionLink("and", "Index", "Search",
                            new { extensions = Model.RouteData.Values["extensions"], tags = Model.RouteData.Values["tags"], page = Model.RouteData.Values["page"], tagAndOr = "And" },
                            new { @class = "andOr" + (ViewData["tagAndOr"].ToString().Equals("And") ? " selected" : "") })%>
                </span>
                <i class="tag_green"></i>tags
                <% if (!string.IsNullOrEmpty((string)Model.RouteData.Values["tags"])) { %>
                <%= Html.ActionLink("[clear]", "Index", "Search",
                        new { extensions = Model.RouteData.Values["extensions"], tags = "", TagAndOr = ViewData["tagAndOr"] },
                        new { @class = "squaredLink" })%>
                <% } %>
            </legend>
            <%= Html.Cloud(Url,Model.Tags, Model.RouteData.Values["tags"].ToString(), Model.RouteData.Values["extensions"].ToString(), 
                (Forganizer.DomainModel.Entities.SearchType)ViewData["tagAndOr"], Forganizer.DomainModel.Entities.TagType.Tag) %>
        </fieldset>
        <fieldset class="box">
            <legend>
                <i class="tag_blue"></i>extensions
                <% if (!string.IsNullOrEmpty((string)Model.RouteData.Values["extensions"])) { %>
                <%= Html.ActionLink("[clear]", "Index", "Search",
                        new { extensions = "", tags = Model.RouteData.Values["tags"], TagAndOr = ViewData["tagAndOr"] },
                        new { @class = "squaredLink" })%>
                    <% } %>
            </legend>
            <%= Html.Cloud(Url,Model.Extensions, Model.RouteData.Values["tags"].ToString(), Model.RouteData.Values["extensions"].ToString(), 
                (Forganizer.DomainModel.Entities.SearchType)ViewData["tagAndOr"], Forganizer.DomainModel.Entities.TagType.Extension) %>
        </fieldset>
        <fieldset class="box">
            <legend><i class="tag_red"></i>categories</legend>
            <%= Html.Cloud(Url,Model.Categories, Model.RouteData.Values["tags"].ToString(), Model.RouteData.Values["extensions"].ToString(),
                (Forganizer.DomainModel.Entities.SearchType)ViewData["tagAndOr"], Forganizer.DomainModel.Entities.TagType.Category)%>
        </fieldset>
    </div>