using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingCalculator {
  public class Game {
    public List<IFrame> Frames { get; }

    public int TotalScore => Frames.Sum(f => f.FrameScore);

    public Game() {
      Frames = new List<IFrame>();
    }

    public void ConvertScorecard(string[] scorecard) {
      for (int i = 0; i < scorecard.Length; i++) {
        Frame frame = new();
        frame.FirstScore = scorecard[i];

        if (!frame.IsStrike || Frames.Count == 9) {
          i++;
          frame.SecondScore = scorecard[i];
        }

        if (Frames.Count == 9 && i+1 < scorecard.Length) {
          i++;
          frame.ThirdScore = scorecard[i];
        }

        Frames.Add(frame);
      }
    }

    public void CalculateFrameScores() {
      for (int i = 0; i < Frames.Count; i++) {
        // If a strike (and not last frame), need to add the next two scores to this frame
        if (Frames[i].IsStrike && i + 1 != Frames.Count) {
          CalculateFrameScoreForStrike(i);
        }

        // If a spare (and not last frame), need to add the next score to this frame
        else if (Frames[i].IsSpare && i + 1 != Frames.Count) {
          CalculateFrameScoreForSpare(i);
        }

        // If not a strike or a spare (and not last frame), simple to work out
        else if (i + 1 != Frames.Count) {
          Frames[i].FrameScore = Convert.ToInt32(Frames[i].FirstScore) + Convert.ToInt32(Frames[i].SecondScore);
        }

        // Special scenario for final frame (due to possible three bowls)
        else {
          CalculateFrameScoreForFinalFrame(i);
        }
      }
    }

    private void CalculateFrameScoreForStrike(int i) {
      Frames[i].FrameScore = 10; // Strike starts at 10

      if (Frames[i + 1].IsStrike) {
        Frames[i].FrameScore += 10; // Add 10 for second strike

        // Special case for 9th frame - if the first bowl from 10th frame is a strike, need to add the value from the second bowl
        if (i + 2 == Frames.Count) {
          Frames[i].FrameScore += Frames[i + 1].HasSecondStrike ? 10 : Convert.ToInt32(Frames[i + 1].SecondScore);
        }
        else if (Frames[i + 2].IsStrike) {
          Frames[i].FrameScore += 10; // Add 10 for third strike
        }
        else {
          Frames[i].FrameScore += Convert.ToInt32(Frames[i + 2].FirstScore);
        }
      }
      else {
        if (Frames[i + 1].IsSpare) {
          Frames[i].FrameScore += 10; // Add 10 for spare
        }
        else {
          Frames[i].FrameScore += Convert.ToInt32(Frames[i + 1].FirstScore);
          Frames[i].FrameScore += Convert.ToInt32(Frames[i + 1].SecondScore);
        }
      }
    }

    private void CalculateFrameScoreForSpare(int i) {
      Frames[i].FrameScore = 10; // Spare starts at 10

      if (Frames[i + 1].IsStrike) {
        Frames[i].FrameScore += 10; // Add 10 for strike
      } else {
        Frames[i].FrameScore += Convert.ToInt32(Frames[i + 1].FirstScore);
      }
    }

    private void CalculateFrameScoreForFinalFrame(int i) {
      // If there is a third score, it means there was either a strike or spare
      if (!string.IsNullOrEmpty(Frames[i].ThirdScore)) {
        if (Frames[i].IsStrike || Frames[i].IsSpare) Frames[i].FrameScore = 10; // Strike & Spare starts at 10

        if (Frames[i].HasSecondStrike) Frames[i].FrameScore += 10; // Add 10 for second strike
        else if (!Frames[i].HasSecondStrike && !Frames[i].IsSpare && !Frames[i].HasSecondSpare) Frames[i].FrameScore += Convert.ToInt32(Frames[i].SecondScore);

        if (Frames[i].HasThirdStrike || Frames[i].HasSecondSpare) Frames[i].FrameScore += 10; // Add 10 for third strike or spare
        else Frames[i].FrameScore += Convert.ToInt32(Frames[i].ThirdScore);
      } else {
        // No third score, which means no strike or spare, so just add values
        Frames[i].FrameScore = Convert.ToInt32(Frames[i].FirstScore) + Convert.ToInt32(Frames[i].SecondScore);
      }
    }
  }
}
