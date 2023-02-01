using System.Globalization;
using System.Xml;
using Application.Features.Notes.Queries.GetNotesByPage;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class RssController : Controller
{
    private readonly IQueryHandler<GetNotesByPageQuery, GetNotesByPageQueryResult> _getNotesByPage;

    public RssController(IQueryHandler<GetNotesByPageQuery, GetNotesByPageQueryResult> getNotesByPage)
    {
        _getNotesByPage = getNotesByPage;
    }

    [Route("/rss")]
    public async Task<ContentResult> Rss()
    {
        var query = new GetNotesByPageQuery { Page = 0 };
        var result = await _getNotesByPage.Handle(query, CancellationToken.None);

        var lastTenNotes = result.Notes;
        var xml = BuildXmlFeed($"https://demns.space", lastTenNotes); // TODO: host variable in appsettings.json
        return new ContentResult
        {
            ContentType = "application/xml",
            Content = xml,
            StatusCode = 200
        };
    }


    private string BuildXmlFeed(string url, IList<GetNotesByPageQueryResultItem> notes)
    {
        var parent = new StringWriter();
        using (var writer = new XmlTextWriter(parent))
        {
            writer.WriteStartElement("rss");
            writer.WriteAttributeString("version", "2.0");
            writer.WriteAttributeString("xmlns:atom", "http://www.w3.org/2005/Atom");

            writer.WriteStartElement("channel");

            writer.WriteElementString("title", "Demn's blog");
            writer.WriteElementString("link", url);
            writer.WriteElementString("ttl", "60");

            writer.WriteStartElement("atom:link");
            writer.WriteAttributeString("href", url);
            writer.WriteAttributeString("rel", "self");
            writer.WriteAttributeString("type", "application/rss+xml");
            writer.WriteEndElement();

            foreach (var note in notes)
            {
                writer.WriteStartElement("item");

                writer.WriteElementString("title", note.Title);
                writer.WriteElementString("pubDate",
                    note.Date.ToString("ddd, dd MMM yyyy", CultureInfo.GetCultureInfo("en-US")) + " 00:00:00 +0000");
                writer.WriteElementString("link", $"{url}/note/{note.Id}");
                writer.WriteElementString("description", note.ShortDescription);

                writer.WriteEndElement();
            }
            
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        return parent.ToString();
    }
}