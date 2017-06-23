using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Game_Launcher
{
    public static class Controller
    {
        public static List<Programm> Spiele = new List<Programm>();

        const string name = "Name";
        const string pfad = "Pfad";

        public static void ProgrammHinzufügen(Programm neuesSpiel)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "exe | *.exe";
            if (neuesSpiel == null)
            {
                throw new NullReferenceException();
                Programm programmx = new Programm(openFileDialog1.SafeFileName, openFileDialog1.FileName);
                Spiele.Add(programmx);
            }
            else
            {
               Spiele.Add(neuesSpiel);
            }
        }

        public static void ProgrammLöschen(string name)
        {
            if (name == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                for (int i = 0; i < Spiele.Count; i++)
                {
                    if (Spiele[i].Name == name)
                    {
                        Spiele.RemoveAt(i);
                    }
                }
            }
        }

        public static void ProgrammStarten(string pfad)
        {
            if (pfad == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                System.Diagnostics.Process.Start(pfad);
            }
        }

        public static void ProgrammStarten()
        {
            //Benutzer gibt die zu ladende Datei an
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult res = DialogResult.Abort;
            res = ofd.ShowDialog();
            //falls Benutzer eine Datei ausgewählt hat
            //andernfalls wäre im Dialogfenster der Abbrechen oder der Schließen Button gedückt worden
            if (res == DialogResult.OK)
            {
                //Try Catch damit das Programm an einer manipulierten Datei nicht abstürzt
                try
                {
                    //Der Quellpfad der Datei
                    string path = ofd.FileName;
                    XmlDocument doc = new XmlDocument();
                    //Läd das XMLdokument
                    doc.Load(path);
                    //Liste aller Autos
                    XmlNodeList alleProgramme = doc.SelectNodes("//Programm/Anwendung");
                    //erstellt alle CDs
                    foreach (XmlNode Anwendungnode in alleProgramme)
                    {
                        //lädt die Attribute aus der Datei
                        XmlElement name = Anwendungnode.SelectSingleNode("Name") as XmlElement;
                        XmlElement pfad = Anwendungnode.SelectSingleNode("Pfad") as XmlElement;
                        Programm programmx = new Programm(name.InnerText, pfad.InnerText);
                        //Fügt der Listbox die Autos hinzu
                        Spiele.Add(programmx);
                    }
                }
                //Fehlermeldung 
                catch (Exception)
                {
                    MessageBox.Show("Die Datei ist keine XML Datei oder die Datei ist beschädigt.");
                }
            }
        }

        public static void ProgrammBeenden()
        {
            //erstellt XMLDocument im Speicher
            XmlDocument xmlDoc = new XmlDocument();
            //Declarationzeile
            XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "ISO-8859-1", null);              //schreibt Declarationzeile als erste Zeile ins doc
            xmlDoc.InsertBefore(declaration, xmlDoc.DocumentElement);
            XmlNode rootnode = xmlDoc.CreateElement("Programm");
            xmlDoc.AppendChild(rootnode);
            for (int i = 0; i < Spiele.Count; i++)
            {
                XmlNode Anwendungnode = xmlDoc.CreateElement("Anwendung");
                rootnode.AppendChild(Anwendungnode);

                XmlNode Namelnode = xmlDoc.CreateElement("Name");
                Namelnode.InnerText = Spiele[i].Name;
                Anwendungnode.AppendChild(Namelnode);

                XmlNode Pfadnode = xmlDoc.CreateElement("Pfad");
                Pfadnode.InnerText = Spiele[i].Pfad;
                Anwendungnode.AppendChild(Pfadnode);
            }
            //Benutzer gibt Speicherpfad an
            SaveFileDialog sfd1 = new SaveFileDialog();

            //wenn der Pfad existiert
            if (sfd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                xmlDoc.Save(sfd1.FileName);
            }
        }

    }
}