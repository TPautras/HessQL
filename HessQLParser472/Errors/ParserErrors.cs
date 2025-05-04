namespace HessQLParser.Errors
{
    public static class ParserErrors
    {
        public const string UnexpectedEOF = "Erreur syntaxique : fin de fichier inattendue alors qu'une expression était attendue.";
        public const string NudNotFound = "NUD handler not found for token {0}";
        public const string LedNotFound = "LED handler not found for token {0}";
        public const string TopNotAnInteger = "La valeur TOP doit être un entier";
        public const string LimitNotAnInteger = "La valeur LIMIT doit être un entier";
        public const string OffsetNotAnInteger = "La valeur OFFSET doit être un entier";
        public const string AlterMissingRename = "ALTER TABLE attend le mot-clé RENAME.";
        public const string AlterMissingTo = "ALTER TABLE RENAME attend le mot-clé TO.";
    }
}