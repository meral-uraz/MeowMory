using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeowMory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();        
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            images_randomize();
        }

        PictureBox firstPictureBox;
        int firstIndex, match, attempt;

        Image[] pictures =
        {
            Properties.Resources.artist_tw_gentleeeeeeecat_0,
            Properties.Resources.artist_tw_gentleeeeeeecat_1,
            Properties.Resources.artist_tw_gentleeeeeeecat_2,
            Properties.Resources.artist_tw_gentleeeeeeecat_3,
            Properties.Resources.artist_tw_gentleeeeeeecat_4,
            Properties.Resources.artist_tw_gentleeeeeeecat_5,
            Properties.Resources.artist_tw_gentleeeeeeecat_6,
            Properties.Resources.artist_tw_gentleeeeeeecat_7
        };

        int [] indexs = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
       
        private void images_randomize()
        {

            Random random = new Random();
            for (int i = 0; i < indexs.Length; i++)
            {
                int num = random.Next(indexs.Length);
                int temp = indexs[i];
                indexs[i] = indexs[num];
                indexs[num] = temp;
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            int pictureBoxNo = int.Parse(pictureBox.Name.Substring(10));
            int indexNo = indexs[pictureBoxNo-1];
            pictureBox.Image = pictures[indexNo];
            pictureBox.Refresh();

            if (firstPictureBox == null)
            {
                firstPictureBox = pictureBox;
                firstIndex = indexNo;
                attempt++;
            }
            else
            {
                Thread.Sleep(1000);
                firstPictureBox.Image = null;
                pictureBox.Image = null;

                if (firstIndex == indexNo)
                {
                    firstPictureBox.Visible = false;
                    pictureBox.Visible = false;
                    match++;

                    if (match == 8) 
                    {
                        DialogResult dr = MessageBox.Show("Congratulations! You completed the " + attempt + " attempts!" + "\nWould you like to play again?", "You Win", MessageBoxButtons.YesNo);

                        if (dr == DialogResult.No)
                        {
                            Application.Exit();
                        }

                        match = 0;
                        attempt = 0;
                        foreach (Control control in Controls)
                        {
                            control.Visible = true;
                        }
                        images_randomize();
                    }
                }
                firstPictureBox = null;
            }
        }
    }
}