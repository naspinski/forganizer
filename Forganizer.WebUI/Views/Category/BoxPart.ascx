<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Forganizer.DomainModel.Entities.Tag>>" %>
<%@ Import Namespace="Forganizer.DomainModel.Extensions" %>

            <ul class="list_horizontal">
                <% foreach (var category in Model.Where(x => x.QueryStringTags.Count() > 0)) { %>
                <li>
                    <%= Html.ActionLink(category.Name, "Index", "Home",
                        new
                        {
                            extensions = ViewData["extensions"].AddToSearch(category.QueryString),
                            tags = ViewData["tags"],
                            TagAndOr = ViewData["tagAndOr"]
                        },
                        new { style = "font-size:" + category.Size + "em;" })%>
                    <% if (ViewData["Extensions"].ToString().SplitTags().Intersect(category.QueryStringTags).Count() > 0) { %>
                        <%= Html.ActionLink("x", "Index", "Home",  
                            new
                            {
                                extensions = ViewData["extensions"].RemoveFromSearch(category.QueryString),
                                tags = ViewData["tags"],
                                TagAndOr = ViewData["tagAndOr"] 
                            },
                            new { @class = "x" })%>
                        <% } %>
                </li>
                <% } %>
            </ul>