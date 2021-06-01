namespace BowlingCalculator {
  public interface IFrame {
    string FirstScore { get; set; }

    string SecondScore { get; set; }

    string ThirdScore { get; set; }

    int FrameScore { get; set; }

    bool IsStrike { get; }

    bool IsSpare { get; }

    bool HasSecondStrike { get; }

    bool HasSecondSpare { get; }

    bool HasThirdStrike { get; }
  }
}
