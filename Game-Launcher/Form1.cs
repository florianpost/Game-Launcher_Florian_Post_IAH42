using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Bitte suchen Sie Ihre gespeicherten Dateien. Vielen Dank!");
            Controller.ProgrammStarten(); //Daten Laden
            ListBoxAktualisieren();
        }

        private void mainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Wollen Sie wirklich die Anwendung schließen?", "Dein Programm",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Controller.ProgrammBeenden();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "exe | *.exe";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Programm programmx = new Programm(openFileDialog1.SafeFileName, openFileDialog1.FileName);
                Controller.ProgrammHinzufügen(programmx);
                ListBoxAktualisieren();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Programm programmx = (listBox1.SelectedItem as Programm);
                Controller.ProgrammStarten(programmx.Pfad);
            }
            catch { MessageBox.Show("Bitte ein Programm auswählen!"); }
            ListBoxAktualisieren();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Programm programmx = (listBox1.SelectedItem as Programm);
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = Bitmap.FromHicon(new Icon(Icon.ExtractAssociatedIcon(programmx.Pfad), new Size(500, 500)).Handle);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch
            {
                MessageBox.Show("Kein Bild gefunden!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Programm programmx = (listBox1.SelectedItem as Programm);
                Controller.ProgrammLöschen(programmx.Name);
            }
            catch { MessageBox.Show("Bitte ein Programm auswählen!"); }
            ListBoxAktualisieren();
        }

        public void ListBoxAktualisieren()
        {
            pictureBox1.Image = null;
            listBox1.Items.Clear();
            for (int i = 0; i < Controller.Spiele.Count; i++)
            {

                listBox1.Items.Add(Controller.Spiele[i]);
            }
        }
    }
}
