namespace CogTourist.Core
{
    public static class CognitiveServiceLogin
    {
        public static readonly string APIKey = "d4aac7248b344c43a617882035202e5c";
        public static readonly string TranslateAPIKey = "f3c9a0877af948b6bcccf6dd47222ca2";
        public static readonly string VisionUrl = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0";
        public static readonly string TranslateTokenUrl = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";
        public static readonly string TranslateUrl = "https://api.microsofttranslator.com/V2/Http.svc/Translate";
    }

    public static class LanguageAbbreviations
    {
        public static readonly string English = "en";
        public static readonly string French = "fr";
        public static readonly string German = "de";
        public static readonly string Italian = "it";
        public static readonly string Japanese = "ja";
        public static readonly string Chinese = "zh-CHS";
    }
}