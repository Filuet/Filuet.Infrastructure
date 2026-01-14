namespace Filuet.Infrastructure.Abstractions.Helpers
{
    internal static class UzbekHelper
    {
        internal static char[] CyrillicAlphabet = ['а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'ь', 'ъ', 'э', 'ю', 'я', 'ў', 'қ', 'ғ', 'ҳ' ];
        internal static char[] LatinCharactersInException = ['w', 'W'];
        internal static char[] CyrillicCharactersInException = ['щ', 'Щ', 'ы', 'Ы'];
        internal static string[] SpecificLetters = ["oʻ", "Oʻ", "o'", "O'", "o`", "O`", "gʻ", "Gʻ", "g'", "G'", "g`", "G`", "ʼ", // in latin alphabet
            "ў", "Ў", "қ", "Қ", "ғ", "Ғ", "ҳ", "Ҳ" ]; // in cyrillic alphabet
        internal static string[] Prepositions = ["bilan", "uchun", "davomida", "ustidan", "hatto", "orqali", "oldidan", "haqida", "tufayli", "ichida", "orasida", "qadar", "gacha",
            "билан", "учун", "орқали", "устида", "остида", "ичида", "ён", "ёнида", "ораси"];
        internal static string[] Conjunctions = ["va", "ham", "hamda", "ammo", "lekin", "birok", "holbuki", "balki", "yoki", "agar",
            "ва", "ёки", "ё", "лекин", "аммо", "бирок", "бироқ", "чунки", "сабаби", "хам", "ҳам", "хамда", "ҳамда", "агар", "ҳолда"];
    }
}