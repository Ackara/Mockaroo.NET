namespace Mockaroo
{
    public struct ExitCode
    {
        public const int Success = 0;
        public const int ParsingError = 1;
        public const int CommunicationError = 2;
        public const int UnknownError = 99;
    }
}