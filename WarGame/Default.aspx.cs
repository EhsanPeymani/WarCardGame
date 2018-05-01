using CardGames;
using System;
using System.Web.UI;

namespace WarGame
{
    public partial class Default : System.Web.UI.Page
    {
        CardGameWar warGame;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                warGame = new CardGameWar("Player 1", "Player 2");
                Session["WarGame"] = warGame;
            }

            Page.MaintainScrollPositionOnPostBack = true;
        }

        protected void deckButton_Click(object sender, EventArgs e)
        {
            warGame = (CardGameWar)Session["WarGame"];

            resultLabel.Text = Print.PrintHeader("Deck of Cards ...");
            resultLabel.Text += Print.PrintDeck(warGame.Deck);
        }

        protected void shuffleDeckButton_Click(object sender, EventArgs e)
        {
            warGame = (CardGameWar)Session["WarGame"];
            warGame.ShuffleDeck();
            Session["WarGame"] = warGame;

            resultLabel.Text = Print.PrintHeader("Shuffling deck ...");
            resultLabel.Text += Print.PrintDeck(warGame.Deck);
        }

        protected void dealCardsButton_Click(object sender, EventArgs e)
        {
            warGame = (CardGameWar)Session["WarGame"];

            // clear the hands of players
            if (warGame.Player1.NumberOfCardsHand > 0) warGame.Player1.PlayerHand.Hand.Clear();
            if (warGame.Player2.NumberOfCardsHand > 0) warGame.Player2.PlayerHand.Hand.Clear();

            warGame.DealAllCards(DealMethod.Even, 2);
            Session["WarGame"] = warGame;

            resultLabel.Text = Print.PrintHeader("Dealing cards ...");
            resultLabel.Text += Print.PrintAllPlayersHand(warGame);
        }

        protected void shuffleDeal_Click(object sender, EventArgs e)
        {
            warGame = (CardGameWar)Session["WarGame"];

            // clear the hands of players
            if (warGame.Player1.NumberOfCardsHand > 0) warGame.Player1.PlayerHand.Hand.Clear();
            if (warGame.Player2.NumberOfCardsHand > 0) warGame.Player2.PlayerHand.Hand.Clear();

            warGame.ShuffleAndDeal(DealMethod.Even, 2);
            Session["WarGame"] = warGame;

            resultLabel.Text = Print.PrintHeader("Shuffling deck and dealing cards ...");
            resultLabel.Text += Print.PrintAllPlayersHand(warGame);
        }

        protected void playerHandButton_Click(object sender, EventArgs e)
        {
            warGame = (CardGameWar)Session["WarGame"];

            resultLabel.Text = Print.PrintHeader("Printing players' hands ...");
            resultLabel.Text += Print.PrintAllPlayersHand(warGame);
        }

        protected void playRoundButton_Click(object sender, EventArgs e)
        {
            warGame = (CardGameWar)Session["WarGame"];
            warGame.PlayRound();
            warGame.DecideRoundWinner();
                
            Session["WarGame"] = warGame;

            resultLabel.Text += Print.PrintRoundResult(warGame);
        }

        protected void playGameButton_Click(object sender, EventArgs e)
        {
            var warGame = new CardGameWar("Player 1", "Player 2", WinType.EmptyHand);
            warGame.ShuffleAndDeal(DealMethod.Even, 2);

            resultLabel.Text = string.Empty;
            resultLabel.Text = Print.PrintHeader("Battle Starts");

            warGame.SetStartTime();

            while (warGame.ContinueGame())
            {
                warGame.PlayRound();
                warGame.DecideRoundWinner();

                resultLabel.Text += Print.PrintRoundResult(warGame);
            }

            warGame.SetEndTime();
            warGame.FinalWinner();

            resultLabel.Text += Print.PrintFinalWinner(warGame);
        }
    }
}