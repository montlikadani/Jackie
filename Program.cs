using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.UI;

namespace Jackie {
    class Program {

        private static readonly List<JackieVersenyzo> jackieAdatai = new List<JackieVersenyzo>();
        
        static void Main(string[] args) {
            foreach (string one in File.ReadAllLines("jackie.txt").Skip(1)) {
                jackieAdatai.Add(new JackieVersenyzo(one));
            }

            Console.WriteLine("3. feladat: " + jackieAdatai.Count);
            Console.WriteLine("4. feladat: " + FourthTask());

            Console.WriteLine("5. feladat:");
            FifthTask();

            string fileName = "jackie.html";
            Console.WriteLine("6. feladat: " + fileName);
            GenerateHtmlToFile(fileName);

            Console.ReadKey();
        }

        private static int FourthTask() {
            int bestRaces = jackieAdatai.Max(jackie => jackie.Races);
            JackieVersenyzo versenyzo = jackieAdatai.Find(jack => jack.Races == bestRaces);

            return versenyzo == null ? 0 : versenyzo.Year;
        }

        private static void FifthTask() {
            Console.WriteLine($"\t70-es évek: {jackieAdatai.Where(jackie => jackie.Year >= 1970 && jackie.Year < 1980).Select(jack => jack.Wins).Sum()} megnyert verseny");
            Console.WriteLine($"\t60-es évek: {jackieAdatai.Where(jackie => jackie.Year >= 1960 && jackie.Year < 1970).Select(jack => jack.Wins).Sum()} megnyert verseny");
        }

        private static void GenerateHtmlToFile(string fileName) {
            if (File.Exists(fileName)) {
                File.Delete(fileName);
            }

            using (StreamWriter streamWriter = File.CreateText("jackie.html")) {
                using (HtmlTextWriter writer = new HtmlTextWriter(streamWriter)) {
                    writer.WriteBeginTag("!doctype html>");
                    writer.WriteLine();

                    writer.RenderBeginTag(HtmlTextWriterTag.Html);

                    writer.RenderBeginTag(HtmlTextWriterTag.Head);
                    writer.RenderEndTag();

                    writer.WriteLine();
                    writer.RenderBeginTag(HtmlTextWriterTag.Style);

                    writer.Write("td { border: 1px solid black; }");

                    writer.RenderEndTag();

                    #region Body
                    writer.WriteLine();
                    writer.RenderBeginTag(HtmlTextWriterTag.Body);

                    writer.RenderBeginTag(HtmlTextWriterTag.H1);
                    writer.Write("Jackie Stewart");
                    writer.RenderEndTag();

                    writer.RenderEndTag();
                    #endregion

                    #region table
                    writer.WriteLine();
                    writer.RenderBeginTag(HtmlTextWriterTag.Table);

                    int i = 0;

                    foreach (JackieVersenyzo jackie in jackieAdatai) {
                        writer.WriteBeginTag("tr>");

                        writer.RenderBeginTag("td");
                        writer.Write(jackie.Year);
                        writer.WriteEndTag("td");

                        writer.RenderBeginTag("td");
                        writer.Write(jackie.Races);
                        writer.WriteEndTag("td");

                        writer.RenderBeginTag("td");
                        writer.Write(jackie.Wins);
                        writer.WriteEndTag("td");

                        writer.WriteEndTag("tr");

                        if (i + 1 < jackieAdatai.Count) {
                            writer.WriteLine();
                        }

                        i++;
                    }

                    writer.WriteLine();
                    writer.WriteEndTag("table");
                    #endregion

                    writer.WriteLine();
                    writer.WriteEndTag("body");

                    writer.WriteLine();
                    writer.WriteEndTag("html");

                    writer.EndRender();
                }
            }
        }
    }
}
