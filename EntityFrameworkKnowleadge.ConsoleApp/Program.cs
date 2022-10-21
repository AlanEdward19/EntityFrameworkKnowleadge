
using EntityFrameworkKnowleadge.ConsoleApp;
using EntityFrameworkKnowleadge.Data;
using EntityFrameworkKnowleadge.Domain;
using Microsoft.EntityFrameworkCore;

var funcs = new funcs();
int op;
string name;

var leagueReference = new League();
var teamReference = new Team();

string resposta;

do
{
    Console.Clear();

    Console.WriteLine("Bem vindo ao Projeto DEBUG - TESTE - EntityFrameworkKnowleage\n" +
                      "\n Selecione alguma das opções a seguir:" +
                      "\n\t [1] Inserir uma nova liga" +
                      "\n\t [2] Inserir um novo time" +
                      "\n\t [3] Atualizar uma liga" +
                      "\n\t [4] Atualizar um time" +
                      "\n\t [5] Deletar uma liga" +
                      "\n\t [6] Deletar um time" +
                      "\n\t [7] Pesquisar uma liga (geral)" +
                      "\n\t [8] Pesquisar um time (geral)" +
                      "\n\t [9] Pesquisar uma liga (nome)" +
                      "\n\t [10] Pesquisar um time (nome)" +
                      "\n\t [11] Salvar Alterações");
    op = int.Parse(Console.ReadLine());

    switch (op)
    {
        case 1: // Inserir uma nova liga
            leagueReference = funcs.CriarLeague();
            funcs.insertNewLeague(leagueReference);
            break;

        case 2: // Inserir um novo time
            teamReference = funcs.CriarTeam();
            funcs.insertNewTeam(teamReference);
            break;

        case 3: // Atualizar uma liga
            Console.WriteLine("Digite o nome da liga que gostaria de atualizar: ");
            name = Console.ReadLine();

            leagueReference = await funcs.NameLeagueSearch(name);

            funcs.UpdateLeague(leagueReference);
            break;

        case 4: // Atualizar um time
            Console.WriteLine("Digite o nome do time que gostaria de atualizar: ");
            name = Console.ReadLine();

            teamReference = await funcs.NameTeamSearch(name);

            funcs.UpdateTeam(teamReference);
            break;

        case 5: // Deletar uma liga
            Console.WriteLine("Digite o nome da liga que gostaria de deletar: ");
            name = Console.ReadLine();

            leagueReference = await funcs.NameLeagueSearch(name);
            funcs.DeleteLeague(leagueReference);
            break;

        case 6: // Deletar um time
            Console.WriteLine("Digite o nome do time que gostaria de deletar: ");
            name = Console.ReadLine();

            teamReference = await funcs.NameTeamSearch(name);

            funcs.DeleteTeam(teamReference);
            break;

        case 7: // Pesquisar uma liga (geral)
            funcs.GeneralLeagueSearch();
            break;

        case 8: // Pesquisar um time (geral)
            funcs.GeneralTeamSearch();
            break;

        case 9: // Pesquisar uma liga (nome)
            Console.WriteLine("Digite o nome da liga que gostaria de pesquisar: ");
            name = Console.ReadLine();

            leagueReference = await funcs.NameLeagueSearch(name);

            break;

        case 10: // Pesquisar um time (nome)
            Console.WriteLine("Digite o nome do time que gostaria de pesquisar: ");
            name = Console.ReadLine();

            teamReference = await funcs.NameTeamSearch(name);

            break;

        case 11: // Salvar Alterações
           funcs.SaveChanges();
           break;

        default:
            Console.WriteLine("Opção Invalida");
            break;
    }

    Console.WriteLine("Para salvar suas alterações volte ao menu principal e utilize a opção [11]");

    Console.WriteLine("Deseja fazer outra consulta?");
    resposta = Console.ReadLine();

} while (resposta.ToLower() == "s" || resposta.ToLower().Contains("sim"));

Console.WriteLine("Obrigado por usar este Software" +
                  "\n Pressione qualquer tecla para fechar...");
Console.ReadKey();





