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

            carros.Add(carro1);
            carros.Add(carro2);

            bool isOnline = true;

            while (isOnline)
            {
                int choice = Menu();
                switch (choice)
                {
                    case 0:
                        isOnline = false;
                        break;
                    case 1:
                        Carro newCarro = AddCarro();
                        carros.Add(newCarro);
                        break;
                    case 2:
                        List<Carro> filteredCars = SearchCarro(carros);

                        if (filteredCars.Count == 0)
                        {
                            Console.WriteLine("Não foi encontrado nenhum veiculo com essa característica.");
                            Console.Write("Aperte qualquer tecla para voltar ao menu. ");
                            Console.ReadKey();
                            break;
                        }

                        PrintCarro(filteredCars);

                        Console.Write("Aperte qualquer tecla para voltar ao menu. ");
                        Console.ReadKey();
                        break;
                    case 3:
                        AddManutencao(carros);

                        Console.Write("Aperte qualquer tecla para voltar ao menu. ");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static int Menu()
        {
            string choice = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Bem Vinde a Concessionária de Carros");
                Console.WriteLine("1 - Adicionar Novo Carro");
                Console.WriteLine("2 - Pesquisar Carro");
                Console.WriteLine("3 - Adicionar Manutenção");
                Console.WriteLine("4 - Adicionar Venda");
                Console.Write("Digite o número da opção desejada: ");
                choice = Console.ReadLine() ?? "";

                if (ValidateIntInput(choice, 0, 4))
                    break;
            }
            return int.Parse(choice);
        }

        static Carro AddCarro()
        {
            Console.Clear();
            Console.Write("Digite o modelo do carro que deseja adicionar: ");
            string modelo = Console.ReadLine() ?? "";
            Console.Write("Digite a marca do carro que deseja adicionar: ");
            string marca = Console.ReadLine() ?? "";

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

            Console.Write("Digite a cor do carro que deseja adicionar: ");
            string cor = Console.ReadLine() ?? "";
            string id = Guid.NewGuid().ToString();

            Carro carro = new();
            carro.Id = id;
            carro.Id = modelo;
            carro.Marca = marca;
            carro.Km = kmInt;
            carro.Cor = cor;
            carro.Status = Carro.StatusCarro.Estoque;
            return carro;
        }

        static void PrintCarro(List<Carro> carroList)
        {
            for (int i = 0; i < carroList.Count; i++)
            {
                Console.Write(
                    $"[{i}] -"
                        + $"\tModelo: {carroList[i].Modelo}"
                        + $"\n\tMarca: {carroList[i].Marca}"
                        + $"\n\tQuilometragem: {carroList[i].Km}"
                        + $"\n\tCor: {carroList[i].Cor}"
                        + $"\n\tStatus: {carroList[i].Status}"
                );
                if (carroList[i].Manutencoes != null)
                {
                    Console.Write("\n\tManutenções:");
                    foreach (var item in carroList[i].Manutencoes)
                    {
                        Console.Write(
                            $"\n\t\tData: {item.Data}" +
                            $"\n\t\tOficina: {item.Oficina}" +
                            $"\n\t\tPeças: {string.Join(", ", item.Items)}"
                        );
                    }
                }
                Console.WriteLine("\n");
            }
        }

        static int SelectCar(List<Carro> carroList)
        {
            string numberOfCar = "";
            bool carIsParsed = false;

            while (!carIsParsed)
            {
                Console.Clear();
                PrintCarro(carroList);
                Console.Write("Selecione o número do carro que deseja alterar alguma informação: ");
                numberOfCar = Console.ReadLine() ?? "";

                carIsParsed = ValidateIntInput(numberOfCar, 0, carroList.Count - 1);
            }

            return int.Parse(numberOfCar);
        }

        static List<Carro> SearchCarro(List<Carro> carros)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Deseja realizar sua busca através de qual opção: ");
                Console.WriteLine("1 - Modelo");
                Console.WriteLine("2 - Marca");
                Console.WriteLine("3 - Cor");
                Console.WriteLine("4 - Km");
                Console.WriteLine("5 - Status");
                Console.Write("Digite o número da opção desejada: ");
                string choice = Console.ReadLine() ?? "";

                if (ValidateIntInput(choice, 1, 5))
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Clear();
                            Console.Write("Digite o modelo que deseja pesquisar: ");
                            choice = Console.ReadLine() ?? "";

                            return carros.Where(car => car.Modelo.Contains(choice)).ToList();

                        case "2":
                            Console.Clear();
                            Console.Write("Digite a marca que deseja pesquisar: ");
                            choice = Console.ReadLine() ?? "";

                            return carros.Where(car => car.Marca.Contains(choice)).ToList();

                        case "3":
                            Console.Clear();
                            Console.Write("Digite a cor que deseja pesquisar: ");
                            choice = Console.ReadLine() ?? "";
                            return carros.Where(car => car.Cor.Contains(choice)).ToList();

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

                            return carros.Where(car => car.Km < int.Parse(choice)).ToList();

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

                            var estoqueOuVendido =
                                int.Parse(choice) == 1
                                    ? Carro.StatusCarro.Estoque
                                    : Carro.StatusCarro.Vendido;
                            return carros.Where(car => car.Status == estoqueOuVendido).ToList();
                    }
                }
            }
        }

        static void AddManutencao(List<Carro> listaCarros)
        {
            string oficina;
            string dia;
            string mes;
            string ano;
            
            DateOnly dataString;
            List<string> pecas = new();

            int numberOfCar = SelectCar(listaCarros);

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
                Console.Write("Digite o nome da peça trocada:  ");
                string peca = Console.ReadLine() ?? "";

                pecas.Add(peca);

                Console.WriteLine("Foi trocado mais alguma peça?");
                Console.Write("Digite 'S' para continuar adicionando: ");
                string continueAddPeca = Console.ReadLine() ?? "";

                if (continueAddPeca.ToUpper()[0] != 'S')
                    morePartsChanged = false;
            }

            Manutencao manutencao = new(oficina, dataString.ToString(), pecas);

            if (listaCarros[numberOfCar].Manutencoes == null)
                listaCarros[numberOfCar].Manutencoes = new() { manutencao };
            else
                listaCarros[numberOfCar].Manutencoes.Add(manutencao);

            Console.WriteLine("Manutenção adicionada com sucesso!");
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
    }
}
