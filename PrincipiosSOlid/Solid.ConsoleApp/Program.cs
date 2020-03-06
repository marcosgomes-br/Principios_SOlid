using Solid.ConsoleApp.Models;
using Solid.Domain;
using Solid.Repository;
using Solid.Validators;
using System;
using System.Linq;

namespace Solid.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuPrincipal();
        }
        #region Menus
        private static void MenuPrincipal()
        {
            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("||===== SISTEMA DE RESERVAS DE QUADRAS =====||");
                Console.WriteLine("||                                          ||");
                Console.WriteLine("|| 1 - Ver Quadras                          ||");
                Console.WriteLine("|| 2 - Adicionar Quadra                     ||");
                Console.WriteLine("|| 0 - Sair                                 ||");
                Console.WriteLine("||                                          ||");
                Console.WriteLine("||==========================================||");
                opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        MostrarQuadras();
                        break;
                    case "2":
                        RegistrarQuadras();
                        break;
                    case "0":
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Valor Inválido, tente novamente");
                        break;
                }
            } while (opcao != "0");
        }
        private static int MenuDetalhesQuadra()
        {
            Console.WriteLine("\n1 - Excluir");
            Console.WriteLine("2 - Dar Manutenção");
            Console.WriteLine("0 - Voltar");
            return int.Parse(Console.ReadLine());
        }
        #endregion
        #region Funções Menu Principal
        private static void MostrarQuadras()
        {
            Console.Clear();
            Console.WriteLine("1 - Quadras de Cimento");
            Console.WriteLine("2 - Quadras de Madeira");
            Console.WriteLine("3 - Quadras de Grama");
            Console.WriteLine("0 - Voltar ao Menu Principal");
            var opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1":
                    ListarQuadrasCimento();
                    break;
                case "2":
                    ListarQuadrasMadeira();
                    break;
                case "3":
                    ListarQuadrasGrama();
                    break;
                case "0":
                    MenuPrincipal();
                    break;
                default:
                    Console.WriteLine("Valor Inválido, tente novamente");
                    MostrarQuadras();
                    break;
            }
        }
        private static void RegistrarQuadras()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRO DE NOVA QUADRA ===");
            Console.WriteLine("Selecione o TIPO de quadra a registrar:");
            Console.WriteLine("1 - QUADRA DE CIMENTO");
            Console.WriteLine("2 - QUADRA DE MADEIRA");
            Console.WriteLine("3 - QUADRA DE GRAMA");
            Console.WriteLine("0 - VOLTAR AO MENU PRINCIPAL");
            var opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1":
                    AdicionarQuadraCimento();
                    break;
                case "2":
                    AdicionarQuadraMadeira();
                    break;
                case "3":
                    AdicionarQuadraGrama();
                    break;
                case "0":
                    MenuPrincipal();
                    break;
                default:
                    Console.WriteLine("Valor Inválido");
                    break;
            }
        }
        #endregion
        #region Adicionar Quadras
        private static void AdicionarQuadraGrama()
        {
            MsgPadraoAddQuadra();
            var quadra = new QuadraGramaDomain();
            var q = AddQuadraGenerica(quadra);
            quadra.Endereco = q.Endereco;
            quadra.DimensoesEmMetros = q.DimensoesEmMetros;
            quadra.DataCadastro = q.DataCadastro;
            Console.WriteLine("Insira a altura IDEAL da Grama: (cm) ");
            quadra.AlturaIdealGrama = Console.ReadLine();
            Console.WriteLine("Insira a altura ATUAL da Grama: (cm) ");
            quadra.AlturaGramaCm = Console.ReadLine();
            var ret = new QuadraGramaRepository().Salvar(quadra);
            if(MsgFinalAdicionamento(ret)) AdicionarQuadraGrama();
        }
        private static void AdicionarQuadraMadeira()
        {
            MsgPadraoAddQuadra();
            var quadra = new QuadraMadeiraDomain();
            var q = AddQuadraGenerica(quadra);
            quadra.Endereco = q.Endereco;
            quadra.DimensoesEmMetros = q.DimensoesEmMetros;
            quadra.DataCadastro = q.DataCadastro;
            Console.WriteLine("Insira a quantidade de madeiras soltas: ");
            quadra.QuantidadeMadeiraSolta = Convert.ToInt32(Console.ReadLine());
            var ret = new QuadraMadeiraRepository().Salvar(quadra);
            if(MsgFinalAdicionamento(ret)) AdicionarQuadraMadeira();
        }
        private static void AdicionarQuadraCimento()
        {
            MsgPadraoAddQuadra();
            var quadra = new QuadraCimentoDomain();
            var q = AddQuadraGenerica(quadra);
            quadra.Endereco = q.Endereco;
            quadra.DimensoesEmMetros = q.DimensoesEmMetros;
            quadra.DataCadastro = q.DataCadastro;
            Console.WriteLine("Insira a cor da quadra: ");
            quadra.Cor = Console.ReadLine();
            Console.WriteLine("Insira a Qualidade da Pintura: ");
            quadra.QualidadePintura = Console.ReadLine();
            var ret = new QuadraCimentoRepository().Salvar(quadra);
            if(MsgFinalAdicionamento(ret)) AdicionarQuadraCimento();
        }
        #endregion
        #region Listar Quadras
        private static void ListarQuadrasCimento()
        {
            Console.Clear();
            var quadras = new QuadraCimentoRepository().ListarTodos();
            VerificaSeHaQuadras(quadras.Count);
            Console.WriteLine("=== QUADRAS DE CIMENTO ===\n\n");
            foreach (QuadraDomain quadra in quadras)
            {
                ListarQuadraNaTela(quadra);
            }
            int id = SelecionarQuadra();
            try
            {

                Console.Clear();
                Console.WriteLine("=== QUADRA DE CIMENTO ===\n\n");
                var quadra = quadras.Single(x => x.Id == id);
                ListarQuadraNaTela(quadra);
                var precisaReparo = new QuadraCimentoValidator().Validate(quadra);
                if (!precisaReparo.IsValid)
                {
                    Console.WriteLine(precisaReparo.ToString());
                    Console.WriteLine("Qualidade da Pintura: " + quadra.QualidadePintura);
                }
                int opcao = MenuDetalhesQuadra();
                switch (opcao)
                {
                    case 1:
                        new QuadraCimentoRepository().Deletar(quadra);
                        break;
                    case 2:
                        new QuadraCimentoModel().Reparar(quadra);
                        break;
                    case 0:
                        MostrarQuadras();
                        break;
                    default:
                        Console.WriteLine("Inválido");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Insira um ID válido!");
                ListarQuadrasCimento();
            }
        }
        private static void ListarQuadrasGrama()
        {
            Console.Clear();
            var quadras = new QuadraGramaRepository().ListarTodos();
            VerificaSeHaQuadras(quadras.Count);
            Console.WriteLine("=== QUADRAS DE GRAMA ===\n\n");
            foreach (QuadraDomain quadra in quadras)
            {
                ListarQuadraNaTela(quadra);
            }
            int id = SelecionarQuadra();
            try
            {
                Console.Clear();
                Console.WriteLine("=== QUADRA DE GRAMA ===\n\n");
                var quadra = quadras.Single(x => x.Id == id);
                ListarQuadraNaTela(quadra);
                var precisaReparo = new QuadraGramaValidator().Validate(quadra);
                if (!precisaReparo.IsValid)
                {
                    Console.WriteLine("\n\n !!! ATENÇÃO !!! \n\n");
                    Console.WriteLine(precisaReparo.ToString());
                    Console.WriteLine("A grama está com: " + quadra.AlturaGramaCm + "cm(s) de altura e a altura ideal é: "
                                        + quadra.AlturaIdealGrama);
                }
                int opcao = MenuDetalhesQuadra();
                switch (opcao)
                {
                    case 1:
                        new QuadraGramaRepository().Deletar(quadra);
                        break;

                    case 2:
                        new QuadraGramaModel().Reparar(quadra);
                        break;

                    case 0:
                        MostrarQuadras();
                        break;
                    default:
                        Console.WriteLine("Inválido");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Insira um ID válido!");
                ListarQuadrasGrama();
            }
        }
        private static void ListarQuadrasMadeira()
        {
            Console.Clear();
            var quadras = new QuadraMadeiraRepository().ListarTodos();
            VerificaSeHaQuadras(quadras.Count);
            Console.WriteLine("=== QUADRAS DE MADEIRA ===\n\n");
            foreach (QuadraDomain quadra in quadras)
            {
                ListarQuadraNaTela(quadra);
            }
            int id = SelecionarQuadra();
            try
            {
                Console.Clear();
                Console.WriteLine("=== QUADRA DE MADEIRA ===\n\n");
                var quadra = quadras.Single(x => x.Id == id);
                ListarQuadraNaTela(quadra);
                var precisaReparo = new QuadraMadeiraValidator().Validate(quadra);
                if (!precisaReparo.IsValid)
                {
                    Console.WriteLine("\n\n !!! ATENÇÃO !!! \n\n");
                    Console.WriteLine(precisaReparo.ToString());
                    Console.WriteLine("Está quadra está com " + quadra.QuantidadeMadeiraSolta + " madeira(s) solta(s)");
                }
                int opcao = MenuDetalhesQuadra();
                switch (opcao)
                {
                    case 1:
                        new QuadraMadeiraRepository().Deletar(quadra);
                        break;
                    case 2:
                        new QuadraMadeiraModel().Reparar(quadra);
                        break;
                    case 0:
                        MostrarQuadras();
                        break;
                    default:
                        Console.WriteLine("Inválido");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Insira um ID válido!");
                ListarQuadrasMadeira();
            }
        }
        #endregion
        #region Funções Genéricas
        private static int SelecionarQuadra()
        {
            Console.WriteLine("Insira o ID para selecionar uma quadra: ");
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Insira apenas números!");
                SelecionarQuadra();
            }
            return 0;
        }
        private static void ListarQuadraNaTela(QuadraDomain quadra)
        {
            Console.WriteLine("ID: {0}", quadra.Id);
            Console.WriteLine("Endereço: {0}", quadra.Endereco);
            Console.WriteLine("Dimensões em metros: {0}", quadra.DimensoesEmMetros);
            Console.WriteLine("Data do Cadastro: {0}", quadra.DataCadastro);
            Console.WriteLine("Data Ultima Manutenção: {0}", quadra.DataUltimaManutencao);
            Console.WriteLine("\n\n");
        }
        private static QuadraDomain AddQuadraGenerica(QuadraDomain quadra)
        {
            Console.WriteLine("Insira o Endereço da Quadra");
            quadra.Endereco = Console.ReadLine();
            Console.WriteLine("Insira o As Dimensões da Quadra (C)x(L)");
            quadra.DimensoesEmMetros = Console.ReadLine();
            quadra.DataCadastro = DateTime.Now;
            return quadra;
        }
        private static void VerificaSeHaQuadras(int contadorQuadras)
        {
            if (contadorQuadras == 0)
            {
                Console.WriteLine("NÃO HÁ QUADRAS REGISTRADAS, PRESSIONE QUALQUER TECLA PARA VOLTAR");
                Console.ReadLine();
                MostrarQuadras();
            }
        }
        private static void MsgPadraoAddQuadra()
        {
            Console.Clear();
            Console.WriteLine("ADICIONAR QUADRA");
        }
        private static bool MsgFinalAdicionamento(int ret)
        {
            if (ret > 0)
            {
                Console.WriteLine("Quadra adicionada. Deseja continuar adicionando? S/N");
                var op = Console.ReadLine();
                return op.ToUpper() == "S" ? true : false;
            }
            else
            {
                Console.WriteLine("ALGO NÃO DEU CERTO. =/ APERTE QUALQUER TECLA PARA RETURNAR AO MENU PRINCIPAL");
                Console.ReadLine();
                return false;
            }
        }
        #endregion
    }
}
