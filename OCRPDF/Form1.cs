using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

//using from cmd:
//1.tesseract OCR
//https://github.com/tesseract-ocr
//2. image magick
//https://www.imagemagick.org/script/binary-releases.php
//3. ghost script (must have for image magick)
//https://ghostscript.com/download/gsdnld.html

namespace OCRPDF
{

    public partial class frmMain : Form
    {
        public frmMain()
        {
            //Delete temp files
            var di = new DirectoryInfo(@"temp\");
            foreach (var file in di.GetFiles())
                file.Delete();
            InitializeComponent();
        }

        #region Common Functions

        private static void RunCmd(string command)
        {
            //http://stackoverflow.com/questions/1469764/run-command-prompt-commands
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = "/C " + command
            };
            var process = new System.Diagnostics.Process { StartInfo = startInfo };
            process.Start();
            process.WaitForExit();
            process.Close();
            process.Dispose();
        }

        private void SetPictureBox(string picture)
        {
            var i = Image.FromFile(picture);
            i.Save(@"temp\temp.png");
            if (i.Width < i.Height)
            {
                pbOriginal.Height = 250;
                pbOriginal.Width = 250 * i.Width / i.Height;
            }
            else
            {
                pbOriginal.Width = 250;
                pbOriginal.Height = 250 * i.Height / i.Width;
            }
            i.Dispose();
            using (var stream = new FileStream(@"temp\temp.png", FileMode.Open, FileAccess.Read))
                pbOriginal.Image = Image.FromStream(stream);
            _cropRectangle = new Rectangle(0, 0, 0, 0);
            _cropStart = new Point(0, 0);
            PaintCropped(@"temp\temp.png");
            pbOriginal.Invalidate();
        }

        private void PaintCropped(string picture)
        {
            var i = Image.FromFile(picture);
            if (picture != @"temp\temp2.png")
                i.Save(@"temp\temp2.png");
            if (i.Width < i.Height)
            {
                pbCrop.Height = 500;
                pbCrop.Width = 500 * i.Width / i.Height;
            }
            else
            {
                pbCrop.Width = 500;
                pbCrop.Height = 500 * i.Height / i.Width;
            }
            i.Dispose();
            using (var stream = new FileStream(@"temp\temp2.png", FileMode.Open, FileAccess.Read))
                pbCrop.Image = Image.FromStream(stream);
            _cropRectangle2 = new Rectangle(0, 0, 0, 0);
            _cropStart2 = new Point(0, 0);
            pbCrop.Invalidate();
            _text = "";
            if (autoCalculate.Checked)
                btnCalculate_Click();
        }

        private string _text
        {
            set
            {
                if (!chkLocked.Checked)
                    txtExtracted.Text =
                        Regex.Replace(Regex.Replace(value.Replace("\r\n", "\n"), "[ ]+", " ").Replace("\n ", "\n"),
                            "\n{3,}", "\n\n\n").Trim(' ').Trim('\n').Trim(' ');
            }
        }

        private bool CheckKeyword(string word, Color color, int startIndex = 0)
        {
            if (txtExtracted.Text.ToLower().Contains(word))
            {
                try
                {
                    int index = -1;
                    int selectStart = txtExtracted.SelectionStart;

                    while ((index = txtExtracted.Text.ToLower().IndexOf(word, (index + 1))) != -1)
                    {
                        txtExtracted.Select((index + startIndex), word.Length);
                        txtExtracted.SelectionColor = color;
                        txtExtracted.SelectionFont = new Font(txtExtracted.Font, FontStyle.Bold);
                        txtExtracted.Select(selectStart, 0);
                        txtExtracted.SelectionColor = Color.Black;
                        txtExtracted.SelectionFont = new Font(txtExtracted.Font, FontStyle.Regular);
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        private void txtExtracted_TextChanged(object sender, EventArgs e)
        {
            txtFlags.Text = "";
            bool found = false;
            string[] keywords_potency =
            {
                "light", "lite", "ultra", "super", "extra", "xtra", "xtralite", "low", "mild", "lower", "reduced"
            };
            found = false;
            foreach (var word in keywords_potency)
                found = found | CheckKeyword(word, Color.Red);
            if (found)
                txtFlags.Text += "POTENCY;  ";
            string[] keywords_contains =
            {
                "additive", "substance", "constituent", "ingredients", "proof"
            };
            found = false;
            foreach (var word in keywords_contains)
                found = found | CheckKeyword(word, Color.Red);
            if (found)
                txtFlags.Text += "CONTENTS;  ";
            string[] keywords_harm =
            {
                "chemical", "toxin", "carcinogen", "nicotine", "tar", "cancer", "smok", "risk", "harm", "disease",
                "exposure", "free", "does", "natural", "safe", "safer", "fewer", "less"
            };
            found = false;
            foreach (var word in keywords_harm)
                found = found | CheckKeyword(word, Color.Red);
            if (found)
                txtFlags.Text += "HARM;  ";
            string[] keywords_endorsement =
            {
                "fda", "approved", "endorsed"
            };
            found = false;
            foreach (var word in keywords_endorsement)
                found = found | CheckKeyword(word, Color.Red);
            if (found)
                txtFlags.Text += "ENDORSEMENT;  ";
            string[] keywords_gifts =
            {
                "gift", "item", "credit", "program", "contest", "reward", "redemption", "points", "cash"
            };
            found = false;
            foreach (var word in keywords_gifts)
                found = found | CheckKeyword(word, Color.Red);
            if (found)
                txtFlags.Text += "GIFT;  ";
            string[] keywords_flavour =
            {
                "apple", "apricot", "avocado", "banana", "honey", "dessert", "caramel", "vanilla", "fruit", "menthol",
                "bilberry", "blackberry", "blackcurrant", "blueberry", "boysenberry", "currant", "cherry", "berry",
                "cherimoya", "cloudberry", "coconut", "cranberry", "custard", "damson", "date", "dragonfruit", "durian",
                "elderberry", "feijoa", "fig", "goji", "gooseberry", "grape", "raisin", "grapefruit", "guava",
                "honeyberry", "huckleberry", "jabuticaba", "jackfruit", "jambul", "jujube", "juniper", "kiwifruit",
                "kumquat", "lemon", "lime", "loquat", "longan", "lychee", "mango", "marionberry", "melon", "cantaloupe",
                "honeydew", "watermelon", "miracle", "mulberry", "nectarine", "nance", "olive", "orange", "blood",
                "clementine", "mandarine", "tangerine", "papaya", "passionfruit", "peach", "pear", "persimmon",
                "physalis", "plantain", "plum", "prune", "dried", "pineapple", "plumcot", "pluot", "pomegranate",
                "pomelo", "purple", "quince", "raspberry", "salmonberry", "rambutan", "redcurrant", "salal", "salak",
                "satsuma", "star", "strawberry", "tamarillo", "tamarind", "tomato", "ugli", "yuzu", "mint", "anise",
                "cinnamon", "coriander", "clove", "tarragon", "thyme", "floral", "ginger", "jasmine", "lemongrass",
                "rose", "rum", "vodka", "cocktail", "coffee", "cappuccino", "latte", "cocoa", "licorice", "chocolate"
            };
            found = false;
            foreach (var word in keywords_flavour)
                found = found | CheckKeyword(word, Color.Red);
            if (found)
                txtFlags.Text += "FLAVOUR;  ";
            string[] keywords_mark =
            {
                "camel", "dukes", "maverick", "new", "pall", "predator", "nascar", "raceway", "speedway", "rodeo",
                "revved", "stonewall", "warrior", "dixon", "silverfoiltubes", "thunder", "ocb", "event", "show"
            };
            found = false;
            foreach (var word in keywords_mark)
                found = found | CheckKeyword(word, Color.Red);
            if (found)
                txtFlags.Text += "COMPANY;  ";
            string[] keywords_event =
            {
                "convention", "conference", "concert", "booth", "exhibit", "register", "expo", "vendor", "theme",
                "venue", "mall"
            };
            found = false;
            foreach (var word in keywords_event)
                found = found | CheckKeyword(word, Color.Red);
            if (found)
                txtFlags.Text += "EVENT;  ";
            string[] keywords_chemicals =
            {
                "light", "additives", "chemicals", "toxins", "carcinogens", "agent", "smo", "substances", "constituents",
                "registered", "inspected", "cleared", "100pc"
            };
            found = false;
            foreach (var word in keywords_chemicals)
                found = found | CheckKeyword(word, Color.Red);
            if (found)
                txtFlags.Text += "CHEMICAL;  ";
            string[] keywords_NSE =
            {
                "mangosteen", "crush", "caliber", "original", "reds", "straight", "wintergreen", "filter", "recessed",
                "java", "tubes", "cigarette", "frosted", "xpert", "virgin", "party", "agents", "harmful", "contain",
                "purchase", "exchange", "bold", "silver", "deep", "blue", "double", "xxl", "smoke", "smoking"
            };
            found = false;
            foreach (var word in keywords_NSE)
                found = found | CheckKeyword(word, Color.Red);
            if (found)
                txtFlags.Text += "NSE;  ";
            string[] keywords_pipe =
            {
                "vaporize", "pipe", "cigar", "blunt", "water", "grind", "smoke", "vape"
            };
            found = false;
            foreach (var word in keywords_pipe)
                found = found | CheckKeyword(word, Color.Red);
            if (found)
                txtFlags.Text += "PIPES;  ";
            string[] keywords_weed =
            {
                "weed", "marijuana", "ganja", "herb", "high", "dab"
            };
            found = false;
            foreach (var word in keywords_weed)
                found = found | CheckKeyword(word, Color.Red);
            if (found)
                txtFlags.Text += "MARIJUANA;  ";

            if (CheckKeyword("@", Color.Blue))
                txtFlags.Text += "EMAIL;  ";
            if (CheckKeyword("www", Color.Blue))
                txtFlags.Text += "WWW;  ";
            if (CheckKeyword(".com", Color.Blue))
                txtFlags.Text += "WEBPAGE;  ";
            if (CheckKeyword("#", Color.Green))
                txtFlags.Text += "HASHTAGS; ";
        }

        #endregion

        #region Buttons

        private string _lastdir = Directory.GetCurrentDirectory() + @"\pdfs";
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            //Ask for file
            var openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = _lastdir,
                Filter = "All files (*.*)|*.*|pdf files (*.pdf)|*.pdf|png files (*.png)|*.png",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            var myStream = openFileDialog1.OpenFile();
            //Delete temp files
            var di = new DirectoryInfo(@"temp\");
            foreach (var file in di.GetFiles())
                file.Delete();
            using (myStream)
            {
                //Disable interaction while processing
                btnCalculate.Enabled = false;
                btnOpenFile.Enabled = false;
                btnPage.Enabled = false;
                //Pdf to Png
                if(openFileDialog1.FileName.Contains(".pdf"))
                    RunCmd($@"magick -density 300 ""{openFileDialog1.FileName}"" ""{Directory.GetCurrentDirectory()}\temp\pdf.png""");
                else
                {
                    var i = Image.FromFile(openFileDialog1.FileName);
                    i.Save(@"temp\pdf.png");
                    i.Dispose();
                }
                //Set pictureboxes
                SetPictureBox(@"temp\" + new DirectoryInfo(@"temp\").GetFiles("pdf*.png")[0]);
                //Reset extarcted text
                _text = "";
                //Set paths
                txtPath.Text = openFileDialog1.SafeFileName;
                _lastdir = Path.GetDirectoryName(openFileDialog1.FileName);
                //get text
                if (autoCalculate.Checked)
                    btnCalculate_Click();
                //Reset interaction
                btnPage.Enabled = true;
                btnCalculate.Enabled = true;
                btnOpenFile.Enabled = true;
            }
        }

        private void btnCalculate_Click(object sender = null, EventArgs e = null)
        {
            //Dont calculate if no file selected
            if (txtPath.Text == "") return;
            //Disable interaction while processing
            btnCalculate.Enabled = false;
            btnOpenFile.Enabled = false;
            btnPage.Enabled = false;
            //OCR
            RunCmd("tesseract " + Directory.GetCurrentDirectory() + @"\temp\temp2.png " + 
                    Directory.GetCurrentDirectory() + @"\temp\out ");
            //Grab text, insert correct endlines, remove extra ones
            _text = File.ReadAllText(Directory.GetCurrentDirectory()
                                + @"\temp\out.txt", Encoding.UTF8).Replace("\n", "\r\n")
                                .TrimEnd('\r', '\n');
            //Reset interaction
            btnPage.Enabled = true;
            btnCalculate.Enabled = true;
            btnOpenFile.Enabled = true;
        }

        private int _current;
        private void btnPage_Click(object sender, EventArgs e)
        {
            //Set pictureboxes
            _current++;
            try
            {
                SetPictureBox(@"temp\" + new DirectoryInfo(@"temp\").GetFiles("pdf*.png")[_current]);
            }
            catch (Exception)
            {
                _current = 0;
                try
                {
                    SetPictureBox(@"temp\" + new DirectoryInfo(@"temp\").GetFiles("pdf*.png")[_current]);
                }
                catch (Exception)
                {
                    pbCrop.Image = null;
                    pbCrop.Width = 500;
                    pbCrop.Height = 500;
                    pbOriginal.Image = null;
                    pbOriginal.Width = 250;
                    pbOriginal.Height = 250;
                    _text = "";
                    txtPath.Text = "";
                }
            }
        }

        #endregion

        #region pbOriginal Events

        private Rectangle _cropRectangle;
        private Point _cropStart;
        private bool _isDragging;
        private bool _moved;

        private void pbOriginal_MouseDown(object sender, MouseEventArgs e)
        {
            //if n ofile is selected, leave
            if (txtPath.Text == "") return;
            //Reset moved variable
            _moved = false;
            if (e.Button == MouseButtons.Left)
            {
                //if left click, prepare to drag
                _cropRectangle = new Rectangle(e.X, e.Y, 0, 0);
                _cropStart = new Point(e.X, e.Y);
                _isDragging = true;
            }
            else
            {
                //if right click, reset crop
                _cropRectangle = new Rectangle(0, 0, 0, 0);
                _cropStart = new Point(0, 0);
                pbOriginal.Invalidate();
                PaintCropped(@"temp\temp.png");
            }
        }

        private void pbOriginal_MouseUp(object sender, MouseEventArgs e)
        {
            //exit if no file or right click
            if (e.Button != MouseButtons.Left || txtPath.Text == "") return;
            if (_moved)
            {
                try
                {
                    //Done selecting crop rectangle, copy to new bitmap
                    var bmpImage = new Bitmap(@"temp\temp.png");
                    var croppedImage = bmpImage.Clone(
                        new Rectangle(_cropRectangle.X * bmpImage.Width / pbOriginal.Width,
                                        _cropRectangle.Y * bmpImage.Height / pbOriginal.Height,
                                        _cropRectangle.Width * bmpImage.Width / pbOriginal.Width,
                                        _cropRectangle.Height * bmpImage.Height / pbOriginal.Height),
                                        bmpImage.PixelFormat);
                    bmpImage.Dispose();
                    //save to new temp and paint in picture box
                    croppedImage.Save(@"temp\temp2.png");
                    croppedImage.Dispose();
                    PaintCropped(@"temp\temp2.png");
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            else
            {
                //if it didnt move, reset selection
                _cropRectangle = new Rectangle(0, 0, 0, 0);
                _cropStart = new Point(0, 0);
                pbOriginal.Invalidate();
                PaintCropped(@"temp\temp.png");
            }
            //Reset variables
            _moved = false;
            _isDragging = false;
        }

        private void pbOriginal_MouseMove(object sender, MouseEventArgs e)
        {
            //if they didnt left click before, leave
            if (!_isDragging) return;
            //tell methods mouse dragged
            _moved = true;
            //Make sure the selection is limited to the controlbox
            var x = e.X > pbOriginal.Width ? pbOriginal.Width - 4 : e.X;
            var y = e.Y > pbOriginal.Height ? pbOriginal.Height - 4 : e.Y;
            x = x >= 2 ? x : 2;
            y = y >= 2 ? y : 2;
            //if it didnt move a considerable amount, move it one pixel
            var w = Math.Abs(x - _cropStart.X) == 0? 2: Math.Abs(x - _cropStart.X);
            var h = Math.Abs(y - _cropStart.Y) == 0? 2: Math.Abs(y - _cropStart.Y);
            //Create the new crop rectangle
            _cropRectangle = new Rectangle(Math.Min(_cropStart.X, x),
                                           Math.Min(_cropStart.Y, y), w, h);
            //redraw
            pbOriginal.Invalidate();
        }

        private void pbOriginal_Paint(object sender, PaintEventArgs e)
        {
            //Paint selection rectangle
            e.Graphics.DrawRectangle(Pens.Red, _cropRectangle);
        }

        #endregion

        #region pbCrop Events

        private Rectangle _cropRectangle2;
        private Point _cropStart2;
        private bool _isDragging2;
        private bool _moved2;

        private void pbCrop_MouseDown(object sender, MouseEventArgs e)
        {
            //if n ofile is selected, leave
            if (txtPath.Text == "") return;
            //Reset moved variable
            _moved2 = false;
            if (e.Button == MouseButtons.Left)
            {
                //if left click, prepare to drag
                _cropRectangle2 = new Rectangle(e.X, e.Y, 0, 0);
                _cropStart2 = new Point(e.X, e.Y);
                _isDragging2 = true;
            }
            else
            {
                //if right click, reset crop
                PaintCropped(@"temp\temp2.png");
            }
        }

        private void pbCrop_MouseUp(object sender, MouseEventArgs e)
        {
            //exit if no file or right click
            if (e.Button != MouseButtons.Left || txtPath.Text == "") return;
            if (_moved2)
            {
                try
                {
                    //Done selecting crop rectangle, copy to new bitmap
                    var bmpImage = new Bitmap(@"temp\temp2.png");
                    var croppedImage = bmpImage.Clone(
                        new Rectangle(_cropRectangle2.X * bmpImage.Width / pbCrop.Width,
                                        _cropRectangle2.Y * bmpImage.Height / pbCrop.Height,
                                        _cropRectangle2.Width * bmpImage.Width / pbCrop.Width,
                                        _cropRectangle2.Height * bmpImage.Height / pbCrop.Height),
                                        bmpImage.PixelFormat);
                    bmpImage.Dispose();
                    //save to new temp and paint in picture box
                    croppedImage.Save(@"temp\temp2.png");
                    croppedImage.Dispose();
                    PaintCropped(@"temp\temp2.png");
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            else
            {
                //if it didnt move, reset selection
                PaintCropped(@"temp\temp2.png");
            }
            //Reset variables
            _moved2 = false;
            _isDragging2 = false;
        }

        private void pbCrop_MouseMove(object sender, MouseEventArgs e)
        {
            //if they didnt left click before, leave
            if (!_isDragging2) return;
            //tell methods mouse dragged
            _moved2 = true;
            //Make sure the selection is limited to the controlbox
            var x = e.X > pbCrop.Width ? pbCrop.Width - 4 : e.X;
            var y = e.Y > pbCrop.Height ? pbCrop.Height - 4 : e.Y;
            x = x >= 2 ? x : 2;
            y = y >= 2 ? y : 2;
            //if it didnt move a considerable amount, move it one pixel
            var w = Math.Abs(x - _cropStart2.X) == 0 ? 2 : Math.Abs(x - _cropStart2.X);
            var h = Math.Abs(y - _cropStart2.Y) == 0 ? 2 : Math.Abs(y - _cropStart2.Y);
            //Create the new crop rectangle
            _cropRectangle2 = new Rectangle(Math.Min(_cropStart2.X, x),
                                            Math.Min(_cropStart2.Y, y), w, h);
            //redraw
            pbCrop.Invalidate();
        }

        private void pbCrop_Paint(object sender, PaintEventArgs e)
        {
            //Paint selection rectangle
            e.Graphics.DrawRectangle(Pens.Red, _cropRectangle2);
            //reset selection
            _cropRectangle = new Rectangle(0, 0, 0, 0);
            _cropStart = new Point(0, 0);
            pbOriginal.Invalidate();
        }

        #endregion
    }
}
