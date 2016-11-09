using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace dog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Point p1 = new Point(25, 35);
        Point p2 = new Point(25, 91);
        Point p3 = new Point(25, 149);
        Point p4 = new Point(25, 200);
        int winner;
        private int Joe_a;
        private int Bob_a;
        private int Al_a;
        static Guy Joe = new Guy("Joe", 50);
        static Guy Bob = new Guy("Bob", 75);
        static Guy Al = new Guy("Al", 45);
        static Bet Joe_b = new Bet(Joe);
        static Bet Bob_b = new Bet(Bob);
        static Bet Al_b = new Bet(Al);
        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            pictureBox2.Location = p1;
            pictureBox3.Location = p2;
            pictureBox4.Location = p3;
            pictureBox5.Location = p4;
            Greyhound g1 = new Greyhound(pictureBox2, 25);
            Greyhound g2 = new Greyhound(pictureBox3, 25);
            Greyhound g3 = new Greyhound(pictureBox4, 25);
            Greyhound g4 = new Greyhound(pictureBox5, 25);
            bool a1, a2, a3, a4;
            while (true)
            {
                //Thread.Sleep(100);
                a1 = g1.Run();
                a2 = g2.Run();
                a3 = g3.Run();
                a4 = g4.Run();
                if (a1 || a2 || a3 || a4)
                {
                    int count = 0;
                    if (a1) count++;
                    if (a2) count++;
                    if (a3) count++;
                    if (a4) count++;
                    bool[] a = { a1, a2, a3, a4 };
                    Greyhound[] g = { g1, g2, g3, g4 };
                    if (count == 1)
                    {
                        for (int i = 0; i <= 3; i++)
                        {
                            if (a[i])
                            {                               
                                button1.Enabled = true;
                                button2.Enabled = false;
                                winner = i + 1;
                                Joe_a = Joe_b.payout(winner);
                                Bob_a = Bob_b.payout(winner);
                                Al_a = Al_b.payout(winner);
                                Joe.cash += Joe_a;
                                Bob.cash += Bob_a;
                                Al.cash += Al_a;
                                Joe.updatelable();
                                Bob.updatelable();
                                Al.updatelable();
                                Joe.ClearBet();
                                Bob.ClearBet();
                                Al.ClearBet();
                                MessageBox.Show((i + 1).ToString() + "号狗赢了");
                                break;
                            } 
                        }
                        break;
                    }
                    else
                    {
                        int c = compare(g1.mypicturebox.Location.X, g2.mypicturebox.Location.X, g3.mypicturebox.Location.X, g4.mypicturebox.Location.X);

                        for (int i = 0; i <= 3; i++)
                        {
                            if (g[i].mypicturebox.Location.X == c)
                            {
                                winner = i + 1;
                                Joe_a = Joe_b.payout(winner);
                                Bob_a = Bob_b.payout(winner);
                                Al_a = Al_b.payout(winner);
                                Joe.cash += Joe_a;
                                Bob.cash += Bob_a;
                                Al.cash += Al_a;
                                Joe.updatelable();
                                Bob.updatelable();
                                Al.updatelable();
                                Joe.ClearBet();
                                Bob.ClearBet();
                                Al.ClearBet();
                                MessageBox.Show((i + 1).ToString() + "号狗赢了");
                                break;
                            }                                
                        }
                        button1.Enabled = true;
                        button2.Enabled = false;
                        break;
                    }
                   
                }
            }
        }
        public int compare(int a, int b, int c, int d)
        {
            int e;
            e = Math.Max(a, b);
            e = Math.Max(e, c);
            e = Math.Max(e, d);
            return e;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Joe.mybet = Joe_b;
            Bob.mybet = Bob_b;
            Al.mybet = Al_b;
            if (radioButton1.Checked)
            {               
                Joe_b.Amount = (int)numericUpDown1.Value; Joe_b.dog = (int)numericUpDown2.Value;
                Joe.cash -= Joe_b.Amount;
                label2.Text = Joe.name;
                Joe.updatelable();
            }
            else if (radioButton2.Checked)
            {                
                Bob_b.Amount = (int)numericUpDown1.Value; Bob_b.dog = (int)numericUpDown2.Value;
                Bob.cash -= Bob_b.Amount;
                label2.Text = Bob.name;
                Bob.updatelable();
            }
            else
            {
                Al_b.Amount = (int)numericUpDown1.Value; Al_b.dog = (int)numericUpDown2.Value;
                Al.cash -= Al_b.Amount;
                label2.Text = Al.name;
                Al.updatelable();
            }
                button2.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Joe.mylabel = label5;
            Joe.myradiobutton = radioButton1;
            Bob.mylabel = label6;
            Bob.myradiobutton = radioButton2;
            Al.mylabel = label7;
            Al.myradiobutton = radioButton3;
        }
    }
    public class Greyhound
        {
            //public int startingposition;
            public PictureBox mypicturebox;
            public int Location;//25,472
            public Random Randomizer;
            public Greyhound(PictureBox picture, int start)
            {
                mypicturebox = picture;
                Location = start;
            }
            public bool Run()
            {
            Randomizer = new Random();
            Point p = mypicturebox.Location;
            p.X += Randomizer.Next(20);
            mypicturebox.Location = p;
            Location = p.X;
            if (mypicturebox.Location.X >= 472)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    public class Bet
    {
        public int Amount = 0;
        public int dog = 0;
        public Guy guy;
        public Bet(Guy guy)
        {
            this.guy = guy;
        }
        public string GetDescription()
        {
            if (Amount == 0)
                return "没有投注";
            else
            {
                return guy.name + "投注了" + dog.ToString() + "号" + Amount.ToString() + "元";
            }
        }
        public int payout(int winner)
        {
            if (winner == dog)
            {
                return 2 * Amount;
            }
            else
            {
                return 0;
            }
        }
    }
    public class Guy
    {
        public string name;
        public Bet mybet;
        public int cash;
        public RadioButton myradiobutton;
        public Label mylabel;
        public Guy(string name, int cash)
        {
            this.name = name;
            this.cash = cash;
        }
        public void updatelable()
        {
            if (mybet == null)
            {
                mylabel.Text = "没有投注";
                myradiobutton.Text = name + "的余额为" + cash + "元";
            }
            else
            {
                mylabel.Text = name + "投注了" + mybet.dog.ToString() + "号" + mybet.Amount.ToString() + "元"; ;
                myradiobutton.Text = name + "的余额为" + cash.ToString() + "元";
            } 
        }
        public void ClearBet()
        {
            mybet = null;
            updatelable();
        }
    }
}
