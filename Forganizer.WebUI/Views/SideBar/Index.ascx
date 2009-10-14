<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Forganizer.DomainModel.Entities.FileAndTagCollection>" %>

    <div id="sidebar" >
       <fieldset class="box">
            <legend>
                <span id="tagFilter">
                    <%= Html.ActionLink("or", "Index", "Search", 
                            new { extensions = ViewData["extensions"], tags = ViewData["tags"], TagAndOr = "Or" },
                            new { @class = "andOr" + (ViewData["tagAndOr"].ToString().Equals("Or") ? " selected" : "") })%>
                    <%= Html.ActionLink("and", "Index", "Search", 
                            new { extensions = ViewData["extensions"], tags = ViewData["tags"], TagAndOr = "And" },
                            new { @class = "andOr" + (ViewData["tagAndOr"].ToString().Equals("And") ? " selected" : "") })%>
                </span>
                <i class="tag_green"></i>tags
                <% if (!string.IsNullOrEmpty((string)ViewData["tags"])) { %>
                <%= Html.ActionLink("[clear]", "Index", "Search",
                        new { extensions = ViewData["extensions"], tags = "", TagAndOr = ViewData["tagAndOr"] },
                        new { @class = "squaredLink" })%>
                <% } %>
            </legend>
            <div class="active">
                <% Html.RenderAction("Container", "SideBar", 
                       new { tagObjects = Model.Tags, active = true, tagType = Forganizer.DomainModel.Entities.TagType.Tag }); %>
            </div>
            <div class="inactive">
                <% Html.RenderAction("Container", "SideBar", 
                       new { tagObjects = Model.Tags, active = false, tagType = Forganizer.DomainModel.Entities.TagType.Tag }); %>
            </div>
        </fieldset>
        <fieldset class="box">
            <legend>
                <i class="tag_blue"></i>extensions
                <% if (!string.IsNullOrEmpty((string)ViewData["extensions"])) { %>
                <%= Html.ActionLink("[clear]", "Index", "Search",
                        new { extensions = "", tags = ViewData["tags"], TagAndOr = ViewData["tagAndOr"] },
                        new { @class = "squaredLink" })%>
                    <% } %>
            </legend>
            <div class="active">
                <% Html.RenderAction("Container", "SideBar", 
                       new { tagObjects = Model.Extensions, active = true, tagType = Forganizer.DomainModel.Entities.TagType.Extension }); %>
            </div>
            <div class="inactive">
                <% Html.RenderAction("Container", "SideBar", 
                       new { tagObjects = Model.Extensions, active = false, tagType = Forganizer.DomainModel.Entities.TagType.Extension }); %>
            </div>    
        </fieldset>
        <fieldset class="box">
            <legend><i class="tag_red"></i>categories</legend>
            <div class="active">
                <% Html.RenderAction("Container", "SideBar", 
                       new { tagObjects = Model.Categories, active = true, tagType = Forganizer.DomainModel.Entities.TagType.Category }); %>
            </div>
            <div class="inactive">
                <% Html.RenderAction("Container", "SideBar", 
                       new { tagObjects = Model.Categories, active = false, tagType = Forganizer.DomainModel.Entities.TagType.Category }); %>
            </div>    
        </fieldset>
    </div>