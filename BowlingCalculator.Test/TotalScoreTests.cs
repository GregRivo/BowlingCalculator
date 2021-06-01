using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingCalculator.Test {
  [TestClass]
  public class TotalScoreTests {
    [TestMethod]
    public void TotalScore_ShouldEqual() {
      Game game = new();
      game.ConvertScorecard(new string[]{ "7", "/", "X", "4", "/", "1", "0", "X", "X", "X", "9", "/", "X", "X", "7", "/"});
      game.CalculateFrameScores();

      Assert.AreEqual(198, game.TotalScore);
    }

    [TestMethod]
    public void TotalScore_ShouldEqual300_WhenAllStrikes() {
      Game game = new();
      game.ConvertScorecard(new string[] { "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X"});
      game.CalculateFrameScores();

      Assert.AreEqual(300, game.TotalScore);
    }

    [TestMethod]
    public void TotalScore_ShouldEqual150_WhenAllSpares() {
      Game game = new();
      game.ConvertScorecard(new string[] { "5", "/", "5", "/", "5", "/", "5", "/", "5", "/", "5", "/", "5", "/", "5", "/", "5", "/", "5", "/", "5" });
      game.CalculateFrameScores();

      Assert.AreEqual(150, game.TotalScore);
    }

    [TestMethod]
    public void TotalScore_ShouldEqual0_WhenAllGutterballs() {
      Game game = new();
      game.ConvertScorecard(new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" });
      game.CalculateFrameScores();

      Assert.AreEqual(0, game.TotalScore);
    }
  }
}
