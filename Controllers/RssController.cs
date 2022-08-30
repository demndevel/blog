using System.Globalization;
using System.Xml;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class RssController : Controller
{
    private readonly ApplicationContext _db;
    
    public RssController(ApplicationContext db)
    {
        _db = db;
    }
    public ContentResult Rss()
    {
        var lastTenNotes = _db.Notes.OrderByDescending(n => n.Date).Take(10).ToList();
        var xml = BuildXmlFeed($"{Request.Scheme}://{Request.Host}", lastTenNotes);
        return new ContentResult
        {
            ContentType = "application/xml",
            Content = xml,
            StatusCode = 200
        };
    }


    private string BuildXmlFeed(string url, List<Note> notes)
    {
        StringWriter parent = new StringWriter();
        using (XmlTextWriter writer = new XmlTextWriter(parent))
        {
            writer.WriteStartElement("rss");
            writer.WriteAttributeString("version", "2.0");
            writer.WriteAttributeString("xmlns:atom", "http://www.w3.org/2005/Atom");

            writer.WriteStartElement("channel");

            writer.WriteElementString("title", $"demn's blog");
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
                    note.Date.ToString("ddd, dd MMM yyyy") + " 00:00:00 +0000");
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