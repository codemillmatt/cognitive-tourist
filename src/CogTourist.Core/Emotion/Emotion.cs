using System;
using Newtonsoft.Json;

namespace CogTourist.Core
{
    public class UploadEmotion
    {
        public string url { get; set; }
    }

	public class Scores
	{
		[JsonProperty("anger")]
		public double Anger { get; set; }

		[JsonProperty("contempt")]
		public double Contempt { get; set; }

		[JsonProperty("disgust")]
		public double Disgust { get; set; }

		[JsonProperty("fear")]
		public double Fear { get; set; }

		[JsonProperty("happiness")]
		public double Happiness { get; set; }

		[JsonProperty("neutral")]
		public double Neutral { get; set; }

		[JsonProperty("sadness")]
		public double Sadness { get; set; }

		[JsonProperty("surprise")]
		public double Surprise { get; set; }
	}

	public class Emotion
	{

		[JsonProperty("faceRectangle")]
		public FaceRectangle FaceRectangle { get; set; }

		[JsonProperty("scores")]
		public Scores Scores { get; set; }
	}

    public class FaceRectangle
    {
        [JsonProperty("left")]
        public int Left { get; set; }

        [JsonProperty("top")]
        public int Top { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }

}
