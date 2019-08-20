using App.Base.Domain.Common;

namespace App.Basic.Domain.AggregateModels.UserAggregate
{
    public class LanguageType : Enumeration
    {
        public static LanguageType SimplifiedChinese = new SimplifiedChineseType();
        public static LanguageType TraditionalChinese = new TraditionalChineseType();
        public static LanguageType AmericanEnglish = new AmericanEnglishType();


        public LanguageType(int id, string name, string description)
        : base(id, name, description)
        {

        }


        private class SimplifiedChineseType : LanguageType
        {
            public SimplifiedChineseType()
                : base(0, "简体中文", "zh-CN")
            {
            }
        }

        private class TraditionalChineseType : LanguageType
        {
            public TraditionalChineseType()
                : base(1, "繁體中文", "zh-TW")
            {
            }
        }

        private class AmericanEnglishType : LanguageType
        {
            public AmericanEnglishType()
                : base(1, "英语", "en-US")
            {
            }
        }

    }
}
