using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Black21
{
    public partial class Play : System.Web.UI.Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=farhan;Initial Catalog=BlackJack;Integrated Security=True;");
        SqlCommand cmd;
        public class Card
        {
            public int Value { get; set; }
            public string Name { get; set; }
        }
        static List<Card> bankercardList = new List<Card>()
        {
            new Card() { Value  = 0, Name = "null" }
        };
        static List<Card> playercardList = new List<Card>()
                {
                    new Card() { Value  = 0, Name = "null" }
                };
        int playercardSum = 0;
        int bankercardSum = 0;
        static int b = 1, n = 1;
        static Random random = new Random();
        List<int> usedCards = new List<int>();

        List<Card> deck = new List<Card>()
            {
                #region spades

                new Card() { Value  = 2, Name = "Two Spades" },
                new Card() { Value = 3, Name = "Three Spades"},
                new Card() { Value = 4, Name =  "Four Spades"},
                new Card() { Value = 5, Name = "Five Spades" },
                new Card() { Value = 6, Name = "Six Spades" },
                new Card() { Value = 7, Name = "Seven Spades" },
                new Card() { Value = 8, Name = "Eight Spades" },
                new Card() { Value = 9, Name = "Nine Spades" },
                new Card() { Value = 10, Name = "Ten Spades"},
                new Card() { Value = 10, Name = "Jack Spades"},
                new Card() { Value = 10, Name = "Queen Spades" },
                new Card(){ Value = 10, Name = "King Spades"},
                new Card(){ Value = 11, Name = "Ace Spades"},

                #endregion

                #region diamonds

                new Card() { Value  = 2, Name = "Two Diamonds" },
                new Card() { Value = 3, Name = "Three Diamonds" },
                new Card() { Value = 4, Name =  "Four Diamonds"},
                new Card() { Value = 5, Name = "Five Diamonds" },
                new Card() { Value = 6, Name = "Six Diamonds" },
                new Card(){ Value = 7, Name = "Seven Diamonds"},
                new Card() { Value = 8, Name = "Eight Diamonds" },
                new Card() { Value = 9, Name = "Nine Diamonds"},
                new Card() { Value = 10, Name = "Ten Diamonds" },
                new Card() { Value = 10, Name = "Jack Diamonds" },
                new Card() { Value = 10, Name = "Queen Diamonds" },
                new Card(){ Value = 10, Name = "King Diamonds" },
                new Card(){ Value = 11, Name = "Ace Diamonds"},

                #endregion

                #region clubs

                new Card() { Value  = 2, Name = "Two Clubs"},
                new Card() { Value = 3, Name = "Three Clubs" },
                new Card() { Value = 4, Name =  "Four Clubs"},
                new Card() { Value = 5, Name = "Five Clubs" },
                new Card() { Value = 6, Name = "Six Clubs" },
                new Card(){ Value = 7, Name = "Seven Clubs" },
                new Card() { Value = 8, Name = "Eight Clubs" },
                new Card() { Value = 9, Name = "Nine Clubs" },
                new Card() { Value = 10, Name = "Ten Clubs"},
                new Card() { Value = 10, Name = "Jack Clubs" },
                new Card() { Value = 10, Name = "Queen Clubs" },
                new Card(){ Value = 10, Name = "King Clubs"},
                new Card(){ Value = 11, Name = "Ace Clubs"},

                #endregion

                #region hearts

                new Card() { Value  = 2, Name = "Two Hearts" },
                new Card() { Value = 3, Name = "Three Hearts" },
                new Card() { Value = 4, Name =  "Four Hearts"},
                new Card() { Value = 5, Name = "Five Hearts" },
                new Card() { Value = 6, Name = "Six Hearts" },
                new Card(){ Value = 7, Name = "Seven Hearts" },
                new Card() { Value = 8, Name = "Eight Hearts"},
                new Card() { Value = 9, Name = "Nine Hearts"},
                new Card() { Value = 10, Name = "Ten Hearts" },
                new Card() { Value = 10, Name = "Jack Hearts" },
                new Card() { Value = 10, Name = "Queen Hearts" },
                new Card(){ Value = 10, Name = "King Hearts" },
                new Card(){ Value = 11, Name = "Ace Hearts"}

                #endregion
            };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sqlCon.Open();
                cmd = new SqlCommand("TRUNCATE TABLE Cards", sqlCon);
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                Hit.Enabled = true;
                Stand.Enabled = true;
                pa.Enabled = false;

                start();
            }
            else
            {
                playercardSum = 0;
                bankercardSum = 0;

                Hit.Enabled = true;
                Stand.Enabled = true;
                pa.Enabled = false;
            }
        }
        private void start()
        {
            playercardSum = 0;
            bankercardSum = 0;
            b = 1;
            n = 1;
            #region init player
            int randomCard1 = selectRandomCard();
            Card card1 = deck[randomCard1];
            usedCards.Add(randomCard1);
            sqlCon.Open();
            cmd = new SqlCommand("insert into Cards(Player) values('" + card1.Name + "')", sqlCon);
            cmd.ExecuteNonQuery();
            sqlCon.Close();
            n++;
            int randomCard2 = selectRandomCard();
            while (usedCards.Contains(randomCard2))
            {
                randomCard2 = selectRandomCard();
            }
            randomCard2 = 1 * randomCard2;
            Card card2 = deck[randomCard2];
            usedCards.Add(randomCard2);
            sqlCon.Open();
            cmd = new SqlCommand("insert into Cards(Player) values('" + card2.Name + "')", sqlCon);
            cmd.ExecuteNonQuery();
            sqlCon.Close();
            n++;
            playercardList.Add(card1);
            playercardList.Add(card2);
            cardsGridView.DataBind();
            #endregion
            #region init banker
            int randomCard3 = selectRandomCard();
            while (usedCards.Contains(randomCard3))
            {
                randomCard3 = selectRandomCard();
            }
            randomCard3 = 1 * randomCard3;
            Card card3 = deck[randomCard3];
            usedCards.Add(randomCard3);
            sqlCon.Open();
            cmd = new SqlCommand("UPDATE Cards SET Dealer='" + card3.Name + "' WHERE Card_No = '" + b + "' ", sqlCon);
            cmd.ExecuteNonQuery();
            sqlCon.Close();
            b++;
            bankercardList.Add(card3);
            cardsGridView.DataBind();
            #endregion

            sumBankerCards();
            sumPlayerCards();
            if (playercardSum == 21)
            {
                String p = playercardSum.ToString();
                ps.Value = p;
                String d = bankercardSum.ToString();
                ds.Value = d;
                String g = "Player wins. Dealer Bust.";
                gs.Value = g;
                sqlCon.Open();
                cmd = new SqlCommand("insert into SessionReport(PlayerTotal,DealerTotal,GameResult) values('" + p + "','" + d + "','" + g + "')", sqlCon);
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                Hit.Enabled = false;
                Stand.Enabled = false;
                pa.Enabled = true;

            }
        }
        protected void Stand_Click(object sender, EventArgs e)
        {
            sumBankerCards();
            while (bankercardSum <= 16)
            {
                int randomCard = selectRandomCard();

                while (usedCards.Contains(randomCard))
                {
                    randomCard = selectRandomCard();
                }
                randomCard = 1 * randomCard;
                Card card = deck[randomCard];
                usedCards.Add(randomCard);
                if (b >= n)
                {
                    sqlCon.Open();
                    cmd = new SqlCommand("insert into Cards(Dealer) values('" + card.Name + "')", sqlCon);
                    cmd.ExecuteNonQuery();
                    sqlCon.Close();
                    b++;
                }
                else
                {
                    sqlCon.Open();
                    cmd = new SqlCommand("UPDATE Cards SET Dealer='" + card.Name + "' WHERE Card_No = '" + b + "'", sqlCon);
                    cmd.ExecuteNonQuery();
                    sqlCon.Close();
                    b++;
                }
                bankercardList.Add(card);
                cardsGridView.DataBind();
                sumBankerCards();
            }

            sumPlayerCards();
            if (bankercardSum > 21)
            {
                String p = playercardSum.ToString();
                ps.Value = p;
                String d = bankercardSum.ToString();
                ds.Value = d;
                String g = "Player wins. Dealer Bust.";
                gs.Value = g;
                sqlCon.Open();
                cmd = new SqlCommand("insert into SessionReport(PlayerTotal,DealerTotal,GameResult) values('" + p + "','" + d + "','" + g + "')", sqlCon);
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                Hit.Enabled = false;
                Stand.Enabled = false;
                pa.Enabled = true;
            }
            else if (playercardSum <= bankercardSum)
            {
                String p = playercardSum.ToString();
                ps.Value = p;
                String d = bankercardSum.ToString();
                ds.Value = d;
                String g = "Dealer wins, Player bust";
                gs.Value = g;
                sqlCon.Open();
                cmd = new SqlCommand("insert into SessionReport(PlayerTotal,DealerTotal,GameResult) values('" + p + "','" + d + "','" + g + "')", sqlCon);
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                Hit.Enabled = false;
                Stand.Enabled = false;
                pa.Enabled = true;
            }
            else
            {
                String p = playercardSum.ToString();
                ps.Value = p;
                String d = bankercardSum.ToString();
                ds.Value = d;
                String g = "Player wins. Dealer Bust.";
                gs.Value = g;
                sqlCon.Open();
                cmd = new SqlCommand("insert into SessionReport(PlayerTotal,DealerTotal,GameResult) values('" + p + "','" + d + "','" + g + "')", sqlCon);
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                Hit.Enabled = false;
                Stand.Enabled = false;
                pa.Enabled = true;
            }
        }

        protected void Hit_Click(object sender, EventArgs e)
        {
            int randomCard = selectRandomCard();
            while (usedCards.Contains(randomCard))
            {
                randomCard = selectRandomCard();
            }
            randomCard = 1 * randomCard;
            Card card = deck[randomCard];
            usedCards.Add(randomCard);
            playercardList.Add(card);

            sqlCon.Open();
            cmd = new SqlCommand("insert into Cards(Player) values('" + card.Name + "')", sqlCon);
            cmd.ExecuteNonQuery();
            sqlCon.Close();
            n++;
            cardsGridView.DataBind();
            sumPlayerCards();
            sumBankerCards();
            if (playercardSum > 21)
            {
                String p = playercardSum.ToString();
                ps.Value = p;
                String d = bankercardSum.ToString();
                ds.Value = d;
                String g = "Dealer wins, Player bust";
                gs.Value = g;
                sqlCon.Open();
                cmd = new SqlCommand("insert into SessionReport(PlayerTotal,DealerTotal,GameResult) values('" + p + "','" + d + "','" + g + "')", sqlCon);
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                Hit.Enabled = false;
                Stand.Enabled = false;
                pa.Enabled = true;
            }
            else if (playercardSum == 21)
            {
                String p = playercardSum.ToString();
                ps.Value = p;
                String d = bankercardSum.ToString();
                ds.Value = d;
                String g = "Player wins. Dealer Bust.";
                gs.Value = g;
                sqlCon.Open();
                cmd = new SqlCommand("insert into SessionReport(PlayerTotal,DealerTotal,GameResult) values('" + p + "','" + d + "','" + g + "')", sqlCon);
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                Hit.Enabled = false;
                Stand.Enabled = false;
                pa.Enabled = true;
            }
        }
        protected void pa_Click(object sender, EventArgs e)
        {
            resetGame();
            start();
        }
        protected void sr_Click(object sender, EventArgs e)
        {
            Response.Redirect("SessionReport.aspx");
        }
        public int selectRandomCard()
        {
            int randomCard;
            randomCard = random.Next(0, deck.Count);
            return randomCard;
        }
        public void resetGame()
        {
            ps.Value = "";
            ds.Value = "";
            gs.Value = "";
            playercardSum = 0;
            bankercardSum = 0;
            playercardList.Clear();
            bankercardList.Clear();
            usedCards.Clear();
            sqlCon.Open();
            cmd = new SqlCommand("TRUNCATE TABLE Cards", sqlCon);
            cmd.ExecuteNonQuery();
            sqlCon.Close();
        }
        public void sumPlayerCards()
        {
            playercardSum = 0;
            for (int i = 0; i < playercardList.Count; i++)
            {
                playercardSum += playercardList[i].Value;
            }

            if (playercardSum > 21)
            {
                foreach (Card c in playercardList)
                {
                    if (c.Value == 11)
                    {
                        playercardSum -= 10;
                        if (playercardSum <= 21)
                        {
                            break;
                        }
                    }
                }
            }
        }
        public void sumBankerCards()
        {
            bankercardSum = 0;
            for (int i = 0; i < bankercardList.Count; i++)
            {
                bankercardSum += bankercardList[i].Value;
            }
            if (bankercardSum > 21)
            {
                foreach (Card c in bankercardList)
                {
                    if (c.Value == 11)
                    {
                        bankercardSum -= 10;
                        if (bankercardSum <= 21)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}