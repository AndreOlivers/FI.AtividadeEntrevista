﻿
namespace WebAtividadeEntrevista.DTOs
{
    public class Beneficiario
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public long? ClienteId { get; set; }
    }
}