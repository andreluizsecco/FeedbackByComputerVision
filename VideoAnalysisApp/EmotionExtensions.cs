using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.Collections.Generic;
using System.Linq;

namespace VideoAnalysisApp
{
    public static class EmotionExtensions
    {
        public static IEnumerable<KeyValuePair<string, double>> ToRankedList(this Emotion emotionScores)
        {
            return new Dictionary<string, double>()
            {
                { "Anger", emotionScores.Anger },
                { "Contempt", emotionScores.Contempt },
                { "Disgust", emotionScores.Disgust },
                { "Fear", emotionScores.Fear },
                { "Happiness", emotionScores.Happiness },
                { "Neutral", emotionScores.Neutral },
                { "Sadness", emotionScores.Sadness },
                { "Surprise", emotionScores.Surprise }
            }
            .OrderByDescending(emotion => emotion.Value)
            .ThenBy(emotion => emotion.Key);
        }

        public static string GetTopHairColor(this IList<HairColor> hairColors)
        {
            if (hairColors.Count == 0)
                return string.Empty;

            return hairColors
                .OrderByDescending(hair => hair.Confidence)
                .FirstOrDefault().Color.ToString();
        }
    }
}
