namespace BetterText2
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private bool darkMode { get; set; } = false;

        private string inputText { get; set; }
        private string outputText { get; set; }
        private string previewText { get; set; }

        private System.Collections.Generic.List<string> markH { get; set; }
            = new System.Collections.Generic.List<string>();
        private System.Collections.Generic.List<string> markE { get; set; }
            = new System.Collections.Generic.List<string>();
        private System.Collections.Generic.List<string> palette { get; set; }
            = new System.Collections.Generic.List<string>();

        private System.Drawing.Color currentColor { get; set; } = new System.Drawing.Color();
        private System.Text.RegularExpressions.Regex markdownRegex { get; set; } = new System.Text.RegularExpressions.Regex(@"\[COLOR=(\w+)](.*?)\[\/COLOR]");
        private System.Collections.Generic.Dictionary<string, System.Drawing.Color> theFlagger { get; set; }
            = new System.Collections.Generic.Dictionary<string, System.Drawing.Color>() {
                { "Black2", System.Drawing.Color.FromArgb(255, 255, 255) }, // Dark
                { "Black", System.Drawing.Color.FromArgb(0, 0, 0) }, // Light
                { "DarkRed", System.Drawing.Color.FromArgb(139, 0, 0) },
                { "Red", System.Drawing.Color.FromArgb(255, 0, 0) },
                { "Magenta", System.Drawing.Color.FromArgb(255, 0, 255) },
                { "Pink", System.Drawing.Color.FromArgb(255, 192, 203) },
                { "Sienna", System.Drawing.Color.FromArgb(160, 82, 45) },
                { "DarkOrange", System.Drawing.Color.FromArgb(255, 140, 0) },
                { "SandyBrown", System.Drawing.Color.FromArgb(244, 164, 96) },
                { "Orange", System.Drawing.Color.FromArgb(255, 165, 0) },
                { "Wheat", System.Drawing.Color.FromArgb(245, 222, 179) },
                { "DarkOliveGreen", System.Drawing.Color.FromArgb(85, 107, 47) },
                { "Olive", System.Drawing.Color.FromArgb(128, 128, 0) },
                { "YellowGreen", System.Drawing.Color.FromArgb(154, 205, 50) },
                { "Yellow", System.Drawing.Color.FromArgb(255, 255, 0) },
                { "LemonChiffon", System.Drawing.Color.FromArgb(255, 250, 205) },
                { "DarkGreen", System.Drawing.Color.FromArgb(0, 100, 0) },
                { "Green", System.Drawing.Color.FromArgb(0, 128, 0) },
                { "SeaGreen", System.Drawing.Color.FromArgb(46, 139, 87) },
                { "Lime", System.Drawing.Color.FromArgb(0, 255, 0) },
                { "PaleGreen", System.Drawing.Color.FromArgb(152, 251, 152) },
                { "DarkSlateBlue", System.Drawing.Color.FromArgb(72, 61, 139) },
                { "Teal", System.Drawing.Color.FromArgb(0, 128, 128) },
                { "MediumTorquoise", System.Drawing.Color.FromArgb(72, 209, 204) },
                { "Cyan", System.Drawing.Color.FromArgb(0, 255, 255) },
                { "PaleTurquoise", System.Drawing.Color.FromArgb(175, 238, 238) },
                { "Navy2", System.Drawing.Color.FromArgb(0, 0, 128) }, // dark
                { "Navy", System.Drawing.Color.FromArgb(167, 167, 255) }, // light
                { "Blue2", System.Drawing.Color.FromArgb(0, 0, 255) }, // dark
                { "Blue", System.Drawing.Color.FromArgb(81, 151, 255) }, // light
                { "RoyalBlue", System.Drawing.Color.FromArgb(65, 105, 225) },
                { "DeepSkyBlue", System.Drawing.Color.FromArgb(0, 191, 255) },
                { "LightBlue", System.Drawing.Color.FromArgb(173, 216, 230) },
                { "Indigo", System.Drawing.Color.FromArgb(75, 0, 130) },
                { "SlateGray", System.Drawing.Color.FromArgb(112, 128, 144) },
                { "Purple", System.Drawing.Color.FromArgb(128, 0, 128) },
                { "DarkOrchid", System.Drawing.Color.FromArgb(153, 50, 204) },
                { "Plum", System.Drawing.Color.FromArgb(221, 160, 221) },
                { "DarkSlateGray", System.Drawing.Color.FromArgb(47, 79, 79) },
                { "DimGray", System.Drawing.Color.FromArgb(105, 105, 105) },
                { "Gray", System.Drawing.Color.FromArgb(128, 128, 128) },
                { "Silver", System.Drawing.Color.FromArgb(192, 192, 192) },
                { "White", System.Drawing.Color.FromArgb(255, 255, 255) }
            };

        private void siticoneTextBox1_TextChanged(object sender, System.EventArgs e)
        {
            inputText = siticoneTextBox1.Text;
            label5.Text = inputText.Length.ToString();
        }

        private void siticoneButton2_Click(object sender, System.EventArgs e)
        {
            outputText = null;
            previewText = null;
            siticoneTextBox2.Text = null;
            richTextBox1.Text = null;
            markH.Clear();
            markE.Clear();
            int counter = 0; string cache;
            foreach (char c in inputText)
            {
                if (counter == palette.Count) counter = 0;
                if (c == ' ') cache = " ";
                //else if (c.ToString() == Environment.NewLine) cache = "\n";
                else cache = $"[COLOR={palette[counter]}]{c}[/COLOR]"; counter++;
                outputText += cache;
            }
            siticoneTextBox2.Text = outputText;
            DarkMode();
        }

        private void Matrix()
        {
            foreach (System.Text.RegularExpressions.Match match in markdownRegex.Matches(outputText))
            {
                markH.Add(match.Groups[1].Value);
                markE.Add(match.Groups[2].Value);
            }
            PreviewText();
        }

        private void PreviewText()
        {
            int marker = 0;
            for(int i = 0; i<inputText.Length; i++)
            {
                if (marker == markH.Count-1) marker = 0;
                else marker++;
                if(inputText[i] != ' ' || inputText[i] != '\n')
                {
                    currentColor = theFlagger[markH[marker]];
                    richTextBox1.AppendText(inputText[i].ToString());
                    richTextBox1.Select(i, 1);
                    richTextBox1.SelectionColor = currentColor;
                }
            }
        }

        private void DarkMode() {
            if(darkMode)
            {
                richTextBox1.BackColor = System.Drawing.Color.FromArgb(24,24,24);
                if (outputText.Contains("[COLOR=Black]") && darkMode) outputText.Replace("[COLOR=Black]", "[COLOR=Black2]");
                if (outputText.Contains("[COLOR=Navy]") && darkMode) outputText.Replace("[COLOR=Navy]", "[COLOR=Navy2]");
                if (outputText.Contains("[COLOR=Blue]") && darkMode) outputText.Replace("[COLOR=Blue]", "[COLOR=Blue2]");
                Matrix();
            }
            else
            {
                richTextBox1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                if (outputText.Contains("[COLOR=Black2]") && darkMode) outputText.Replace("[COLOR=Black2]", "[COLOR=Black]");
                if (outputText.Contains("[COLOR=Navy2]") && darkMode) outputText.Replace("[COLOR=Navy2]", "[COLOR=Navy]");
                if (outputText.Contains("[COLOR=Blue2]") && darkMode) outputText.Replace("[COLOR=Blue2]", "[COLOR=Blue]");
                Matrix();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            palette.Add(siticoneComboBox1.SelectedItem.ToString());
            palette.Add(siticoneComboBox2.SelectedItem.ToString());
            palette.Add(siticoneComboBox3.SelectedItem.ToString());
            palette.Add(siticoneComboBox4.SelectedItem.ToString());
            palette.Add(siticoneComboBox5.SelectedItem.ToString());
            palette.Add(siticoneComboBox10.SelectedItem.ToString());
            palette.Add(siticoneComboBox9.SelectedItem.ToString());
            palette.Add(siticoneComboBox8.SelectedItem.ToString());
            palette.Add(siticoneComboBox7.SelectedItem.ToString());
            palette.Add(siticoneComboBox6.SelectedItem.ToString());
        }

        private void siticoneOSToggleSwith1_CheckedChanged(object sender, System.EventArgs e)
        {
            previewText = null;
            richTextBox1.Text = null;
            if (darkMode) darkMode = false;
            else darkMode = true;
            DarkMode();
        }

        private void siticoneComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            palette[0] = siticoneComboBox1.SelectedItem.ToString();
            palette[1] = siticoneComboBox2.SelectedItem.ToString();
            palette[2] = siticoneComboBox3.SelectedItem.ToString();
            palette[3] = siticoneComboBox4.SelectedItem.ToString();
            palette[4] = siticoneComboBox5.SelectedItem.ToString();
            palette[5] = siticoneComboBox10.SelectedItem.ToString();
            palette[6] = siticoneComboBox9.SelectedItem.ToString();
            palette[7] = siticoneComboBox8.SelectedItem.ToString();
            palette[8] = siticoneComboBox7.SelectedItem.ToString();
            palette[9] = siticoneComboBox6.SelectedItem.ToString();
        }
    }
}

/*
public struct Vector3
{
    public Vector3(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public int X { get; }
    public int Y { get; }
    public int Z { get; }
}
*/

/*
Black
DarkRed
Red
Magenta
Pink
Sienna
DarkOrange
SandyBrown
Orange
Wheat
DarkOliveGreen
Olive
YellowGreen
Yellow
LemonChiffon
DarkGreen
Green
SeaGreen
Lime
PaleGreen
DarkSlateBlue
Teal
MediumTorquoise
Cyan
PaleTurquoise
Navy
Blue
RoyalBlue
DeekSkyBlue
LightBlue
Indigo
SlateGray
Purple
DarkOrchid
Plum
DarkSlateGray
DimGray
Gray
Silver
White
 */