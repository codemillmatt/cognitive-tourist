namespace CogTourist.Core
{
    public static class CognitiveServiceLogin
    {
        public static readonly string VisionAPIKey = "d4aac7248b344c43a617882035202e5c";
        public static readonly string TranslateAPIKey = "f3c9a0877af948b6bcccf6dd47222ca2";
        public static readonly string EmotionAPIKey = "b1890d4f12134ef7a6f9e7d694838c33";
        public static readonly string VisionUrl = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0";
        public static readonly string TranslateTokenUrl = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";
        public static readonly string TranslateUrl = "https://api.microsofttranslator.com/V2/Http.svc/Translate";
        public static readonly string EmotionUrl = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize";
    }
}