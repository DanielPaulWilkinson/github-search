using github_search.Services.Interfaces;
using github_search.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Document = iTextSharp.text.Document;

namespace github_search.Services.Services
{
    public class PdfService : IPdfService
    {
        #region Core Methods
        public MemoryStream GetProfile(ProfileVM vm)
        {
            //set bounds
            Document doc = new Document(PageSize.A4, 20, 20, 30, 30);

            // I dont want to save this anywhere so I am going to pass this as a content result.
            MemoryStream memoryStream = new MemoryStream();

            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);

            //Add meta data
            AddMetaData(doc);

            //Open pdf to write content.
            doc.Open();

            //Add image
            AddProfileImage(doc, vm.User.avatar_url, Element.ALIGN_CENTER);

            //Add name of candidate 
            AddText(doc, $"{vm.User.name ?? vm.User.login}", FontFactory.GetFont("Arial", 30, BaseColor.BLACK), Element.ALIGN_CENTER);

            //Add bio.
            AddText(doc, $"{vm.User.bio ?? "Unspecified Bio"}", FontFactory.GetFont("Arial", 11, Font.ITALIC, BaseColor.BLACK), Element.ALIGN_CENTER);

            //Add detail sub title.
            AddText(doc, "Details", FontFactory.GetFont("Arial", 30, BaseColor.BLACK), Element.ALIGN_LEFT);

            //Add detail info.
            AddDetailsList(doc, vm);

            //Add repo sub title
            AddText(doc, "Repositories", FontFactory.GetFont("Arial", 30, BaseColor.BLACK), Element.ALIGN_LEFT);

            //list all repo's
            AddRepositoryTable(doc, vm);

            doc.Close();

            writer.Close();

            return memoryStream;
        }
        #endregion

        #region PDF Writer Helper Methods
        private void AddRepositoryTable(Document doc, ProfileVM vm)
        {
            PdfPTable hypTable = new PdfPTable(4);
            hypTable.TotalWidth = 500f;
            hypTable.LockedWidth = true;
            float[] widths = new float[] { 4f, 8f, 3f, 3f };
            hypTable.SetWidths(widths);
            hypTable.HorizontalAlignment = 0;
            hypTable.SpacingBefore = 20;
            PdfPCell hypCell1 = new PdfPCell(new Phrase("Name", FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));
            hypCell1.Padding = 10;
            hypCell1.Colspan = 1;
            hypCell1.HorizontalAlignment = 1;
            hypTable.AddCell(hypCell1);
            PdfPCell hypCell2 = new PdfPCell(new Phrase("Description", FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));
            hypCell2.Padding = 10;
            hypCell2.Colspan = 1;
            hypCell2.HorizontalAlignment = 1;
            hypTable.AddCell(hypCell2);

            PdfPCell hypCell3 = new PdfPCell(new Phrase("Watchers", FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));
            hypCell3.Padding = 10;
            hypCell3.Colspan = 1;
            hypCell3.HorizontalAlignment = 1;
            hypTable.AddCell(hypCell3);
            PdfPCell hypCell4 = new PdfPCell(new Phrase("Stargazers", FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));
            hypCell4.Padding = 10;
            hypCell4.Colspan = 1;
            hypCell4.HorizontalAlignment = 1;
            hypTable.AddCell(hypCell4);
            if (vm.Repositories != null)
            {
                List<PdfPCell> CellList = new List<PdfPCell>();

                foreach (var node in vm.Repositories)
                {
                    CellList.Add(new PdfPCell(new Phrase($"{node.name}", FontFactory.GetFont("Arial", 11, BaseColor.BLACK))));
                    CellList.Add(new PdfPCell(new Phrase($"{node.description}", FontFactory.GetFont("Arial", 11, BaseColor.BLACK))));
                    CellList.Add(new PdfPCell(new Phrase($"{node.watchers_count}", FontFactory.GetFont("Arial", 11, BaseColor.BLACK))));
                    CellList.Add(new PdfPCell(new Phrase($"{node.stargazers_count}", FontFactory.GetFont("Arial", 11, BaseColor.BLACK))));
                }

                foreach (var cell in CellList)
                {
                    cell.Padding = 10;
                    hypTable.AddCell(cell);
                }
            }
            doc.Add(hypTable);
        }

        private void AddDetailsList(Document doc, ProfileVM vm)
        {
            List list = new List(List.UNORDERED);
            list.SetListSymbol("\u2022");
            list.Add(new ListItem(new Phrase($" Candidate Github Score: {vm.User.score ?? "Unknown"}", FontFactory.GetFont("Arial", 11, BaseColor.BLACK))));
            list.Add(new ListItem(new Phrase($" Is candidate hireable: {vm.User.hireable ?? "Unspecified"}", FontFactory.GetFont("Arial", 11, BaseColor.BLACK))));
            list.Add(new ListItem(new Phrase($" Location of candidate: {vm.User.location ?? "Unspecified"}", FontFactory.GetFont("Arial", 11, BaseColor.BLACK))));
            list.Add(new ListItem(new Phrase($" Email of candidate: {vm.User.email ?? "Unspecified"}", FontFactory.GetFont("Arial", 11, BaseColor.BLACK))));
            list.Add(new ListItem(new Phrase($" Currently working for: {vm.User.company ?? "Unspecified"}", FontFactory.GetFont("Arial", 11, BaseColor.BLACK))));
            doc.Add(list);
        }

        private void AddProfileImage(Document doc, string path, int alignment = Element.ALIGN_CENTER)
        {
            Image avitar = Image.GetInstance(path);
            avitar.ScaleAbsolute(200, 200);
            avitar.Alignment = alignment;
            doc.Add(avitar);
        }

        private void AddText(Document doc, string text, Font f, int alignment = Element.ALIGN_CENTER)
        {
            var p = new Paragraph(text, f);
            p.Alignment = alignment;
            doc.Add(p);
        }

        private void AddMetaData(Document doc)
        {
            doc.AddAuthor("BGL Talent Search Application");
            doc.AddCreator("Daniel Wilkinson");
            doc.AddKeywords("Profile Report");
            doc.AddSubject("Profile Report");
            doc.AddTitle("Profile Report");
        }
        #endregion
    }
}
