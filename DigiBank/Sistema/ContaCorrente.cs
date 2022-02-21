namespace DigiBank.Sistema
{
    public class ContaCorrente : Conta
    {
        public ContaCorrente()
        {
            this.NumeroConta = "00" + Conta.NumeroDaContaSquencial;
        }
    }
}
