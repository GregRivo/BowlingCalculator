namespace BowlingCalculator {
  public class Frame : IFrame {
    public string FirstScore { get; set; }

    public string SecondScore { get; set; }

    public string ThirdScore { get; set; }

    public int FrameScore { get; set; }

    public bool IsStrike => FirstScore == "X";

    public bool IsSpare => SecondScore == "/";

    public bool HasSecondStrike => SecondScore == "X";

    public bool HasSecondSpare => ThirdScore == "/";

    public bool HasThirdStrike => ThirdScore == "X";
  }
}
