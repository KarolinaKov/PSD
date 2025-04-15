using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace pokus
{
    public partial class Form1 : Form
    {
        private bool matrix_state;
        private MaticeIncidence mine_matrix;
        private PrepTDiagnostika prepT;
        private List<I_Observer>SeznamObserveru;
        private VykreslovaniGrafu Graf;

        public Form1()
        {
            InitializeComponent();
            matrix_state = false;
            prepT = new PrepTDiagnostika();
            Graf = new VykreslovaniGrafu();
            SeznamObserveru = new List<I_Observer> {prepT,Graf};
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);






        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (matrix_state == false)
            {
                mine_matrix = new MaticeIncidence();
                matrix_state = true;
                for (int i = 0; i < SeznamObserveru.Count; i++)
                {
                    mine_matrix.AddSubscriber(SeznamObserveru[i]);
                }
                comboBox1.Items.Add($"M{mine_matrix.GetModuleCount()}");
                comboBox2.Items.Add($"M{mine_matrix.GetModuleCount()}");
                mine_matrix.NotifySubscribers();
            }
            else
            {
                if (mine_matrix.GetModuleCount() == 10)
                {

                    MessageBox.Show("Byl dosažen maximální počet modulů.");
                }
                else
                {
                    mine_matrix.AddModule();
                    comboBox1.Items.Add($"M{mine_matrix.GetModuleCount()}");
                    comboBox2.Items.Add($"M{mine_matrix.GetModuleCount()}");
                }
            }
            UpdateLabel4Text();
            UpdateLabel5Text();
            UpdateLabel6Text();
            pictureBox1.Invalidate(); // Vyvolá překreslení PictureBox 


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text) ||
              !comboBox1.Items.Contains(comboBox1.Text) || !comboBox2.Items.Contains(comboBox2.Text))
            {
                MessageBox.Show("Zvolená kombinace neexistuje");
            }
            else if (comboBox1.Text == comboBox2.Text)
            {
                MessageBox.Show("Nelze zvolit stejné moduly");
            }
            else
            {
                int ControligModule = int.Parse(comboBox1.Text.Substring(1));
                int ControledModule = int.Parse(comboBox2.Text.Substring(1));
                if (mine_matrix.ModuleRelationship(ControligModule, ControledModule))
                {
                    MessageBox.Show($"Modul M{ControligModule} již kontroluje modul M{ControledModule}.");
                }
                else
                {
                   mine_matrix.AddControl(ControligModule, ControledModule);
                    UpdateLabel5Text();
                    pictureBox1.Invalidate();
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)

        {
            if (this.matrix_state == false)
            {
                MessageBox.Show("Nelze odebrat modul, pokud nejsou žádné moduly.");
            }
            else
            {
                comboBox1.Items.Remove($"M{mine_matrix.GetModuleCount()}");
                comboBox2.Items.Remove($"M{mine_matrix.GetModuleCount()}");
                mine_matrix.RemoveModule();
                if (mine_matrix.GetModuleCount() == 0)
                {
                    matrix_state = false;
                    comboBox1.Text = string.Empty; // Vymaže text z comboBox1
                    comboBox2.Text = string.Empty;

                }
                UpdateLabel4Text();
                UpdateLabel5Text();
                UpdateLabel6Text();
                pictureBox1.Invalidate();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text) ||
              !comboBox1.Items.Contains(comboBox1.Text) || !comboBox2.Items.Contains(comboBox2.Text))
            {
                MessageBox.Show("Zvolená kombinace neexistuje");
            }
            else if (comboBox1.Text == comboBox2.Text)
            {
                MessageBox.Show("Nelze zvolit stejné moduly");
            }
            else
            {

                int ControligModule = int.Parse(comboBox1.Text.Substring(1));
                int ControledModule = int.Parse(comboBox2.Text.Substring(1));
                if (mine_matrix.ModuleRelationship(ControligModule, ControledModule))
                {
                    mine_matrix.RemoveControl(ControligModule, ControledModule);
                    UpdateLabel5Text();
                    pictureBox1.Invalidate();
                }
                else
                {
                    MessageBox.Show($"Modul M{ControligModule} nekontroluje modul M{ControledModule}.");
                }
            }


        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graf.VykresliGraf(e.Graphics, pictureBox1.Width, pictureBox1.Height);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void UpdateLabel6Text()
        {
            if (this.matrix_state == false)
            {
                label6.Text = "0";
            }
            else
            {
                label6.Text = prepT.CountTMax().ToString();
            }
        }
        private void UpdateLabel4Text()
        {
            if (this.matrix_state == false)
            {
                label4.Text = "0";
            }
            else
            {
                label4.Text = prepT.CountL().ToString();
            }
        }
        private void UpdateLabel5Text()
        {
            if (this.matrix_state == false)
            {
                label5.Text = "0";
            }
            else
            {
                label5.Text = prepT.CountT().ToString();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {   
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.matrix_state == false)
            {
                MessageBox.Show("Proc? K cemu ti to je?" +
                    " Opravdu to ted potrebujes? Zamysli se nad tim cos udelal :/");
            }
            else
            {
                bool podminka = false;
                while (!podminka)
                {
                    string userInput = Microsoft.VisualBasic.Interaction.InputBox(
                    "Zadejte pravděpodobnost selhání modulu:", "Vstupní okno", "0,5");
                    if (string.IsNullOrEmpty(userInput))
                    {
                        break;
                    }
                    if (System.Text.RegularExpressions.Regex.IsMatch(userInput, @"^0\,\d{1,9}$"))
                    {
                        if (double.TryParse(userInput, out double result))
                        {
                            PravdepodbnostSpravneDiagnostiky PSD = new PravdepodbnostSpravneDiagnostiky(result);
                            this.mine_matrix.AddSubscriber(PSD);
                            double vysledek = PSD.PravdepodobnostSP();
                            if (vysledek == 1)
                            {
                                MessageBox.Show("Pravděpodobnost správné diagnostiky podle grafu je 100% " +
                                    "(to teda pekne pochybuju, 'chyba' C# -> 0^0 == 1)");
                                podminka = true;
                            }
                            else
                            {
                                MessageBox.Show($"Pravděpodobnost správné diagnostiky podle grafu je {vysledek*100}%" );
                                podminka = true;
                            }
                            this.mine_matrix.RemoveSubscriber(PSD);
                        }
                    }
                    else
                    {
                        if (int.TryParse(userInput, out int result) && result == 1)
                        {
                            MessageBox.Show("Pravdepodobnost selhání tvého systému je tvoje mama"); 
                        }
                        MessageBox.Show("Zadejte pravděpodobnost ve formátu 0,xxxxxxxx" +
                            " (pocet cislic misto 'x' je libovolny");
                    }
                }
            }
        }


    }
}
