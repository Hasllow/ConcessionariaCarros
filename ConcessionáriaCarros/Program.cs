using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace ConcessionáriaCarros
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Carro> carros = new List<Carro>();
            var carro1 = new Carro();
            carro1.Id = Guid.NewGuid().ToString();
            carro1.Modelo = "Modelo";
            carro1.Marca = "Marca";
            carro1.Km = 10000;
            carro1.Cor = "Azul";
            carro1.Comprador = new Cliente("Oi", "333");

            var carro2 = new Carro();
            carro2.Id = Guid.NewGuid().ToString();
            carro2.Modelo = "modelo2";
            carro2.Marca = "marca2";
            carro2.Km = 50000;
            carro2.Cor = "Verde";
            carro2.Status = Carro.StatusCarro.Vendido;
            carro2.Comprador = new("Comprador", "123");

            carros.Add(carro1);
            carros.Add(carro2);

            bool isOnline = true;
            Carro selectedCar;

            while (isOnline)
            {
                string choice = Menu();
                switch (choice)
                {
                    case "":
                        isOnline = false;

                        Console.Clear();
                        Console.WriteLine("Sistema desligando.");
                        Console.Write("Aperte qualquer tecla para fechar. ");
                        Console.ReadKey();
                        break;

                    case "1":
                        Carro newCarro = AddCarro();
                        carros.Add(newCarro);

                        Console.Clear();
                        Console.WriteLine($"{newCarro.Modelo} foi adicionado com sucesso.");
                        Console.Write("Aperte qualquer tecla para voltar ao menu. ");
                        Console.ReadKey();
                        break;

                    case "2":
                        ReturnSearchType filteredCars = SearchCarro(carros);
                        if (filteredCars.HasSearched) 
                        {
                            PrintCarroList(filteredCars.ListCars);
                            Console.Write("Aperte qualquer tecla para voltar ao menu. ");
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        selectedCar = SelectCar(carros);
                        if(selectedCar.Marca != "") AddManutencao(selectedCar);
                        break;

                    case "4":
                        selectedCar = SelectCar(carros);
                        if (selectedCar.Marca != "") AddVenda(selectedCar);
                        break;

                    case "5":
                        selectedCar = SelectCar(carros);
                        if (selectedCar.Marca != "") DeleteCarro(selectedCar, carros);
                        break;

                    default:
                        Console.Write("Opção Inválida. ");
                        Console.Write("Aperte qualquer tecla para voltar ao menu. ");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static string Menu()
        {
            string choice = "";

            Console.Clear();
            Console.WriteLine("Bem Vinde a Concessionária de Carros");
            Console.WriteLine("1 - Adicionar Novo Carro");
            Console.WriteLine("2 - Pesquisar Carro");
            Console.WriteLine("3 - Adicionar Manutenção");
            Console.WriteLine("4 - Adicionar Venda");
            Console.WriteLine("5 - Excluir Carro");
            Console.WriteLine("\nAperte Enter para sair");
            Console.Write("Digite o número da opção desejada: ");
            choice = Console.ReadLine() ?? "";

            return choice;
        }

        static void PrintCarroList(List<Carro> carroList)
        {
            if (carroList.Count == 0)
            {
                Console.WriteLine("Não foi encontrado nenhum veiculo com essa característica.");
                return;
            }

            for (int i = 0; i < carroList.Count; i++)
            {
                Console.Write($"[{i}] -");
                Console.WriteLine(carroList[i].ToString());
                Console.WriteLine("###################################################\n");
            }
        }

        static ReturnSearchType SearchCarro(List<Carro> carros)
        {
            bool isSearcing = true;
            bool hasSearched = true;
            List<Carro> listCars = new ();

            while (isSearcing)
            {
                Console.Clear();
                Console.WriteLine("Deseja realizar sua busca através de qual opção: ");
                Console.WriteLine("1 - Modelo");
                Console.WriteLine("2 - Marca");
                Console.WriteLine("3 - Cor");
                Console.WriteLine("4 - Km");
                Console.WriteLine("5 - Status");
           
                Console.Write("Digite o número da opção desejada ou aperte Enter para voltar: ");
                string choice = Console.ReadLine() ?? "";
                switch (choice)
                {
                    case "":
                        isSearcing=false;
                        hasSearched = false;
                        break;

                    case "1":
                        Console.Clear();
                        Console.Write("Digite o modelo que deseja pesquisar: ");
                        choice = Console.ReadLine() ?? "";

                        listCars =  carros
                            .Where(car => car.Modelo.ToUpper().Contains(choice.ToUpper()))
                            .ToList();
                        
                        isSearcing = false;
                        break;

                    case "2":
                        Console.Clear();
                        Console.Write("Digite a marca que deseja pesquisar: ");
                        choice = Console.ReadLine() ?? "";

                        listCars = carros
                            .Where(car => car.Marca.ToUpper().Contains(choice.ToUpper()))
                            .ToList();
                        
                        isSearcing = false;
                        break;

                    case "3":
                        Console.Clear();
                        Console.Write("Digite a cor que deseja pesquisar: ");
                        choice = Console.ReadLine() ?? "";
                        listCars =  carros
                            .Where(car => car.Cor.ToUpper().Contains(choice.ToUpper()))
                            .ToList();

                        isSearcing = false;
                        break;

                    case "4":
                        while (true)
                        {
                            Console.Clear();
                            Console.Write(
                                "Digite a quilometragem máxima dos veículos que deseja pesquisar (apenas números): "
                            );
                            choice = Console.ReadLine() ?? "";

                            if (ValidateIntInput(choice, 1, 999999))
                                break;
                        }

                        listCars =  carros.Where(car => car.Km < int.Parse(choice)).ToList();
                        
                        isSearcing = false;
                        break;

                    case "5":
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Status");
                            Console.WriteLine("1 - Estoque");
                            Console.WriteLine("2 - Vendido");
                            Console.Write("Digite o numero do status que deseja pesquisar: ");
                            choice = Console.ReadLine() ?? "";

                            if (ValidateIntInput(choice, 1, 2))
                                break;
                        }

                        var estoqueOuVendido = int.Parse(choice) == 1 ? Carro.StatusCarro.Estoque : Carro.StatusCarro.Vendido;
                        listCars = carros.Where(car => car.Status == estoqueOuVendido).ToList();
                        
                        isSearcing = false;
                        break;

                    default:
                        Console.Write("Opção Inválida. ");
                        Console.Write("Aperte qualquer tecla para tentar novamente. ");
                        Console.ReadKey();
                        break;
                }
            }
            return new(listCars, hasSearched);

        }

        static Carro SelectCar(List<Carro> carroList)
        {
            string numberOfCar = "";
            bool carIsParsed = false;

            while (!carIsParsed)
            {
                Console.Clear();
                PrintCarroList(carroList);

                Console.Write("Selecione o número do carro que deseja alterar alguma informação ou aperte Enter para voltar: ");
                numberOfCar = Console.ReadLine() ?? "";

                if (numberOfCar == "") return new Carro();

                carIsParsed = ValidateIntInput(numberOfCar, 0, carroList.Count - 1);
            }

            return carroList[int.Parse(numberOfCar)];
        }

        static Carro AddCarro()
        {
            string id = Guid.NewGuid().ToString();

            Console.Clear();
            Console.Write("Digite o modelo do carro que deseja adicionar: ");
            string modelo = Console.ReadLine() ?? "";

            Console.Write("Digite a marca do carro que deseja adicionar: ");
            string marca = Console.ReadLine() ?? "";

            Console.Write("Digite a cor do carro que deseja adicionar: ");
            string cor = Console.ReadLine() ?? "";

            Console.Write("Digite a quilometragem do carro que deseja adicionar: ");
            string km = Console.ReadLine() ?? "";
            int kmInt;
            bool conversionKM = int.TryParse(km, out kmInt);
            while (!conversionKM)
            {
                Console.WriteLine("Quilometragem invalida, digite apenas números.");
                Console.Write("Digite a quilometragem do carro que deseja adicionar: ");

                km = Console.ReadLine() ?? "";
                conversionKM = int.TryParse(km, out kmInt);
            }

            Carro carro = new(id, modelo, marca, kmInt, cor);
            return carro;
        }

        static void DeleteCarro(Carro carro, List<Carro> carroList) {
            Console.Clear();
            carroList.Remove(carro);
            Console.WriteLine($"{carro.Modelo} foi deletado com sucesso.");
        }

        static void AddManutencao(Carro carro)
        {
            string oficina;
            string dia;
            string mes;
            string ano;

            DateOnly dataString;
            List<string> pecas = new();

            Console.Clear();
            Console.Write("Digite o nome da oficina que foi realizada a manutenção: ");
            oficina = Console.ReadLine() ?? "";

            bool dateIsParsed = false;
            while (!dateIsParsed)
            {
                Console.Clear();
                Console.Write("Digite o dia (apenas números) em que foi feita a manutenção: ");
                dia = Console.ReadLine() ?? "";

                Console.Write("Digite o mês (apenas números) em que foi feita a manutenção: ");
                mes = Console.ReadLine() ?? "";

                Console.Write("Digite o ano (apenas números) em que foi feita a manutenção: ");
                ano = Console.ReadLine() ?? "";

                dateIsParsed = DateOnly.TryParse($"{dia}-{mes}-{ano}", out dataString);

                if (!dateIsParsed)
                {
                    Console.WriteLine("Data digitada inválida.");
                    Console.Write("Aperte qualquer tecla para tentar novamente. ");
                    Console.ReadKey();
                }
            }

            bool morePartsChanged = true;
            while (morePartsChanged)
            {
                Console.Clear();
                Console.Write("Digite o nome de uma peça trocada:  ");
                string peca = Console.ReadLine() ?? "";

                pecas.Add(peca);

                Console.Clear();
                Console.WriteLine("Foi trocado mais alguma peça?");
                Console.Write(
                    "Digite 'S' para continuar adicionando, ou qualquer outra tecla para finalizar: "
                );

                var continueAddPeca = Console.ReadKey();
                if (continueAddPeca.Key.ToString().ToUpper() != "S")
                    morePartsChanged = false;
            }

            Manutencao manutencao = new(oficina, dataString.ToString(), pecas);
            carro.Manutencoes.Add(manutencao);

            Console.Clear();
            Console.WriteLine("Manutenção adicionada com sucesso!");
        }

        static void AddVenda(Carro carro)
        {
            if (carro.Status == Carro.StatusCarro.Vendido)
            {
                Console.WriteLine("Carro já consta como vendido");
                return;
            }

            Console.Write("Digite o nome do cliente que comprou o carro: ");
            string nomeCliente = Console.ReadLine() ?? "";

            Console.Write("Digite o CPF do cliente que comprou o carro: ");
            string cpfCliente = Console.ReadLine() ?? "";

            carro.Status = Carro.StatusCarro.Vendido;
            carro.Comprador = new(nomeCliente, cpfCliente);

            Console.WriteLine("Venda adicionada com sucesso!");
        }

        static bool ValidateIntInput(string input, int minRange, int maxRange)
        {
            if (
                String.IsNullOrWhiteSpace(input)
                || input.Length > 1
                || !Char.IsNumber(input[0])
                || Char.GetNumericValue(input[0]) < minRange
                || Char.GetNumericValue(input[0]) > maxRange
            )
            {
                Console.WriteLine("Opção inválida.");
                Console.Write("Aperte qualquer tecla para tentar novamente. ");
                Console.ReadKey();
                return false;
            }
            return true;
        }

        public struct ReturnSearchType
        {
            public List<Carro> ListCars;
            public bool HasSearched;

            public ReturnSearchType(List<Carro> listCars, bool hasSearched)
            {
                ListCars = listCars;
                HasSearched = hasSearched;
            }
        }
    }
}
