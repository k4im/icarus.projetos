namespace projeto.service.tests.Helpers
{
    public class FakeProjeto
    {
        public static Projeto FactoryProjeto()
            => new Projeto("Projeto FAKE", "status", DateTime.UtcNow, DateTime.UtcNow.AddHours(1), 1, 5, "asd", 155.5);

        public static Projeto FactoryProjetoNameError()
            => new Projeto("Projeto F@KE", "status", DateTime.UtcNow, DateTime.UtcNow.AddHours(1), 1, 5, "asd", 155.5);
        public static Projeto FactoryProjetoDescError()
            => new Projeto("Projeto FAKE", "status", DateTime.UtcNow, DateTime.UtcNow.AddHours(1), 1, 5, "descric@ão", 155.5);
        public static Projeto FactoryProjetoDescErrorNull()
                    => new Projeto("Projeto FAKE", "string.Empty", DateTime.UtcNow, DateTime.UtcNow.AddHours(1), 1, 5, string.Empty, 155.5);

        public static Projeto FactoryProjetoValMenorQueZero()
                            => new Projeto("Projeto FAKE", "string.Empty", DateTime.UtcNow, DateTime.UtcNow.AddHours(1), 1, 5, "stringEmpty", -1);


        public static Projeto FactoryProjetoNameErrorNull()
            => new Projeto(string.Empty, "status", DateTime.UtcNow, DateTime.UtcNow.AddHours(1), 1, 5, "asd", 155.5);

        public static List<Projeto> FactoryListaProjetos()
        {
            return new List<Projeto>{
                FactoryProjeto(),
                FactoryProjeto(),
                FactoryProjeto(),
                FactoryProjeto(),
                FactoryProjeto()
            };
        }
    }
}