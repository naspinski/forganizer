<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Forganizer.DomainModel.Entities.Tag>>" %>
<%@ Import Namespace="Forganizer.DomainModel.Extensions" %>

                <ul class="list_horizontal">
                    <% foreach(var tag in Model) { %>
                    <li>
                        <%= Html.ActionLink(tag.Name, "Index", "Search",  
                            new 
                            { 
                                tags = ((Forganizer.DomainModel.Entities.TagType)ViewData["tagType"] == Forganizer.DomainModel.Entities.TagType.Tag
                                    ? ViewData["tags"].AddToSearch(tag.QueryString)
                                    : ViewData["tags"]),
                                extensions = ((Forganizer.DomainModel.Entities.TagType)ViewData["tagType"] == Forganizer.DomainModel.Entities.TagType.Tag
                                    ? ViewData["extensions"]
                                    : ViewData["extensions"].AddToSearch(tag.QueryString)),
                                TagAndOr = ViewData["tagAndOr"]
                            }, 
                            new { style = "font-size:" + tag.Size.ToString("0.0") + "em;" })%>
                        <% if(
                                ((Forganizer.DomainModel.Entities.TagType)ViewData["tagType"] == Forganizer.DomainModel.Entities.TagType.Tag && 
                                    ViewData["tags"].ToString().SplitTags().Contains(tag.Name)) ||
                                ((Forganizer.DomainModel.Entities.TagType)ViewData["tagType"] == Forganizer.DomainModel.Entities.TagType.Extension && 
                                    ViewData["extensions"].ToString().SplitTags().Contains(tag.Name)) ||
                                ((Forganizer.DomainModel.Entities.TagType)ViewData["tagType"] ==Forganizer.DomainModel.Entities.TagType.Category &&
                                    ViewData["extensions"].ToString().SplitTags().Intersect(tag.QueryStringTags).Count() > 0)
                               )
                           { %>
                        <%= Html.ActionLink("x", "Index", "Search",  
                            new
                            {
                                tags = ((Forganizer.DomainModel.Entities.TagType)ViewData["tagType"] == Forganizer.DomainModel.Entities.TagType.Tag
                                    ? ViewData["tags"].RemoveFromSearch(tag.QueryString)
                                    : ViewData["tags"]),
                                extensions = ((Forganizer.DomainModel.Entities.TagType)ViewData["tagType"] == Forganizer.DomainModel.Entities.TagType.Tag
                                    ? ViewData["extensions"]
                                    : ViewData["extensions"].RemoveFromSearch(tag.QueryString)),
                                TagAndOr = ViewData["tagAndOr"]
                            },
                            new { @class = "x" })%>
                        <% } %>
                    </li>
                    <% } %>
                </ul>