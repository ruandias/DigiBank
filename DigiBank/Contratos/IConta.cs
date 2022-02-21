using DigiBank.Sistema;
using System.Collections.Generic;

namespace DigiBank.Contratos
{
    public interface IConta
    {
        void Deposita(double valor);
        bool Saca(double valor);
        double ConsultaSaldo();
        string GetCodigoDoBanco();
        string GetNumeroAgencia();
        string GetNumeroDaConta();
        List<Extrato> Extrato();
    }
}
