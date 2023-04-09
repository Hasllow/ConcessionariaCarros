using ConcessionáriaCarros.Repositories;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace ConcessionáriaCarros
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var carroRepositorie = new CarroRepositorie();

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

            carroRepositorie.Carros.Add(carro1);
            carroRepositorie.Carros.Add(carro2);

            var isOnline = true;

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
                        carroRepositorie.AddCarro();
                        
                        Console.Clear();
                        Console.WriteLine($"Novo carro foi adicionado com sucesso.");
                        Console.Write("Aperte qualquer tecla para voltar ao menu. ");
                        Console.ReadKey();
                        break;

                    case "2":
                        var filteredCars = carroRepositorie.SearchCarro();
                        if (filteredCars.HasSearched) 
                        {
                            PrintCarroList(filteredCars.ListCars);
                            Console.Write("Aperte qualquer tecla para voltar ao menu. ");
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        selectedCar = carroRepositorie.SelectCar();
                        if(selectedCar.Marca != "") carroRepositorie.AddManutencao(selectedCar);
                        break;

                    case "4":
                        selectedCar = carroRepositorie.SelectCar();
                        if (selectedCar.Marca != "") carroRepositorie.AddVenda(selectedCar);
                        break;

                    case "5":
                        selectedCar = carroRepositorie.SelectCar();
                        if (selectedCar.Marca != "") carroRepositorie.DeleteCarro(selectedCar);
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
    }
}
